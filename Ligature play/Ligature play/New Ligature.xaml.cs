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
using System.Windows.Forms;
using System.IO;

namespace Ligature_play
{
    /// <summary>
    /// New_Ligature.xaml 的交互逻辑
    /// </summary>
    public partial class New_Ligature : Window
    {
        private Int32 number = 0;
        private String filename;
        private StreamWriter sw0;
        private StreamWriter sw;
        private int puzzleDotNumber = 1;

        public New_Ligature()
        {
            InitializeComponent();
            MainWindow.epuzzle.Dots.Clear();
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

        private void Grid_MouseLeftButtonUp(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (File.Exists(filename) == true)
            {
                Point p = e.GetPosition(Grid);

                this.sw = File.AppendText(filename);
                this.sw.WriteLine(String.Format("            MainWindow.puzzle.Dots.Add(new Point( {0}   ,   {1}     ));", p.X, p.Y));
                sw.Close();

                this.puzzleDotNumber++;
                Text1.Text = String.Format("No.{0} : ( {1} , {2} )", puzzleDotNumber, p.X, p.Y);

                MainWindow.epuzzle.Dots.Add(new Point(p.X, p.Y));
                this.DrawPuzzle(MainWindow.epuzzle);
            }
        }

        private void Write_Point(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition(Grid);

            Text1.Text = String.Format("No.{0} : ( {1} , {2} )", puzzleDotNumber, p.X, p.Y);
        }

        public void DrawPuzzle(MainWindow.eDotPuzzle puzzle)
        {
            PuzzleBoardElement.Children.Clear();

            if (puzzle != null)
            {
                for (int i = 0; i < puzzle.Dots.Count; i++)
                {
                    Grid dotContainer = new Grid();
                    dotContainer.Width = 60;
                    dotContainer.Height = 60;
                    dotContainer.Children.Add(new Ellipse { Fill = Brushes.Gray });

                    TextBlock dotLabel = new TextBlock();
                    dotLabel.Text = (i + 1).ToString();
                    dotLabel.Foreground = Brushes.White;
                    dotLabel.FontSize = 24;
                    dotLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    dotLabel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    dotContainer.Children.Add(dotLabel);

                    //draw dots in UI
                    Canvas.SetTop(dotContainer, puzzle.Dots[i].Y - (dotContainer.Height / 2));
                    Canvas.SetLeft(dotContainer, puzzle.Dots[i].X - (dotContainer.Width / 2));
                    PuzzleBoardElement.Children.Add(dotContainer);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
