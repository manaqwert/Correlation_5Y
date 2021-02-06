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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            //int xxx = 1;
            //Form2_plot(xxx);
        }

        public void Form2_plot(double[,,] plot, double points, string stock_number, string stock_name, double[] file_OKNG)
        {
            string[] recommend_stock = new string[7];

            //recommend_stock[1] = "平均";
            //recommend_stock[2] = "file1";
            //recommend_stock[3] = "file2";
            //recommend_stock[4] = "file3";
            //recommend_stock[5] = "file4";
            //recommend_stock[6] = "file5";

            chart1.Series.Clear();


            for ( int i = 0; i <= 6; i++ )
            {
                if (i == 1)
                {
                    recommend_stock[i] = "Average";
                }
                else
                {
                    recommend_stock[i] = "file" + Convert.ToString(i - 1);
                }

                if (i == 1)
                {
                    //
                    //Seriesの作成
                    //

                    chart1.Series.Add(recommend_stock[i]);

                    //
                    //グラフタイプの設定
                    //
                    chart1.Series[recommend_stock[i]].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                    //chart1.Series[recommend_stock[i]].IsVisibleInLegend = true;

                }
                else if (i > 0 && file_OKNG[(i - 1)] == 1)
                {
                    //
                    //Seriesの作成
                    //
                    chart1.Series.Add(recommend_stock[i]);

                    //
                    //グラフタイプの設定
                    //
                    chart1.Series[recommend_stock[i]].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                    //chart1.Series[recommend_stock[i]].IsVisibleInLegend = true;

                }
                else
                {

                }
            }

            /*
            //
            //Seriesの作成
            //
            chart1.Series.Add("s1");
            chart1.Series.Add("s2");
            chart1.Series.Add("s3");
            chart1.Series.Add("s4");
            chart1.Series.Add("s5");
            chart1.Series.Add("s6");

            //
            //グラフタイプの設定
            //
            chart1.Series["s1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["s2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["s3"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["s4"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["s5"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["s6"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            */

            for ( int l = 1; l < 7; l++ )
            {
                if (l == 1)
                {
                    for (int j = 8; j <= points + 8; j++)
                    {
                        chart1.Series[recommend_stock[l]].Points.AddXY(plot[l, 1, j]*1.2, plot[l, 2, j]);
                        //chart1.Series["stock_name"].Points.AddXY(plot[l, 1, j], plot[l, 2, j]);

                        //chart1.Series[recommend_stock[l]].Name = stock_name + "平均";
                        //chart1.Series["stock_name"].Name = stock_name + "平均";
                    }
                }
                else if (file_OKNG[l-1] == 1)
                {
                    for (int j = 8; j <= points + 8; j++)
                    {
                        chart1.Series[recommend_stock[l]].Points.AddXY(plot[l, 1, j]*1.2, plot[l, 2, j]);
                        //chart1.Series["stock_name"].Points.AddXY(plot[l, 1, j], plot[l, 2, j]);

                        //chart1.Series[recommend_stock[l]].Name = stock_name + "file" + ( l - 1 );
                        //chart1.Series["stock_name"].Name = stock_name + "file" + (l - 1);
                    }
                }
                else
                {

                }
            }

            /*for (int j = 8; j <= points + 8; j++)
            {
                chart1.Series["s1"].Points.AddXY(plot[1, 1, j], plot[1, 2, j]);
                //chart1.Series["s1"].Name = "平均";
                if ( file_OKNG[1] == 1)
                {
                    chart1.Series["s2"].Points.AddXY(plot[2, 1, j], plot[2, 2, j]);
                    chart1.Series["s2"].Name = "file1";
                }
                else
                {

                }
                if (file_OKNG[2] == 1)
                {
                    chart1.Series["s3"].Points.AddXY(plot[3, 1, j], plot[3, 2, j]);
                    //chart1.Series["s3"].Name = stock_name + "file2";
                }
                else
                {

                }
                if (file_OKNG[3] == 1)
                {
                    chart1.Series["s4"].Points.AddXY(plot[4, 1, j], plot[4, 2, j]);
                    //chart1.Series["s4"].Name = stock_name + "file3";
                }
                else
                {

                }
                if (file_OKNG[4] == 1)
                {
                    chart1.Series["s5"].Name = stock_name + "file4";
                    //chart1.Series["s5"].Points.AddXY(plot[5, 1, j], plot[5, 2, j]);
                }
                else
                {

                }
                if (file_OKNG[5] == 1)
                {
                    chart1.Series["s6"].Points.AddXY(plot[6, 1, j], plot[6, 2, j]);
                    //chart1.Series["s6"].Name = stock_name + "file5";
                }
                else
                {

                }
            }*/

            //
            //グラフ下のテキストボックスに銘柄番号、銘柄を記入
            //
            textBox1.Text = stock_number + " " + stock_name;
            //
            //ウィンドウタイトルに銘柄番号、銘柄を記入
            //
            this.Text = stock_number + " " + stock_name;

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textbox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
