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
        public 複数ファイルを選択する()
        {
            InitializeComponent();
        }

        private void File1_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        string File1_sel_button_Click(object sender, EventArgs e)
        {
            //フォルダ選択ダイアログの初期値指定
            //string file_sel.SelectedPath = @"C:\Data\OneDrive\stock";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            string folder_name = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);

            //dnameにテキストボックスのフォルダ名を入れる
            string dname = openFileDialog1.FileName;

            //テキストボックスに選択したファイル名を表示する
            File1_sel_Box.Text = dname;

            if (System.IO.File.Exists(dname) == false)
            {
                MessageBox.Show(dname + "が見つかりません。", "通知");
                return dname;
            }

            return dname;

        }

        private void File2_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        string File2_sel_button_Click(object sender, EventArgs e)
        {
            //フォルダ選択ダイアログの初期値指定
            //string file_sel.SelectedPath = @"C:\Data\OneDrive\stock";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();

            string folder_name = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);

            //dnameにテキストボックスのフォルダ名を入れる
            string dname = openFileDialog1.FileName;

            //テキストボックスに選択したファイル名を表示する
            File2_sel_Box.Text = dname;

            if (System.IO.File.Exists(dname) == false)
            {
                MessageBox.Show(dname + "が見つかりません。", "通知");
                return dname;
            }

            return dname;

        }

        private void File3_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void File3_sel_button_Click(object sender, EventArgs e)
        {

        }

        private void File4_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void File4_sel_button_Click(object sender, EventArgs e)
        {

        }

        private void File5_sel_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void File5_sel_button_Click(object sender, EventArgs e)
        {

        }

        private void 複数ファイルを選択する_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Compute_button_Click(object sender, EventArgs e)
        {
            //
            //プログラム本体
            //

            //string dname;
            //dname = opening_file();


        }

        private void Quit_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("終了します。", "通知");
            //アプリケーションを終了する。
            Application.Exit();

        }

        //
        //ここからサブルーチン
        //


    }
}
