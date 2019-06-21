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

namespace Ligature_play
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        public static DotPuzzle puzzle;
        public static eDotPuzzle epuzzle;


        private int puzzleDotIndex;
        #endregion Member Variables

        public MainWindow()
        {
            InitializeComponent();
            puzzle = new DotPuzzle();
            epuzzle = new eDotPuzzle();
            puzzleDotsAdd();
            this.puzzleDotIndex = -1;

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

            DrawPuzzle(MainWindow.puzzle);
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

        public class DotPuzzle
        {
            public List<Point> Dots { get; set; }
            public DotPuzzle()
            {
                this.Dots = new List<Point>();
            }

            public void AllClear()
            {
                Dots = new List<Point>();
            }

            public void eAllClear()
            {
                Dots.RemoveRange(1, Dots.Count - 1);
            }
        }

        public class eDotPuzzle
        {
            public List<Point> Dots { get; set; }
            public eDotPuzzle()
            {
                this.Dots = new List<Point>();
            }

            public void AllClear()
            {
                Dots = new List<Point>();
            }

            public void eAllClear()
            {
                Dots.RemoveRange(1, Dots.Count - 1);
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

                    Skeleton[] dataSet2 = new Skeleton[this.frameSkeletons.Length];
                    frame.CopySkeletonDataTo(dataSet2);

                    if(skeleton == null)
                    {
                        HandCursorElement.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        Joint primaryHand = GetPrimaryHand(skeleton);
                        TrackHand(primaryHand);
                        TrackPuzzle(primaryHand.Position);
                    }
                }
            }
        }

        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if(skeletons != null)
            {
                //find the nearest player
                for(int i = 0; i < skeletons.Length; i++)
                {
                    if(skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if(skeleton == null)
                        {
                            skeleton = skeletons[i];
                        }
                        else
                        {
                            if(skeleton.Position.Z > skeletons[i].Position.Z)
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
            if(skeleton != null)
            {
                primaryHand = skeleton.Joints[JointType.HandLeft];
                Joint rightHand = skeleton.Joints[JointType.HandRight];
                if(rightHand.TrackingState != JointTrackingState.NotTracked)
                {
                    if(primaryHand.TrackingState == JointTrackingState.NotTracked)
                    {
                        primaryHand = rightHand;
                    }
                    else
                    {
                        if(primaryHand.Position.Z > rightHand.Position.Z)
                        {
                            primaryHand = rightHand;
                        }
                    }
                }
            }
            return primaryHand;
        }

        private void TrackHand(Joint hand)
        {
            if(hand.TrackingState == JointTrackingState.NotTracked)
            {
                HandCursorElement.Visibility = Visibility.Collapsed;
            }
            else
            {
                HandCursorElement.Visibility = Visibility.Visible;
                #pragma warning disable CS0618
                DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(hand.Position, this.kinectDevice.DepthStream.Format);
                #pragma warning restore CS0618
                point.X = (int)((point.X * LayoutRoot.ActualWidth / kinectDevice.DepthStream.FrameWidth) - (HandCursorElement.ActualWidth / 2.0));
                point.Y = (int)((point.Y * LayoutRoot.ActualHeight / kinectDevice.DepthStream.FrameHeight) - (HandCursorElement.ActualHeight / 2.0));

                Canvas.SetLeft(HandCursorElement, point.X);
                Canvas.SetTop(HandCursorElement, point.Y);

                if(hand.JointType == JointType.HandRight)
                {
                    HandCursorScale.Angle = 60;
                }
                else
                {
                    HandCursorScale.Angle = -60;
                }
            }
        }

        private void puzzleDotsAdd()
        {
            MainWindow.puzzle.Dots.Add(new Point(470.048158640227, 417.844192634561));
            MainWindow.puzzle.Dots.Add(new Point(1190.12747875354, 399.677053824363));
            MainWindow.puzzle.Dots.Add(new Point(615.385269121813, 837.339943342776));
            MainWindow.puzzle.Dots.Add(new Point(825.133144475921, 117.260623229462));
            MainWindow.puzzle.Dots.Add(new Point(1094.33711048159, 810.915014164306));
        }

        public void DrawPuzzle(DotPuzzle puzzle)
        {
            PuzzleBoardElement.Children.Clear();

            if (puzzle != null)
            {
                for (int i = 0; i < puzzle.Dots.Count; i++)
                {
                    Grid dotContainer = new Grid();
                    dotContainer.Width = 70;
                    dotContainer.Height = 70;
                    dotContainer.Children.Add(new Ellipse { Fill = Brushes.Gray });

                    TextBlock dotLabel = new TextBlock();
                    dotLabel.Text = (i + 1).ToString();
                    dotLabel.Foreground = Brushes.White;
                    dotLabel.FontSize = 36;
                    dotLabel.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    dotLabel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    dotContainer.Children.Add(dotLabel);

                    //draw dots in UI
                    Canvas.SetTop(dotContainer, puzzle.Dots[i].Y - (dotContainer.Height / 2)-10);
                    Canvas.SetLeft(dotContainer, puzzle.Dots[i].X - (dotContainer.Width / 2)-50);
                    PuzzleBoardElement.Children.Add(dotContainer);
                }
            }
        }

        private void TrackPuzzle(SkeletonPoint position)
        {
            if(this.puzzleDotIndex == MainWindow.puzzle.Dots.Count)
            {
                //get~!
            }
            else
            {
                Point dot;
                if(this.puzzleDotIndex + 1 < MainWindow.puzzle.Dots.Count)
                {
                    dot = MainWindow.puzzle.Dots[this.puzzleDotIndex + 1];
                }
                else
                {
                    dot = MainWindow.puzzle.Dots[0];
                }

                #pragma warning disable CS0618
                DepthImagePoint point = this.kinectDevice.MapSkeletonPointToDepth(position, kinectDevice.DepthStream.Format);
                #pragma warning restore CS0618
                point.X = (int)(point.X * LayoutRoot.ActualWidth / kinectDevice.DepthStream.FrameWidth);
                point.Y = (int)(point.Y * LayoutRoot.ActualHeight / kinectDevice.DepthStream.FrameHeight);
                Point handPoint = new Point(point.X, point.Y);
                Point dotDiff = new Point(dot.X - handPoint.X, dot.Y - handPoint.Y);
                double length = Math.Sqrt(dotDiff.X * dotDiff.X + dotDiff.Y * dotDiff.Y);

                int lastPoint = this.CrayonElement.Points.Count - 1;
                
                if(length < 30)
                {
                    if(lastPoint > 0)
                    {
                        this.CrayonElement.Points.RemoveAt(lastPoint);
                    }

                    this.CrayonElement.Points.Add(new Point(dot.X, dot.Y));

                    this.CrayonElement.Points.Add(new Point(dot.X, dot.Y));

                    this.puzzleDotIndex++;
                    if(this.puzzleDotIndex == MainWindow.puzzle.Dots.Count)
                    {
                        //get~!
                    }
                }
                else
                {
                    if (lastPoint > 0)
                    {
                        Point lineEndpoint = this.CrayonElement.Points[lastPoint];
                        this.CrayonElement.Points.RemoveAt(lastPoint);

                        lineEndpoint.X = handPoint.X;
                        lineEndpoint.Y = handPoint.Y;
                        this.CrayonElement.Points.Add(lineEndpoint);
                    }
                }
            }
        }

        private void NewLigature(object sender, RoutedEventArgs e)
        {
            New_Ligature NewLigature = new New_Ligature();
            NewLigature.Show();
        }

        private void RedrawPuzzle(object sender, RoutedEventArgs e)
        {
            this.puzzleDotIndex = -1;
            this.CrayonElement.Points.Clear();
            MainWindow.puzzle.Dots = MainWindow.epuzzle.Dots;
            DrawPuzzle(MainWindow.puzzle);
        }
        
        private void ReadPuzzle(object sender, RoutedEventArgs e)
        {
            Read_Ligature ReadLigature = new Read_Ligature();
            ReadLigature.Show();
        }
        
        #endregion Methods
    }
}
