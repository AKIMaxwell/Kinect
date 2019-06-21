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
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Video_Stream
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
            //System.Drawing.Image O_Image = System.Drawing.Image.FromStream(WebRequest.Create("http://192.168.1.1:8080/?action=stream").GetResponse().GetResponseStream());
            PictureBox box1 = new PictureBox();
            //box1.Image = O_Image;
            box1.Load("http://192.168.1.1:8080/?action=stream");
        }
    }
}
