﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.IO;
using System.Threading;
using System.Timers;
//checked
namespace GestureTraceLibrary
{
    public class KinectCursorEventArgs : RoutedEventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public CursorAdorner Cursor { get; set; }

        public KinectCursorEventArgs(double x, double y)
        {
            X = x;
            Y = y;
        }

        public KinectCursorEventArgs(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public KinectCursorEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        { }

        public KinectCursorEventArgs(RoutedEvent routedEvent, double x, double y, double z)
            : base(routedEvent)
        { X = x; Y = y; Z = z; }

        public KinectCursorEventArgs(RoutedEvent routedEvent, Point point)
            : base(routedEvent)
        { X = point.X; Y = point.Y; }

        public KinectCursorEventArgs(RoutedEvent routedEvent, Point point, double z)
            : base(routedEvent)
        { X = point.X; Y = point.Y; Z = z; }

        public KinectCursorEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        { }

        public KinectCursorEventArgs(RoutedEvent routedEvent, object source, double x, double y, double z)
            : base(routedEvent, source)
        { X = x; Y = y; Z = z; }

        public KinectCursorEventArgs(RoutedEvent routedEvent, object source, Point point)
            : base(routedEvent, source)
        { X = point.X; Y = point.Y; }

        public KinectCursorEventArgs(RoutedEvent routedEvent, object source, Point point, double z)
            : base(routedEvent, source)
        { X = point.X; Y = point.Y; Z = z; }
    }
}
