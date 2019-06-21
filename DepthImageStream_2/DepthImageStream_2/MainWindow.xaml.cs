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

namespace DepthImageStream_1
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

        private WriteableBitmap depthImageBitMap;
        private Int32Rect depthImageBitmapRect;
        private int depthImageStride;
        //private byte[] depthImagePixelData;

        private void InitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                DepthImageStream depthStream = kinectSensor.DepthStream;
                depthStream.Enable();
                this.depthImageBitMap = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Gray16, null);
                this.depthImageBitmapRect = new Int32Rect(0, 0, depthStream.FrameWidth, depthStream.FrameHeight);
                this.depthImageStride = depthStream.FrameWidth * depthStream.FrameBytesPerPixel;

                DepthImage.Source = this.depthImageBitMap;
                kinectSensor.DepthFrameReady += kinectSensor_DepthFrameReady;
                //kinectSensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(kinectSensor_DepthFrameReady);
                kinectSensor.Start();
            }
        }

        private void UninitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                kinectSensor.Stop();
                kinectSensor.DepthFrameReady -= new EventHandler<DepthImageFrameReadyEventArgs>(kinectSensor_DepthFrameReady);
            }
        }

        private DepthImageFrame depthFrame;
        private short[] depthPixelData;
        void kinectSensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            if (depthFrame != null)
            {
                depthFrame.Dispose();
                depthFrame = null;
            }

            depthFrame = e.OpenDepthImageFrame();

            {
                if (depthFrame != null)
                {
                    depthPixelData = new short[depthFrame.PixelDataLength];
                    depthFrame.CopyPixelDataTo(depthPixelData);
                    this.depthImageBitMap.WritePixels(this.depthImageBitmapRect, depthPixelData, this.depthImageStride, 0);

                    CreateLighterShadesOfGray(this.depthFrame, depthPixelData);
                    CreateDepthHistogram(this.depthFrame, depthPixelData);
                }
            }
        }

        private void CreateLighterShadesOfGray(DepthImageFrame depthFrame, short[] pixelData)
        {
            Int32 depth;
            Int32 loThreashold = 0;
            Int32 hiThreshold = 3500;
            short[] enhPixelData = new short[depthFrame.Width * depthFrame.Height];
            for (int i = 0; i < pixelData.Length; i++)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if (depth < loThreashold || depth > hiThreshold)
                {
                    enhPixelData[i] = 0xFF;
                }
                else
                {
                    enhPixelData[i] = (short)~pixelData[i];
                }
            }
            FilteredDepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Gray16, null, enhPixelData, depthFrame.Width * depthFrame.BytesPerPixel);
        }

        private void CreateDepthHistogram(DepthImageFrame depthFrame, short[] pixelData)
        {
            int depth;
            int[] depths = new int[4096];
            double chartBarWidth = Math.Max(3, DepthHistogram.ActualWidth / depths.Length);
            int maxValue = 0;
            Int32 LoDepthThreshold = 0;
            Int32 HiDepthThreshold = 3500;

            DepthHistogram.Children.Clear();

            //statistics the depths
            for (int i = 0; i < pixelData.Length; i++)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if(depth >= LoDepthThreshold && depth <= HiDepthThreshold)
                {
                    depths[depth]++;
                }
            }

            //find the maximal depth
            for(int i = 0; i < depths.Length; i++)
            {
                maxValue = Math.Max(maxValue, depths[i]);
            }

            //paint the histogram
            for(int i = 0; i < depths.Length; i++)
            {
                if(depths[i] > 0)
                {
                    Rectangle r = new Rectangle();
                    r.Fill = Brushes.Gold;
                    r.Width = chartBarWidth;
                    r.Height = DepthHistogram.ActualHeight * (depths[i] / (double)maxValue);
                    r.Margin = new Thickness(1, 0, 1, 0);
                    r.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    DepthHistogram.Children.Add(r);
                }
            }
        }
    }
}
