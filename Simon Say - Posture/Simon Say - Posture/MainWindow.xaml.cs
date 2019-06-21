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
using System.Windows.Threading;
using Microsoft.Kinect;

namespace Simon_Say___Posture
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        private GamePhase currentPhase;
        private int[] instructionSequence;
        private int instructionPosition;
        private int currentLevel;
        private Random rnd = new Random();

        private Pose[] poseLibrary;
        private Pose startPose;
        private DispatcherTimer poseTimer;

        public enum GamePhase
        {
            GameOver = 0,
            SimonInstructing = 1,
            PlayerPerforming = 2
        }
        #endregion Member Variables

        private void PopulatePoseLibrary()
        {
            this.poseLibrary = new Pose[4];

            //Pose - Arms Extended
            this.startPose = new Pose();
            this.startPose.Title = "StartPose";
            this.startPose.Angles = new PoseAngle[4];
            this.startPose.Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.startPose.Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 180, 20);
            this.startPose.Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.startPose.Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 0, 20);

            //Pose1 - Both Hands Up
            this.poseLibrary[0] = new Pose();
            this.poseLibrary[0].Title = "举起手来(Arms Up)";
            this.poseLibrary[0].Angles = new PoseAngle[4];
            this.poseLibrary[0].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary[0].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            this.poseLibrary[0].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary[0].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 90, 20);

            //Pose2 - Both Hands Down
            this.poseLibrary[1] = new Pose();
            this.poseLibrary[1].Title = "放下手(Arms Down)";
            this.poseLibrary[1].Angles = new PoseAngle[4];
            this.poseLibrary[1].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 270, 20);
            this.poseLibrary[1].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            this.poseLibrary[1].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 270, 20);
            this.poseLibrary[1].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 270, 20);

            //Pose3 - Left Up and Right Down
            this.poseLibrary[2] = new Pose();
            this.poseLibrary[2].Title = "举起左手(Left Up and Right Down)";
            this.poseLibrary[2].Angles = new PoseAngle[4];
            this.poseLibrary[2].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary[2].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            this.poseLibrary[2].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary[2].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 270, 20);

            //Pose4 - Right Up and Left Down
            this.poseLibrary[3] = new Pose();
            this.poseLibrary[3].Title = "举起右手(Right Up and Left Down)";
            this.poseLibrary[3].Angles = new PoseAngle[4];
            this.poseLibrary[3].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary[3].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            this.poseLibrary[3].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary[3].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 90, 20);

        }

        public class PoseAngle
        {
            public PoseAngle(JointType centerJoint, JointType angleJoint, double angle, double threshold)
            {
                CenterJoint = centerJoint;
                AngleJoint = angleJoint;
                Angle = angle;
                Threshold = threshold;
            }
            public JointType CenterJoint { get; private set; }
            public JointType AngleJoint { get; private set; }
            public double Angle { get; private set; }
            public double Threshold { get; private set; }

        }

        public struct Pose
        {
            public string Title;
            public PoseAngle[] Angles;
        }

        public MainWindow()
        {
            InitializeComponent();

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

            PopulatePoseLibrary();

            ChangePhase(GamePhase.GameOver);
            this.currentLevel = 0;

            this.poseTimer = new DispatcherTimer();
            this.poseTimer.Interval = TimeSpan.FromSeconds(10);
            this.poseTimer.Tick += (s, e) => { ChangePhase(GamePhase.GameOver); };
            this.poseTimer.Stop();
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
                        ChangePhase(GamePhase.GameOver);
                    }
                    else
                    {
                        if (this.currentPhase == GamePhase.SimonInstructing)
                        {
                            LeftHandElement.Visibility = Visibility.Collapsed;
                            RightHandElement.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            TrackHand(skeleton.Joints[JointType.HandLeft], skeleton.Joints[JointType.HandRight]);

                            switch (this.currentPhase)
                            {
                                case GamePhase.GameOver:
                                    ProcessGameOver(skeleton);
                                    break;

                                case GamePhase.PlayerPerforming:
                                    ProcessPlayerPerforming(skeleton);
                                    break;
                            }
                        }
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

        private void ProcessGameOver(Skeleton skeleton)
        {
            if (HitTest(skeleton.Joints[JointType.HandLeft], LeftHandStartElement)
                && HitTest(skeleton.Joints[JointType.HandRight], RightHandStartElement))
            {
                ChangePhase(GamePhase.SimonInstructing);
            }
        }

        private bool HitTest(Joint joint, UIElement target)
        {
            return (GetHitTarget(joint, target) != null);
        }

        private IInputElement GetHitTarget(Joint joint, UIElement target)
        {
            Point targetPoint = LayoutRoot.TranslatePoint(GetJointPoint(this.KinectDevice, joint, LayoutRoot.RenderSize, new Point()), target);
            return target.InputHitTest(targetPoint);
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

        private void ChangePhase(GamePhase newPhase)
        {
            if (newPhase != this.currentPhase)
            {
                this.currentPhase = newPhase;

                switch (this.currentPhase)
                {
                    case GamePhase.GameOver:
                        this.currentLevel = 0;
                        RedBlock.Opacity = 0.2;
                        BlueBlock.Opacity = 0.2;
                        GreenBlock.Opacity = 0.2;
                        YellowBlock.Opacity = 0.2;

                        GameStateElement.Text = "GAME OVER!";
                        ControlCanvas.Visibility = Visibility.Visible;
                        GameInstructionsElement.Text = "将双手放入红色框框内开始游戏ლ(╹◡╹ლ)";

                        break;

                    case GamePhase.SimonInstructing:
                        this.currentLevel++;
                        GameStateElement.Text = string.Format("Level {0}", this.currentLevel);
                        ControlCanvas.Visibility = Visibility.Collapsed;
                        GameInstructionsElement.Text = "请将手放到中间空白区域，并且注意提示";
                        GenerateInstructions();
                        DisplayInstructions();
                        break;

                    case GamePhase.PlayerPerforming:
                        this.instructionPosition = 0;
                        GameInstructionsElement.Text += "按顺序Go~~";
                        break;
                }
            }
        }


        private bool IsPose(Skeleton skeleton, Pose pose)
        {
            bool isPose = true;
            double angle;
            double poseAngle;
            double poseThreshold;
            double loAngle;
            double hiAngle;

            for(int i = 0; i < pose.Angles.Length && isPose; i++)
            {
                poseAngle = pose.Angles[i].Angle;
                poseThreshold = pose.Angles[i].Threshold;
                angle = GetJointAngle(skeleton.Joints[pose.Angles[i].CenterJoint], skeleton.Joints[pose.Angles[i].AngleJoint]);
                hiAngle = poseAngle + poseThreshold;
                loAngle = poseAngle - poseThreshold;

                if(hiAngle >= 360 || loAngle < 0)
                {
                    loAngle = (loAngle < 0) ? 360 + loAngle : loAngle;
                    hiAngle = hiAngle % 360;
                    isPose = !(loAngle > angle && angle > hiAngle);
                }
                else
                {
                    isPose = (loAngle <= angle && hiAngle >= angle);
                }
            }
            return isPose;
        }

        private double GetJointAngle(Joint centerJoint, Joint angleJoint)
        {
            Point primaryPoint = GetJointPoint(this.kinectDevice, centerJoint, this.LayoutRoot.RenderSize, new Point());
            Point anglePoint = GetJointPoint(this.kinectDevice, angleJoint, this.LayoutRoot.RenderSize, new Point());
            Point x = new Point(primaryPoint.X + anglePoint.X, primaryPoint.Y);

            double a, b, c;
            a = Math.Sqrt(Math.Pow(primaryPoint.X - anglePoint.X, 2) + Math.Pow(primaryPoint.Y - anglePoint.Y, 2));
            b = anglePoint.X;
            c = Math.Sqrt(Math.Pow(x.X - anglePoint.X, 2) + Math.Pow(x.Y - anglePoint.Y, 2));

            double angleRad = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            double angleDeg = angleRad * 180 / Math.PI;

            if(primaryPoint.Y < anglePoint.Y)
            {
                angleDeg = 360 - angleRad;
            }

            return angleDeg;
        }

        private void GenerateInstructions()
        {
            this.instructionSequence = new int[this.currentLevel];

            for (int i = 0; i < this.currentLevel; i++)
            {
                instructionSequence[i] = rnd.Next(1, 5);
            }
        }

        private void DisplayInstructions()
        {
            String text = "";
            int instructionSeq = this.instructionSequence[this.instructionPosition];

            for (int i = 0; i < this.currentLevel; i++)
            {
                text += this.poseLibrary[instructionSeq - 1].Title;
            }
            GameInstructionsElement.Text = text;

            ChangePhase(GamePhase.PlayerPerforming);
        }

        private void ProcessPlayerPerforming(Skeleton skeleton)
        {
            int instructionSeq = this.instructionSequence[this.instructionPosition];
            if(IsPose(skeleton, this.poseLibrary[instructionSeq-1]))
            {
                this.poseTimer.Stop();
                this.instructionPosition++;
                if(this.instructionPosition >= this.instructionSequence.Length)
                {
                    ChangePhase(GamePhase.SimonInstructing);
                }
                else
                {
                    this.poseTimer.Start();
                }
            }
        }

    }
}
