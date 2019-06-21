using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
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
using GestureTraceLibrary;

namespace FruitKiller
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region variable
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        private Int32 number = 0;
        private String filename;
        private StreamWriter sw;
        private const bool isTest = false;
        private bool isdowm;

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private readonly int MOUSEEVENTF_MOVE = 0x0001;//模拟鼠标移动
        private readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标左键按下
        private readonly int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起
        private readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;//鼠标绝对位置
        private readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        private readonly int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        private readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        private readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起 

        private double _handLeft;
        public double HandLeft
        {
            get { return _handLeft; }
            set
            {
                _handLeft = value;
                OnPropertyChanged("HandLeft");
            }
        }

        private double _handTop;
        public double HandTop
        {
            get { return _handTop; }
            set
            {
                _handTop = value;
                OnPropertyChanged("HandTop");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion variable

        public MainWindow()
        {
            InitializeComponent();
            if (isTest)
            {
                do
                {
                    ++this.number;
                    this.filename = "ligature " + this.number + ".txt";
                }
                while (File.Exists(filename) == true);

                this.sw = new StreamWriter(filename);
                //sw.WriteLine("Copy:");
                sw.Close();
            }

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
            Viewer.isO = true;
        }

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
                        Viewer.KinectDevice = null;
                        this.kinectDevice.ColorStream.Disable();
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
                            Viewer.KinectDevice = this.KinectDevice;
                            this.kinectDevice.Start();

                            this.kinectDevice.ColorStream.Enable();
                        }
                    }
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

                    if (skeleton == null)
                    {
                        
                    }
                    else
                    {
                        DepthImagePoint leftpoint = TrackHand(skeleton.Joints[JointType.HandLeft], LeftHandElement);
                        DepthImagePoint rightpoint = TrackHand(skeleton.Joints[JointType.HandRight], RightHandElement);
                        DepthImagePoint centerpoint = TrackCenter(skeleton.Joints[JointType.ShoulderCenter]);

                        if (leftpoint != new DepthImagePoint() && rightpoint != new DepthImagePoint() && leftpoint.Depth > rightpoint.Depth)
                        {
                            MakeMouseMove(rightpoint);
                            KillTest(rightpoint, centerpoint);
                        }
                        else
                        {
                            MakeMouseMove(leftpoint);
                            KillTest(leftpoint, centerpoint);
                        }
                    }
                }
            }
        }

        private void MakeMouseMove(DepthImagePoint point)
        {
            Size size = new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
            Point OriginalPoint = new Point(point.X * size.Width / 640, point.Y * size.Height / 480);
            Point effectivePoint = EffectivePoint(OriginalPoint, size, 1.6, 1.5);
            //HandTop = depthPoint.Y * SystemParameters.PrimaryScreenHeight / 480;
            //HandLeft = depthPoint.X * SystemParameters.PrimaryScreenWidth / 640;
            HandTop = effectivePoint.Y / size.Height * 65535;
            HandLeft = effectivePoint.X / size.Width * 65535;
            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
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

        private DepthImagePoint TrackHand(Joint joint, Ellipse element)
        {
            if(joint.TrackingState == JointTrackingState.NotTracked)
            {
                element.Visibility = Visibility.Collapsed;
                return new DepthImagePoint();
            }
            else
            {
                element.Visibility = Visibility.Visible;
                #pragma warning disable CS0618
                DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(joint.Position, this.kinectDevice.DepthStream.Format);
                #pragma warning restore CS0618
                point.X = (int)((point.X * LayoutRoot.ActualWidth / kinectDevice.DepthStream.FrameWidth) - (element.ActualWidth / 2.0));
                point.Y = (int)((point.Y * LayoutRoot.ActualHeight / kinectDevice.DepthStream.FrameHeight) - (element.ActualHeight / 2.0));

                Canvas.SetLeft(element, point.X);
                Canvas.SetTop(element, point.Y);

                return point;
            }
        }

        private DepthImagePoint TrackCenter(Joint centerPoint)
        {
            #pragma warning disable CS0618
            DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(centerPoint.Position, this.kinectDevice.DepthStream.Format);
            #pragma warning restore CS0618
            return point;
        }

        private void KillTest(DepthImagePoint aimPoint, DepthImagePoint centerPoint)
        {
            if (isTest)
            {
                this.sw = File.AppendText(filename);
                this.sw.WriteLine("    X    Y    Z    C");
                this.sw.WriteLine(String.Format("{0} {1} {2} {3}", aimPoint.X, aimPoint.Y, aimPoint.Depth, centerPoint.Depth));
                sw.Close();
            }

            int det = centerPoint.Depth - aimPoint.Depth;

            if (det > 600 && !isdowm)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
                isdowm = true;
            }
            else if (det < 580 && isdowm)
            {
                mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
                isdowm = false;
            }

        }

        private Point EffectivePoint(Point OriginalPoint, Size size, Double fi_x, Double fi_y)
        {
            Point centre = new Point(size.Width / 2, size.Height / 2);
            Point DisposePoint = new Point(((OriginalPoint.X - centre.X) * fi_x + centre.X), ((OriginalPoint.Y - centre.Y) * fi_y + centre.Y));

            if (DisposePoint.X < 0) { DisposePoint.X = 0; }
            if (DisposePoint.X > size.Width) { DisposePoint.X = size.Width; }
            if (DisposePoint.Y < 0) { DisposePoint.Y = 0; }
            if (DisposePoint.Y > size.Height) { DisposePoint.Y = size.Height; }

            return DisposePoint;
        }

        private void Viewer_Initialized(object sender, EventArgs e)
        {

        }
    }
}
