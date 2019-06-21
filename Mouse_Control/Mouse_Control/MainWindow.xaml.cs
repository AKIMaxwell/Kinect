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
using System.Windows.Forms;
using Microsoft.Kinect;

namespace Mouse_Control
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;

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
                            this.kinectDevice.Start();

                            this.kinectDevice.ColorStream.Enable();
                        }
                    }
                }
            }
        }
        #endregion Properties

        #region Methods
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

                    if(skeleton != null)
                    {
                        //Size size = new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
                        Size size = new Size(65535, 65535);
                        Joint primaryHand = GetPrimaryHand(skeleton);

                        Point point = GetJointPoint(this.kinectDevice, primaryHand, size, new Point());

                        //Console.Write(string.Format("( {0}, {1} )  ", point.X, point.Y));
                        point = EffectivePoint(point, size, 1.6, 1.5);
                        mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, (int)point.X, (int)point.Y, 0, 0);//移动到需要点击的位置

                        Judge_Click(skeleton.Joints[JointType.HandRight], skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ElbowRight]);
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

        private Point GetJointPoint(KinectSensor kinectDevice, Joint joint, Size renderSize, Point point)
        {
            #pragma warning disable CS0618
            DepthImagePoint _point = kinectDevice.MapSkeletonPointToDepth(joint.Position, kinectDevice.DepthStream.Format);
            #pragma warning restore CS0618

            point.X = _point.X * (int)renderSize.Width / kinectDevice.DepthStream.FrameWidth;
            point.Y = _point.Y * (int)renderSize.Height / kinectDevice.DepthStream.FrameHeight;

            return point;
        }

        private Point EffectivePoint(Point OriginalPoint, Size size, Double fi_x, Double fi_y)
        {
            Point centre = new Point(size.Width / 2, size.Height / 2);
            Point DisposePoint = new Point(((OriginalPoint.X - centre.X) * fi_x + centre.X), ((OriginalPoint.Y - centre.Y) * fi_y + centre.Y));

            if(DisposePoint.X < 0) { DisposePoint.X = 0; }
            if(DisposePoint.X > size.Width) { DisposePoint.X = size.Width; }
            if(DisposePoint.Y < 0) { DisposePoint.Y = 0; }
            if(DisposePoint.Y > size.Height) { DisposePoint.Y = size.Height; }

            return DisposePoint;
        }
        #endregion Methods

        private double Angle_1;
        private double Angle_2;

        private void Judge_Click(Joint joint_1, Joint joint_2, Joint joint_3)
        {
            double a = Math.Sqrt((joint_1.Position.X - joint_2.Position.X) * (joint_1.Position.X - joint_2.Position.X)
                + (joint_1.Position.Y - joint_2.Position.Y) * (joint_1.Position.Y - joint_2.Position.Y)
                + (joint_1.Position.Z - joint_2.Position.Z) * (joint_1.Position.Z - joint_2.Position.Z));
            double b = Math.Sqrt((joint_3.Position.X - joint_2.Position.X) * (joint_3.Position.X - joint_2.Position.X)
                + (joint_3.Position.Y - joint_2.Position.Y) * (joint_3.Position.Y - joint_2.Position.Y)
                + (joint_3.Position.Z - joint_2.Position.Z) * (joint_3.Position.Z - joint_2.Position.Z));
            double c = Math.Sqrt((joint_1.Position.X - joint_3.Position.X) * (joint_1.Position.X - joint_3.Position.X)
                + (joint_1.Position.Y - joint_3.Position.Y) * (joint_1.Position.Y - joint_3.Position.Y)
                + (joint_1.Position.Z - joint_3.Position.Z) * (joint_1.Position.Z - joint_3.Position.Z));
            this.Angle_2 = Math.Acos((a * a + c * c - b * b) / (2 * a * b));
            this.Angle_1 = this.Angle_2;

            Console.Write(string.Format("  {0}°  ", this.Angle_1));
            Console.WriteLine(a + "   " + b + "   " + c);
        }

    }
}
