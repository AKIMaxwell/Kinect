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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace Read_the_Ligature
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>

    public partial class MainWindow : Window
    {
        private String path = null;

        public MainWindow()
        {
            InitializeComponent();
            //DrawPuzzle();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //System.Windows.Forms.Application.Run(new From1());
        }

        private void button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            dilog.Description = "Please choice a floder";
            //if(dilog.ShowDialog() == DialogResult.OK || )
        }

        protected void button1_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine("open prepare...");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开(Open)";
            ofd.FileName = "";
            ofd.InitialDirectory = @"D:\Kinect\ours\Save_the_Ligature\Save_the_Ligature\bin\Debug";
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

            for(int i = 1; i < richTextBox1.LineCount - 1; i++)
            {
                dots[i] = richTextBox1.GetLineText(i);
                Console.Write(dots[i]);
                points[i].X = int.Parse(dots[i].Substring(43, 3));
                points[i].Y = int.Parse(dots[i].Substring(47, 4));
                Console.WriteLine(points[i]);
            }
        }
    }
}

