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

namespace DepthImageStream_4
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
        private WriteableBitmap _depthImageBitMap;
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
                this._depthImageBitMap = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Gray16, null);
                this.depthImageBitmapRect = new Int32Rect(0, 0, depthStream.FrameWidth, depthStream.FrameHeight);
                this.depthImageStride = depthStream.FrameWidth * depthStream.FrameBytesPerPixel;

                SkeletonStream skeletonStream = kinectSensor.SkeletonStream;
                skeletonStream.Enable();

                //suggest choice the last
                //DepthImage.Source = this.depthImageBitMap;
                DepthImage.Source = this._depthImageBitMap;
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

                    CreatePlayerDepthImage(this.depthFrame, depthPixelData);
                    CalculatePlayerSize(this.depthFrame, depthPixelData);
                }
            }
        }

        private void CreatePlayerDepthImage(DepthImageFrame depthFrame, short[] pixelData)
        {
            int playerIndex;
            const int depthBytePerPixel = 4;
            byte[] enhPixelData = new byte[depthFrame.Width * depthFrame.Height * depthBytePerPixel];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += depthFrame.BytesPerPixel)
            {
                playerIndex = pixelData[i] & DepthImageFrame.PlayerIndexBitmask;

                if (playerIndex == 0)
                {
                    enhPixelData[j] = 0xFF;
                    enhPixelData[j + 1] = 0xFF;
                    enhPixelData[j + 2] = 0xFF;
                }
                else
                {
                    enhPixelData[j] = 0x00;
                    enhPixelData[j + 1] = 0x00;
                    enhPixelData[j + 2] = 0x00;
                }
            }
            this._depthImageBitMap.WritePixels(this.depthImageBitmapRect, enhPixelData, this.depthImageStride, 0);
        }

        private void CalculatePlayerSize(DepthImageFrame depthFrame, short[] pixelData)
        {
            int depth;
            int playerIndex;
            int pixelIndex;
            int bytesPerPixel = depthFrame.BytesPerPixel;
            PlayerDepthData[] players = new PlayerDepthData[6];

            for(int row = 0; row < depthFrame.Height; row++)
            {
                for(int col = 0; col < depthFrame.Width; col++)
                {
                    pixelIndex = col + (row * depthFrame.Width);
                    depth = pixelData[pixelIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                    if(depth != 0)
                    {
                        playerIndex = (pixelData[pixelIndex] & DepthImageFrame.PlayerIndexBitmask) - 1;

                        if(playerIndex > -1)
                        {
                            if(players[playerIndex] == null)
                            {
                                players[playerIndex] = new PlayerDepthData(playerIndex + 1, depthFrame.Width, depthFrame.Height);
                            }

                            players[playerIndex].UpdataData(col, row, depth);
                        }
                    }
                }
            }

            PlayerDepthData.ItemsSource = players;
        }
    }

    internal class PlayerDepthData
    {
        #region Member Variables
        private const double MillimetersPerInch = 0.0393700787;
        private static readonly double HorizontalTanA = Math.Tan(57.0 / 2.0 * Math.PI / 180);
        private static readonly double VerticalTanA = Math.Abs(Math.Tan(43.0 / 2.0 * Math.PI / 180));

        private int _DepthSum;
        private int _DepthCount;
        private int _LoWidth;
        private int _HiWidth;
        private int _LoHeight;
        private int _HiHeight;
        #endregion Member Variables

        #region Constructor
        public PlayerDepthData(int playerId, double frameWidth, double frameHeight)
        {
            this.PlayerId = playerId;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;

            this._LoWidth = int.MaxValue;
            this._HiWidth = int.MinValue;

            this._LoHeight = int.MaxValue;
            this._HiHeight = int.MinValue;
        }
        #endregion Constructor

        #region Methods
        public void UpdataData(int x, int y, int depth)
        {
            this._DepthCount++;
            this._DepthSum += depth;

            this._LoWidth = Math.Min(this._LoWidth, x);
            this._HiWidth = Math.Max(this._HiWidth, x);
            this._LoHeight = Math.Min(this._LoHeight, y);
            this._HiHeight = Math.Max(this._HiHeight, y);
        }
        #endregion Methods

        #region Properties
        public int PlayerId { get; private set; }
        public double FrameWidth { get; private set; }
        public double FrameHeight { get; private set; }

        public double Depth
        {
            get { return this._DepthSum / (double)this._DepthCount; }
        }

        public int PixelWidth
        {
            get { return this._HiWidth - this._LoWidth; }
        }

        public int PixelHeight
        {
            get { return this._HiHeight - this._LoHeight; }
        }

        public string RealWidth
        {
            get
            {
                double inches = this.RealWidthInches;
                return string.Format("{0:0.0}mm", inches * 25.4);
            }
        }

        public string RealHeight
        {
            get
            {
                double inches = this.RealHeightInches;
                return string.Format("{0:0.0}mm", inches * 25.4);
            }
        }

        public double RealWidthInches
        {
            get
            {
                double opposite = this.Depth * HorizontalTanA;
                return this.PixelWidth * 2 * opposite / this.FrameWidth * MillimetersPerInch;
            }
        }

        public double RealHeightInches
        {
            get
            {
                double opposite = this.Depth * VerticalTanA;
                return this.PixelHeight * 2 * opposite / this.FrameHeight * MillimetersPerInch;
            }
        }
        #endregion Properties
    }
}
