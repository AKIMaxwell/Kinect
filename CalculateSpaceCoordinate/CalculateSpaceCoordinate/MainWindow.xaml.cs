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

namespace CalculateSpaceCoordinate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;

        private DepthImageFrame depthFrame;
        private short[] depthPixelData;

        private readonly Size DepthStreamSize = new Size(640, 480);
        private readonly Size AngleSize = new Size(57, 43);
        #endregion Member Variables


        public MainWindow()
        {
            InitializeComponent();
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
        }

        #region Properties
        public KinectSensor KinectDevice
        {
            get { return this.kinectDevice; }
            set
            {
                if (this.kinectDevice != value)
                {
                    //Uninitialize
                    if (this.kinectDevice != null)
                    {
                        this.kinectDevice.Stop();
                        this.kinectDevice.SkeletonFrameReady -= KinectDevice_SkeletonFrameReady;
                        this.kinectDevice.SkeletonStream.Disable();
                        this.frameSkeletons = null;
                        SkeletonViewer.KinectDevice = null;

                        this.kinectDevice.ColorStream.Disable();

                        kinectDevice.DepthFrameReady -= new EventHandler<DepthImageFrameReadyEventArgs>(KinectDevice_DepthFrameReady);
                    }

                    this.kinectDevice = value;

                    //Initialize
                    if (this.kinectDevice != null)
                    {
                        if (this.kinectDevice.Status == KinectStatus.Connected)
                        {
                            this.kinectDevice.SkeletonStream.Enable();
                            this.frameSkeletons = new Skeleton[this.kinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                            this.kinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;
                            this.kinectDevice.Start();
                            SkeletonViewer.KinectDevice = this.KinectDevice;

                            this.kinectDevice.ColorStream.Enable();

                            DepthImageStream depthStream = KinectDevice.DepthStream;
                            depthStream.Enable();

                            KinectDevice.DepthFrameReady += KinectDevice_DepthFrameReady;
                        }
                    }
                }
            }
        }

        #endregion Properties

        #region Methods
        private void KinectDevice_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
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
                }
            }
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                case KinectStatus.Connected:
                case KinectStatus.NotPowered:
                case KinectStatus.NotReady:
                case KinectStatus.DeviceNotGenuine:
                    this.KinectDevice = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    this.KinectDevice = null;
                    break;
                default:
                    break;
            }
        }

        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    frame.CopySkeletonDataTo(this.frameSkeletons);
                    Skeleton skeleton = GetPrimarySkeleton(this.frameSkeletons);

                    if (skeleton != null)
                    {
                        Joint primaryHand = GetPrimaryHand(skeleton);

                        Double z = GetZ(D_GetJointPoint(primaryHand));
                        number_z.Text = z.ToString();

                        Point WristRight = D_GetJointPoint(skeleton.Joints[JointType.ShoulderRight]);
                        Point ElbowRight = D_GetJointPoint(skeleton.Joints[JointType.ShoulderLeft]);

                        Double Length = GetLength(WristRight, ElbowRight);
                        LengthText.Text = Length.ToString();
                    }
                }
            }
        }

        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
            {
                //find the nearest player
                for (int i = 0; i < skeletons.Length; i++)
                {
                    if (skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (skeleton == null)
                        {
                            skeleton = skeletons[i];
                        }
                        else
                        {
                            if (skeleton.Position.Z > skeletons[i].Position.Z)
                            {
                                skeleton = skeletons[i];
                            }
                        }
                    }
                }
            }
            return skeleton;
        }

        private static Joint GetPrimaryHand(Skeleton skeleton)
        {
            Joint primaryHand = new Joint();
            if (skeleton != null)
            {
                primaryHand = skeleton.Joints[JointType.HandLeft];
                Joint rightHand = skeleton.Joints[JointType.HandRight];
                if (rightHand.TrackingState != JointTrackingState.NotTracked)
                {
                    if (primaryHand.TrackingState == JointTrackingState.NotTracked)
                    {
                        primaryHand = rightHand;
                    }
                    else
                    {
                        if (primaryHand.Position.Z > rightHand.Position.Z)
                        {
                            primaryHand = rightHand;
                        }
                    }
                }
            }
            return primaryHand;
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

        private Point D_GetJointPoint(Joint joint)
        {
            #pragma warning disable CS0618
            DepthImagePoint point = this.KinectDevice.MapSkeletonPointToDepth(joint.Position, this.KinectDevice.DepthStream.Format);
            #pragma warning restore CS0618

            Double PointX = point.X * this.DepthStreamSize.Width / KinectDevice.DepthStream.FrameWidth;
            Double PointY = point.Y * this.DepthStreamSize.Height / KinectDevice.DepthStream.FrameHeight;

            return new Point(PointX, PointY);
        }

        private Double GetZ(Point p)
        {
            Double depth;
            if (depthPixelData != null && depthPixelData.Length > 0 && this.depthFrame != null)
            {
                Int32 pixelIndex = (Int32)(p.X + ((Int32)p.Y * this.depthFrame.Width));
                if(pixelIndex <= this.depthPixelData.Length)
                {
                   depth = this.depthPixelData[pixelIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                }
                else
                {
                    depth = 0;
                }
                return depth;
            }
            else
            {
                return depth = new Double();
            }
            
        }

        private Double GetLength(Point BeginPoint, Point EndPoint)
        {
            Point BeginAngle = new Point(BeginPoint.X / this.DepthStreamSize.Width * this.AngleSize.Width, BeginPoint.X / this.DepthStreamSize.Height * this.AngleSize.Height);
            Point EndAngle = new Point(EndPoint.X / this.DepthStreamSize.Width * this.AngleSize.Width, EndPoint.X / this.DepthStreamSize.Height * this.AngleSize.Height);
            Point BegintoEndAngle = new Point(BeginAngle.X - EndAngle.X, BeginAngle.Y - EndAngle.Y);

            Double BeginZ = GetZ(BeginPoint);
            Double EndZ = GetZ(EndPoint);

            Double Length = Math.Sqrt(BeginZ * BeginZ + EndZ * EndZ - 2 * BeginZ * EndZ * Math.Cos(BegintoEndAngle.X) * Math.Cos(BegintoEndAngle.Y));

            BeginPointText.Text = BeginPoint.ToString();
            EndPointText.Text = EndPoint.ToString();
            BegintoEndAngleText.Text = BegintoEndAngle.ToString();
            BeginZText.Text = BeginZ.ToString();
            EndZText.Text = EndZ.ToString();


            return Length;
        }
        #endregion Methods
    }
}
