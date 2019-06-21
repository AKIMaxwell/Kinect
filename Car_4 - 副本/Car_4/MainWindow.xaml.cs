using AreoLibrary;
using GestureTraceLibrary;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using WIFIRobotCMDEngineV2;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO.Ports;

namespace Car_4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        private Thickness leftThickness0;
        private Thickness rightThickness0;
        private Thickness _leftThickness;
        private Thickness _rightThickness;
        private System.Windows.Size size0;
        private List<String> leftStrings = new List<string>();
        private List<String> rightStrings = new List<string>();
        private bool isVideoOpen = false;
        private bool _leftHandable;
        private bool _rightHandable;
        private bool isLeftForward;
        private bool isTurn;
        private bool isGo;
        private bool isSet;
        private double scale;
        private double leftSheel;
        private double rightSheel;
        private double leftPointAngle;
        private double rightPointAngle;
        private double vaRetangle_v;
        private double haRetangle_h;
        private int _leftSpeed;
        private int _rightSpeed;
        private int _vaAngle;
        private int _haAngle;
        private double _steerAngle;
        private Thickness leftWheelThickness;
        private Thickness rightWheelThickness;
        private Thickness leftBigCircleThickness;
        private Thickness rightBigCircleThickness;
        private System.Windows.Point steeringOPoint;
        private enum Mode
        {
            mode1 = 1,
            mode2 = 2
        }
        private enum Sport
        {
            stop = 0,
            go = 1,
            turn = 2,
            goForward = 3,
            goBack = 4,
            turnleft = 5,
            turnright = 6
        }
        private Mode currentMode = Mode.mode1;
        private Sport currentSport = Sport.stop;
        
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        };
        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        #endregion Member Variables


        //主窗体构造函数   程序由此启动
        public MainWindow()
        {
            //由于该窗体继承自Window类，故首先初始化构造器
            InitializeComponent();
            //注册Kinect传感器状态事件，以进行传感器状态监听与处理；绑定挥手动作处理函数
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            waveGesture.GestureDetected += WaveGesture_GestureDetected;
            //将连接到电脑的查找到的第一个可用的传感器设置到属性
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

            //设置骨骼辅助控件是否可视以及其长宽属性
            //isO(bool)为是否可视的依赖项属性，封装于SkeletonViewer中
            viewer.isO = true;
            viewer.Width = grid.Width;
            viewer.Height = grid.Height;
            //记录UI界面中所使用的控件的位置
            InitializeThick();

            //从配置文件获取通讯地址、命令等信息
            GetIni();
            //实例化小车引擎
            SetPanle();
            RobotEngine2 = new WifiRobotCMDEngineV2((Object)this.statusBar);
            //建立Socket连接
            OpenWifi();
        }


        #region Properties
        //KinectSensor属性
        public KinectSensor KinectDevice
        {
            get { return this.kinectDevice; }
            set
            {
                //如果传感器的状态发生变化
                if (this.kinectDevice != value)
                {
                    //Uninitialize
                    if (this.kinectDevice != null)
                    {
                        //停止数据流
                        this.kinectDevice.Stop();
                        //取消骨骼流事件监听并停止骨骼流
                        this.kinectDevice.SkeletonFrameReady -= KinectDevice_SkeletonFrameReady;
                        this.kinectDevice.SkeletonStream.Disable();
                        this.frameSkeletons = null;
                        viewer.KinectDevice = null;
                        //停止色彩流和深度流
                        kinectDevice.ColorFrameReady -= new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
                        kinectDevice.DepthFrameReady -= new EventHandler<DepthImageFrameReadyEventArgs>(kinectSensor_DepthFrameReady);
                        
                        //停止语音识别引擎
                        _sre.RecognizeAsyncCancel();
                        _sre.RecognizeAsyncStop();
                        _sre.Dispose();
                    }

                    this.kinectDevice = value;

                    //Initialize
                    if (this.kinectDevice != null)
                    {
                        //当传感器的处于连接状态时
                        //其它可能的Kinect状态见枚举类型KinectStatus
                        if (this.kinectDevice.Status == KinectStatus.Connected)
                        {
                            //启动骨骼流
                            this.kinectDevice.SkeletonStream.Enable();
                            //为骨骼数组获取空间，使用FrameSkeletonArrayLength能比较优雅地来设置它，一般长度为6，即能够同时追踪六个人的骨骼数据
                            this.frameSkeletons = new Skeleton[this.kinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                            //注册骨骼流监听事件
                            this.kinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;
                            //设置骨骼辅助控件的传感器对象
                            viewer.KinectDevice = this.KinectDevice;

                            //实例化色彩流
                            ColorImageStream colorStream = kinectDevice.ColorStream;
                            //以RGB色彩模式、1280*960像素、每秒12帧的设置使能色彩流
                            //其它模式见枚举类型ColorImageFormat
                            kinectDevice.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                            //位图板是C#中一种比较高效的绘图方式
                            //以指定长宽、分辨率、通道模式初始化位图板
                            this.colorImageBitmap = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null);
                            //以指定长宽设置绘图区域
                            this.colorImageBitmapRect = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                            //计算图像处理的跨度
                            this.colorImageStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;
                            //设置UI界面图像的源
                            ColorImageElement.Source = this.colorImageBitmap;
                            //注册图像更新事件
                            kinectDevice.ColorFrameReady += kinectSensor_ColorFrameReady;

                            //深度图像数据结构基本一致，实现方法同上
                            DepthImageStream depthStream = kinectDevice.DepthStream;
                            depthStream.Enable();
                            //16位灰度通道
                            this.depthImageBitMap = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Gray16, null);
                            this.depthImageBitmapRect = new Int32Rect(0, 0, depthStream.FrameWidth, depthStream.FrameHeight);
                            this.depthImageStride = depthStream.FrameWidth * depthStream.FrameBytesPerPixel;
                            DepthImage.Source = this.depthImageBitMap;
                            kinectDevice.DepthFrameReady += kinectSensor_DepthFrameReady;

                            //启动语音识别引擎
                            StartSpeechRecognition();

                            //启动KinectSensor
                            this.kinectDevice.Start();
                        }
                    }
                }
            }
        }

        private bool leftHandable
        {
            set { _leftHandable = value; SetCursor(); }
            get { return _leftHandable; }
        }
        private bool rightHandable
        {
            set { _rightHandable = value; SetCursor(); }
            get { return _rightHandable; }
        }

        public int leftSpeed
        {
            set
            {
                if (isWiFiOK && value / 10 != _leftSpeed / 10)
                    //SetSpeedright(Math.Abs(value) / 10);
                leftRotateTransform.Angle = leftPointAngle + 3 * Math.Abs(value);
                _leftSpeed = value;
            }
            get { return _leftSpeed; }
        }
        public int rightSpeed
        {
            set
            {
                if (isWiFiOK && value / 10 != _rightSpeed / 10)
                    //SetSpeedleft(Math.Abs(value) / 10);
                rightRotateTransform.Angle = rightPointAngle + 3 * Math.Abs(value);
                _rightSpeed = value;
            }
            get { return _rightSpeed; }
        }

        public int vaAngle
        {
            set
            {
                if (isWiFiOK && value != _vaAngle)
                    vaAngleChange(value);
                _vaAngle = value;
            }
            get { return _vaAngle; }
        }
        public int haAngle
        {
            set
            {
                if (isWiFiOK && value != _haAngle)
                    haAngleChange(value);
                _haAngle = value;
            }
            get { return _haAngle; }
        }
        public double steerAngle
        {
            set
            {
                if (isWiFiOK && value != _steerAngle)
                {
                    _steerAngle = value;
                    if (!isTurn)
                    {
                        if (value > 25 && value < 115)
                        {
                            isTurn = true;
                            isGo = false;
                            Stop();
                            GoRight();
                        }
                        else if (value < -25 && value > -115)
                        {
                            isTurn = true;
                            isGo = false;
                            Stop();
                            GoLeft();
                        }
                    }
                    else
                    {
                        if (value > 25 && value < 115)
                        {
                            //GoRight();
                        }
                        else if (value < -25 && value > -115)
                        {
                            //GoLeft();
                        }
                        else
                        {
                            isTurn = false;
                            if(!isGo)
                                Stop();
                            //isGo = false;
                        }
                    }
                }
            }
            get { return _steerAngle; }
        }
        #endregion Properties

        #region Methods
        //Kinect状态监听
        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                case KinectStatus.Connected:
                //Kinect在使用时必须由外部供电
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
                    //获取最前的骨骼
                    Skeleton skeleton = GetPrimarySkeleton(this.frameSkeletons);

                    if (skeleton != null)
                    {
                        //依据实际构造的UI界面，记录其元素位置
                        if (_leftThickness == new Thickness(0, 0, 0, 0))
                            InitializeThick();

                        System.Windows.Size size = new System.Windows.Size(grid.ActualWidth, grid.ActualHeight);
                        //获取骨骼最前的手部关节
                        Joint primaryHand = GetPrimaryHand(skeleton);
                        //获取最前手部对应的点的二维坐标
                        System.Windows.Point point = GetJointPoint(this.kinectDevice, primaryHand, size, new System.Windows.Point());

                        //获取双手和肩部中心的关节点的二维坐标
                        Joint leftJoint = skeleton.Joints[JointType.HandLeft];
                        Joint rightJoint = skeleton.Joints[JointType.HandRight];
                        DepthImagePoint leftPoint = GetJointPoint(this.kinectDevice, leftJoint, size);
                        DepthImagePoint rightPoint = GetJointPoint(this.kinectDevice, rightJoint, size);
                        DepthImagePoint centerpoint = TrackCenter(skeleton.Joints[JointType.ShoulderCenter]);
                        //讲道理上面的可以重写一下，有点繁琐

                        //计算时间戳
                        long now = ((currentTime.Hour * 60 + currentTime.Minute) * 60 + currentTime.Second) * 1000 + currentTime.Millisecond;
                        //更新姿势
                        waveGesture.Update(this.frameSkeletons, now);

                        //模式一下
                        if (currentMode == Mode.mode1)
                        {
                            //更新云台、运动、速度调整等数据
                            if(!isSet)
                                SetThickness(leftSmallCircle, _leftThickness, leftPoint, leftThickness0, 0.05, true);
                            SetThickness(rightSmallCircle, _rightThickness, rightPoint, rightThickness0, 0.05, false);
                            SetScale(rightPoint, centerpoint, rightSmallCircle);
                        }
                        //模式二下
                        else
                        {
                            //更新运动数据
                            //我说怎么模式二那么不稳定，
                            //——steerAngle属性里的方法和Go函数的方法写重了
                            //待以后修_(:3/∟)_
                            //另外这种计算角度的方法不是很好，即计算两手对屏幕中心点角度的均值
                            //不如用caculorAngle2
                            steerAngle = (caculorAngle(steeringOPoint, leftPoint, true) + caculorAngle(steeringOPoint, rightPoint, false)) / 2;
                            //steerAngle = caculorAngle2(leftPoint, rightPoint);
                            steerRotate.Angle = steerAngle;
                            Go(caculorSpeed(leftPoint, centerpoint), caculorSpeed(rightPoint, centerpoint));
                        }

                        //记录哪只手在前，
                        //重复的代码
                        if (leftPoint.Depth < rightPoint.Depth)
                            this.isLeftForward = true;
                        else
                            this.isLeftForward = false;
                    }
                }
            }
        }

        //记录UI元素的位置
        private void InitializeThick()
        {
            leftThickness0 = leftSmallCircle.Margin;
            rightThickness0 = rightSmallCircle.Margin;
            size0 = new System.Windows.Size(rightSmallCircle.Width, rightSmallCircle.Height);
            leftPointAngle = leftRotateTransform.Angle;
            rightPointAngle = rightRotateTransform.Angle;
            vaRetangle_v = vaRectangle.Margin.Left;
            haRetangle_h = haRectangle.Margin.Top;
            leftWheelThickness = leftDash.Margin;
            rightWheelThickness = rightDash.Margin;
            leftBigCircleThickness = leftBigCircle.Margin;
            rightBigCircleThickness = rightBigCircle.Margin;
            steeringOPoint = new System.Windows.Point(steeringWheel.Margin.Left + steeringWheel.ActualWidth / 2, steeringWheel.Margin.Top + steeringWheel.ActualHeight / 2);

            _leftThickness = new Thickness(grid.ActualWidth / 4, grid.ActualHeight / 2, 0, 0);
            _rightThickness = new Thickness(grid.ActualWidth * 3 / 4, grid.ActualHeight / 2, 0, 0);
        }

        //设置光标是否可见，以及按钮是否可用
        private void SetCursor()
        {
            if ((!leftHandable && isLeftForward) || (!rightHandable && !isLeftForward) || isSet)
            {
                openVideoButton.IsEnabled = true;
                takePictureButton.IsEnabled = true;
                lightButton.IsEnabled = true;
                modeChangedButton.IsEnabled = true;
                CursorAdorner.SetVisibility(true);
            }
            else
            {
                openVideoButton.IsEnabled = false;
                takePictureButton.IsEnabled = false;
                lightButton.IsEnabled = false;
                modeChangedButton.IsEnabled = false;
                CursorAdorner.SetVisibility(false);
            }
        }

        //importance
        //根据左手右手运动计算小车云台和运动
        private void SetThickness(Ellipse SmallCircle, Thickness _thickness, DepthImagePoint point, Thickness thickness0, double c, bool _isLeft)
        {
            //计算手部坐标距离原点的偏差
            System.Windows.Point distance = new System.Windows.Point(point.X - _thickness.Left, point.Y - _thickness.Top);
            System.Windows.Point newdistance = new System.Windows.Point();
            Thickness newThickness = new Thickness();

            //当距离大于220像素时，手部脱离控制状态
            if (Math.Abs(distance.X) > 220 || Math.Abs(distance.Y) > 220)
            {
                if (_isLeft && leftHandable)
                    leftHandable = false;
                if (!_isLeft && rightHandable)
                    rightHandable = false;
                SmallCircle.Margin = thickness0;
            }
            //当距离小于等于100像素时，手部保持控制状态
            else
            {
                if (_isLeft && !leftHandable)
                    leftHandable = true;
                if (!_isLeft && !rightHandable)
                    rightHandable = true;

                //使用Logistic函数调整位移
                newdistance = new System.Windows.Point(Logistic(distance.X, c), Logistic(distance.Y, c));
                //设置虚拟操纵杆的位置，25为杆的半径(Magic Number)
                //newThickness变量好像没用
                newThickness = new Thickness(thickness0.Left + newdistance.X + 25 - SmallCircle.ActualWidth / 2, thickness0.Top + newdistance.Y + 25 - SmallCircle.ActualWidth / 2, thickness0.Right, thickness0.Bottom);
                SmallCircle.Margin = newThickness;

                //左手控制云台
                if (_isLeft)
                {
                    //调试用的错误的代码
                    if ((int)(90 + 90 * newdistance.X / Math.Abs(distance.X)) < -200)
                        Console.WriteLine("error");

                    //计算云台水平角度[0,180]
                    vaAngle = (int)(90 - 90 * newdistance.X / 100);
                    //计算云台竖直角度[0,90]
                    haAngle = (int)(distance.Y < 0 ? 90 * newdistance.Y / 100 : 0);

                    //以队列方式输出云台信息
                    //其实这里可以做出cooler的效果来
                    if (leftStrings == null)
                        leftStrings.Add(String.Format("{0}° , {1}°", vaAngle, -haAngle));
                    else
                        leftStrings.Insert(0, String.Format("{0}° , {1}°", vaAngle, -haAngle));
                    while (leftStrings.Count > 6)
                        leftStrings.RemoveAt(6);
                    leftTextBlock.Text = "云台信息";
                    foreach (String s in leftStrings)
                    {
                        leftTextBlock.Text += "\n";
                        leftTextBlock.Text += s;
                    }

                    //更新标尺位置
                    vaRectangle.Margin = new Thickness(vaRetangle_v + (distance.X == 0 ? 0 : 200 * newdistance.X / Math.Abs(distance.X)), vaRectangle.Margin.Top, vaRectangle.Margin.Right, vaRectangle.Margin.Bottom);
                    haRectangle.Margin = new Thickness(haRectangle.Margin.Left, haRetangle_h - 400 * (distance.Y < 0 ? newdistance.Y / distance.Y : 0), haRectangle.Margin.Right, haRectangle.Margin.Bottom);
                }

                //右手控制运动
                else
                {
                    //if调试用的来着
                    if (true)
                    {
                        //计算方法如论文
                        leftSheel = -(newdistance.Y / 100 - newdistance.X / 100) / 2.0 * (scale - 1);
                        rightSheel = -(newdistance.Y / 100 + newdistance.X / 100) / 2.0 * (scale - 1);
                    }
                    //设置速度
                    leftSpeed = (int)(100 * leftSheel);
                    rightSpeed = (int)(100 * rightSheel);
                    //判断运动命令
                    JudgeSport(leftSpeed, rightSpeed);

                    //计算均速
                    double middleSheel = leftSheel + rightSheel;

                    //以队列方式输出运动信息
                    if (rightStrings == null)
                        rightStrings.Add(String.Format("{0}% , {1}% , {2}%", leftSpeed, rightSpeed, (int)(100 * middleSheel)));
                    else
                        rightStrings.Insert(0, String.Format("{0}% , {1}% , {2}%", leftSpeed, rightSpeed, (int)(100 * middleSheel)));
                    while (rightStrings.Count > 6)
                        rightStrings.RemoveAt(6);
                    rightTextBlock.Text = "运动信息";
                    foreach (String s in rightStrings)
                    {
                        rightTextBlock.Text += "\n";
                        rightTextBlock.Text += s;
                    }
                }
            }
        }

        //计算手部关于中心点的角度
        private double caculorAngle(System.Windows.Point center, DepthImagePoint aimPoint, bool isleft)
        {
            double x = aimPoint.X - center.X;
            double y = aimPoint.Y - center.Y;
            //详情见函数Atan2
            double angle = Math.Atan2(y, x);
            angle = angle / Math.PI * 180;
            //为进一步计算，对左手的角度进行调整
            if (isleft)
            {
                if (angle < 0)
                    angle += 180;
                else
                    angle -= 180;
            }
            return angle;
        }

        //计算两手之间的角度
        private double caculorAngle2(DepthImagePoint leftHand, DepthImagePoint rightHand)
        {
            double x = leftHand.X - rightHand.X;
            double y = leftHand.Y - rightHand.Y;
            double angle = Math.Atan2(y, x);
            return angle;
        }

        //计算速度调整参数
        private void SetScale(DepthImagePoint aimPoint, DepthImagePoint centerPoint, Ellipse ellipse)
        {
            //计算过程见论文
            int det = centerPoint.Depth - aimPoint.Depth;
            if (rightHandable)
            {
                if (det > 600)
                {
                    scale = 2.5;
                }
                else if (det < 300)
                {
                    scale = 1;
                }
                else
                {
                    scale = det / 200.0 - 0.5;
                }
            }
            else
                scale = 1;
            //反馈到UI元素
            ellipse.Height = size0.Height * scale;
            ellipse.Width = size0.Width * scale;

        }

        //根据运动速度与当前运动状态发出运动命令
        private void JudgeSport(int leftSpeed, int rightSpeed)
        {
            if (leftSpeed > 10 && rightSpeed > 10)
            {
                if (currentSport != Sport.goForward)
                {
                    Stop();
                    GoForward();
                }
                currentSport = Sport.goForward;
            }
            else if (leftSpeed > 10 && rightSpeed < -10)
            {
                if (currentSport != Sport.turnright)
                {
                    Stop();
                    GoRight();
                }
                currentSport = Sport.turnright;
            }
            else if (leftSpeed < -10 && rightSpeed > 10)
            {
                if (currentSport != Sport.turnleft)
                {
                    Stop();
                    GoLeft();
                }
                currentSport = Sport.turnleft;
            }
            else if (leftSpeed < -10 && rightSpeed < -10)
            {
                if (currentSport != Sport.goBack)
                {
                    Stop();
                    GoBackward();
                }
                currentSport = Sport.goBack;
            }
            else
            {
                if (currentSport != Sport.stop)
                    Stop();
                currentSport = Sport.stop;
            }
        }

        //判断前进还是后退
        private void Go(int left, int right)
        {
            int speed = (left + right) / 2;
            leftSpeed = Math.Abs(speed);
            rightSpeed = Math.Abs(speed);
            if(!isTurn)
            {
                if (speed > 0)
                {
                    if(!isGo)
                    {
                        GoForward();
                        isGo = true;
                    }
                }
                else if (speed < 0)
                {
                    if (!isGo)
                    {
                        GoBackward();
                        isGo = true;
                    }
                }
                else
                {
                    Stop();
                    isGo = false;
                }
            }

        }

        //根据手部距离身体距离计算速度
        private int caculorSpeed(DepthImagePoint aimPoint, DepthImagePoint centerPoint)
        {
            //计算方法见论文
            int det = centerPoint.Depth - aimPoint.Depth;
            int speed;
            if (det > 330 && det < 530)
                speed = det / 4 - 182;
            else if (det > 600 && det < 800)
                speed = det / 4 - 100;
            else
                speed = 0;
            return speed;
        }

        //逻辑斯蒂调整函数
        private double Logistic(double x, double c)
        {
            double y;
            if (x > 0)
            {
                y = 100 / (1 + 100 * Math.Pow(Math.E, -c * x)) - 100.0 / 101.0;
                Console.WriteLine(String.Format("{0},{1}", x, y));
                return y;
            }
            else
            {
                y = 100 / (1 + 100 * Math.Pow(Math.E, c * x)) - 100.0 / 101.0;
                Console.WriteLine(String.Format("{0},{1}", x, y));
                return -y;
            }
        }

        //获取最前方的骨骼
        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
            {
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

        //获取最前方的手
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

        //获取关节坐标(pixel，pixel，mm)，原点为Depth图像左上角，左手系
        private System.Windows.Point GetJointPoint(KinectSensor kinectDevice, Joint joint, System.Windows.Size renderSize, System.Windows.Point point)
        {
            #pragma warning disable CS0618
            DepthImagePoint _point = kinectDevice.MapSkeletonPointToDepth(joint.Position, kinectDevice.DepthStream.Format);
            #pragma warning restore CS0618

            point.X = _point.X * (int)renderSize.Width / kinectDevice.DepthStream.FrameWidth;
            point.Y = _point.Y * (int)renderSize.Height / kinectDevice.DepthStream.FrameHeight;

            return point;
        }

        //获取关节坐标
        //重复的函数
        private DepthImagePoint GetJointPoint(KinectSensor kinectDevice, Joint joint, System.Windows.Size renderSize)
        {
            #pragma warning disable CS0618
            DepthImagePoint _point = kinectDevice.MapSkeletonPointToDepth(joint.Position, kinectDevice.DepthStream.Format);
            #pragma warning restore CS0618

            _point.X = _point.X * (int)renderSize.Width / kinectDevice.DepthStream.FrameWidth;
            _point.Y = _point.Y * (int)renderSize.Height / kinectDevice.DepthStream.FrameHeight;

            return _point;
        }

        //获取关节坐标
        //重复的函数
        private DepthImagePoint TrackCenter(Joint centerPoint)
        {
            #pragma warning disable CS0618
            DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(centerPoint.Position, this.kinectDevice.DepthStream.Format);
            #pragma warning restore CS0618
            return point;
        }

        //甜区处理
        private System.Windows.Point EffectivePoint(System.Windows.Point OriginalPoint, System.Windows.Size size, Double fi_x, Double fi_y)
        {
            System.Windows.Point centre = new System.Windows.Point(size.Width / 2, size.Height / 2);
            System.Windows.Point DisposePoint = new System.Windows.Point(((OriginalPoint.X - centre.X) * fi_x + centre.X), ((OriginalPoint.Y - centre.Y) * fi_y + centre.Y));

            if (DisposePoint.X < 0) { DisposePoint.X = 0; }
            if (DisposePoint.X > size.Width) { DisposePoint.X = size.Width; }
            if (DisposePoint.Y < 0) { DisposePoint.Y = 0; }
            if (DisposePoint.Y > size.Height) { DisposePoint.Y = size.Height; }

            return DisposePoint;
        }
        #endregion Methods
        //开启项目的Aero效果，要想使用Aero效果需要事先开启系统的Aero功能
        private void ExtendAeroGlass(Window window)
        {
            try
            {
                // 为WPF程序获取窗口句柄  
                IntPtr mainWindowPtr = new WindowInteropHelper(window).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

                // 设置Margins  
                MARGINS margins = new MARGINS();

                // 扩展Aero Glass  
                margins.cxLeftWidth = -1;
                margins.cxRightWidth = -1;
                margins.cyTopHeight = -1;
                margins.cyBottomHeight = -1;

                int hr = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                if (hr < 0)
                {
                    System.Windows.MessageBox.Show("DwmExtendFrameIntoClientArea Failed");
                }
            }
            catch (DllNotFoundException)
            {
                System.Windows.Application.Current.MainWindow.Background = System.Windows.Media.Brushes.White;
            }
        }

        //元素的动态效果
        private void BeginVideo(double a, double b, UIElement uiElement)
        {
            Storyboard storyBoard = new Storyboard();
            DoubleAnimationUsingKeyFrames animation;

            uiElement.ApplyAnimationClock(FrameworkElement.OpacityProperty, null);

            animation = new DoubleAnimationUsingKeyFrames();
            animation.FillBehavior = FillBehavior.Stop;
            animation.BeginTime = TimeSpan.FromMilliseconds(0);
            Storyboard.SetTarget(animation, uiElement);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyBoard.Children.Add(animation);

            animation.KeyFrames.Add(new EasingDoubleKeyFrame(a, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            animation.KeyFrames.Add(new EasingDoubleKeyFrame(b, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2000))));

            storyBoard.Completed += (s, e) =>
            {
                ExtendAeroGlass(this);
                uiElement.Opacity = b;
            };
            storyBoard.Begin(grid);
        }

        //更改当前模式，并调整UI界面
        private void modeChangedButton_Click(object sender, RoutedEventArgs e)
        {
            ModeChange();
        }
        private void ModeChange()
        {
            if (currentMode == Mode.mode1)
            {
                currentMode = Mode.mode2;

                leftSmallCircle.Visibility = Visibility.Collapsed;
                rightSmallCircle.Visibility = Visibility.Collapsed;
                leftBigCircle.Visibility = Visibility.Collapsed;
                rightBigCircle.Visibility = Visibility.Collapsed;
                veRule.Visibility = Visibility.Collapsed;
                heRule.Visibility = Visibility.Collapsed;
                vaRectangle.Visibility = Visibility.Collapsed;
                haRectangle.Visibility = Visibility.Collapsed;
                leftTextBlock.Visibility = Visibility.Collapsed;
                rightTextBlock.Visibility = Visibility.Collapsed;
                steeringWheel.Visibility = Visibility.Visible;
                leftPointer.Margin = new Thickness(leftPointer.Margin.Left + leftBigCircleThickness.Left - leftDash.Margin.Left, leftPointer.Margin.Top + leftBigCircleThickness.Top - leftDash.Margin.Top, 0, 0);
                rightPointer.Margin = new Thickness(rightPointer.Margin.Left + rightBigCircleThickness.Left - rightDash.Margin.Left, rightPointer.Margin.Top + rightBigCircleThickness.Top - rightDash.Margin.Top, 0, 0);
                leftDash.Margin = leftBigCircleThickness;
                rightDash.Margin = rightBigCircleThickness;
                titleTextBlock.FontSize = 30;

                vaAngle = 90;
                haAngle = 0;
            }
            else
            {
                currentMode = Mode.mode1;

                leftSmallCircle.Visibility = Visibility.Visible;
                rightSmallCircle.Visibility = Visibility.Visible;
                leftBigCircle.Visibility = Visibility.Visible;
                rightBigCircle.Visibility = Visibility.Visible;
                veRule.Visibility = Visibility.Visible;
                heRule.Visibility = Visibility.Visible;
                vaRectangle.Visibility = Visibility.Visible;
                haRectangle.Visibility = Visibility.Visible;
                leftTextBlock.Visibility = Visibility.Visible;
                rightTextBlock.Visibility = Visibility.Visible;
                steeringWheel.Visibility = Visibility.Collapsed;
                leftPointer.Margin = new Thickness(leftPointer.Margin.Left + leftWheelThickness.Left - leftDash.Margin.Left, leftPointer.Margin.Top + leftWheelThickness.Top - leftDash.Margin.Top, 0, 0);
                rightPointer.Margin = new Thickness(rightPointer.Margin.Left + rightWheelThickness.Left - rightDash.Margin.Left, rightPointer.Margin.Top + rightWheelThickness.Top - rightDash.Margin.Top, 0, 0);
                leftDash.Margin = leftWheelThickness;
                rightDash.Margin = rightWheelThickness;
                titleTextBlock.FontSize = 48;
            }
        }

        //显示小车WiFi视频
        private void openVideoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenVideo();
        }
        private void OpenVideo()
        {
            if (isVideoOpen)
            {
                BrushConverter brushConverter = new BrushConverter();
                System.Windows.Media.Brush brush = (System.Windows.Media.Brush)brushConverter.ConvertFromString("#FF0045AA");
                this.Background = brush;
                BeginVideo(0, 1, schoolImage);
                isVideoOpen = false;
            }
            else
            {
                this.Background = System.Windows.Media.Brushes.Transparent;
                BeginVideo(1, 0, schoolImage);
                isVideoOpen = true;
            }
        }

        //将实时图像进行拍照
        private void takePictureButton_Click(object sender, EventArgs e)
        {
            TakePicture();
        }
        private void TakePicture()
        {
            //使用绘图函数CopyFromScreen进行截图
            //使用方法参见CopyFromScreen
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            //imgGraphics.CopyFromScreen(0, 0, 20, 20, new System.Drawing.Size(40, 40));
            //保存图片
            Int32 number = 0;
            string fileName;
            do
            {
                ++number;
                fileName = "snapshot(" + number + ").jpg";
            }
            while (File.Exists(fileName) == true);
            //while (File.Exists("snapshot(" + (number++) + ").jpg") == true);
            SavePicture(fileName, image);
        }

        //保存图片
        private void SavePicture(string fileName, System.Drawing.Image _image)
        {
            using (FileStream savedSnapshot = new FileStream(fileName, FileMode.CreateNew))
            {
                _image.Save(savedSnapshot, System.Drawing.Imaging.ImageFormat.Jpeg);
                savedSnapshot.Flush();
                savedSnapshot.Close();
                savedSnapshot.Dispose();
                Console.Beep();
                Console.WriteLine(fileName + " had been saved.");
            }
        }

        #region SpeechRecognitionEngine
        SpeechRecognitionEngine _sre;
        KinectAudioSource _source;

        //猜测文本属性
        private string _hypothesizedText;
        public string HypothesizedText
        {
            get { return _hypothesizedText; }
            set
            {
                _hypothesizedText = value;
            }
        }

        //文本可信度
        private string _confidence;
        public string Confidence
        {
            get { return _confidence; }
            set
            {
                _confidence = value;
            }
        }

        //实例化音频源
        private KinectAudioSource CreateAudioSource()
        {

            var source = KinectSensor.KinectSensors[0].AudioSource;
            //关闭自动增益
            source.AutomaticGainControlEnabled = false;
            source.EchoCancellationMode = EchoCancellationMode.None;
            return source;
        }

        private void StartSpeechRecognition()
        {
            _source = CreateAudioSource();

            //以指定参数初始化引擎
            Func<RecognizerInfo, bool> matchingFunc = r =>
            {
                string value;
                r.AdditionalInfo.TryGetValue("Kinect", out value);
                return "True".Equals(value, StringComparison.InvariantCultureIgnoreCase)
                        && "en-US".Equals(r.Culture.Name, StringComparison.InvariantCultureIgnoreCase);
            };
            RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();

            //实例化引擎
            _sre = new SpeechRecognitionEngine(ri.Id);
            //创建语料库
            CreateGrammars(ri);
            //监听识别事件
            _sre.SpeechRecognized += sre_SpeechRecoginzed;
            _sre.SpeechHypothesized += sre_SpeechHypothesized;
            _sre.SpeechRecognitionRejected += sre_SpeechRecognitionRejected;

            //启动音频流
            Stream s = _source.Start();
            //启动语音识别引擎
            _sre.SetInputToAudioStream(s, new Microsoft.Speech.AudioFormat.SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            _sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void sre_SpeechRecoginzed(object sender, SpeechRecognizedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<SpeechRecognizedEventArgs>(InterpretCommand), e);
        }

        private void sre_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            HypothesizedText = e.Result.Text;
        }

        private void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            HypothesizedText += " Rejected";
            Confidence = Math.Round(e.Result.Confidence, 2).ToString();
        }

        //创建语料库
        private void CreateGrammars(RecognizerInfo ri)
        {
            var q = new GrammarBuilder { Culture = ri.Culture };
            q.Append("quit application");
            var quit = new Grammar(q);
            _sre.LoadGrammar(quit);

            var q2 = new GrammarBuilder { Culture = ri.Culture };
            q2.Append("pie job");
            var quit2 = new Grammar(q2);
            _sre.LoadGrammar(quit2);

            var q3 = new GrammarBuilder { Culture = ri.Culture };
            q3.Append("shipping");
            var quit3 = new Grammar(q3);
            _sre.LoadGrammar(quit3);

            var q4 = new GrammarBuilder { Culture = ri.Culture };
            q4.Append("more she");
            var quit4 = new Grammar(q4);
            _sre.LoadGrammar(quit4);

            var q5 = new GrammarBuilder { Culture = ri.Culture };
            q5.Append("moshi");
            var quit5 = new Grammar(q5);
            _sre.LoadGrammar(quit5);
        }

        //命令处理
        private void InterpretCommand(SpeechRecognizedEventArgs e)
        {
            var result = e.Result;
            Confidence = Math.Round(result.Confidence, 2).ToString();

            if (result.Confidence > 0.9 && result.Words[0].Text == "quit" && result.Words[1].Text == "application")
            {
                this.Close();
                //HypothesizedText = "Please speech clear";
            }

            if (result.Confidence > 0.6 && result.Words[0].Text == "pie" && result.Words[1].Text == "job")
            {
                Console.WriteLine(result.Confidence);
                TakePicture();
            }

            if (result.Confidence > 0.6 && result.Words[0].Text == "shipping")
            {
                Console.WriteLine(result.Confidence);
                OpenVideo();
            }

            if (result.Confidence > 0.6 && (result.Words[0].Text == "more" && result.Words[1].Text == "she" || result.Words[0].Text == "moshi"))
            {
                Console.WriteLine(result.Confidence);
                ModeChange();
            }
        }
        #endregion SpeechRecognitionEngine

        #region SendMassage
        private const int statLength = 15;
        private int[] statCount = new int[statLength];
        private SerialPort comm = new SerialPort();
        private StringBuilder builder = new StringBuilder();
        public bool Send_status = true;

        private System.Windows.Forms.StatusBar statusBar = new System.Windows.Forms.StatusBar();
        private System.Windows.Forms.StatusBarPanel fpsPanel = new System.Windows.Forms.StatusBarPanel();
        private StatusBarPanel GroupINFO = new System.Windows.Forms.StatusBarPanel();
        private StatusBarPanel Systemstatus = new System.Windows.Forms.StatusBarPanel();

        string CameraIp = "";
        string ControlIp = "192.168.1.1";
        string Port = "2001";
        string CMD_Forward = "", CMD_Backward = "", CMD_TurnLeft = "", CMD_TurnRight = "", CMD_Stop = "";
        string CMD_TurnOnLight = "", CMD_TurnOffLight = "", CMD_Beep = "";
        string CMD_LeftForward = "", CMD_RightForward = "", CMD_LeftBackward = "", CMD_RightBackward = "";
        string AutoSetScreen;
        private int controlType = 3;
        private bool isWiFiOK = false;
        private string btCom;
        private string btBaudrate;
        public WifiRobotCMDEngine RobotEngine;//实例化引擎
        public WifiRobotCMDEngineV2 RobotEngine2;//实例化引擎
        static IPAddress ips;
        static IPEndPoint ipe;
        static Socket socket = null;
        static string RootPath = System.Windows.Forms.Application.StartupPath;
        static string FileName = RootPath + "\\Config.ini";

        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        //使用该statusBar实例化引擎使其有效
        private void SetPanle()
        {
            Form form = new Form();
            form.Text = "WIFI/蓝牙智能小车操纵平台 正式版V1.1 By  Liuviking      机器人创意工作室       www.wifi-robots.com";
            form.Opacity = 0;
            form.WindowState = FormWindowState.Minimized;
            form.Show();

            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 586);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.fpsPanel,
            this.Systemstatus,
            this.GroupINFO});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(1064, 24);
            this.statusBar.TabIndex = 1;
            this.Opacity = 1;
            // 
            // fpsPanel
            // 
            this.fpsPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.fpsPanel.MinWidth = 300;
            this.fpsPanel.Name = "fpsPanel";
            this.fpsPanel.Width = 347;
            // 
            // Systemstatus
            // 
            this.Systemstatus.MinWidth = 40;
            this.Systemstatus.Name = "Systemstatus";
            this.Systemstatus.Width = 200;
            // 
            // GroupINFO
            // 
            this.GroupINFO.Name = "GroupINFO";
            this.GroupINFO.Text = "www.wifi-robots.com WIFI机器人网·机器人创意工作室  QQ群： 145181710/196564839 ";
            this.GroupINFO.Width = 500;
        }

        //销毁Socket
        private void Window_Closed(object sender, EventArgs e)
        {
            DestorySocket();
        }

        //打开WiFi
        private void OpenWifi()
        {
            if (0 == controlType) return;
            //开启WIFI模式
            controlType = InitWIFISocket(ControlIp, Port) ? 0 : 2;
            if (0 == controlType)
            {
                InitHeartPackage();
            }
        }

        //初始化Socket连接
        bool ret = false;
        private bool InitWIFISocket(String controlIp, String port)
        {
            massageBlock.Text = "正在尝试连接WIFI板···";
            try
            {
                ips = IPAddress.Parse(controlIp.ToString());
                ipe = new IPEndPoint(ips, Convert.ToInt32(port.ToString()));
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                RobotEngine2.SOCKET = socket;
                RobotEngine2.IPE = ipe;
                ret = RobotEngine2.SocketConnect();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("WIFI初始化失败：" + e.Message, "WIFI初始化失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ret;
        }

        //销毁socket
        private void DestorySocket()
        {
            try
            {
                socket.Close();
            }
            catch { }
        }

        //从ini读入控制命令字段
        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
        private void GetIni()
        {
            CameraIp = ReadIni("VideoUrl", "videoUrl", "");
            ControlIp = ReadIni("ControlUrl", "controlUrl", "");
            Port = ReadIni("Port", "port", "");


            CMD_Forward = ReadIni("Forward", "forward", "");
            CMD_Backward = ReadIni("Backward", "backward", "");
            CMD_TurnLeft = ReadIni("Left", "left", "");
            CMD_TurnRight = ReadIni("Right", "right", "");
            CMD_Stop = ReadIni("Stop", "stop", "");

            CMD_LeftForward = ReadIni("LeftForward", "leftForward", "");
            CMD_RightForward = ReadIni("RightForward", "rightForward", "");
            CMD_LeftBackward = ReadIni("LeftBackward", "leftBackward", "");
            CMD_RightBackward = ReadIni("RightBackward", "rightBackward", "");


            CMD_TurnOnLight = ReadIni("TurnOnLight", "turnOnLight", "");
            CMD_TurnOffLight = ReadIni("TurnOffLight", "turnOffLight", "");
            CMD_Beep = ReadIni("Speaker", "speaker", "");

            btCom = ReadIni("BTCOM", "btcom", "");
            btBaudrate = ReadIni("BTBaudrate", "btbaudrate", "");

            AutoSetScreen = ReadIni("AutoSetScreen", "autoSetScreen", "");

        }

        //建立心跳包机制
        private void InitHeartPackage()
        {
            Thread HThread = new Thread(HeartPackage);
            HThread.IsBackground = true;
            HThread.Start();
            massageBlock.Text = "WiFi已连接";
            isWiFiOK = true;
        }
        private void HeartPackage()
        {
            while (true)
            {
                RobotEngine2.SendHeartCMD(controlType, comm);
                Thread.Sleep(10000);
            }
        }

        //检查WiFi
        private void CheakWiFi(object sender, RoutedEventArgs e)
        {
            massageBlock.Text = controlType.ToString();
            OpenWifi();
            SetSpeedright(99);
            SetSpeedleft(99);
        }

        //发送命令改变竖直/水平舵机角度
        private void vaAngleChange(int vaAngle)
        {
            //System.Windows.Forms.Application.DoEvents();
            byte[] Gear7_data = RobotEngine2.CreateData(0X01, 0X07, Convert.ToByte(vaAngle));
            RobotEngine2.SendCMD(controlType, Gear7_data, comm);
        }
        private void haAngleChange(int haAngle)
        {
            //System.Windows.Forms.Application.DoEvents();
            byte[] Gear8_data = RobotEngine2.CreateData(0X01, 0X08, Convert.ToByte(Math.Abs(haAngle)));//舵机数据打包
            //socket.BeginSend(Gear8_data, 0, Gear8_data.Length, SocketFlags.None, new AsyncCallback(this.AEcZrapoyQRs8myN5), socket);
            RobotEngine2.SendCMD(controlType, Gear8_data, comm);
        }

        //发送命令改变左轮/右轮速度
        private void SetSpeedleft(int v)
        {
            System.Windows.Forms.Application.DoEvents();
            byte[] Speedleft_data = RobotEngine2.CreateData(0X02, 0X02, Convert.ToByte(v));//舵机数据打包第一个参数代表舵机，第二个代表哪个舵机，第三个代表转动角度值
            RobotEngine2.SendCMD(controlType, Speedleft_data, comm);
        }
        private void SetSpeedright(int v)
        {
            System.Windows.Forms.Application.DoEvents();
            byte[] Speedright_data = RobotEngine2.CreateData(0X02, 0X01, Convert.ToByte(v));//舵机数据打包第一个参数代表舵机，第二个代表哪个舵机，第三个代表转动角度值
            RobotEngine2.SendCMD(controlType, Speedright_data, comm);
        }

        //发送运动命令
        private void GoForward()
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_Forward, comm);
                Send_status = false;
            }
        }
        private void GoBackward()
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_Backward, comm);
                Send_status = false;
            }
        }
        private void GoLeft()
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_TurnLeft, comm);
                Send_status = false;
            }
        }
        private void GoRight()
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_TurnRight, comm);
                Send_status = false;
            }
        }

        private void Stop()
        {
            Send_status = true;
            RobotEngine2.SendCMD(controlType, CMD_Stop, comm);
        }

        //调试用
        private void button_Click(object sender, RoutedEventArgs e)
        {
            leftSpeed = Convert.ToInt32(textBox.Text);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            rightSpeed = Convert.ToInt32(textBox.Text);
        }

        private void btnLeftForward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_LeftForward, comm);
                Send_status = false;
            }
        }
        private void btnRightForward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_RightForward, comm);
                Send_status = false;
            }
        }
        private void btnLeftBack_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_LeftBackward, comm);
                Send_status = false;
            }
        }
        private void btnRightBack_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_RightBackward, comm);
                Send_status = false;
            }
        }

        //建立键盘映射
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (controlType == 3)
                return;
            if (e.Key == Key.W)
            {
                GoForward();
            }
            else if (e.Key == Key.S)
            {
                GoBackward();
            }
            else if (e.Key == Key.A)
            {
                GoLeft();
            }
            else if (e.Key == Key.D)
            {
                GoRight();
            }
        }
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Stop();
        }
        #endregion SendMassage

        #region WaveGesture
        private WaveGesture waveGesture = new WaveGesture();
        private DateTime currentTime = new DateTime();

        //挥动状态
        private enum WavePosition
        {
            None = 0,
            Left = 1,
            Right = 2,
            Neutral = 3
        }
        //姿势识别状态
        private enum WaveGestureState
        {
            None = 0,
            Success = 1,
            Failure = 2,
            InProgress = 3
        }

        private struct WaveGestureTracker
        {
            public int IterationCount;
            public WaveGestureState State;
            public long Timestamp;
            public WavePosition StartPosition;
            public WavePosition CurrentPosition;

            //重置追踪器
            public void Reset()
            {
                IterationCount = 0;
                State = WaveGestureState.None;
                Timestamp = 0;
                StartPosition = WavePosition.None;
                CurrentPosition = WavePosition.None;
            }
            //更新识别状态
            public void UpdateState(WaveGestureState state, long timestamp)
            {
                State = state;
                Timestamp = timestamp;
            }
            //更新姿势位置
            public void UpdatePosition(WavePosition position, long timestamp)
            {
                if (CurrentPosition != position)
                {
                    if (position == WavePosition.Left || position == WavePosition.Right)
                    {
                        if (State != WaveGestureState.InProgress)
                        {
                            State = WaveGestureState.InProgress;
                            IterationCount = 0;
                            StartPosition = position;
                        }
                        //累加计数器
                        IterationCount++;
                    }

                    CurrentPosition = position;
                    Timestamp = timestamp;
                }
            }
        }

        public class WaveGesture
        {
            private const float WAVE_THRESHOLD = 0.1f;
            private const int WAVE_MOVEMENT_TIMEOUT = 5000;
            private const int LEEF_HAND = 0;
            private const int RIGHT_HAND = 1;
            private const int REQUIRED_ITERATIONS = 4;

            private WaveGestureTracker[,] _PlayerWaveTracker = new WaveGestureTracker[6, 2];

            public event EventHandler GestureDetected;

            //使用skeleton更新对象
            public void Update(Skeleton[] skeletons, long frameTimestamp)
            {
                if (skeletons != null)
                {
                    Skeleton skeleton;

                    for (int i = 0; i < skeletons.Length; i++)
                    {
                        skeleton = skeletons[i];
                        if (skeleton.TrackingState != SkeletonTrackingState.NotTracked)
                        {
                            TrackWave(skeleton, true, ref this._PlayerWaveTracker[i, LEEF_HAND], frameTimestamp);
                            TrackWave(skeleton, false, ref this._PlayerWaveTracker[i, RIGHT_HAND], frameTimestamp);
                        }
                        else
                        {
                            this._PlayerWaveTracker[i, LEEF_HAND].Reset();
                            this._PlayerWaveTracker[i, RIGHT_HAND].Reset();
                        }
                    }
                }
            }

            //追踪
            private void TrackWave(Skeleton skeleton, bool isLeft, ref WaveGestureTracker tracker, long timestamp)
            {
                JointType handJointId = (isLeft) ? JointType.HandLeft : JointType.HandRight;
                JointType elbowJointId = (isLeft) ? JointType.ElbowLeft : JointType.ElbowRight;
                Joint hand = skeleton.Joints[handJointId];
                Joint elbow = skeleton.Joints[elbowJointId];

                if (hand.TrackingState != JointTrackingState.NotTracked && elbow.TrackingState != JointTrackingState.NotTracked)
                {
                    //相邻两帧时间过长则追踪失败
                    if (tracker.State == WaveGestureState.InProgress && tracker.Timestamp + WAVE_MOVEMENT_TIMEOUT < timestamp)
                    {
                        tracker.UpdateState(WaveGestureState.Failure, timestamp);
                        System.Diagnostics.Debug.WriteLine("Fail!");
                    }
                    else if (hand.Position.Y > elbow.Position.Y)
                    {
                        //center position (0, 0)，阈值判定
                        if (hand.Position.X <= elbow.Position.X - WAVE_THRESHOLD)
                        {
                            tracker.UpdatePosition(WavePosition.Left, timestamp);
                        }
                        else if (hand.Position.X >= elbow.Position.X + WAVE_THRESHOLD)
                        {
                            tracker.UpdatePosition(WavePosition.Right, timestamp);
                        }
                        else
                        {
                            tracker.UpdatePosition(WavePosition.Neutral, timestamp);
                        }

                        if (tracker.State != WaveGestureState.Success && tracker.IterationCount == REQUIRED_ITERATIONS)
                        {
                            tracker.UpdateState(WaveGestureState.Success, timestamp);
                            System.Diagnostics.Debug.WriteLine("Success!");

                            //触发事件
                            if (GestureDetected != null)
                            {
                                GestureDetected(this, new EventArgs());
                            }
                        }
                    }
                    else
                    {
                        if (tracker.State == WaveGestureState.InProgress)
                        {
                            tracker.UpdateState(WaveGestureState.Failure, timestamp);
                            System.Diagnostics.Debug.WriteLine("Fail!");
                        }
                        else
                        {
                            tracker.Reset();
                        }
                    }
                }
                else
                {
                    tracker.Reset();
                }
            }
        }

        //反转云台锁定状态
        private void WaveGesture_GestureDetected(object sender, EventArgs e)
        {
            if (!isSet)
            {
                isSet = true;
                vaAngle = 90;
                haAngle = 0;
                leftTextBlock.Text = "云台信息";
                leftSmallCircle.Margin = leftThickness0;
                leftHandable = false;
            }
            else
            {
                isSet = false;
                leftHandable = true;
            }
            Console.Beep();
        }
        #endregion WavwGesture

        #region ColorImage
        private WriteableBitmap colorImageBitmap;
        private Int32Rect colorImageBitmapRect;
        private int colorImageStride;

        //更新色彩图像
        void kinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            //方便结束时Dispose frame对象
            using (ColorImageFrame frame = e.OpenColorImageFrame())
            {
                if (frame != null)
                {
                    //将流数据拷贝成像素数据
                    byte[] pixelData = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(pixelData);
                    //更新绘图，使用方法详见WritePixels函数
                    this.colorImageBitmap.WritePixels(this.colorImageBitmapRect, pixelData, this.colorImageStride, 0);
                }
            }
        }
        #endregion ColorImage

        #region DepthImage
        private WriteableBitmap depthImageBitMap;
        private Int32Rect depthImageBitmapRect;
        private int depthImageStride;
        private DepthImageFrame depthFrame;
        private short[] depthPixelData;

        //更新深度图像
        void kinectSensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            //与using语句作用相同
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

                    //可选择是否进行深度图像增强，即将灰度图像渲染为彩色图像
                    //CreateColorDepthImage(this.depthFrame, depthPixelData);
                }
            }
        }

        private void CreateColorDepthImage(DepthImageFrame deprhFrame, short[] pixelData)
        {
            Int32 depth;
            Double hue;
            Int32 loThreshold = 1200;
            Int32 hiThreshold = 3500;
            Int32 bytesPerPixel = 4;
            byte[] rgb = new byte[3];
            byte[] enhPixelData = new byte[depthFrame.Width * depthFrame.Height * bytesPerPixel];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += bytesPerPixel)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if (depth < loThreshold || depth > hiThreshold)
                {
                    enhPixelData[j] = 0x00;
                    enhPixelData[j + 1] = 0x00;
                    enhPixelData[j + 2] = 0x00;
                }
                else
                {
                    hue = ((360 * depth / 0xFFF) + loThreshold);
                    ConvertHslToRgb(hue, 100, 100, rgb);

                    enhPixelData[j] = rgb[2];
                    enhPixelData[j + 1] = rgb[1];
                    enhPixelData[j + 2] = rgb[0];
                    enhPixelData[j + 3] = 0xFF;
                }
            }
            EnhancedDepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Pbgra32, null, enhPixelData, depthFrame.Width * bytesPerPixel);
        }

        public void ConvertHslToRgb(Double hue, Double saturation, Double lightness, byte[] rgb)
        {
            Double red = 0.0;
            Double green = 0.0;
            Double blue = 0.0;
            hue = hue % 360.0;
            saturation = saturation / 100.0;
            lightness = lightness / 100.0;

            if (saturation == 0.0)
            {
                red = lightness;
                green = lightness;
                blue = lightness;
            }
            else
            {
                Double huePrime = hue / 60.0;
                Int32 x = (Int32)huePrime;
                Double xPrime = huePrime - (Double)x;
                Double L0 = lightness * (1.0 - saturation);
                Double L1 = lightness * (1.0 - (saturation * xPrime));
                Double L2 = lightness * (1.0 - (saturation * (1.0 - xPrime)));

                switch (x)
                {
                    case 0:
                        red = lightness;
                        green = L2;
                        blue = L0;
                        break;
                    case 1:
                        red = L1;
                        green = lightness;
                        blue = L0;
                        break;
                    case 2:
                        red = L0;
                        green = lightness;
                        blue = L2;
                        break;
                    case 3:
                        red = L0;
                        green = L1;
                        blue = lightness;
                        break;
                    case 4:
                        red = L2;
                        green = L0;
                        blue = lightness;
                        break;
                    case 5:
                        red = lightness;
                        green = L0;
                        blue = L1;
                        break;
                }
            }
            rgb[0] = (byte)(255.0 * red);
            rgb[1] = (byte)(255.0 * green);
            rgb[2] = (byte)(255.0 * blue);
        }
        #endregion DepthImage
    }
}