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

namespace SkeletonViewer
{
    /// <summary>
    /// SkeletonViewer.xaml 的交互逻辑
    /// </summary>
    public partial class SkeletonViewer : UserControl
    {
        private readonly Brush[] _SkeletonBrushes = new Brush[] { Brushes.Black, Brushes.Crimson, Brushes.Indigo, Brushes.DodgerBlue, Brushes.Purple, Brushes.Pink };
        private Skeleton[] _FrameSkeletons;

        protected const string KinectDevicePropertyName = "KinectDevice";
        public static readonly DependencyProperty KinectDeviceProperty = DependencyProperty.Register(KinectDevicePropertyName, typeof(KinectSensor), typeof(SkeletonViewer), new PropertyMetadata(null, KinectDeviceChanged));

        public SkeletonViewer()
        {
            InitializeComponent();
        }

        public KinectSensor KinectDevice
        {
            get { return (KinectSensor)GetValue(KinectDeviceProperty); }
            set { SetValue(KinectDeviceProperty, value); }
        }

        private static void KinectDeviceChanged(DependencyObject owner, DependencyPropertyChangedEventArgs e)
        {
            SkeletonViewer viewer = (SkeletonViewer)owner;

            if(e.OldValue != null)
            {
                ((KinectSensor)e.OldValue).SkeletonFrameReady -= viewer.KinectDevice_SkeletonFrameReady;
                viewer._FrameSkeletons = null;
            }

            if(e.NewValue != null)
            {
                viewer.KinectDevice = (KinectSensor)e.NewValue;
                viewer.KinectDevice.SkeletonFrameReady += viewer.KinectDevice_SkeletonFrameReady;
                viewer._FrameSkeletons = new Skeleton[viewer.KinectDevice.SkeletonStream.FrameSkeletonArrayLength];
            }
        }

        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            SkeletonPanel.Children.Clear();
            JointInfoPanel.Children.Clear();
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (this.IsEnabled)
                {
                    frame.CopySkeletonDataTo(this._FrameSkeletons);
                    for(int i = 0; i < this._FrameSkeletons.Length; i++)
                    {
                        DrawSkeleton(this._FrameSkeletons[i], this._SkeletonBrushes[i]);
                        TrackJoint(this._FrameSkeletons[i].Joints[JointType.HandLeft], this._SkeletonBrushes[i]);
                        TrackJoint(this._FrameSkeletons[i].Joints[JointType.HandRight], this._SkeletonBrushes[i]);
                    }
                }
            }
        }

        private void TrackJoint(Joint joint, Brush brush)
        {
            if(joint.TrackingState != JointTrackingState.NotTracked)
            {
                Canvas container = new Canvas();
                Point jointPoint = GetJointPoint(joint);

                double z = joint.Position.Z;

                Ellipse element = new Ellipse();
                element.Height = 15;
                element.Width = 15;
                element.Fill = brush;
                Canvas.SetLeft(element, 0 - (element.Width / 2));
                Canvas.SetTop(element, 0 - (element.Height / 2));
                container.Children.Add(element);

                TextBlock positionText = new TextBlock();
                positionText.Text = string.Format("<{0:0.00},{1:0.00},{2:0.00}>", jointPoint.X, jointPoint.Y, z);
                positionText.Foreground = brush;
                positionText.FontSize = 24;
                positionText.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Canvas.SetLeft(positionText, 35);
                Canvas.SetTop(positionText, 15);
                container.Children.Add(positionText);

                Canvas.SetLeft(container, jointPoint.X);
                Canvas.SetTop(container, jointPoint.Y);

                JointInfoPanel.Children.Add(container);
            }
        }

        private void DrawSkeleton(Skeleton skeleton, Brush userBrush)
        {
            if (skeleton != null && skeleton.TrackingState == SkeletonTrackingState.Tracked)
            {
                Polyline figure;

                //figure the head and boby
                figure = CreateFigure(skeleton, userBrush, new[] { JointType.Head, JointType.ShoulderCenter, JointType.ShoulderLeft, JointType.Spine, JointType.ShoulderRight, JointType.ShoulderCenter, JointType.HipCenter });
                SkeletonPanel.Children.Add(figure);

                figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipLeft, JointType.HipRight });
                SkeletonPanel.Children.Add(figure);

                //figure the leg
                figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipCenter, JointType.HipLeft, JointType.KneeLeft, JointType.AnkleLeft, JointType.FootLeft });
                SkeletonPanel.Children.Add(figure);

                figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipCenter, JointType.HipRight, JointType.KneeRight, JointType.AnkleRight, JointType.FootRight });
                SkeletonPanel.Children.Add(figure);

                //figure the arm
                figure = CreateFigure(skeleton, userBrush, new[] { JointType.ShoulderLeft, JointType.ElbowLeft, JointType.WristLeft, JointType.HandLeft });
                SkeletonPanel.Children.Add(figure);

                figure = CreateFigure(skeleton, userBrush, new[] { JointType.ShoulderRight, JointType.ElbowRight, JointType.WristRight, JointType.HandRight });
                SkeletonPanel.Children.Add(figure);
            }
        }

        private Polyline CreateFigure(Skeleton skeleton, Brush brush, JointType[] joints)
        {
            Polyline figure = new Polyline();

            figure.StrokeThickness = 8;
            figure.Stroke = brush;

            for (int i = 0; i < joints.Length; i++)
            {
                figure.Points.Add(GetJointPoint(skeleton.Joints[joints[i]]));
            }

            return figure;
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

    }
}
