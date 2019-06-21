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
using System.Text;

namespace Save_the_Ligature
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Int32 number = 0;
        private String filename;
        private StreamWriter sw0;
        private StreamWriter sw;
        private int puzzleDotNumber = 1;
        public MainWindow()
        {
            InitializeComponent();
            do
            {
                ++this.number;
                this.filename = "ligature " + this.number + ".txt";
            }
            while (File.Exists(filename) == true);

            this.sw0 = new StreamWriter(filename);
            sw0.WriteLine("Copy:");
            sw0.Close();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            if (File.Exists(filename) == true)
            {
                Point p = e.GetPosition(Grid);

                this.sw = File.AppendText(filename);
                this.sw.WriteLine(String.Format("            this.puzzle.Dots.Add(new Point( {0}   ,   {1}     ));", p.X, p.Y));
                sw.Close();

                this.puzzleDotNumber++;
                Text1.Text = String.Format("No.{0} : ( {1} , {2} )", puzzleDotNumber, p.X, p.Y);
            }
        }

        private void Write_Point(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(Grid);

            Text1.Text = String.Format("No.{0} : ( {1} , {2} )", puzzleDotNumber, p.X, p.Y);
        }
    }
}
