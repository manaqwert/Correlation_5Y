using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _011_複数ファイルを選択する
{
    public partial class 複数ファイルを選択する : Form
    {
        //入力する配列定義
        string[,] File1cont = new string[300, 10000];
        string[,] File2cont = new string[300, 10000];
        string[,] File3cont = new string[300, 10000];
        string[,] File4cont = new string[300, 10000];
        string[,] File5cont = new string[300, 10000];

        //出力する配列定義
        string[,,] recomend_stock = new string[365, 10000, 6];

        string dt;

        string dname;

        public 複数ファイルを選択する()
        {
            InitializeComponent();
        }

        //
        //ファイルを選択する
        //
        string Open_file()
        {
            //フォルダ選択ダイアログの初期値指定
            //string file_sel.SelectedPath = @"C:\Data\OneDrive\stock";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            string folder_name = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);

            //dnameにテキストボックスのフォルダ名を入れる
            dname = openFileDialog1.FileName;

            if (System.IO.File.Exists(dname) == false)
            {
                MessageBox.Show(dname + "が見つかりません。", "通知");
                return dname;
            }

            return dname;

        }


        //
        //ファイルを読み込む
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
                    //ファイル一行読み込み、カンマ区切り毎にfield[]に格納
                    string file_contents_2 = sr.ReadLine();

                    string[] fields = file_contents_2.Split(',');

                    //
                    //列方向に文字があるだけ読んで配列に格納。要素数はfileds.Length
                    //
                    for (stock_num = 0; stock_num < fields.Length; stock_num++)
                    {
                        in_data_file_temp[days, stock_num] = fields[stock_num];
                    }
                }
                else
                {
                    //最後の行まで読み終わった場合の終了処置
                    //file_contents_Box.Items.Add(days + ", " + stock_num + ", " + "EndOfStream");
                    days2 = days;
                    days = 369;
                }
                //days3 = days3++;
            }

            //
            //列番号を銘柄番号に並び替え
            //
            for (int stock_num2 = 0; stock_num2 < 10000; stock_num2++)
            {
                for (stock_num = 0; stock_num < 10000; stock_num++)
                {
                    string stock_num_string;
                    stock_num_string = stock_num.ToString();
                    if (in_data_file_temp[1, stock_num2] == stock_num_string)
                    {
                        //stock_numと１行目が一致すれば格納
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
                        //stock_numと１行目が一致しなければ空欄
                    }
                }
            }

            listBox1.Items.Add("178行目 dname = " + dname + ", days3 = " + days3 + ", days2 = " + days2 + ", ");


            //days2は年初から年末までの営業日数
            int days4 = 0;

            for (double n2 = 0; n2 <= 369; n2++)
            {
                if (n2 > days2 - 2)
                {
                    in_data_file[(int)n2, 0] = "end";
                    //
                    //データの最終行識別のため（2018/1/21）
                    //
                    n2 = 370;
                }
                else
                {
                    //一列目は日付ではなく、年初から年末までの営業日数正規化
                    if (n2 <= 7)
                    {
                        //
                        //銘柄番号、銘柄名の行は無視
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
            //デバッグ用ファイル書き出し(in_data_file)
            //
            //
            //新たなファイルを用意する前に既存ファイル削除
            //
            dt = DateTime.Now.ToString($"yyyyMMddMHHmmss");

            System.IO.File.Delete(@dname + "_" + dt + "_indatafile.csv");

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            //sjisEnc = Encoding.GetEncoding("Shift_JIS");
            System.IO.StreamWriter writer = new System.IO.StreamWriter(@dname + "_" + dt + "_indatafile.csv", true, sjisEnc);
            //writer = new System.IO.StreamWriter(@dname + "_" + "_indatafile.csv", true, sjisEnc);

            //
            //計算結果の書き出し
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
            //ここまでファイル書き出し
            //

            //}

            //
            //ここまでファイル内容読み込み＆配列格納
            //

            return in_data_file;
        }


        //
        //ここからWindow生成関係
        //
        private void File1_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        public void File1_sel_button_Click(object sender, EventArgs e)
        {
            string File1;
            File1 = Open_file();

            //テキストボックスに選択したファイル名を表示する
            File1_sel_Box.Text = File1;

            //ファイルを読み込む
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

            //テキストボックスに選択したファイル名を表示する
            File2_sel_Box.Text = File2;

            //ファイルを読み込む
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

            //テキストボックスに選択したファイル名を表示する
            File3_sel_Box.Text = File3;

            //ファイルを読み込む
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

            //テキストボックスに選択したファイル名を表示する
            File4_sel_Box.Text = File4;

            //ファイルを読み込む
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

            //テキストボックスに選択したファイル名を表示する
            File5_sel_Box.Text = File5;

            //ファイルを読み込む
            //string[,] File5cont = new string[300, 5000];
            File5cont = read_file(File5);

        }

        private void 複数ファイルを選択する_Load(object sender, EventArgs e)
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
            double updown_threshold = 25; //騰落率（％）
            double correlation_threshold = 5; //標準偏差上限

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
            //プログラム本体
            //
            //listBox1.Items.Add("Compute_button_Click, ");

            dt = DateTime.Now.ToString($"yyyyMMddMHHmmss");

            //
            //相関係数取るため、ファイル数をチェック
            //「1001日経225」があればファイルありと判断
            //
            if ( File1cont[1,1001] == 1001.ToString() )
            {
                //fileが存在する場合は相関係数計算用の係数×1
                file1 = 1;
            }
            else
            {
                //fileが存在する場合は相関係数計算用の係数×0
                file1 = 0;
            }
            if (File2cont[1, 1001] == 1001.ToString())
            {
                //fileが存在する場合は相関係数計算用の係数×1
                file2 = 1;
            }
            else
            {
                //fileが存在する場合は相関係数計算用の係数×0
                file2 = 0;
            }
            if (File3cont[1, 1001] == 1001.ToString())
            {
                //fileが存在する場合は相関係数計算用の係数×1
                file3 = 1;
            }
            else
            {
                //fileが存在する場合は相関係数計算用の係数×0
                file3 = 0;
            }
            if (File4cont[1, 1001] == 1001.ToString())
            {
                //fileが存在する場合は相関係数計算用の係数×1
                file4 = 1;
            }
            else
            {
                //fileが存在する場合は相関係数計算用の係数×0
                file4 = 0;
            }
            if (File5cont[1, 1001] == 1001.ToString())
            {
                //fileが存在する場合は相関係数計算用の係数×1
                file5 = 1;
            }
            else
            {
                //fileが存在する場合は相関係数計算用の係数×0
                file5 = 0;
            }

            listBox1.Items.Add("426行目 file1 = " + file1 + ", file2 = " + file2 + ", file3 = " + file3 + ", file4 = " + file4 + ", file5 = " + file5 + ", ");

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
                    //ファイルが選択されていない場合のエラー処理必要
                    //
                }
            }

            File_correlation_rms = correlation(Filecont, file_OKNG, correlation_threshold, updown_threshold);
        }


        private void Quit_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("終了します。", "通知");
            //アプリケーションを終了する。
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
            //各銘柄毎に平均を計算
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
                        //各銘柄及び日付毎に平均を算出
                        //
                        Average[n, stock_num] = (File1cont_double + File2cont_double + File3cont_double + File4cont_double + File5cont_double) / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
                    }

                }

                //
                //各ファイルの日数を、日付欄でカウント
                //
                for (int m = 1; m < 6; m++)
                {
                    if (String.IsNullOrEmpty(Filecont[m, n, 0]) || Filecont[m, n, 0] == "end")
                    {
                        //listBox1.Items.Add("622行目 Filecont[m, n, 0] = " + Filecont[m, n, 0] + ", n =" + n + ", ");
                    }
                    else
                    {
                        //listBox1.Items.Add("626目 Filecont[m, n, 0] = " + Filecont[m, n, 0] + ", n =" + n + ", ");
                        days_count_file[m] = days_count_file[m] + 1;
                    }
                }
            }

            //
            //days_count_fileXの最大値探索
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
            //days_count_fileXの最小値探索
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
            //days_count_fileXの平均値探索
            //
            for (int m = 1; m < 6; m++)
            {
                ave_days_count_file = ave_days_count_file + days_count_file[m];
            }
            ave_days_count_file = ave_days_count_file / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);

            listBox1.Items.Add("686行目 Average[n, stock_num]計算, ");
            for(int m = 1; m < 6; m++)
            {
                listBox1.Items.Add("689行目 days_count_file[m] = " + days_count_file[m] + ", m = " + m +"\r\n");
            }
            listBox1.Items.Add("691行目 max_days_count_file = " + max_days_count_file + "\r\n");
            listBox1.Items.Add("692行目 min_days_count_file = " + min_days_count_file + "\r\n");
            listBox1.Items.Add("693行目 ave_days_count_file = " + ave_days_count_file + "\r\n");

            //
            //各ファイルの日付欄を正規化して、File_cont_doubleに入力
            //
            for (int days_ave = 1; days_ave <= max_days_count_file + 1; days_ave++)
            {
                for (int n = 1; n <= 5; n++)
                {
                    //if (String.IsNullOrEmpty(Filecont[n, 1, stock_num]))
                    //{
                        if (n == 1 && file_OKNG[1] == 1)
                        {
                            //最初の行が、0の時（データ無い時）forループ終了処置
                            days_ave = 370;
                            //listBox1.Items.Add("707行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 2 && file_OKNG[2] == 1)
                        {
                            //最初の行が、0の時（データ無い時）forループ終了処置
                            days_ave = 370;
                            //listBox1.Items.Add("720行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 3 && file_OKNG[3] == 1)
                        {
                            //最初の行が、0の時（データ無い時）forループ終了処置
                            days_ave = 370;
                            //listBox1.Items.Add("732行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 4 && file_OKNG[4] == 1)
                        {
                            //最初の行が、0の時（データ無い時）forループ終了処置
                            days_ave = 370;
                            //listBox1.Items.Add("744行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }

                        if (n == 5 && file_OKNG[5] == 1)
                        {
                            //最初の行が、0の時（データ無い時）forループ終了処置
                            days_ave = 370;
                            //listBox1.Items.Add("756行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                        }
                        else
                        {

                        }
                }

                //
                //日付欄のaverageを算出
                //
                Average[days_ave, 0] = Average[days_ave, 0] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
            }

            int progress_status = 0;

            //
            //同じ行数での正規化した株価の最小二乗値を計算
            //
            double[] Filecont_rms_double = new double[10001];

            double[,] Filecont2ave_rms = new double[371, 10001];

            for (stock_num = 1; stock_num < 10000; stock_num++)
            {
                //Filecont_rms_double[stock_num] = 0;

                //プログレスバーの設定
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 10000;

                //int progress_status = (stock_num / 10000) * 100;
                //progressBar1.Increment(progress_status);

                //プログレスバーの設置
                progress_status = (int)(stock_num / 10000) * 100;
                //progressBar1.Increment(progress_status);
                progressBar1.PerformStep();


                for (int days_ave = 1; days_ave <= max_days_count_file + 1; days_ave++)
                //
                //days_aveの最大値をファイルの日付数（days_count_file1～5の最大値）にしたい。
                //（2018/1/28）
                //
                {
                    for (int n = 1; n <= 5; n++)
                    {
                        if (String.IsNullOrEmpty(Filecont[n, 1, stock_num]))
                        {
                            if (n == 1 && file_OKNG[1] == 1)
                            {
                                //最初の行が、0の時（データ無い時）forループ終了処置
                                days_ave = 371;
                                //listBox1.Items.Add("707行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 2 && file_OKNG[2] == 1)
                            {
                                //最初の行が、0の時（データ無い時）forループ終了処置
                                days_ave = 371;
                                //listBox1.Items.Add("720行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 3 && file_OKNG[3] == 1)
                            {
                                //最初の行が、0の時（データ無い時）forループ終了処置
                                days_ave = 371;
                                //listBox1.Items.Add("732行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 4 && file_OKNG[4] == 1)
                            {
                                //最初の行が、0の時（データ無い時）forループ終了処置
                                days_ave = 371;
                                //listBox1.Items.Add("744行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }

                            if (n == 5 && file_OKNG[5] == 1)
                            {
                                //最初の行が、0の時（データ無い時）forループ終了処置
                                days_ave = 371;
                                //listBox1.Items.Add("756行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            if (days_ave > 7 && days_ave < 371)
                            //
                            //最大値、最小値等を含めたため変更、2→7
                            //2018/1/25
                            //
                            {
                                //
                                //途中のデータ抜け等を補完
                                //
                                if (String.IsNullOrEmpty(Filecont[n, days_ave, stock_num]))
                                {
                                    if (String.IsNullOrEmpty(Filecont[n, (days_ave - 1), stock_num]))
                                    {
                                        Filecont_double[n, days_ave, stock_num] = 100;
                                        //listBox1.Items.Add("データ抜け補完　779行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                                    }
                                    else
                                    {
                                        Filecont_double[n, days_ave, stock_num] = Convert.ToDouble(Filecont[n, (days_ave - 1), stock_num]);
                                        //listBox1.Items.Add("データ抜け補完　784行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                                    }
                                }
                                else
                                {
                                    Filecont_double[n, days_ave, stock_num] = Convert.ToDouble(Filecont[n, days_ave, stock_num]);
                                    //listBox1.Items.Add("データ抜け補完　790行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", ");
                                }

                                //
                                //標準偏差計算用
                                //
                                //Filecont_rms_double[stock_num] = Filecont_rms_double[stock_num] + Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2);
                                Filecont_rms_double[stock_num] = Filecont_rms_double[stock_num] + (Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2) + Math.Pow((Filecont_double[n, days_ave, 0] - Average[days_ave, 0]), 2));

                                //
                                //横軸正規化していないため、含めるとエラー
                                //（2018/1/28）
                                //

                                //
                                //5年平均値からの距離を加算
                                //
                                if (days_ave < 7)
                                {

                                }
                                else if (days_ave == 8)
                                {
                                    //Filecont2ave_rms[days_ave, stock_num] = Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2));
                                    Filecont2ave_rms[days_ave, stock_num] = Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2) + Math.Pow((Filecont_double[n, days_ave, 0] - Average[days_ave, 0]), 2));
                                    //listBox1.Items.Add("814行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                                }
                                else if (days_ave < 371)
                                {
                                    //Filecont2ave_rms[days_ave, stock_num] = Filecont2ave_rms[(days_ave - 1), stock_num] + Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2));
                                    Filecont2ave_rms[days_ave, stock_num] = Filecont2ave_rms[(days_ave - 1), stock_num] + Math.Sqrt(Math.Pow((Filecont_double[n, days_ave, stock_num] - Average[days_ave, stock_num]), 2) + Math.Pow((Filecont_double[n, days_ave, 0] - Average[days_ave, 0]), 2));
                                    //listBox1.Items.Add("819行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                                }
                                else
                                {
                                    //listBox1.Items.Add("823行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                                }
                            }
                            else
                            {
                                //listBox1.Items.Add("828行目 n = " + n + ", days_ave = " + days_ave + ", stock_num = " + stock_num);
                            }
                        }
                    }
                    //
                    //加算した年数分平均化
                    //
                    if (days_ave == 371)
                    {
                        //最初の行が、0の時（データ無い時）なにもしない
                    }
                    else
                    {
                        Filecont2ave_rms[days_ave, stock_num] = Filecont2ave_rms[days_ave, stock_num] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5]);
                        //listBox1.Items.Add("842目 days_ave = " + days_ave + ", stock_num = " + stock_num + ", Filecont2ave_rms = " + Filecont2ave_rms[days_ave, stock_num]);
                    }
                }
                //
                //加算した年数及びmax_days_count_fileで平均化
                //
                //Filecont_rms_double[stock_num] = (Filecont_rms_double[stock_num] / (file_OKNG[1] + file_OKNG[2] + file_OKNG[3] + file_OKNG[4] + file_OKNG[5])) / max_days_count_file;
                Filecont_rms_double[stock_num] = Math.Sqrt( Filecont_rms_double[stock_num] / ( max_days_count_file - 1 ) );
                //
                //これ標準偏差か？
                //
                //Filecont_rms_double[stock_num]の出力が0!!（2018/1/30）
                //
            }


            listBox1.Items.Add("1005行目 Filecont_rms_double[stock_num]算出, ");

            for (stock_num = 1; stock_num < 10000; stock_num++)
            {
                //listBox1.Items.Add("792行目 銘柄番号 = " + Filecont[1, 1, stock_num] + ", 銘柄名 = " + Filecont[1, 2, stock_num] + ", 標準偏差 = " + Filecont_rms_double[stock_num] + ", ");
            }

            //
            //デバッグ用のファイル書き出し
            //

            //
            //ファイル出力
            //

            //
            //新たなファイルを用意する前に既存ファイル削除
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
            //計算結果の書き出し
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
            //ここまでファイル書き出し
            //

            listBox1.Items.Add("1072行目 Debug_Filecont_rms_double" + dt + ".csv書き出し, ");


            //
            //まずはMAX、MIN比率計算して、ある程度以上でなければ計算しない
            //しきい値は外部関数外部から入力
            //double updown_threshold
            //

            for (stock_num = 2; stock_num < 10000; stock_num++)
            {
                for (int n = 1; n < 370; n++)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(Filecont2ave_rms[1, stock_num])))
                    {
                        //最初の行が、0の時（データ無い時）forループ終了処置
                        n = 370;
                    }
                    else
                    {

                    }
                    if (n > 7)
                    //
                    //最大値、最小値等を含めたため変更、2→7
                    //2018/1/25
                    //
                    {
                        Filecont2ave_rms[n, stock_num] = Filecont2ave_rms[n, stock_num];
                    }
                    else if (n == 4 || n == 6)
                    {
                        //
                        //最大値、最小値等は5年分の単純平均を出力
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
                        //最大値日付、最小値日付は0
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
                        //銘柄番号、銘柄名はそのまま出力
                        //2018/1/25
                        //
                    }
                }
            }


            //
            //推奨銘柄のlistBox1への書き出し
            //
            double correlation_index_high = correlation_threshold; //抽出する相関係数の上限閾値
            //double correlation_index_low = 0.0020479; //抽出する相関係数の下限閾値
            double correlation_index_low = 0; //抽出する相関係数の下限閾値

            double high_low = updown_threshold; //抽出する騰落率の閾値
            int recommend_count = 0; //推奨する銘柄の数
            string[,] recommend = new string[370, 10000];

            listBox1.Items.Add("");
            listBox1.Items.Add("");

            listBox1.Items.Add("1166行目 =====推奨銘柄抽出=====");
            listBox1.Items.Add("");

            //
            //推奨銘柄の条件書き出し
            //
            listBox1.Items.Add("1172行目 標準偏差閾値上限 = " + correlation_index_high + ", 標準偏差閾値下限 = " + correlation_index_low + ", 騰落率閾値 = " + high_low + ", ");
            listBox1.Items.Add("");

            double[] high_low_ratio = new double[10000];

            for (stock_num = 1; stock_num < 10000; stock_num++)
            {
                //
                //標準偏差が一定以下を抽出
                //
                //Filecont2ave_rms[n, stock_num]
                if (Filecont_rms_double[stock_num] <= correlation_index_high && Filecont_rms_double[stock_num] > correlation_index_low)
                {
                    //
                    //最大値と最小値から騰落率を算出
                    //
                    high_low_ratio[stock_num] = ( Filecont2ave_rms[4, stock_num] / Filecont2ave_rms[6, stock_num] ) * 100;

                    //listBox1.Items.Add("1079行目 stock_num = " + stock_num + ", Filecont2ave_rms[3, stock_num] = " + Filecont2ave_rms[3, stock_num] + ", Filecont2ave_rms[5, stock_num] = " + Filecont2ave_rms[5, stock_num] + ", ");

                    //
                    //騰落率一定以上を抽出
                    //
                    //if (high_low_ratio[stock_num] >= 100 + updown_threshold )
                    if (high_low_ratio[stock_num] >= 100 + updown_threshold && Filecont2ave_rms[3, stock_num] > Filecont2ave_rms[5, stock_num])
                    {
                        //
                        //推奨銘柄数カウント
                        //
                        recommend_count = recommend_count + 1;

                        //
                        //推奨銘柄のデータ書き出し
                        //
                        listBox1.Items.Add("1203行目 銘柄番号 = " + Filecont[1, 1, stock_num] + " , 銘柄名 = " + Filecont[1, 2, stock_num] + ", 標準偏差 = " + Filecont_rms_double[stock_num] + ", 騰落率 = " + high_low_ratio[stock_num] + ", ");

                        //
                        //推奨銘柄配列に格納
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
                        //条件に合致しない場合は何もしない
                    }
                }
                else
                {
                    //条件に合致しない場合は何もしない
                }
            }

            //
            //ファイル出力
            //

            //
            //新たなファイルを用意する前に既存ファイル削除
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
            //推奨銘柄の書き出し
            //
            writer.Write("標準偏差閾値上限, " + correlation_index_high + "\r\n");
            writer.Write("標準偏差閾値下限, " + correlation_index_low + "\r\n");
            writer.Write("騰落率閾値, " + high_low + "\r\n");

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
            //ここまでファイル書き出し
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

                                    //listBox1.Items.Add("1160行目 plot[i, 1, j] = " + plot[i, 1, j] + " , plot[i, 2, j] = " + plot[i, 2, j] + ", ");
                                //}
                            }
                            else if (file_OKNG[i-1] == 1)
                            {

                                plot[i, 1, j] = (j - 8) / (days_count_file[i-1] - 8);

                                if (Filecont[i - 1, j, (Convert.ToInt16(recommend[1, n3]))] == "end")
                                {
                                    plot[i, 2, j] = plot[i, 2, (j - 1)];

                                    //listBox1.Items.Add("1171行目 plot[i, 1, j] = " + plot[i, 1, j] + " , plot[i, 2, j] = " + plot[i, 2, j] + ", ");
                                }
                                else
                                {
                                    plot[i, 2, j] = Filecont_double[i - 1, j, (Convert.ToInt16(recommend[1, n3]))];

                                    //listBox1.Items.Add("1177行目 plot[i, 1, j] = " + plot[i, 1, j] + " , plot[i, 2, j] = " + plot[i, 2, j] + ", ");
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
                listBox1.Items.Add("1318行目 推奨銘柄なし");
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


