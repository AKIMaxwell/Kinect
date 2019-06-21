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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Car_1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        private Phase currentPhase;
        private UIElement[] instructionSequence;
        private int instructionPosition;
        private int instructionNumber;
        private Random rnd = new Random();
        private IInputElement leftHandTarget;
        private IInputElement rightHandTarget;
        private IntPtr VK_A;
        private IntPtr hwnd;
        private IntPtr htextbox;
        private IntPtr htextbox2;
        private uint currentCommand;

        //当前控制状态枚举
        public enum Phase
        {
            Waiting = 0,
            UseInstructing = 1,
            UserCommanding = 2
        }

        #endregion Member Variables

        //程序入口
        public MainWindow()
        {
            //初始化窗口
            InitializeComponent();
            //初始化端口
            UnInitializePtr();

            //注册Kinect状态改变事件
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            //返回可用的Kinect
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

            //初始化系统状态
            ChangePhase(Phase.Waiting);
            this.instructionNumber = 4;
        }

        //设置Kinect
        public KinectSensor KinectDevice
        {
            get { return this.kinectDevice; }
            set
            {
                if (this.kinectDevice != value)
                {
                    //合理释放Kinect
                    if (this.kinectDevice != null)
                    {
                        this.kinectDevice.Stop();
                        //停止骨骼流
                        this.kinectDevice.SkeletonFrameReady -= KinectDevice_SkeletonFrameReady;
                        this.kinectDevice.SkeletonStream.Disable();
                        this.frameSkeletons = null;
                        //停止色彩流
                        this.kinectDevice.ColorStream.Disable();
                    }

                    this.kinectDevice = value;

                    //初始化Kinect
                    if (this.kinectDevice != null)
                    {
                        if (this.kinectDevice.Status == KinectStatus.Connected)
                        {
                            //启动骨骼流
                            this.kinectDevice.SkeletonStream.Enable();
                            this.frameSkeletons = new Skeleton[this.kinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                            this.kinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;
                            //启动色彩流
                            this.kinectDevice.ColorStream.Enable();
                            //启动Kinect
                            this.kinectDevice.Start();
                        }
                    }
                }
            }
        }

        //处理Kinect状态
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

        //骨骼流触发事件
        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    //拷贝当前数据流
                    frame.CopySkeletonDataTo(this.frameSkeletons);
                    Skeleton skeleton = GetPrimarySkeleton(this.frameSkeletons);

                    if (skeleton == null)
                    {
                        ChangePhase(Phase.Waiting);
                    }
                    else
                    {
                        //UI界面更新
                        if (this.currentPhase == Phase.UseInstructing)
                        {
                            LeftHandElement.Visibility = Visibility.Collapsed;
                            RightHandElement.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            //追踪手部
                            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight]);

                            switch (this.currentPhase)
                            {
                                case Phase.Waiting:
                                    ProcessWaiting(skeleton);
                                    break;

                                case Phase.UserCommanding:
                                    ProcessUserCommanding(skeleton);
                                    break;
                            }
                        }
                    }
                }
            }
        }
        
        //获得最前面的骨骼
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

        //追踪手部
        private void TrackHand(Joint leftHand, Joint rightHand)
        {
            if (leftHand.TrackingState == JointTrackingState.NotTracked)
            {
                LeftHandElement.Visibility = Visibility.Collapsed;
            }
            else
            {
                LeftHandElement.Visibility = Visibility.Visible;
#pragma warning disable CS0618
                DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(leftHand.Position, this.kinectDevice.DepthStream.Format);
#pragma warning restore CS0618
                point.X = (int)((point.X * LayoutRoot.ActualWidth / kinectDevice.DepthStream.FrameWidth) - (LeftHandElement.ActualWidth / 2.0));
                point.Y = (int)((point.Y * LayoutRoot.ActualHeight / kinectDevice.DepthStream.FrameHeight) - (LeftHandElement.ActualHeight / 2.0));

                Canvas.SetLeft(LeftHandElement, point.X);
                Canvas.SetTop(LeftHandElement, point.Y);
            }

            if (rightHand.TrackingState == JointTrackingState.NotTracked)
            {
                RightHandElement.Visibility = Visibility.Collapsed;
            }
            else
            {
                RightHandElement.Visibility = Visibility.Visible;
#pragma warning disable CS0618
                DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(rightHand.Position, this.kinectDevice.DepthStream.Format);
#pragma warning restore CS0618
                point.X = (int)((point.X * LayoutRoot.ActualWidth / kinectDevice.DepthStream.FrameWidth) - (RightHandElement.ActualWidth / 2.0) + 52);
                point.Y = (int)((point.Y * LayoutRoot.ActualHeight / kinectDevice.DepthStream.FrameHeight) - (RightHandElement.ActualHeight / 2.0) - 17);

                Canvas.SetLeft(RightHandElement, point.X);
                Canvas.SetTop(RightHandElement, point.Y);
            }
        }

        //得到骨骼关节位置并转换为二维点
        private Point GetJointPoint(KinectSensor kinectDevice, Joint joint, Size renderSize, Point point)
        {
#pragma warning disable CS0618
            DepthImagePoint _point = kinectDevice.MapSkeletonPointToDepth(joint.Position, kinectDevice.DepthStream.Format);
#pragma warning restore CS0618

            point.X = _point.X * (int)renderSize.Width / kinectDevice.DepthStream.FrameWidth;
            point.Y = _point.Y * (int)renderSize.Height / kinectDevice.DepthStream.FrameHeight;

            return point;
        }

        //等待用户发出开始指令
        private void ProcessWaiting(Skeleton skeleton)
        {
            if (HitTest(skeleton.Joints[JointType.HandRight], RightHandStartElement))
            {
                ChangePhase(Phase.UseInstructing);
            }
        }

        //碰触检测
        private bool HitTest(Joint joint, UIElement target)
        {
            return (GetHitTarget(joint, target) != null);
        }

        //返回当前碰触目标
        private IInputElement GetHitTarget(Joint joint, UIElement target)
        {
            Point targetPoint = LayoutRoot.TranslatePoint(GetJointPoint(this.KinectDevice, joint, LayoutRoot.RenderSize, new Point()), target);
            return target.InputHitTest(targetPoint);
        }

        //更新文本交互内容
        private void ChangePhase(Phase newPhase)
        {
            if (newPhase != this.currentPhase)
            {
                this.currentPhase = newPhase;

                switch (this.currentPhase)
                {
                    case Phase.Waiting:
                        this.instructionNumber = 4;
                        RedBlock.Opacity = 0.2;
                        BlueBlock.Opacity = 0.2;
                        GreenBlock.Opacity = 0.2;
                        YellowBlock.Opacity = 0.2;

                        StateElement.Text = "小车实时交互系统";
                        ControlCanvas.Visibility = Visibility.Visible;
                        InstructionsElement.Text = "将手放入框内开始控制车体";

                        break;

                    case Phase.UseInstructing:
                        StateElement.Text = string.Format("准备...");
                        ControlCanvas.Visibility = Visibility.Collapsed;
                        InstructionsElement.Text = "当手放置在方块上时发出命令";
                        GenerateInstructions();
                        DisplayInstructions();
                        break;

                    case Phase.UserCommanding:
                        this.instructionPosition = 0;
                        InstructionsElement.Text = "出发！";
                        break;
                }
            }
        }

        //创建演示顺序
        private void GenerateInstructions()
        {
            this.instructionSequence = new UIElement[this.instructionNumber];

                        this.instructionSequence[0] = RedBlock;
                        this.instructionSequence[1] = GreenBlock;
                        this.instructionSequence[2] = BlueBlock;
                        this.instructionSequence[3] = YellowBlock;
        }

        //加载帧演示操作
        private void DisplayInstructions()
        {
            Storyboard instructionSequence = new Storyboard();
            DoubleAnimationUsingKeyFrames animation;

            for (int i = 0; i < this.instructionSequence.Length; i++)
            {
                this.instructionSequence[i].ApplyAnimationClock(FrameworkElement.OpacityProperty, null);

                animation = new DoubleAnimationUsingKeyFrames();
                animation.FillBehavior = FillBehavior.Stop;
                animation.BeginTime = TimeSpan.FromMilliseconds(i * 1500);
                Storyboard.SetTarget(animation, this.instructionSequence[i]);
                Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                instructionSequence.Children.Add(animation);

                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.Zero)));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1300))));

            }
            instructionSequence.Completed += (s, e) => { ChangePhase(Phase.UserCommanding); };
            instructionSequence.Begin(LayoutRoot);
        }

        //加载碰触交互动画
        private void CommandingInteraction(UIElement currentBlock)
        {
            Storyboard instructionSequence = new Storyboard();
            DoubleAnimationUsingKeyFrames animation;

            currentBlock.ApplyAnimationClock(FrameworkElement.OpacityProperty, null);

                animation = new DoubleAnimationUsingKeyFrames();
                animation.FillBehavior = FillBehavior.Stop;
                animation.BeginTime = TimeSpan.FromMilliseconds(0);
                Storyboard.SetTarget(animation, currentBlock);
                Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                instructionSequence.Children.Add(animation);

                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.Zero)));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.65, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.4, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(600))));

            instructionSequence.Begin(LayoutRoot);

        }

        //接受并处理用户命令
        private void ProcessUserCommanding(Skeleton skeleton)
        {
            //Determine if user is hitting a target and if that target is in the correct sequence.
            UIElement correctTarget = this.instructionSequence[this.instructionPosition];
            IInputElement leftTarget = GetHitTarget(skeleton.Joints[JointType.HandLeft], GameCanvas);
            IInputElement rightTarget = GetHitTarget(skeleton.Joints[JointType.HandRight], GameCanvas);
            bool hasTargetChange = (rightTarget != this.rightHandTarget);
            if(hasTargetChange)
            {
                this.rightHandTarget = rightTarget;

                if (rightTarget == RedBlock)
                {
                    CommandingInteraction(RedBlock);
                    StateElement.Text = "Go Stright!";
                    currentCommand = 87;
                    MessageGo();
                }
                else if (rightTarget == GreenBlock)
                {
                    CommandingInteraction(GreenBlock);
                    StateElement.Text = "Go Backward!";
                    currentCommand = 83;
                    MessageGo();

                }
                else if (rightTarget == BlueBlock)
                {
                    CommandingInteraction(BlueBlock);
                    StateElement.Text = "Go Left!";
                    currentCommand = 65;
                    MessageGo();

                }
                else if (rightTarget == YellowBlock)
                {
                    CommandingInteraction(YellowBlock);
                    StateElement.Text = "Go Right!";
                    currentCommand = 68;
                    MessageGo();

                }
                else
                {
                    StateElement.Text = "Waiting...";
                    MessageStop();
                }

            }
        }

        #region Dll_Import
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_CHAR = 0x0102;


        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string classname, string captionName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindowEx(IntPtr parent, IntPtr child, string classname, string captionName);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]

        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        static extern bool PostMessage(IntPtr hwnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion Dll_Import

        //初始化端口
        private void UnInitializePtr()
        {
            this.VK_A = new IntPtr(65);
            this.hwnd = FindWindow(null, "WIFI/蓝牙智能小车操纵平台 正式版V1.1 By  Liuviking      机器人创意工作室       www.wifi-robots.com");
            this.htextbox = FindWindowEx(hwnd, IntPtr.Zero, "EDIT", null);
            this.htextbox2 = FindWindowEx(hwnd, htextbox, "EDIT", null);
        }

        //发送启动命令
        private void MessageGo()
        {

            PostMessage(hwnd, WM_KEYDOWN, this.currentCommand, 0);
        }

        //发送停止命令
        private void MessageStop()
        {
            PostMessage(hwnd, WM_KEYUP, this.currentCommand, 0);
        }
    }
}
