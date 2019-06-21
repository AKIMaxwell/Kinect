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

namespace SkeletonStream_1_Composition
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        public KinectSensor KinectDevice
        {
            get { return this._KinectDevice; }
            set
            {
                if (this._KinectDevice != value)
                {
                    if (this._KinectDevice != null)
                    {
                        UninitializeKinectSensor(this._KinectDevice);
                        this._KinectDevice = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        this._KinectDevice = value;
                        InitializeKinectSensor(this._KinectDevice);
                    }
                }
            }
        }
        #endregion Properties

        public MainWindow()
        {
            InitializeComponent();

            skeletonBrushes = new Brush[] { Brushes.Black, Brushes.Crimson, Brushes.Indigo, Brushes.DodgerBlue, Brushes.Purple, Brushes.Pink };

            this._DoUsePolling = true;

            if (this._DoUsePolling)
            {
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
            else
            {
                KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
                this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
            }
        }

        #region Member Variables
        private KinectSensor _KinectDevice;
        private WriteableBitmap _GreenScreenImage;
        private Int32Rect _GreenScreenImageRect;
        private int _GreenScreenImageStride;
        private short[] _DepthPixelData;
        private byte[] _ColorPixelData;
        private bool _DoUsePolling;
        private readonly Brush[] skeletonBrushes;
        private Skeleton[] frameSkeletons;
        #endregion Member Variables

        #region Methods
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            DiscoverKinect();

            if (this.KinectDevice != null)
            {
                try
                {
                    using (ColorImageFrame colorFrame = this.KinectDevice.ColorStream.OpenNextFrame(100))
                    {
                        using (DepthImageFrame depthFrame = this.KinectDevice.DepthStream.OpenNextFrame(100))
                        {
                            using (SkeletonFrame skeletonFrame = this.KinectDevice.SkeletonStream.OpenNextFrame(1000))
                            {
                                RenderGreenScreen(this._KinectDevice, colorFrame, depthFrame);

                                KinectDevice_SkeletonFrameReady(skeletonFrame);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //Do nothing, because the likely result is that the Kinect has been unplugged.
                }
            }
        }

        private void DiscoverKinect()
        {
            if (this._KinectDevice != null && this._KinectDevice.Status != KinectStatus.Connected)
            {
                UninitializeKinectSensor(this._KinectDevice);
                this._KinectDevice = null;
            }
            if (this._KinectDevice == null)
            {
                KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
                this._KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

                if (this._KinectDevice != null)
                {
                    InitializeKinectSensor(this._KinectDevice);
                }
            }
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Connected:
                    if (this._KinectDevice == null)
                        this._KinectDevice = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (this._KinectDevice == e.Sensor)
                    {
                        this._KinectDevice = null;
                        this._KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (this._KinectDevice == null)
                        {
                            //pushed
                        }
                    }
                    break;
            }
        }

        private void InitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                sensor.DepthStream.Range = DepthRange.Default;

                sensor.SkeletonStream.Enable();
                sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                sensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);

                DepthImageStream depthStream = sensor.DepthStream;
                this._GreenScreenImage = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Bgra32, null);
                this._GreenScreenImageRect = new Int32Rect(0, 0, (int)Math.Ceiling(this._GreenScreenImage.Width), (int)Math.Ceiling(this._GreenScreenImage.Height));
                this._GreenScreenImageStride = depthStream.FrameWidth * 4;
                this.GreenScreenImage.Source = this._GreenScreenImage;

                this._DepthPixelData = new short[this._KinectDevice.DepthStream.FramePixelDataLength];
                this._ColorPixelData = new byte[this._KinectDevice.ColorStream.FramePixelDataLength];

                this.frameSkeletons = new Skeleton[this._KinectDevice.SkeletonStream.FrameSkeletonArrayLength];


                if (!this._DoUsePolling)
                {
                    sensor.AllFramesReady += KinectDevice_AllFramesReady;
                }

                sensor.Start();
            }
        }

        private void UninitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                sensor.Stop();
                sensor.ColorStream.Disable();
                sensor.DepthStream.Disable();
                sensor.SkeletonStream.Disable();
                sensor.AllFramesReady -= KinectDevice_AllFramesReady;
            }
        }

        private void KinectDevice_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                {
                    using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
                    {
                        RenderGreenScreen(this._KinectDevice, colorFrame, depthFrame);

                        KinectDevice_SkeletonFrameReady(skeletonFrame);
                    }
                }
            }
        }

        private void RenderGreenScreen(KinectSensor kinectDevice, ColorImageFrame colorFrame, DepthImageFrame depthFrame)
        {
            if (kinectDevice != null && depthFrame != null && colorFrame != null)
            {
                int depthPixelIndex;
                int playerIndex;
                int colorPixelIndex;
                ColorImagePoint colorPoint;
                //DepthImagePoint depthPoint = new DepthImagePoint();
                int colorStride = colorFrame.BytesPerPixel * colorFrame.Width;
                int bytesPerPixel = 4;
                byte[] playerImage = new byte[depthFrame.Height * this._GreenScreenImageStride];
                int playerImageIndex = 0;

                depthFrame.CopyPixelDataTo(this._DepthPixelData);
                colorFrame.CopyPixelDataTo(this._ColorPixelData);

                for (int depthY = 0; depthY < depthFrame.Height; depthY++)
                {
                    for (int depthX = 0; depthX < depthFrame.Width; depthX++, playerImageIndex += bytesPerPixel)
                    {
                        depthPixelIndex = depthX + (depthY * depthFrame.Width);
                        playerIndex = this._DepthPixelData[depthPixelIndex] & DepthImageFrame.PlayerIndexBitmask;

                        if (playerIndex != 0)
                        {
                            //depthPoint.X = depthX;
                            //depthPoint.Y = depthY;
                            //colorPoint = kinectDevice.CoordinateMapper.MapDepthPointToColorPoint(depthFrame.Format, depthPoint, colorFrame.Format);
                            #pragma warning disable CS0618
                            colorPoint = kinectDevice.MapDepthToColorImagePoint(depthFrame.Format, depthX, depthY, this._DepthPixelData[depthPixelIndex], colorFrame.Format);
                            #pragma warning restore CS0618
                            colorPixelIndex = (colorPoint.X * colorFrame.BytesPerPixel) + (colorPoint.Y * colorStride);

                            playerImage[playerImageIndex] = this._ColorPixelData[colorPixelIndex];          //Blue
                            playerImage[playerImageIndex + 1] = this._ColorPixelData[colorPixelIndex + 1];  //Green
                            playerImage[playerImageIndex + 2] = this._ColorPixelData[colorPixelIndex + 2];  //Red
                            playerImage[playerImageIndex + 3] = 0xFF;                                       //Alpha
                        }
                    }
                }

                this._GreenScreenImage.WritePixels(this._GreenScreenImageRect, playerImage, this._GreenScreenImageStride, 0);
            }
        }

        private void KinectDevice_SkeletonFrameReady(SkeletonFrame frame)
        {
                if (frame != null)
                {
                    Polyline figure;
                    Brush userBrush;
                    Skeleton skeleton;

                    LayoutRoot.Children.Clear();
                    frame.CopySkeletonDataTo(this.frameSkeletons);

                    for (int i = 0; i < this.frameSkeletons.Length; i++)
                    {
                        skeleton = this.frameSkeletons[i];

                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            userBrush = this.skeletonBrushes[i % this.skeletonBrushes.Length];

                            //figure the head and boby
                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.Head, JointType.ShoulderCenter, JointType.ShoulderLeft, JointType.Spine, JointType.ShoulderRight, JointType.ShoulderCenter, JointType.HipCenter });
                            LayoutRoot.Children.Add(figure);

                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipLeft, JointType.HipRight });
                            LayoutRoot.Children.Add(figure);

                            //figure the leg
                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipCenter, JointType.HipLeft, JointType.KneeLeft, JointType.AnkleLeft, JointType.FootLeft });
                            LayoutRoot.Children.Add(figure);

                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipCenter, JointType.HipRight, JointType.KneeRight, JointType.AnkleRight, JointType.FootRight });
                            LayoutRoot.Children.Add(figure);

                            //figure the arm
                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.ShoulderLeft, JointType.ElbowLeft, JointType.WristLeft, JointType.HandLeft });
                            LayoutRoot.Children.Add(figure);

                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.ShoulderRight, JointType.ElbowRight, JointType.WristRight, JointType.HandRight });
                            LayoutRoot.Children.Add(figure);
                        }
                    }
                }
        }

        private Polyline CreateFigure(Skeleton skeleton, Brush brush, JointType[] joints)
        {
            Polyline figure = new Polyline();

            figure.StrokeThickness = 8;
            figure.Stroke = brush;

            for (int i = 0; i < joints.Length; i++)
            {
                figure.Points.Add(GetJointPoint(skeleton.Joints[joints[i]]));
            }

            return figure;
        }

        private Point GetJointPoint(Joint joint)
        {
            #pragma warning disable CS0618
            DepthImagePoint point = this.KinectDevice.MapSkeletonPointToDepth(joint.Position, this.KinectDevice.DepthStream.Format);
            #pragma warning restore CS0618

            point.X *= (int)this.LayoutRoot.ActualWidth / KinectDevice.DepthStream.FrameWidth;
            point.Y *= (int)this.LayoutRoot.ActualHeight / KinectDevice.DepthStream.FrameHeight;

            return new Point(point.X, point.Y);
        }
        
        #endregion Methods
    }
}
