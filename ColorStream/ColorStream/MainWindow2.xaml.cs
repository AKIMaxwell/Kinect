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
using System.IO;
using System.Threading;
using System.Timers;

namespace initialication_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    public partial class MainWindow : Window
    {
        //private Kinectsensor object
        private KinectSensor kinect;

        public KinectSensor Kinect
        {
            get { return this.kinect; }
            set
            {
                if (this.kinect != value)
                {
                    if (this.kinect != null)
                    {
                        UninitializeKinectSensor(this.kinect);
                        this.kinect = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        this.kinect = value;
                        InitializeKinectSensor(this.kinect);
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => DiscoverKinectSensor();
            this.Unloaded += (s, e) => this.kinect = null;
        }

        private void DiscoverKinectSensor()
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Connected:
                    if (this.kinect == null)
                        this.kinect = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (this.kinect == e.Sensor)
                    {
                        this.kinect = null;
                        this.kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (this.kinect == null)
                        {
                            //pushed
                        }
                    }
                    break;
            }
        }

        private WriteableBitmap colorImageBitmap;
        private Int32Rect colorImageBitmapRect;
        private int colorImageStride;
        //private byte[] colorImagePixelData;

        private void InitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                ColorImageStream colorStream = kinectSensor.ColorStream;
                kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                this.colorImageBitmap = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null);
                this.colorImageBitmapRect = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                this.colorImageStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;
                ColorImageElement.Source = this.colorImageBitmap;
                kinectSensor.ColorFrameReady += kinectSensor_ColorFrameReady;
                //kinectSensor.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
                kinectSensor.Start();
            }
        }

        private void UninitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                kinectSensor.Stop();
                kinectSensor.ColorFrameReady -= new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
            }
        }

        void kinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame frame = e.OpenColorImageFrame())
            {
                if (frame != null)
                {
                    byte[] pixelData = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(pixelData);
                    //for (int i = 0; i < pixelData.Length; i += frame.BytesPerPixel)
                    //{
                    //Only red
                    //pixelData[i] = 0x00;
                    //pixelData[i + 1] = 0x00;

                    //Inverted color
                    //pixelData[i] = (byte)~pixelData[i];
                    //pixelData[i + 1] = (byte)~pixelData[i + 1];
                    //pixelData[i + 2] = (byte)~pixelData[i + 2];

                    //Apocalyptic zombie
                    //pixelData[i] = pixelData[i + 1];
                    //pixelData[i + 1] = pixelData[i];
                    //pixelData[i + 2] = (byte)~pixelData[i + 2];

                    //Gray csale
                    //byte gray = Math.Max(pixelData[i], pixelData[i + 1]);
                    //gray = Math.Max(gray, pixelData[i + 2]);
                    //pixelData[i] = gray;
                    //pixelData[i + 1] = gray;
                    //pixelData[i + 2] = gray;

                    //Grain black and white movie
                    //byte gray = Math.Max(pixelData[i], pixelData[i + 1]);
                    //gray = Math.Max(gray, pixelData[i + 2]);
                    //pixelData[i] = gray;
                    //pixelData[i + 1] = gray;
                    //pixelData[i + 2] = gray;

                    //Washed out color
                    //double gray = (pixelData[i] * 0.11) + (pixelData[i + 1] * 0.59) + (pixelData[i + 2] * 0.3);
                    //double desaturation = 0.75;
                    //pixelData[i] = (byte)(pixelData[i] + desaturation * (gray - pixelData[i]));
                    //pixelData[i + 1] = (byte)(pixelData[i + 1] + desaturation * (gray - pixelData[i + 1]));
                    //pixelData[i + 2] = (byte)(pixelData[i + 2] + desaturation * (gray - pixelData[i + 2]));

                    //High saturation
                    //if (pixelData[i] < 0x33 || pixelData[i] > 0xE5) { pixelData[i] = 0x00; }
                    //else { pixelData[i] = 0xFF; }
                    //if (pixelData[i + 1] < 0x33 || pixelData[i + 1] > 0xE5) { pixelData[i + 1] = 0x00; }
                    //else { pixelData[i + 1] = 0xFF; }
                    //if (pixelData[i + 2] < 0x33 || pixelData[i + 2] > 0xE5) { pixelData[i + 2] = 0x00; }
                    //else { pixelData[i + 2] = 0xFF; }
                    //}
                    //ColorImageElement.Source = BitmapImage.Create(frame.Width, frame.Height, 96, 96,
                    //    PixelFormats.Bgr32, null, pixelData, frame.Width * frame.BytesPerPixel);

                    this.colorImageBitmap.WritePixels(this.colorImageBitmapRect, pixelData, this.colorImageStride, 0);
                }
            }
        }

        private String fileName;
        private void TakePictureButton_Click(object sender, RoutedEventArgs e)
        {
            Int32 number = 0;
            do
            {
                ++number;
                fileName = "snapshot(" + number + ").jpg";
            }
            while (File.Exists(fileName) == true);

            SavePicture();
        }
        Int32 number2 = 0;
        System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒；

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private void TakeThreePicture(object sender, RoutedEventArgs e)
        {

            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Start();
            
        }
 
        private void _TakeThreePicture(object sender, RoutedEventArgs e)
        {
            Int32 number1 = 56;
            do
            {
                ++number1;
                fileName = "snapshot(" + number1 + ".1).jpg";
            }
            while (File.Exists(fileName) == true);

            if(File.Exists("snapshot(" + (number1 - 1) + ".3).jpg") != true)
            {
                fileName = "snapshot(" + (number1 - 1) + ".3).jpg";
            }
            if (File.Exists("snapshot(" + (number1 - 1) + ".2).jpg") != true)
            {
                fileName = "snapshot(" + (number1 - 1) + ".2).jpg";
            }

            SavePicture();
        }
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            mouse_event(0x0010, 0, 0, 0, 0);
            number2++;
            if (number2 == 3)
            {
                t.Dispose();
                number2 = 0;
            }
            Console.Write("OK!");
        }

        private void SavePicture()
        {
            using (FileStream savedSnapshot = new FileStream(fileName, FileMode.CreateNew))
            {
                BitmapSource image = (BitmapSource)ColorImageElement.Source;
                JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
                jpgEncoder.QualityLevel = 70;
                jpgEncoder.Frames.Add(BitmapFrame.Create(image));
                jpgEncoder.Save(savedSnapshot);

                savedSnapshot.Flush();
                savedSnapshot.Close();
                savedSnapshot.Dispose();
                Console.Beep();
                Console.WriteLine(fileName + " had been saved.");
            }
        }
    }
}
