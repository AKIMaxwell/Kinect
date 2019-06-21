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

namespace HuaJiDX
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor kinectDevice;
        private readonly Brush[] skeletonBrushes;
        private Skeleton[] frameSkeletons;

        private WriteableBitmap depthImageBitMap;
        private WriteableBitmap _depthImageBitMap;
        private Int32Rect depthImageBitmapRect;
        private int depthImageStride;
        private DepthImageFrame depthFrame;
        private short[] depthPixelData;


        private WriteableBitmap colorImageBitmap;
        private Int32Rect colorImageBitmapRect;
        private int colorImageStride;
        


        public MainWindow()
        {
            InitializeComponent();
            skeletonBrushes = new Brush[] { Brushes.LightGreen, Brushes.LightSkyBlue, Brushes.LightYellow, Brushes.DodgerBlue, Brushes.Gold, Brushes.Pink };
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
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
                        UninitializeKinectSensor(this.kinectDevice);
                    }

                    this.kinectDevice = value;

                    //Initialize
                    if (this.kinectDevice != null)
                    {
                        if (this.kinectDevice.Status == KinectStatus.Connected)
                        {
                            InitializeKinectSensor(this.kinectDevice);
                        }
                    }
                }
            }
        }

        private void InitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                ColorImageStream colorStream = kinectSensor.ColorStream;
                kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                this.colorImageBitmap = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null);
                this.colorImageBitmapRect = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                this.colorImageStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;
                ColorImageElement.Source = this.colorImageBitmap;
                kinectSensor.ColorFrameReady += kinectSensor_ColorFrameReady;
                //kinectSensor.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);

                this.kinectDevice.SkeletonStream.Enable();
                this.frameSkeletons = new Skeleton[this.kinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                this.kinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;

                DepthImageStream depthStream = this.kinectDevice.DepthStream;
                depthStream.Enable();
                this.depthImageBitMap = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Gray16, null);
                this._depthImageBitMap = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Gray16, null);
                this.depthImageBitmapRect = new Int32Rect(0, 0, depthStream.FrameWidth, depthStream.FrameHeight);
                this.depthImageStride = depthStream.FrameWidth * depthStream.FrameBytesPerPixel;

                SkeletonStream skeletonStream = this.kinectDevice.SkeletonStream;
                skeletonStream.Enable();

                //suggest choice the last
                //DepthImage.Source = this.depthImageBitMap;
                //DepthImage.Source = this._depthImageBitMap;
                this.kinectDevice.DepthFrameReady += kinectSensor_DepthFrameReady;
                //kinectSensor.DepthFrameReady += new EventHandler<DepthImageFrameReadyEventArgs>(kinectSensor_DepthFrameReady);

                this.kinectDevice.Start();

            }
        }

        private void UninitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                kinectSensor.Stop();
                kinectSensor.ColorFrameReady -= new EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
                this.kinectDevice.SkeletonFrameReady -= KinectDevice_SkeletonFrameReady;
                this.kinectDevice.SkeletonStream.Disable();
                this.frameSkeletons = null;

                this.kinectDevice.DepthFrameReady -= new EventHandler<DepthImageFrameReadyEventArgs>(kinectSensor_DepthFrameReady);

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


        void kinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame frame = e.OpenColorImageFrame())
            {
                if (frame != null)
                {
                    byte[] pixelData = new byte[frame.PixelDataLength];
                    frame.CopyPixelDataTo(pixelData);
                    //for (int i = 0; i < pixelData.Length; i += frame.BytesPerPixel)
                    //{
                    //Only red
                    //pixelData[i] = 0x00;
                    //pixelData[i + 1] = 0x00;

                    //Inverted color
                    //pixelData[i] = (byte)~pixelData[i];
                    //pixelData[i + 1] = (byte)~pixelData[i + 1];
                    //pixelData[i + 2] = (byte)~pixelData[i + 2];

                    //Apocalyptic zombie
                    //pixelData[i] = pixelData[i + 1];
                    //pixelData[i + 1] = pixelData[i];
                    //pixelData[i + 2] = (byte)~pixelData[i + 2];

                    //Gray csale
                    //byte gray = Math.Max(pixelData[i], pixelData[i + 1]);
                    //gray = Math.Max(gray, pixelData[i + 2]);
                    //pixelData[i] = gray;
                    //pixelData[i + 1] = gray;
                    //pixelData[i + 2] = gray;

                    //Grain black and white movie
                    //byte gray = Math.Max(pixelData[i], pixelData[i + 1]);
                    //gray = Math.Max(gray, pixelData[i + 2]);
                    //pixelData[i] = gray;
                    //pixelData[i + 1] = gray;
                    //pixelData[i + 2] = gray;

                    //Washed out color
                    //double gray = (pixelData[i] * 0.11) + (pixelData[i + 1] * 0.59) + (pixelData[i + 2] * 0.3);
                    //double desaturation = 0.75;
                    //pixelData[i] = (byte)(pixelData[i] + desaturation * (gray - pixelData[i]));
                    //pixelData[i + 1] = (byte)(pixelData[i + 1] + desaturation * (gray - pixelData[i + 1]));
                    //pixelData[i + 2] = (byte)(pixelData[i + 2] + desaturation * (gray - pixelData[i + 2]));

                    //High saturation
                    //if (pixelData[i] < 0x33 || pixelData[i] > 0xE5) { pixelData[i] = 0x00; }
                    //else { pixelData[i] = 0xFF; }
                    //if (pixelData[i + 1] < 0x33 || pixelData[i + 1] > 0xE5) { pixelData[i + 1] = 0x00; }
                    //else { pixelData[i + 1] = 0xFF; }
                    //if (pixelData[i + 2] < 0x33 || pixelData[i + 2] > 0xE5) { pixelData[i + 2] = 0x00; }
                    //else { pixelData[i + 2] = 0xFF; }
                    //}
                    //ColorImageElement.Source = BitmapImage.Create(frame.Width, frame.Height, 96, 96,
                    //    PixelFormats.Bgr32, null, pixelData, frame.Width * frame.BytesPerPixel);

                    this.colorImageBitmap.WritePixels(this.colorImageBitmapRect, pixelData, this.colorImageStride, 0);
                }
            }
        }


        void kinectSensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            if (depthFrame != null)
            {
                depthFrame.Dispose();
                depthFrame = null;
            }

            depthFrame = e.OpenDepthImageFrame();

            {
                if (depthFrame != null)
                {
                    depthPixelData = new short[depthFrame.PixelDataLength];
                    depthFrame.CopyPixelDataTo(depthPixelData);
                    this.depthImageBitMap.WritePixels(this.depthImageBitmapRect, depthPixelData, this.depthImageStride, 0);

                    CreatePlayerDepthImage(this.depthFrame, depthPixelData);
                }
            }
        }

        private void CreatePlayerDepthImage(DepthImageFrame depthFrame, short[] pixelData)
        {
            int playerIndex;
            const int depthBytePerPixel = 4;
            byte[] enhPixelData = new byte[depthFrame.Width * depthFrame.Height * depthBytePerPixel];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += depthFrame.BytesPerPixel)
            {
                playerIndex = pixelData[i] & DepthImageFrame.PlayerIndexBitmask;

                //if (playerIndex == 0)
                //{
                //    enhPixelData[j] = 0xFF;
                //    enhPixelData[j + 1] = 0xFF;
                //    enhPixelData[j + 2] = 0xFF;
                //    enhPixelData[j + 3] = 0x00;
                //}
                //else
                {
                    enhPixelData[j] = 0x00;
                    enhPixelData[j + 1] = 0x00;
                    enhPixelData[j + 2] = 0x00;
                    enhPixelData[j + 3] = 0xFF;
                }
            }
            DepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Pbgra32, null, enhPixelData, depthFrame.Width * depthBytePerPixel);

            //CreateColorDepthImage(depthFrame, enhPixelData, pixelData);
        }

        private void CreateColorDepthImage(DepthImageFrame deprhFrame, byte[] enhPixelData, short[] pixelData)
        {
            Int32 depth;
            Double hue;
            Int32 loThreshold = 1200;
            Int32 hiThreshold = 3500;
            Int32 bytesPerPixel = 4;
            byte[] rgb = new byte[3];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += bytesPerPixel)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if (depth < loThreshold || depth > hiThreshold)
                {
                    enhPixelData[j] = 0x00;
                    enhPixelData[j + 1] = 0x00;
                    enhPixelData[j + 2] = 0x00;
                }
                else
                {
                    hue = ((360 * depth / 0xFFF) + loThreshold);
                    ConvertHslToRgb(hue, 100, 100, rgb);

                    enhPixelData[j] = rgb[2];
                    enhPixelData[j + 1] = rgb[1];
                    enhPixelData[j + 2] = rgb[0];
                    enhPixelData[j + 3] = 0xFF;
                }
            }
            DepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Pbgra32, null, enhPixelData, depthFrame.Width * bytesPerPixel);
        }

        public void ConvertHslToRgb(Double hue, Double saturation, Double lightness, byte[] rgb)
        {
            Double red = 0.0;
            Double green = 0.0;
            Double blue = 0.0;
            hue = hue % 360.0;
            saturation = saturation / 100.0;
            lightness = lightness / 100.0;

            if (saturation == 0.0)
            {
                red = lightness;
                green = lightness;
                blue = lightness;
            }
            else
            {
                Double huePrime = hue / 60.0;
                Int32 x = (Int32)huePrime;
                Double xPrime = huePrime - (Double)x;
                Double L0 = lightness * (1.0 - saturation);
                Double L1 = lightness * (1.0 - (saturation * xPrime));
                Double L2 = lightness * (1.0 - (saturation * (1.0 - xPrime)));

                switch (x)
                {
                    case 0:
                        red = lightness;
                        green = L2;
                        blue = L0;
                        break;
                    case 1:
                        red = L1;
                        green = lightness;
                        blue = L0;
                        break;
                    case 2:
                        red = L0;
                        green = lightness;
                        blue = L2;
                        break;
                    case 3:
                        red = L0;
                        green = L1;
                        blue = lightness;
                        break;
                    case 4:
                        red = L2;
                        green = L0;
                        blue = lightness;
                        break;
                    case 5:
                        red = lightness;
                        green = L0;
                        blue = L1;
                        break;
                }
            }
            rgb[0] = (byte)(255.0 * red);
            rgb[1] = (byte)(255.0 * green);
            rgb[2] = (byte)(255.0 * blue);
        }



        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Polyline figure;
                    Brush userBrush;
                    Skeleton skeleton;
                    Image[] huaji = { huaji0, huaji1, huaji2, huaji3, huaji4, huaji5 };

                    LayoutRoot.Children.Clear();
                    frame.CopySkeletonDataTo(this.frameSkeletons);

                    for (int i = 0; i < this.frameSkeletons.Length; i++)
                    {
                        skeleton = this.frameSkeletons[i];

                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            userBrush = this.skeletonBrushes[i % this.skeletonBrushes.Length];

                            //figure the head and boby
                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.Head, JointType.ShoulderCenter, JointType.ShoulderLeft, JointType.Spine, JointType.ShoulderRight, JointType.ShoulderCenter, JointType.HipCenter });
                            LayoutRoot.Children.Add(figure);

                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipLeft, JointType.HipRight });
                            LayoutRoot.Children.Add(figure);

                            //figure the leg
                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipCenter, JointType.HipLeft, JointType.KneeLeft, JointType.AnkleLeft, JointType.FootLeft });
                            LayoutRoot.Children.Add(figure);

                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.HipCenter, JointType.HipRight, JointType.KneeRight, JointType.AnkleRight, JointType.FootRight });
                            LayoutRoot.Children.Add(figure);

                            //figure the arm
                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.ShoulderLeft, JointType.ElbowLeft, JointType.WristLeft, JointType.HandLeft });
                            LayoutRoot.Children.Add(figure);

                            figure = CreateFigure(skeleton, userBrush, new[] { JointType.ShoulderRight, JointType.ElbowRight, JointType.WristRight, JointType.HandRight });
                            LayoutRoot.Children.Add(figure);

                            huaji[i].Opacity = 1;
                            DrawPicture(skeleton.Joints[JointType.Head], huaji[i]);
                        }
                        else
                        {
                            huaji[i].Opacity = 0;
                        }
                    }
                }
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
            DepthImagePoint point = this.KinectDevice.MapSkeletonPointToDepth(joint.Position, this.KinectDevice.DepthStream.Format);

            point.X *= (int)this.LayoutRoot.ActualWidth / KinectDevice.DepthStream.FrameWidth;
            point.Y *= (int)this.LayoutRoot.ActualHeight / KinectDevice.DepthStream.FrameHeight;

            return new Point(point.X, point.Y);
        }

        private void DrawPicture(Joint joint, Image ui)
        {
            DepthImagePoint point = this.KinectDevice.MapSkeletonPointToDepth(joint.Position, this.KinectDevice.DepthStream.Format);
            point.X *= (int)this.LayoutRoot.ActualWidth / KinectDevice.DepthStream.FrameWidth;
            point.Y *= (int)this.LayoutRoot.ActualHeight / KinectDevice.DepthStream.FrameHeight;
            ui.Width = 200 * Math.Abs(4096-point.Depth) / 1000;
            ui.Height = 200 * Math.Abs(4096 - point.Depth) / 1000;
            ui.Margin = new Thickness(point.X - ui.Width / 2 - 430, point.Y - ui.Height / 2 - 530, 0, 0);
        }
    }
}
