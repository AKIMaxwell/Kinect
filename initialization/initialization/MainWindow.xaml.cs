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
using Microsoft.Kinect;

namespace initialization
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static void sensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (var depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame == null) return;
                short[] bits = new short[depthFrame.PixelDataLength];
                depthFrame.CopyPixelDataTo(bits);
                foreach (var bit in bits)
                    Console.Write(bit);
            }
        }

        static void Main(string[] args)
        {
            //initialize senor
            KinectSensor sensor = KinectSensor.KinectSensors[0];

            //initialize camera
            sensor.DepthStream.Enable();
            sensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(sensor_DepthFrameReady);

            Console.ForegroundColor = ConsoleColor.Green;

            //open the datas
            sensor.Start();

            while (Console.Read() != 0)
            {

            }
        }
    }
}


