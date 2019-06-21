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

namespace Simon_Say_1
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
        private UIElement[] instructionSequence;
        private int instructionPosition;
        private int currentLevel;
        private Random rnd = new Random();
        private IInputElement leftHandTarget;
        private IInputElement rightHandTarget;

        public enum GamePhase
        {
            GameOver = 0,
            SimonInstructing = 1,
            PlayerPerforming = 2
        }

        #endregion Member Variables

        public MainWindow()
        {
            InitializeComponent();

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

            ChangePhase(GamePhase.GameOver);
            this.currentLevel = 0;
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
            using(SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if(frame != null)
                {
                    frame.CopySkeletonDataTo(this.frameSkeletons);
                    Skeleton skeleton = GetPrimarySkeleton(this.frameSkeletons);

                    if(skeleton == null)
                    {
                        ChangePhase(GamePhase.GameOver);
                    }
                    else
                    {
                        if(this.currentPhase == GamePhase.SimonInstructing)
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
            if(HitTest(skeleton.Joints[JointType.HandLeft], LeftHandStartElement)
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
            if(newPhase != this.currentPhase)
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
                        GameInstructionsElement.Text = "请将手放到中间空白区域，不要碰到色块——并且注意提示";
                        GenerateInstructions();
                        DisplayInstructions();
                        break;

                    case GamePhase.PlayerPerforming:
                        this.instructionPosition = 0;
                        GameInstructionsElement.Text = "按顺序戳~~";
                        break;
                }
            }
        }

        private void GenerateInstructions()
        {
            this.instructionSequence = new UIElement[this.currentLevel];

            for(int i = 0; i < this.currentLevel; i++)
            {
                switch (rnd.Next(1, 5))
                {
                    case 1:
                        this.instructionSequence[i] = RedBlock;
                        break;

                    case 2:
                        this.instructionSequence[i] = BlueBlock;
                        break;

                    case 3:
                        this.instructionSequence[i] = GreenBlock;
                        break;

                    case 4:
                        this.instructionSequence[i] = YellowBlock;
                        break;
                }
            }
        }

        private void DisplayInstructions()
        {
            Storyboard instructionSequence = new Storyboard();
            DoubleAnimationUsingKeyFrames animation;

            for(int i = 0; i < this.instructionSequence.Length; i++)
            {
                this.instructionSequence[i].ApplyAnimationClock(FrameworkElement.OpacityProperty, null);

                animation = new DoubleAnimationUsingKeyFrames();
                animation.FillBehavior = FillBehavior.Stop;
                animation.BeginTime = TimeSpan.FromMilliseconds(i * 1500);
                Storyboard.SetTarget(animation, this.instructionSequence[i]);
                Storyboard.SetTargetProperty(animation,new PropertyPath("Opacity"));
                instructionSequence.Children.Add(animation);

                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.Zero)));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1300))));

                instructionSequence.Completed += (s, e) => { ChangePhase(GamePhase.PlayerPerforming); };
                instructionSequence.Begin(LayoutRoot);
            }
        }

        private void ProcessPlayerPerforming(Skeleton skeleton)
        {
            //Determine if user is hitting a target and if that target is in the correct sequence.
            UIElement correctTarget = this.instructionSequence[this.instructionPosition];
            IInputElement leftTarget = GetHitTarget(skeleton.Joints[JointType.HandLeft], GameCanvas);
            IInputElement rightTarget = GetHitTarget(skeleton.Joints[JointType.HandRight], GameCanvas);
            bool hasTargetChange = (leftTarget != this.leftHandTarget) || (rightTarget != this.rightHandTarget);

            if (hasTargetChange)
            {
                #region if
                if(leftTarget != null && rightTarget != null)
                {
                    ChangePhase(GamePhase.GameOver);
                }
                else if((leftHandTarget == correctTarget && rightHandTarget == null) || 
                        (rightHandTarget == correctTarget && leftHandTarget == null))
                {
                    this.instructionPosition++;

                    if(this.instructionPosition >= this.instructionSequence.Length)
                    {
                        ChangePhase(GamePhase.SimonInstructing);
                    }
                }
                else if(leftTarget != null || rightTarget != null)
                {
                    //Do nothing - target found
                }
                else
                {
                    ChangePhase(GamePhase.GameOver);
                }
                #endregion if

                if(leftTarget != this.leftHandTarget)
                {
                    if(this.leftHandTarget != null)
                    {
                        ((FrameworkElement)this.leftHandTarget).Opacity = 0.2;
                    }

                    if(leftTarget != null)
                    {
                        ((FrameworkElement)leftTarget).Opacity = 1;
                    }

                    this.leftHandTarget = leftTarget;
                }

                if(rightTarget != this.rightHandTarget)
                {
                    if(this.rightHandTarget != null)
                    {
                        ((FrameworkElement)this.rightHandTarget).Opacity = 0.2;
                    }

                    if(rightTarget != null)
                    {
                        ((FrameworkElement)rightTarget).Opacity = 1;
                    }

                    this.rightHandTarget = rightTarget;
                }
            }
        }
    }
}
