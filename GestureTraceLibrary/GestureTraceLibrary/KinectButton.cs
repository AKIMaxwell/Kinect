using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
//checked
namespace GestureTraceLibrary
{
    public class KinectButton : Button
    {
        public static readonly RoutedEvent KinectCursorEnterEvent = KinectInput.KinectCursorEnterEvent.AddOwner(typeof(KinectButton));
        public static readonly RoutedEvent KinectCursorLeaveEvent = KinectInput.KinectCursorLeaveEvent.AddOwner(typeof(KinectButton));
        public static readonly RoutedEvent KinectCursorMoveEvent = KinectInput.KinectCursorMoveEvent.AddOwner(typeof(KinectButton));
        public static readonly RoutedEvent KinectCursorActivatedEvent = KinectInput.KinectCursorActivatedEvent.AddOwner(typeof(KinectButton));
        public static readonly RoutedEvent KinectCursorDeactivatedEvent = KinectInput.KinectCursorDeactivatedEvent.AddOwner(typeof(KinectButton));

        public event KinectCursorEventHandler KinectCursorEnter
        {
            add { base.AddHandler(KinectCursorEnterEvent, value); }
            remove { base.RemoveHandler(KinectCursorEnterEvent, value); }
        }
        public event KinectCursorEventHandler KinectCursorLeave
        {
            add { base.AddHandler(KinectCursorLeaveEvent, value); }
            remove { base.RemoveHandler(KinectCursorLeaveEvent, value); }
        }
        public event KinectCursorEventHandler KinectCursorMove
        {
            add { base.AddHandler(KinectCursorMoveEvent, value); }
            remove { base.RemoveHandler(KinectCursorMoveEvent, value); }
        }
        public event KinectCursorEventHandler KinectCursorActivated
        {
            add { base.AddHandler(KinectCursorActivatedEvent, value); }
            remove { base.RemoveHandler(KinectCursorActivatedEvent, value); }
        }
        public event KinectCursorEventHandler KinectCursorDeactivated
        {
            add { base.AddHandler(KinectCursorDeactivatedEvent, value); }
            remove { base.RemoveHandler(KinectCursorDeactivatedEvent, value); }
        }

        public KinectButton()
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                KinectCursorManager.Create(Application.Current.MainWindow);

            this.KinectCursorEnter += new KinectCursorEventHandler(OnKinectCursorEnter);
            this.KinectCursorLeave += new KinectCursorEventHandler(OnKinectCursorLeave);
            this.KinectCursorMove += new KinectCursorEventHandler(OnKinectCursorMove);
        }

        protected virtual void OnKinectCursorEnter(object sender, KinectCursorEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }
        protected virtual void OnKinectCursorLeave(object sender, KinectCursorEventArgs e)
        { }
        protected virtual void OnKinectCursorMove(object sender, KinectCursorEventArgs e)
        { }

    }
}
