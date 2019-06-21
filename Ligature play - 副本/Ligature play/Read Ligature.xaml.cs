using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace Ligature_play
{
    /// <summary>
    /// Read_Ligature.xaml 的交互逻辑
    /// </summary>
    public partial class Read_Ligature : Window
    {
        private String path = null;

        public Read_Ligature()
        {
            InitializeComponent();

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            MainWindow.epuzzle.AllClear();
        }

        protected void button1_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine("open prepare...");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开(Open)";
            ofd.FileName = "";
            ofd.InitialDirectory = @"D:\Kinect\ours\Ligature play\Ligature play\bin\Debug";
            ofd.Filter = "文本文件(*.txt)|*.txt";
            ofd.ValidateNames = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            try
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = ofd.FileName;
                    StreamReader sr = new StreamReader(ofd.FileName, System.Text.Encoding.Default);
                    this.richTextBox1.Text = sr.ReadToEnd();
                    puzzleDotsAdd(path);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
        }

        private void puzzleDotsAdd(String path)
        {
            String[] dots = new String[richTextBox1.LineCount];
            Point[] points = new Point[richTextBox1.LineCount - 1];

            for (int i = 1; i < richTextBox1.LineCount - 1; i++)
            {
                dots[i] = richTextBox1.GetLineText(i);
                Console.Write(dots[i]);
                points[i].X = float.Parse(dots[i].Substring(50, 16));
                points[i].Y = float.Parse(dots[i].Substring(72, 17));
                Console.WriteLine(points[i]);

                MainWindow.epuzzle.Dots.Add(new Point(points[i].X, points[i].Y));
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
