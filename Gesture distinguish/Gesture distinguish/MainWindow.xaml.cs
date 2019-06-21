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

namespace Gesture_distinguish
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        private WaveGesture waveGesture = new WaveGesture();
        private DateTime currentTime = new DateTime();

        private enum WavePosition
        {
            None = 0,
            Left = 1,
            Right = 2,
            Neutral = 3
        }

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

            public void Reset()
            {
                IterationCount = 0;
                State = WaveGestureState.None;
                Timestamp = 0;
                StartPosition = WavePosition.None;
                CurrentPosition = WavePosition.None;
            }

            public void UpdateState(WaveGestureState state, long timestamp)
            {
                State = state;
                Timestamp = timestamp;
            }

            public void UpdatePosition(WavePosition position, long timestamp)
            {
                if (CurrentPosition != position)
                {
                    if(position == WavePosition.Left || position == WavePosition.Right)
                    {
                        if(State != WaveGestureState.InProgress)
                        {
                            State = WaveGestureState.InProgress;
                            IterationCount = 0;
                            StartPosition = position;
                        }

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


            private void TrackWave(Skeleton skeleton, bool isLeft, ref WaveGestureTracker tracker, long timestamp)
            {
                JointType handJointId = (isLeft) ? JointType.HandLeft : JointType.HandRight;
                JointType elbowJointId = (isLeft) ? JointType.ElbowLeft : JointType.ElbowRight;
                Joint hand = skeleton.Joints[handJointId];
                Joint elbow = skeleton.Joints[elbowJointId];

                if(hand.TrackingState != JointTrackingState.NotTracked && elbow.TrackingState != JointTrackingState.NotTracked)
                {
                    if (tracker.State == WaveGestureState.InProgress && tracker.Timestamp + WAVE_MOVEMENT_TIMEOUT < timestamp)
                    {
                        tracker.UpdateState(WaveGestureState.Failure, timestamp);
                        System.Diagnostics.Debug.WriteLine("Fail!");
                    }
                    else if (hand.Position.Y > elbow.Position.Y)
                    {
                        //center position (0, 0)
                        if(hand.Position.X <= elbow.Position.X - WAVE_THRESHOLD)
                        {
                            tracker.UpdatePosition(WavePosition.Left, timestamp);
                        }
                        else if(hand.Position.X >= elbow.Position.X + WAVE_THRESHOLD)
                        {
                            tracker.UpdatePosition(WavePosition.Right, timestamp);
                        }
                        else
                        {
                            tracker.UpdatePosition(WavePosition.Neutral, timestamp);
                        }

                        if(tracker.State != WaveGestureState.Success && tracker.IterationCount == REQUIRED_ITERATIONS)
                        {
                            tracker.UpdateState(WaveGestureState.Success, timestamp);
                            System.Diagnostics.Debug.WriteLine("Success!");
                            
                            if(GestureDetected != null)
                            {
                                GestureDetected(this, new EventArgs());
                            }
                        }
                    }
                    else
                    {
                        if(tracker.State == WaveGestureState.InProgress)
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

        public MainWindow()
        {
            InitializeComponent();
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
            waveGesture.GestureDetected += WaveGesture_GestureDetected;
        }

        private void WaveGesture_GestureDetected(object sender, EventArgs e)
        {
            Console.WriteLine("waved!");
            Console.Beep();
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
                    long now = ((currentTime.Hour *60 + currentTime.Minute) * 60 + currentTime.Second) * 1000 + currentTime.Millisecond;
                    waveGesture.Update(this.frameSkeletons, now);
                }
            }
        }
        #endregion Methods
    }
}
