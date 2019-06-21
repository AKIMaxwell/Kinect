using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//checked
namespace GestureTraceLibrary
{
    class MagneticSlide : MagnetButton
    {
        private bool isLookingForSwipes;

        public MagneticSlide()
        {
            base.isLockOn = false;
        }

        private void InitializeSwipe()
        {
            if (isLookingForSwipes) return;
            var kinectMgr = KinectCursorManager.Instance;
            kinectMgr.GesturePointTrackingInitialize(SwipeLength, MaxDeviation, MaxSwipeTime, XOutOfBoundsLength);

            kinectMgr.swipeDetected += new KinectCursorEventHandler(kinectMgr_swipeDetected);
            kinectMgr.swipeOutofBoundDetected += new KinectCursorEventHandler(kinectMgr_swipeOutofBoundDetected);
            kinectMgr.GesturePointTrackingStart();
        }

        private void DeInitializeSwipe()
        {
            var KinectMgr = KinectCursorManager.Instance;
            KinectMgr.swipeDetected -= new KinectCursorEventHandler(kinectMgr_swipeDetected);
            KinectMgr.swipeOutofBoundDetected -= new KinectCursorEventHandler(kinectMgr_swipeOutofBoundDetected);
            KinectMgr.GesturePointTrackingStop();
            isLookingForSwipes = false;
        }

        private void kinectMgr_swipeOutofBoundDetected(object sender, KinectCursorEventArgs e)
        {
            DeInitializeSwipe();
            RaiseEvent(new KinectCursorEventArgs(SwipeOutOfBoundsEvent));
        }

        private void kinectMgr_swipeDetected(object sender, KinectCursorEventArgs e)
        {
            DeInitializeSwipe();
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        protected override void OnKinectCursorEnter(object sender, KinectCursorEventArgs e)
        {
            InitializeSwipe();
            base.OnKinectCursorEnter(sender, e);
        }

        public static readonly DependencyProperty SwipeLengthProperty =
            DependencyProperty.Register("SwipeLength", typeof(double), typeof(MagneticSlide), new UIPropertyMetadata(-500d));
        public double SwipeLength
        {
            get { return (double)GetValue(SwipeLengthProperty); }
            set { SetValue(SwipeLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxDeviationProperty =
            DependencyProperty.Register("MaxDeviation", typeof(double), typeof(MagneticSlide), new UIPropertyMetadata(100d));
        public double MaxDeviation
        {
            get { return (double)GetValue(MaxDeviationProperty); }
            set { SetValue(MaxDeviationProperty, value); }
        }

        public static readonly DependencyProperty XOutOfBoundsLengthProperty =
            DependencyProperty.Register("XOutOfBoundsLength", typeof(double), typeof(MagneticSlide), new UIPropertyMetadata(-700d));
        public double XOutOfBoundsLength
        {
            get { return (double)GetValue(XOutOfBoundsLengthProperty); }
            set { SetValue(XOutOfBoundsLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxSwipeTimeProperty =
            DependencyProperty.Register("MaxSwipeTime", typeof(int), typeof(MagneticSlide), new UIPropertyMetadata(300d));
        public int MaxSwipeTime
        {
            get { return (int)GetValue(MaxSwipeTimeProperty); }
            set { SetValue(MaxSwipeTimeProperty, value); }
        }

        public static readonly RoutedEvent SwipeOutOfBoundsEvent = EventManager.RegisterRoutedEvent("SwipeOutOfBounds", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));

        public event RoutedEventHandler SwipeOutOfBounds
        {
            add { AddHandler(SwipeOutOfBoundsEvent, value); }
            remove { RemoveHandler(SwipeOutOfBoundsEvent, value); }
        }
    }
}
