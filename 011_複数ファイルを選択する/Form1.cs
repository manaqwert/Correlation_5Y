using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _011_�����t�@�C����I������
{
    public partial class �����t�@�C����I������ : Form
    {
        //���͂���z���`
        string[,] File1cont = new string[300, 10000];
        string[,] File2cont = new string[300, 10000];
        string[,] File3cont = new string[300, 10000];
        string[,] File4cont = new string[300, 10000];
        string[,] File5cont = new string[300, 10000];

        //�o�͂���z���`
        string[,,] recomend_stock = new string[365, 10000, 6];

        string dt;

        string dname;

        public �����t�@�C����I������()
        {
            InitializeComponent();
        }

        //
        //�t�@�C����I������
        //
        string Open_file()
        {
            //�t�H���_�I���_�C�A���O�̏����l�w��
            //string file_sel.SelectedPath = @"C:\Data\OneDrive\stock";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            string folder_name = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);

            //dname�Ƀe�L�X�g�{�b�N�X�̃t�H���_��������
            dname = openFileDialog1.FileName;

            if (System.IO.File.Exists(dname) == false)
            {
                MessageBox.Show(dname + "��������܂���B", "�ʒm");
                return dname;
            }

            return dname;

        }


        //
        //�t�@�C����ǂݍ���
        //
        public string[,] read_file(string dname)
        {
            int days = 1;
            int days2 = 0;
            int days3 = 1;
            int stock_num = 1;
            string[,] in_data_file_temp = new string[370, 10000];
            string[,] in_data_file = new string[370, 10000];

            System.IO.StreamReader sr = new System.IO.StreamReader(dname, Encoding.GetEncoding("Shift_JIS"));

            for (days = 1; days <= 369; days++)
            {
                if (sr.EndOfStream == false)
                {
                    //�t�@�C����s�ǂݍ��݁A�J���}��؂薈��field[]�Ɋi�[
                    string file_contents_2 = sr.ReadLine();

                    string[] fields = file_contents_2.Split(',');

                    //
                    //������ɕ��������邾���ǂ�Ŕz��Ɋi�[�B�v�f����fileds.Length
                    //
                    for (stock_num = 0; stock_num < fields.Length; stock_num++)
                    {
                        in_data_file_temp[days, stock_num] = fields[stock_num];
                    }
                }
                else
                {
                    //�Ō�̍s�܂œǂݏI������ꍇ�̏I�����u
                    //file_contents_Box.Items.Add(days + ", " + stock_num + ", " + "EndOfStream");
                    days2 = days;
                    days = 369;
                }
                //days3 = days3++;
            }

            //
            //��ԍ�������ԍ��ɕ��ёւ�
            //
            for (int stock_num2 = 0; stock_num2 < 10000; stock_num2++)
            {
                for (stock_num = 0; stock_num < 10000; stock_num++)
                {
                    string stock_num_string;
                    stock_num_string = stock_num.ToString();
                    if (in_data_file_temp[1, stock_num2] == stock_num_string)
                    {
                        //stock_num�ƂP�s�ڂ���v����Ίi�[
                        days3 = 1;
                        for (days = 1; days < 369; days++)
                        {
                            //if (days == 3)
                            //{
                            //}
                            ////else if (days == 2)
                            ////{
                            ////}
                            //else if (days == 4)
                            //{
                            //}
                            //else if (days == 5)
                            //{
                            //}
                            //else if (days == 6)
                            //{
                            //}
                            //else if (days == 7)
                            //{
                            //}
                            //else
                            //{
                                in_data_file[days3, stock_num] = in_data_file_temp[days, stock_num2];
                                days3 = days3 + 1;
                            //}
                        }
                    }
                    else
                    {
                        //stock_num�ƂP�s�ڂ���v���Ȃ���΋�
                    }
                }
            }

            listBox1.Items.Add("178�s�� dname = " + dname + ", days3 = " + days3 + ", days2 = " + days2 + ", ");


            //days2�͔N������N���܂ł̉c�Ɠ���
            int days4 = 0;

            for (double n2 = 0; n2 <= 369; n2++)
            {
                if (n2 > days2 - 2)
                {
                    in_data_file[(int)n2, 0] = "end";
                    //
                    //�f�[�^�̍ŏI�s���ʂ̂��߁i2018/1/21�j
                    //
                    n2 = 370;
                }
                else
                {
                    //���ڂ͓��t�ł͂Ȃ��A�N������N���܂ł̉c�Ɠ������K��
                    if (n2 <= 7)
                    {
                        //
                        //�����ԍ��A�������̍s�͖���
                        //
                    }
                    else
                    {
                        double nom_day;
                        nom_day = (n2 - 8) / (double)(days2 - 10);
                        in_data_file[(int)n2, 0] = nom_day.ToString("G4");
                    }
                }

                days4 = days4 + 1;
            }

            //
            //�f�o�b�O�p�t�@�C�������o��(in_data_file)
            //
            //
            //�V���ȃt�@�C����p�ӂ���O�Ɋ����t�@�C���폜
            //
            dt = DateTime.Now.ToString($"yyyyMMddMHHmmss");

            System.IO.File.Delete(@dname + "_" + dt + "_indatafile.csv");

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            //sjisEnc = Encoding.GetEncoding("Shift_JIS");
            System.IO.StreamWriter writer = new System.IO.StreamWriter(@dname + "_" + dt + "_indatafile.csv", true, sjisEnc);
            //writer = new System.IO.StreamWriter(@dname + "_" + "_indatafile.csv", true, sjisEnc);

            //
            //�v�Z���ʂ̏����o��
            //
            for (int n3 = 1; n3 <= days2; n3++)
            {
                for (stock_num = 0; stock_num < 10000; stock_num++)
                {
                    writer.Write(in_data_file[n3, stock_num] + ",");
                }
                writer.Write("\r\n");

            }

            writer.Close();

            //
            //�����܂Ńt�@�C�������o��
            //

            //}

            //
            //�����܂Ńt�@�C�����e�ǂݍ��݁��z��i�[
            //

            return in_data_file;
        }


        //
        //��������Window�����֌W
        //
        private void File1_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        public void File1_sel_button_Click(object sender, EventArgs e)
        {
            string File1;
            File1 = Open_file();

            //�e�L�X�g�{�b�N�X�ɑI�������t�@�C������\������
            File1_sel_Box.Text = File1;

            //�t�@�C����ǂݍ���
            //string[,] File1cont = new string[300, 5000];
            File1cont = read_file(File1);

        }

        private void File2_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        public void File2_sel_button_Click(object sender, EventArgs e)
        {
            string File2;
            File2 = Open_file();

            //�e�L�X�g�{�b�N�X�ɑI�������t�@�C������\������
            File2_sel_Box.Text = File2;

            //�t�@�C����ǂݍ���
            //string[,] File2cont = new string[300, 5000];
            File2cont = read_file(File2);

        }

        private void File3_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        public void File3_sel_button_Click(object sender, EventArgs e)
        {
            string File3;
            File3 = Open_file();

            //�e�L�X�g�{�b�N�X�ɑI�������t�@�C������\������
            File3_sel_Box.Text = File3;

            //�t�@�C����ǂݍ���
            //string[,] File3cont = new string[300, 5000];
            File3cont = read_file(File3);

        }

        private void File4_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        public void File4_sel_button_Click(object sender, EventArgs e)
        {
            string File4;
            File4 = Open_file();

            //�e�L�X�g�{�b�N�X�ɑI�������t�@�C������\������
            File4_sel_Box.Text = File4;

            //�t�@�C����ǂݍ���
            //string[,] File4cont = new string[300, 5000];
            File4cont = read_file(File4);

        }

        private void File5_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        public void File5_sel_button_Click(object sender, EventArgs e)
        {
            string File5;
            File5 = Open_file();

            //�e�L�X�g�{�b�N�X�ɑI�������t�@�C������\������
            File5_sel_Box.Text = File5;

            //�t�@�C����ǂݍ���
            //string[,] File5cont = new string[300, 5000];
            File5cont = read_file(File5);

        }

        private void �����t�@�C����I������_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void Compute_button_Click(object sender, EventArgs e)
        {
            double[] file_OKNG = new double[6];

            int file1 = 0;
            int file2 = 0;
            int file3 = 0;
            int file4 = 0;
            int file5 = 0;
            double updown_threshold = 25; //�������i���j
            double correlation_threshold = 5; //�W���΍����

            if (textBox10.Text != "")
            {
                updown_threshold = Convert.ToDouble(textBox10.Text);
            }
            else
            {

            }
            if (textBox11.Text != "")
            {
                correlation_threshold = Convert.ToDouble(textBox11.Text);
            }
            else
            {

            }

            //
            //�v���O�����{��
            //
            //listBox1.Items.Add("Compute_button_Click, ");

            dt = DateTime.Now.ToString($"yyyyMMddMHHmmss");

            //
            //���֌W����邽�߁A�t�@�C�������`�F�b�N
            //�u1001���o225�v������΃t�@�C������Ɣ��f
            //
            if ( File1cont[1,1001] == 1001.ToString() )
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~1
                file1 = 1;
            }
            else
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~0
                file1 = 0;
            }
            if (File2cont[1, 1001] == 1001.ToString())
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~1
                file2 = 1;
            }
            else
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~0
                file2 = 0;
            }
            if (File3cont[1, 1001] == 1001.ToString())
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~1
                file3 = 1;
            }
            else
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~0
                file3 = 0;
            }
            if (File4cont[1, 1001] == 1001.ToString())
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~1
                file4 = 1;
            }
            else
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~0
                file4 = 0;
            }
            if (File5cont[1, 1001] == 1001.ToString())
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~1
                file5 = 1;
            }
            else
            {
                //file�����݂���ꍇ�͑��֌W���v�Z�p�̌W���~0
                file5 = 0;
            }

            listBox1.Items.Add("426�s�� file1 = " + file1 + ", file2 = " + file2 + ", file3 = " + file3 + ", file4 = " + file4 + ", file5 = " + file5 + ", ");

            file_OKNG[1] = file1;
            file_OKNG[2] = file2;
            file_OKNG[3] = file3;
            file_OKNG[4] = file4;
            file_OKNG[5] = file5;

            double[,] File_correlation_rms = new double[6, 10000];

            //File_correlation_rms = correlation(File1cont, File2cont, File3cont, File4cont, File5cont, file_OKNG, updown_threshold, correlation_threshold);

            string[,,] Filecont = new string[6, 370, 10000];

            for (int n = 1; n < 370; n++)
            {
                for (int stock_num = 0; stock_num < 10000; stock_num++)
                {
                    if (file_OKNG[1] == 1)
                    {
                        Filecont[1, n, stock_num] = File1cont[n, stock_num];
                    }
                    else
                    {

                    }
                    if (file_OKNG[2] == 1)
                    {
                        Filecont[2, n, stock_num] = File2cont[n, stock_num];
                    }
                    else
                    {

                    }
                    if (file_OKNG[3] == 1)
                    {
                        Filecont[3, n, stock_num] = File3cont[n, stock_num];
                    }
                    else
                    {

                    }
                    if (file_OKNG[4] == 1)
                    {
                        Filecont[4, n, stock_num] = File4cont[n, stock_num];
                    }
                    else
                    {

                    }
                    if (file_OKNG[5] == 1)
                    {
                        Filecont[5, n, stock_num] = File5cont[n, stock_num];
                    }
                    else
                    {

                    }
                    //
                    //�t�@�C�����I������Ă��Ȃ��ꍇ�̃G���[�����K�v
                    //
                }
            }

            File_correlation_rms = correlation(Filecont, file_OKNG, correlation_threshold, updown_threshold);
        }


        private void Quit_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("�I�����܂��B", "�ʒm");
            //�A�v���P�[�V�������I������B
            Application.Exit();

        }

       //double[,] correlation(string[,] File1cont, string[,] File2cont, string[,] File3cont, string[,] File4cont, string[,] File5cont, double[] file_OKNG, double correlation_threshold, double updown_threshold)
        double[,] correlation(string[,,] Filecont, double[] file_OKNG, double correlation_threshold, double updown_threshold)
        {
            double File1cont_double = 1;
            double File2cont_double = 1;
            double File3cont_double = 1;
            double File4cont_double = 1;
            double File5cont_double = 1;

            double[,,] Filecont_double = new double[6, 370, 10000];

            //
            //�e�������ɕ��ς��v�Z
            //
            double[,] Average = new double[371, 10001];
            int stock_num = 0;

            double[] days_count_file = new double[6];
            double max_days_count_file = 0;
            double min_days_count_file = 370;
            double ave_days_count_file = 0;

            for (int n = 1; n < 370; n++)
            {
                //if( n == 2 || n == 2 || n == 4 || n == 6 )
                if ( n <= 2 )
                {

                }
                else
                {
                    for (stock_num = 0; stock_num < 10000; stock_num++)
                    {
                        if (String.IsNullOrEmpty(Filecont[1, n, stock_num]))
                        {
                            File1cont_double = 0;
                        }
                        else
                        {
                            if(Filecont[1, n, stock_num] == "end")
                            {

                            }
                            else
                            {
                                File1cont_double = Convert.ToDouble(Filecont[1, n, stock_num]);
                            }
                        }

                        if (String.IsNullOrEmpty(Filecont[2, n, stock_num]))
                        {
                            File2cont_double = 0;
                        }
                        else
                        {
                            if (Filecont[2, n, stock_num] == "end")
                            {

                            }
                            else
                            {
                                File2cont_double = Convert.ToDouble(Filecont[2, n, stock_num]);
                            }
                        }

                        if (String.IsNullOrEmpty(Filecont[3, n, stock_num]))
                        {
                            File3cont_double = 0;
                        }
                        else
                        {
                            if (Filecont[3, n, stock_num] == "end")
                            {

                            }
                            else
                            {
                                File3cont_double = Convert.ToDouble(Filecont[3, n, stock_num]);
                            }
                        }

                        if (String.IsNullOrEmpty(Filecont[4, n, stock_num]))
                        {
                            File4cont_double = 0;
                        }
                        else
                        {
                            if (Filecont[4, n, stock_num] == "end")
                            {

                            }
                            else
                            {
                                File4cont_double = Convert.ToDouble(Filecont[4, n, stock_num]);
                            }
                        }

                        if (String.IsNullOrEmpty(Filecont[5, n, stock_num]))
                        {
                            File5cont_double = 0;
                        }
                        else
                        {
                            if (Filecont[5, n, stock_num] == "end")
                            {

                            }
                            else
                            {
                                File5cont_double = Convert.ToDouble(Filecont[5, n, stock_num]);
                            }
                        }

                        //
                        //�e�����y�ѓ��t���ɕ��ς��Z�o
                        //
                        Average[n, stock_num] = (File1cont_double + File2cont_double + File3cont_double + File4cont_double + File5cont_double) / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
                    }

                }

                //
                //�e�t�@�C���̓������A���t���ŃJ�E���g
                //
                for (int m = 1; m < 6; m++)
                {
                    if (String.IsNullOrEmpty(Filecont[m, n, 0]) || Filecont[m, n, 0] == "end")
                    {
                        //listBox1.Items.Add("622�s�� Filecont[m, n, 0] = " + Filecont[m, n, 0] + ", n =" + n + ", ");
                    }
                    else
                    {
                        //listBox1.Items.Add("626�� Filecont[m, n, 0] = " + Filecont[m, n, 0] + ", n =" + n + ", ");
                        days_count_file[m] = days_count_file[m] + 1;
                    }
                }
            }

            //
            //days_count_fileX�̍ő�l�T��
            //
            for(int m = 1; m < 6; m++)
            {
                if (max_days_count_file < days_count_file[m])
                {
                    max_days_count_file = days_count_file[m];
                }
                else
                {

                }
            }

            //
            //days_count_fileX�̍ŏ��l�T��
            //
            for (int m = 1; m < 6; m++)
            {
                if(file_OKNG[m] ==1)
                {
                    if (min_days_count_file > days_count_file[m])
                    {
                        min_days_count_file = days_count_file[m];
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }

            //
            //days_count_fileX�̕��ϒl�T��
            //
            for (int m = 1; m < 6; m++)
            {
                ave_days_count_file = ave_days_count_file + days_count_file[m];
            }
            ave_days_count_file = ave_days_count_file / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);

            listBox1.Items.Add("686�s�� Average[n, stock_num]�v�Z, ");
            for(int m = 1; m < 6; m++)
            {
                listBox1.Items.Add("689�s�� days_count_file[m] = " + days_count_file[m] + ", m = " + m +"\r\n");
            }
            listBox1.Items.Add("691�s�� max_days_count_file = " + max_days_count_file + "\r\n");
            listBox1.Items.Add("692�s�� min_days_count_file = " + min_days_count_file + "\r\n");
            listBox1.Items.Add("693�s�� ave_days_count_file = " + ave_days_count_file + "\r\n");

            //
            //�e�t�@�C���̓��t���𐳋K�����āAFile_cont_double�ɓ���
            //
            for (int days_ave = 1; days_ave <= max_days_count_file + 1; days_ave++)
            {
                for (int n = 1; n <= 5; n++)
                {
                    //if (String.IsNullOrEmpty(Filecont[n, 1, stock_num]))
                    //{
                        if (n == 1 && file_OKNG[1] == 1)
                        {
                            //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                            days_ave = 370;
                            //listBox1.Items.Add("707�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 2 && file_OKNG[2] == 1)
                        {
                            //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                            days_ave = 370;
                            //listBox1.Items.Add("720�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 3 && file_OKNG[3] == 1)
                        {
                            //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                            days_ave = 370;
                            //listBox1.Items.Add("732�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 4 && file_OKNG[4] == 1)
                        {
                            //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                            days_ave = 370;
                            //listBox1.Items.Add("744�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 5 && file_OKNG[5] == 1)
                        {
                            //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                            days_ave = 370;
                            //listBox1.Items.Add("756�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }
                }

                //
                //���t����average���Z�o
                //
                Average[days_ave, 0] = Average[days_ave, 0] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
            }

            int progress_status = 0;

            //
            //�����s���ł̐��K�����������̍ŏ����l���v�Z
            //
            double[] Filecont_rms_double = new double[10001];

            double[,] Filecont2ave_rms = new double[371, 10001];

            for (stock_num = 1; stock_num < 10000; stock_num++)
            {
                //Filecont_rms_double[stock_num] = 0;

                //�v���O���X�o�[�̐ݒ�
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 10000;

                //int progress_status = (stock_num / 10000) * 100;
                //progressBar1.Increment(progress_status);

                //�v���O���X�o�[�̐ݒu
                progress_status = (int)(stock_num / 10000) * 100;
                //progressBar1.Increment(progress_status);
                progressBar1.PerformStep();


                for (int days_ave = 1; days_ave <= max_days_count_file + 1; days_ave++)
                //
                //days_ave�̍ő�l���t�@�C���̓��t���idays_count_file1�`5�̍ő�l�j�ɂ������B
                //�i2018/1/28�j
                //
                {
                    for (int n = 1; n <= 5; n++)
                    {
                        if (String.IsNullOrEmpty(Filecont[n, 1, stock_num]))
                        {
                            if (n == 1 && file_OKNG[1] == 1)
                            {
                                //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                                days_ave = 371;
                                //listBox1.Items.Add("707�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 2 && file_OKNG[2] == 1)
                            {
                                //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                                days_ave = 371;
                                //listBox1.Items.Add("720�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 3 && file_OKNG[3] == 1)
                            {
                                //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                                days_ave = 371;
                                //listBox1.Items.Add("732�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 4 && file_OKNG[4] == 1)
                            {
                                //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                                days_ave = 371;
                                //listBox1.Items.Add("744�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 5 && file_OKNG[5] == 1)
                            {
                                //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                                days_ave = 371;
                                //listBox1.Items.Add("756�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            if (days_ave > 7 && days_ave < 371)
                            //
                            //�ő�l�A�ŏ��l�����܂߂����ߕύX�A2��7
                            //2018/1/25
                            //
                            {
                                //
                                //�r���̃f�[�^��������⊮
                                //
                                if (String.IsNullOrEmpty(Filecont[n, days_ave, stock_num]))
                                {
                                    if (String.IsNullOrEmpty(Filecont[n, (days_ave - 1), stock_num]))
                                    {
                                        Filecont_double[n, days_ave, stock_num] = 100;
                                        //listBox1.Items.Add("�f�[�^�����⊮�@779�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                                    }
                                    else
                                    {
                                        Filecont_double[n, days_ave, stock_num] = Convert.ToDouble(Filecont[n, (days_ave - 1), stock_num]);
                                        //listBox1.Items.Add("�f�[�^�����⊮�@784�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                                    }
                                }
                                else
                                {
                                    Filecont_double[n, days_ave, stock_num] = Convert.ToDouble(Filecont[n, days_ave, stock_num]);
                                    //listBox1.Items.Add("�f�[�^�����⊮�@790�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                                }

                                //
                                //�W���΍��v�Z�p
                                //
                                //Filecont_rms_double[stock_num] = Filecont_rms_double[stock_num] + Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2);
                                Filecont_rms_double[stock_num] = Filecont_rms_double[stock_num] + (Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2) + Math.Pow((Filecont_double[n, days_ave, 0] - Average[days_ave, 0]), 2));

                                //
                                //�������K�����Ă��Ȃ����߁A�܂߂�ƃG���[
                                //�i2018/1/28�j
                                //

                                //
                                //5�N���ϒl����̋��������Z
                                //
                                if (days_ave < 7)
                                {

                                }
                                else if (days_ave == 8)
                                {
                                    //Filecont2ave_rms[days_ave, stock_num] = Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2));
                                    Filecont2ave_rms[days_ave, stock_num] = Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2) + Math.Pow((Filecont_double[n, days_ave, 0] - Average[days_ave, 0]), 2));
                                    //listBox1.Items.Add("814�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                                }
                                else if (days_ave < 371)
                                {
                                    //Filecont2ave_rms[days_ave, stock_num] = Filecont2ave_rms[(days_ave - 1), stock_num] + Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2));
                                    Filecont2ave_rms[days_ave, stock_num] = Filecont2ave_rms[(days_ave - 1), stock_num] + Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2) + Math.Pow((Filecont_double[n, days_ave, 0] - Average[days_ave, 0]), 2));
                                    //listBox1.Items.Add("819�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                                }
                                else
                                {
                                    //listBox1.Items.Add("823�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                                }
                            }
                            else
                            {
                                //listBox1.Items.Add("828�s�� n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num);
                            }
                        }
                    }
                    //
                    //���Z�����N�������ω�
                    //
                    if (days_ave == 371)
                    {
                        //�ŏ��̍s���A0�̎��i�f�[�^�������j�Ȃɂ����Ȃ�
                    }
                    else
                    {
                        Filecont2ave_rms[days_ave, stock_num] = Filecont2ave_rms[days_ave, stock_num] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
                        //listBox1.Items.Add("842�� days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                    }
                }
                //
                //���Z�����N���y��max_days_count_file�ŕ��ω�
                //
                //Filecont_rms_double[stock_num] = (Filecont_rms_double[stock_num] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5])) / max_days_count_file;
                Filecont_rms_double[stock_num] = Math.Sqrt( Filecont_rms_double[stock_num] / ( max_days_count_file - 1 ) );
                //
                //����W���΍����H
                //
                //Filecont_rms_double[stock_num]�̏o�͂�0!!�i2018/1/30�j
                //
            }


            listBox1.Items.Add("1005�s�� Filecont_rms_double[stock_num]�Z�o, ");

            for (stock_num = 1; stock_num < 10000; stock_num++)
            {
                //listBox1.Items.Add("792�s�� �����ԍ� = " + Filecont[1, 1, stock_num] + ", ������ = " + Filecont[1, 2, stock_num] + ", �W���΍� = " + Filecont_rms_double[stock_num] + ", ");
            }

            //
            //�f�o�b�O�p�̃t�@�C�������o��
            //

            //
            //�t�@�C���o��
            //

            //
            //�V���ȃt�@�C����p�ӂ���O�Ɋ����t�@�C���폜
            //

            System.IO.File.Delete("Debug_Filecont_rms_double.csv");

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            //sjisEnc = Encoding.GetEncoding("Shift_JIS");
            System.IO.StreamWriter writer = new System.IO.StreamWriter("Debug_Filecont_rms_double.csv", true, sjisEnc);
            //writer = new System.IO.StreamWriter(@dname + "_" + "_indatafile.csv", true, sjisEnc);

            /*
            System.IO.File.Delete(@dname + "Debug_Filecont_rms_double_" + dt + ".csv");

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            //sjisEnc = Encoding.GetEncoding("Shift_JIS");
            System.IO.StreamWriter writer = new System.IO.StreamWriter(@dname + "Debug_Filecont_rms_double_" + dt + ".csv", true, sjisEnc);
            //writer = new System.IO.StreamWriter(@dname + "_" + "_indatafile.csv", true, sjisEnc);
            */

            //
            //�v�Z���ʂ̏����o��
            //

            for (int n3 = 1; n3 <= min_days_count_file + 8; n3++)
            {
                for (stock_num = 0; stock_num < 10000; stock_num++)
                {
                    if( n3 <= 7 )
                    {
                        writer.Write(Filecont[1, n3, stock_num] + ",");
                    }
                    else
                    {
                        if( stock_num > 0 )
                        {
                            writer.Write(Filecont2ave_rms[n3, stock_num] + ",");
                        }
                        else
                        {
                            writer.Write(Average[n3, 0] + ",");
                        }
                    }
                }
                writer.Write("\r\n");
            }

            writer.Write("Filecont_rms_double" + "\r\n");

            for (stock_num = 0; stock_num < 10000; stock_num++)
            {
                writer.Write(Filecont_rms_double[stock_num] + ",");
            }
            writer.Write("\r\n");

            writer.Close();

            //
            //�����܂Ńt�@�C�������o��
            //

            listBox1.Items.Add("1072�s�� Debug_Filecont_rms_double" + dt + ".csv�����o��, ");


            //
            //�܂���MAX�AMIN�䗦�v�Z���āA������x�ȏ�łȂ���Όv�Z���Ȃ�
            //�������l�͊O���֐��O���������
            //double updown_threshold
            //

            for (stock_num = 2; stock_num < 10000; stock_num++)
            {
                for (int n = 1; n < 370; n++)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(Filecont2ave_rms[1, stock_num])))
                    {
                        //�ŏ��̍s���A0�̎��i�f�[�^�������jfor���[�v�I�����u
                        n = 370;
                    }
                    else
                    {

                    }
                    if (n > 7)
                    //
                    //�ő�l�A�ŏ��l�����܂߂����ߕύX�A2��7
                    //2018/1/25
                    //
                    {
                        Filecont2ave_rms[n, stock_num] = Filecont2ave_rms[n, stock_num];
                    }
                    else if (n == 4 || n == 6)
                    {
                        //
                        //�ő�l�A�ŏ��l����5�N���̒P�����ς��o��
                        //2018/1/25
                        //
                        for (int l = 1; l < 6; l++)
                        {
                            if (String.IsNullOrEmpty(Filecont[l, n, stock_num]))
                            {

                            }
                            else
                            {
                                Filecont2ave_rms[n, stock_num] = Filecont2ave_rms[n, stock_num] + Convert.ToDouble(Filecont[l, n, stock_num]);
                            }
                        }
                        Filecont2ave_rms[n, stock_num] = Filecont2ave_rms[n, stock_num] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
                    }
                    else if (n == 3 || n == 5)
                    {
                        //
                        //�ő�l���t�A�ŏ��l���t��0
                        //2018/1/30
                        //
                        for (int l = 1; l < 6; l++)
                        {
                            if (String.IsNullOrEmpty(Filecont[l, n, stock_num]))
                            {

                            }
                            else
                            {
                                Filecont2ave_rms[n, stock_num] = Filecont2ave_rms[n, stock_num] + Convert.ToDouble(Filecont[l, n, stock_num]);
                            }
                        }
                        Filecont2ave_rms[n, stock_num] = Filecont2ave_rms[n, stock_num] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);

                        //Filecont2ave_rms[n, stock_num] = 0;
                    }
                    else
                    {
                        //
                        //�����ԍ��A�������͂��̂܂܏o��
                        //2018/1/25
                        //
                    }
                }
            }


            //
            //����������listBox1�ւ̏����o��
            //
            double correlation_index_high = correlation_threshold; //���o���鑊�֌W���̏��臒l
            //double correlation_index_low = 0.0020479; //���o���鑊�֌W���̉���臒l
            double correlation_index_low = 0; //���o���鑊�֌W���̉���臒l

            double high_low = updown_threshold; //���o���铫������臒l
            int recommend_count = 0; //������������̐�
            string[,] recommend = new string[370, 10000];

            listBox1.Items.Add("");
            listBox1.Items.Add("");

            listBox1.Items.Add("1166�s�� =====�����������o=====");
            listBox1.Items.Add("");

            //
            //���������̏��������o��
            //
            listBox1.Items.Add("1172�s�� �W���΍�臒l��� = " + correlation_index_high + ", �W���΍�臒l���� = " + correlation_index_low + ", ������臒l = " + high_low + ", ");
            listBox1.Items.Add("");

            double[] high_low_ratio = new double[10000];

            for (stock_num = 1; stock_num < 10000; stock_num++)
            {
                //
                //�W���΍������ȉ��𒊏o
                //
                //Filecont2ave_rms[n, stock_num]
                if (Filecont_rms_double[stock_num] <= correlation_index_high && Filecont_rms_double[stock_num] > correlation_index_low)
                {
                    //
                    //�ő�l�ƍŏ��l���瓫�������Z�o
                    //
                    high_low_ratio[stock_num] = ( Filecont2ave_rms[4, stock_num] / Filecont2ave_rms[6, stock_num] ) * 100;

                    //listBox1.Items.Add("1079�s�� stock_num = " + stock_num + ", Filecont2ave_rms[3, stock_num] = " + Filecont2ave_rms[3, stock_num] + ", Filecont2ave_rms[5, stock_num] = " + Filecont2ave_rms[5, stock_num] + ", ");

                    //
                    //���������ȏ�𒊏o
                    //
                    //if (high_low_ratio[stock_num] >= 100 + updown_threshold )
                    if (high_low_ratio[stock_num] >= 100 + updown_threshold && Filecont2ave_rms[3, stock_num] > Filecont2ave_rms[5, stock_num])
                    {
                        //
                        //�����������J�E���g
                        //
                        recommend_count = recommend_count + 1;

                        //
                        //���������̃f�[�^�����o��
                        //
                        listBox1.Items.Add("1203�s�� �����ԍ� = " + Filecont[1, 1, stock_num] + " , ������ = " + Filecont[1, 2, stock_num] + ", �W���΍� = " + Filecont_rms_double[stock_num] + ", ������ = " + high_low_ratio[stock_num] + ", ");

                        //
                        //���������z��Ɋi�[
                        //
                        recommend[1, recommend_count] = Filecont[1, 1, stock_num];
                        recommend[2, recommend_count] = Filecont[1, 2, stock_num];
                        recommend[3, recommend_count] = Convert.ToString(Filecont_rms_double[stock_num]);
                        recommend[4, recommend_count] = Convert.ToString(high_low_ratio[stock_num]);
                        recommend[6, recommend_count] = Convert.ToString(Filecont2ave_rms[6, stock_num]);
                        recommend[7, recommend_count] = Convert.ToString(Filecont2ave_rms[4, stock_num]);
                    }
                    else
                    {
                        //�����ɍ��v���Ȃ��ꍇ�͉������Ȃ�
                    }
                }
                else
                {
                    //�����ɍ��v���Ȃ��ꍇ�͉������Ȃ�
                }
            }

            //
            //�t�@�C���o��
            //

            //
            //�V���ȃt�@�C����p�ӂ���O�Ɋ����t�@�C���폜
            //

            System.IO.File.Delete("Debug_Recommend.csv");

            //Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            //sjisEnc = Encoding.GetEncoding("Shift_JIS");
            writer = new System.IO.StreamWriter("Debug_Recommend.csv", true, sjisEnc);
            //System.IO.StreamWriter writer = new System.IO.StreamWriter("Debug_Recommend.csv", true, sjisEnc);

            /*
            System.IO.File.Delete(@dname + "Debug_Recommend_" + dt + ".csv");

            //Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            //sjisEnc = Encoding.GetEncoding("Shift_JIS");
            writer = new System.IO.StreamWriter(@dname + "Debug_Recommend_" + dt + ".csv", true, sjisEnc);
            //System.IO.StreamWriter writer = new System.IO.StreamWriter("Debug_Recommend.csv", true, sjisEnc);
            */

            //
            //���������̏����o��
            //
            writer.Write("�W���΍�臒l���, " + correlation_index_high + "\r\n");
            writer.Write("�W���΍�臒l����, " + correlation_index_low + "\r\n");
            writer.Write("������臒l, " + high_low + "\r\n");

            for (int i = 1; i < 10; i++)
            {
                for (int n3 = 0; n3 <= recommend_count; n3++)
                {
                    writer.Write(recommend[i, n3] + ",");
                }
                writer.Write("\r\n");
            }
            writer.Close();

            //
            //�����܂Ńt�@�C�������o��
            //



            if( recommend_count != 0 )
            {
                for (int n3 = 1; n3 < recommend_count + 1; n3++)
                {
                    double[,,] plot = new double[7, 3, 300];

                    for (int i = 1; i < 7; i++)
                    {
                        for (int j = 8; j <= max_days_count_file + 8; j++)
                        {
                            if (i == 1)
                            {
                                //if (j > ave_days_count_file )
                                //{

                                //}
                                //else
                                //{
                                    plot[i, 1, j] = (j - 8) / (ave_days_count_file - 9);
                                    //plot[i, 1, j] = Average[j, 0];

                                    plot[i, 2, j] = Average[j, (Convert.ToInt16(recommend[1, n3]))];

                                    //listBox1.Items.Add("1160�s�� plot[i, 1, j] = " + plot[i, 1, j] + " , plot[i, 2, j] = " + plot[i, 2, j] + ", ");
                                //}
                            }
                            else if (file_OKNG[i-1] == 1)
                            {

                                plot[i, 1, j] = (j - 8) / (days_count_file[i-1] - 8);

                                if (Filecont[i - 1, j, (Convert.ToInt16(recommend[1, n3]))] == "end")
                                {
                                    plot[i, 2, j] = plot[i, 2, (j - 1)];

                                    //listBox1.Items.Add("1171�s�� plot[i, 1, j] = " + plot[i, 1, j] + " , plot[i, 2, j] = " + plot[i, 2, j] + ", ");
                                }
                                else
                                {
                                    plot[i, 2, j] = Filecont_double[i - 1, j, (Convert.ToInt16(recommend[1, n3]))];

                                    //listBox1.Items.Add("1177�s�� plot[i, 1, j] = " + plot[i, 1, j] + " , plot[i, 2, j] = " + plot[i, 2, j] + ", ");
                                }
                            }
                            else
                            {

                            }
                        }
                    }

                    if(n3 != 0)
                    {
                        Form2 form2 = new Form2();
                        form2.Show();

                        form2.Form2_plot(plot, max_days_count_file, recommend[1, n3], recommend[2, n3], file_OKNG);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                listBox1.Items.Add("1318�s�� ���������Ȃ�");
            }


            //return Filecont_rms_double;
            return Filecont2ave_rms;

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

    }

}


