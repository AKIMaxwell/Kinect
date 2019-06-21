using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//checked
namespace GestureTraceLibrary
{
    public delegate void KinectCursorEventHandler(object sender, KinectCursorEventArgs e);
    public static class KinectInput
    {
        public static readonly RoutedEvent KinectCursorEnterEvent = EventManager.RegisterRoutedEvent("KinectCursorEnter", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public static void AddKinectCursorEnterHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorEnterEvent, handler);
        }
        public static void RemoveKinectCursorEnterHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorEnterEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorLeaveEvent = EventManager.RegisterRoutedEvent("KinectCursorLeave", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public static void AddKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorLeaveEvent, handler);
        }
        public static void RemoveKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorLeaveEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorMoveEvent = EventManager.RegisterRoutedEvent("KinectCursorMove", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public static void AddKinectCursorMoveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorMoveEvent, handler);
        }
        public static void RemoveKinectCursorMoveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorMoveEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorActivatedEvent = EventManager.RegisterRoutedEvent("KinectCursorActivated", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public static void AddKinectCursorActivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorActivatedEvent, handler);
        }
        public static void RemoveKinectCursorActivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorActivatedEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorDeactivatedEvent = EventManager.RegisterRoutedEvent("KinectCursorDeactivated", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public static void AddKinectCursorDeactivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorDeactivatedEvent, handler);
        }
        public static void RemoveKinectCursorDeactivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorDeactivatedEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorLockEvent = EventManager.RegisterRoutedEvent("KinectCursorLock", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public static void AddKinectCursorLockHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorLeaveEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorUnlockEvent =
            EventManager.RegisterRoutedEvent("KinectCursorUnlock", RoutingStrategy.Bubble, typeof(KinectCursorEventArgs), typeof(KinectInput));
        public static void RemoveKinectCursorUnlockHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorLockEvent, handler);
        }


    }
}
