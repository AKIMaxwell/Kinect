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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using VideoSource;

using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO.Ports;

using motion;
using System.Threading;
using System.Diagnostics;
using ChangeVGA;
using WIFIRobot;

using WIFIRobot.Properties;

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

        public enum Phase
        {
            Waiting = 0,
            UseInstructing = 1,
            UserCommanding = 2
        }

        #endregion Member Variables

        public MainWindow()
        {
            InitializeComponent();

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

            ChangePhase(Phase.Waiting);
            this.instructionNumber = 4;
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
                        ChangePhase(Phase.Waiting);
                    }
                    else
                    {
                        if (this.currentPhase == Phase.UseInstructing)
                        {
                            LeftHandElement.Visibility = Visibility.Collapsed;
                            RightHandElement.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
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

        private Point GetJointPoint(KinectSensor kinectDevice, Joint joint, Size renderSize, Point point)
        {
#pragma warning disable CS0618
            DepthImagePoint _point = kinectDevice.MapSkeletonPointToDepth(joint.Position, kinectDevice.DepthStream.Format);
#pragma warning restore CS0618

            point.X = _point.X * (int)renderSize.Width / kinectDevice.DepthStream.FrameWidth;
            point.Y = _point.Y * (int)renderSize.Height / kinectDevice.DepthStream.FrameHeight;

            return point;
        }

        private void ProcessWaiting(Skeleton skeleton)
        {
            if (HitTest(skeleton.Joints[JointType.HandRight], RightHandStartElement))
            {
                ChangePhase(Phase.UseInstructing);
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

                        StateElement.Text = "你咋不上天呢";
                        ControlCanvas.Visibility = Visibility.Visible;
                        InstructionsElement.Text = "将手放入框内开始上天";

                        break;

                    case Phase.UseInstructing:
                        StateElement.Text = string.Format("准备上天");
                        ControlCanvas.Visibility = Visibility.Collapsed;
                        InstructionsElement.Text = "将手放置在次元门上发出命令";
                        GenerateInstructions();
                        DisplayInstructions();
                        break;

                    case Phase.UserCommanding:
                        this.instructionPosition = 0;
                        InstructionsElement.Text = "一库！";
                        break;
                }
            }
        }

        private void GenerateInstructions()
        {
            this.instructionSequence = new UIElement[this.instructionNumber];

                        this.instructionSequence[0] = RedBlock;
                        this.instructionSequence[1] = GreenBlock;
                        this.instructionSequence[2] = BlueBlock;
                        this.instructionSequence[3] = YellowBlock;
        }

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

        private void ProcessUserCommanding(Skeleton skeleton)
        {
            //Determine if user is hitting a target and if that target is in the correct sequence.
            UIElement correctTarget = this.instructionSequence[this.instructionPosition];
            IInputElement leftTarget = GetHitTarget(skeleton.Joints[JointType.HandLeft], GameCanvas);
            IInputElement rightTarget = GetHitTarget(skeleton.Joints[JointType.HandRight], GameCanvas);
            bool hasTargetChange = (leftTarget != this.leftHandTarget) || (rightTarget != this.rightHandTarget);

            if (rightTarget == RedBlock)
            {
                CommandingInteraction(RedBlock);
                StateElement.Text = "Go Stright!";
                
            }
            else if (rightTarget == GreenBlock)
            {
                CommandingInteraction(GreenBlock);
                StateElement.Text = "Go Backward!";
            }
            else if (rightTarget == BlueBlock)
            {
                CommandingInteraction(BlueBlock);
                StateElement.Text = "Go Left!";
            }
            else if (rightTarget == YellowBlock)
            {
                CommandingInteraction(YellowBlock);
                StateElement.Text = "Go Right!";
            }
            else
            {
                StateElement.Text = "Waiting...";
            }
        }
    }
}
