using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Kinect;
using Microsoft.Speech;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.AudioFormat;
using System.Diagnostics;
using System.Runtime.InteropServices;




namespace Mouse_Control_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Member Variables

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private readonly int MOUSEEVENTF_MOVE = 0x0001;//模拟鼠标移动
        private readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标左键按下
        private readonly int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起
        private readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;//鼠标绝对位置
        private readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        private readonly int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        private readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        private readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起 

        private IntPtr VK_A;
        private IntPtr hwnd;
        private IntPtr htextbox;
        private IntPtr htextbox2;

        #endregion Member Variables



        #region Property
        private double _handLeft;
        public double HandLeft
        {
            get { return _handLeft; }
            set
            {
                _handLeft = value;
                OnPropertyChanged("HandLeft");
            }
        }

        private double _handTop;
        public double HandTop
        {
            get { return _handTop; }
            set
            {
                _handTop = value;
                OnPropertyChanged("HandTop");
            }
        }

        private string _hypothesizedText;
        public string HypothesizedText
        {
            get { return _hypothesizedText; }
            set
            {
                _hypothesizedText = value;
                OnPropertyChanged("HypothesizedText");
            }
        }

        private string _confidence;
        public string Confidence
        {
            get { return _confidence; }
            set
            {
                _confidence = value;
                OnPropertyChanged("Confidence");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion Property


        private KinectAudioSource CreateAudioSource()
        {
            var source = KinectSensor.KinectSensors[0].AudioSource;
            source.AutomaticGainControlEnabled = false;
            source.EchoCancellationMode = EchoCancellationMode.None;
            return source;
        }

        KinectSensor _kinectSensor;
        SpeechRecognitionEngine _sre;
        KinectAudioSource _source;
        public MainWindow()
        {
            InitializeComponent();
            UnInitializePtr();

            this.DataContext = this;
            this.Unloaded += delegate
            {
                _kinectSensor.SkeletonStream.Disable();
                _sre.RecognizeAsyncCancel();
                _sre.RecognizeAsyncStop();
                _sre.Dispose();
            };
            this.Loaded += delegate
            {
                _kinectSensor = KinectSensor.KinectSensors[0];
                _kinectSensor.SkeletonStream.Enable(new
                    TransformSmoothParameters()
                {
                    Correction = 0.5f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f,
                    Smoothing = 0.5f
                });
                _kinectSensor.SkeletonFrameReady += nui_SkeletonFrameReady;
                _kinectSensor.Start();
                StartSpeechRecognition();
            };
        }

        void nui_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame == null)
                    return;

                var skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                skeletonFrame.CopySkeletonDataTo(skeletons);
                foreach (Skeleton skeletonData in skeletons)
                {
                    if (skeletonData.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        Microsoft.Kinect.SkeletonPoint rightHandVec = skeletonData.Joints[JointType.HandRight].Position;
                        var depthPoint = _kinectSensor.MapSkeletonPointToDepth(rightHandVec, DepthImageFormat.Resolution640x480Fps30);

                        Size size = new Size(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
                        Point OriginalPoint = new Point(depthPoint.X * size.Width / 640, depthPoint.Y * size.Height / 480);
                        Point effectivePoint = EffectivePoint(OriginalPoint, size, 1.6, 1.5);
                        //HandTop = depthPoint.Y * SystemParameters.PrimaryScreenHeight / 480;
                        //HandLeft = depthPoint.X * SystemParameters.PrimaryScreenWidth / 640;
                        HandTop = effectivePoint.Y / size.Height * 65535;
                        HandLeft = effectivePoint.X / size.Width * 65535;
                        mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置

                    }
                }
            }
        }

        private Point EffectivePoint(Point OriginalPoint, Size size, Double fi_x, Double fi_y)
        {
            Point centre = new Point(size.Width / 2, size.Height / 2);
            Point DisposePoint = new Point(((OriginalPoint.X - centre.X) * fi_x + centre.X), ((OriginalPoint.Y - centre.Y) * fi_y + centre.Y));

            if (DisposePoint.X < 0) { DisposePoint.X = 0; }
            if (DisposePoint.X > size.Width) { DisposePoint.X = size.Width; }
            if (DisposePoint.Y < 0) { DisposePoint.Y = 0; }
            if (DisposePoint.Y > size.Height) { DisposePoint.Y = size.Height; }

            return DisposePoint;
        }


        private void StartSpeechRecognition()
        {
            _source = CreateAudioSource();

            Func<RecognizerInfo, bool> matchingFunc = r =>
            {
                string value;
                r.AdditionalInfo.TryGetValue("Kinect", out value);
                return "True".Equals(value, StringComparison.InvariantCultureIgnoreCase)
                        && "en-US".Equals(r.Culture.Name, StringComparison.InvariantCultureIgnoreCase);
            };
            RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(matchingFunc).FirstOrDefault();

            _sre = new SpeechRecognitionEngine(ri.Id);
            CreateGrammars(ri);
            _sre.SpeechRecognized += sre_SpeechRecoginzed;
            _sre.SpeechHypothesized += sre_SpeechHypothesized;
            _sre.SpeechRecognitionRejected += sre_SpeechRecognitionRejected;

            Stream s = _source.Start();
            _sre.SetInputToAudioStream(s, new Microsoft.Speech.AudioFormat.SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            _sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void sre_SpeechRecoginzed(object sender, SpeechRecognizedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action<SpeechRecognizedEventArgs>(InterpretCommand), e);
        }

        private void sre_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            HypothesizedText = e.Result.Text;
        }

        private void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            HypothesizedText += " Rejected";
            Confidence = Math.Round(e.Result.Confidence, 2).ToString();
        }

        private void CreateGrammars(RecognizerInfo ri)
        {
            var RorL = new Choices();
            RorL.Add("left");
            RorL.Add("right");
            RorL.Add("middle");

            var location = new Choices();
            location.Add("up");
            location.Add("down");

            var next = new Choices();
            next.Add("go");
            next.Add("next");
            //next.Add("square");
            //next.Add("diamond");

            var gb = new GrammarBuilder();
            gb.Culture = ri.Culture;
            //gb.Append("mouse");
            //gb.AppendWildcard();
            gb.Append("click");
            gb.Append(RorL);

            var g = new Grammar(gb);
            _sre.LoadGrammar(g);

            var gb2 = new GrammarBuilder();
            gb2.Culture = ri.Culture;
            gb2.Append("page");
            //gb.AppendWildcard();
            gb2.Append(location);
            gb2.Append("now");

            var g2 = new Grammar(gb2);
            _sre.LoadGrammar(g2);


            var q = new GrammarBuilder { Culture = ri.Culture };
            q.Append("quit application");
            var quit = new Grammar(q);
            _sre.LoadGrammar(quit);

            var n = new GrammarBuilder { Culture = ri.Culture };
            n.Append(next);
            var g_next = new Grammar(n);
            _sre.LoadGrammar(g_next);

        }

        private void InterpretCommand(SpeechRecognizedEventArgs e)
        {
            var result = e.Result;
            Confidence = Math.Round(result.Confidence, 2).ToString();
            Console.WriteLine(Confidence);
            if (Double.Parse(Confidence) < 0.3)
                return;

            Console.WriteLine(result.Text.ToString());

            if (result.Words.Count >= 2 && result.Confidence < 95 && result.Words[0].Text == "quit" && result.Words[1].Text == "application")
            {
                //this.Close();
                Console.Beep(1000, 500);
            }

            if(result.Words.Count >= 2)
            {
                //if (result.Words[0].Text == "mouse" || result.Words[1].Text == "click")
                if (result.Words[0].Text == "click")
                {
                    var rlString = result.Words[1].Text;
                    switch (rlString)
                    {
                        case "left":
                            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
                            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
                            break;
                        case "right":
                            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
                            mouse_event(MOUSEEVENTF_RIGHTUP | MOUSEEVENTF_ABSOLUTE, (int)HandLeft, (int)HandTop, 0, 0);//移动到需要点击的位置
                            break;
                        default:
                            return;
                    }
                }

            }

            if (result.Words.Count >= 3)
            {
                if (result.Words[0].Text == "page" || result.Words[2].Text == "now")
                {
                    var loString = result.Words[1].Text;
                    switch (loString)
                    {
                        case "up":
                            PPT_PageUp();
                            break;
                        case "down":
                            PPT_PageDown();
                            break;
                        default:
                            return;
                    }
                }
            }

            if (result.Words.Count == 1)
            {
                if (result.Words[0].Text == "go" || result.Words[0].Text == "next")
                {
                    PPT_PageDown();
                }
            }

        }

        #region Dll_Import
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_CHAR = 0x0102;


        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string classname, string captionName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindowEx(IntPtr parent, IntPtr child, string classname, string captionName);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [return: MarshalAs(UnmanagedType.Bool)]

        [DllImport("user32.dll", EntryPoint = "PostMessage", CallingConvention = CallingConvention.Winapi)]
        static extern bool PostMessage(IntPtr hwnd, int msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public enum KeyEventFlag : int
        {
            Down = 0x0000,
            Up = 0x0002,
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(Byte bVk, Byte bScan, KeyEventFlag dwFlags, Int32 dwExtraInfo);
        #endregion Dll_Import

        //初始化端口
        private void UnInitializePtr()
        {
            this.VK_A = new IntPtr(65);
            this.hwnd = FindWindow(null, "刘雪庆1150110309 基于体感的人机交互系统设计与实验 立项答辩.pptx - WPS 演示");
            this.htextbox = FindWindowEx(hwnd, IntPtr.Zero, "EDIT", null);
            this.htextbox2 = FindWindowEx(hwnd, htextbox, "EDIT", null);
        }

        //键盘操作
        public void keybd(Byte _bVk, KeyEventFlag _dwFlags)
        {
            keybd_event(_bVk, 0, _dwFlags, 0);
        }

        public Byte getKeys(string key)
        {
            switch (key)
            {
                case "A": return (Byte)Keys.A;
                case "B": return (Byte)Keys.B;
                case "C": return (Byte)Keys.C;
                case "D": return (Byte)Keys.D;
                case "E": return (Byte)Keys.E;
                case "F": return (Byte)Keys.F;
                case "G": return (Byte)Keys.G;
                case "H": return (Byte)Keys.H;
                case "I": return (Byte)Keys.I;
                case "J": return (Byte)Keys.J;
                case "K": return (Byte)Keys.K;
                case "L": return (Byte)Keys.L;
                case "M": return (Byte)Keys.M;
                case "N": return (Byte)Keys.N;
                case "O": return (Byte)Keys.O;
                case "P": return (Byte)Keys.P;
                case "Q": return (Byte)Keys.Q;
                case "R": return (Byte)Keys.R;
                case "S": return (Byte)Keys.S;
                case "T": return (Byte)Keys.T;
                case "U": return (Byte)Keys.U;
                case "V": return (Byte)Keys.V;
                case "W": return (Byte)Keys.W;
                case "X": return (Byte)Keys.X;
                case "Y": return (Byte)Keys.Y;
                case "Z": return (Byte)Keys.Z;
                case "Add": return (Byte)Keys.Add;
                case "Back": return (Byte)Keys.Back;
                case "Cancel": return (Byte)Keys.Cancel;
                case "Capital": return (Byte)Keys.Capital;
                case "CapsLock": return (Byte)Keys.CapsLock;
                case "Clear": return (Byte)Keys.Clear;
                case "Crsel": return (Byte)Keys.Crsel;
                case "ControlKey": return (Byte)Keys.ControlKey;
                case "D0": return (Byte)Keys.D0;
                case "D1": return (Byte)Keys.D1;
                case "D2": return (Byte)Keys.D2;
                case "D3": return (Byte)Keys.D3;
                case "D4": return (Byte)Keys.D4;
                case "D5": return (Byte)Keys.D5;
                case "D6": return (Byte)Keys.D6;
                case "D7": return (Byte)Keys.D7;
                case "D8": return (Byte)Keys.D8;
                case "D9": return (Byte)Keys.D9;
                case "Decimal": return (Byte)Keys.Decimal;
                case "Delete": return (Byte)Keys.Delete;
                case "Divide": return (Byte)Keys.Divide;
                case "Down": return (Byte)Keys.Down;
                case "End": return (Byte)Keys.End;
                case "Enter": return (Byte)Keys.Enter;
                case "Escape": return (Byte)Keys.Escape;
                case "F1": return (Byte)Keys.F1;
                case "F2": return (Byte)Keys.F2;
                case "F3": return (Byte)Keys.F3;
                case "F4": return (Byte)Keys.F4;
                case "F5": return (Byte)Keys.F5;
                case "F6": return (Byte)Keys.F6;
                case "F7": return (Byte)Keys.F7;
                case "F8": return (Byte)Keys.F8;
                case "F9": return (Byte)Keys.F9;
                case "F10": return (Byte)Keys.F10;
                case "F11": return (Byte)Keys.F11;
                case "F12": return (Byte)Keys.F12;
                case "Help": return (Byte)Keys.Help;
                case "Home": return (Byte)Keys.Home;
                case "Insert": return (Byte)Keys.Insert;
                case "LButton": return (Byte)Keys.LButton;
                case "LControl": return (Byte)Keys.LControlKey;
                case "Left": return (Byte)Keys.Left;
                case "LMenu": return (Byte)Keys.LMenu;
                case "LShift": return (Byte)Keys.LShiftKey;
                case "LWin": return (Byte)Keys.LWin;
                case "MButton": return (Byte)Keys.MButton;
                case "Menu": return (Byte)Keys.Menu;
                case "Multiply": return (Byte)Keys.Multiply;
                case "Next": return (Byte)Keys.Next;
                case "NumLock": return (Byte)Keys.NumLock;
                case "NumPad0": return (Byte)Keys.NumPad0;
                case "NumPad1": return (Byte)Keys.NumPad1;
                case "NumPad2": return (Byte)Keys.NumPad2;
                case "NumPad3": return (Byte)Keys.NumPad3;
                case "NumPad4": return (Byte)Keys.NumPad4;
                case "NumPad5": return (Byte)Keys.NumPad5;
                case "NumPad6": return (Byte)Keys.NumPad6;
                case "NumPad7": return (Byte)Keys.NumPad7;
                case "NumPad8": return (Byte)Keys.NumPad8;
                case "NumPad9": return (Byte)Keys.NumPad9;
                case "PageDown": return (Byte)Keys.PageDown;
                case "PageUp": return (Byte)Keys.PageUp;
                case "Process": return (Byte)Keys.ProcessKey;
                case "RButton": return (Byte)Keys.RButton;
                case "Right": return (Byte)Keys.Right;
                case "RControl": return (Byte)Keys.RControlKey;
                case "RMenu": return (Byte)Keys.RMenu;
                case "RShift": return (Byte)Keys.RShiftKey;
                case "Scroll": return (Byte)Keys.Scroll;
                case "Space": return (Byte)Keys.Space;
                case "Tab": return (Byte)Keys.Tab;
                case "Up": return (Byte)Keys.Up;
            }
            return 0;
        }

        //发送启动命令
        private void PPT_PageUp()
        {
            PostMessage(hwnd, WM_KEYDOWN, getKeys("PageUp"), 0);
            PostMessage(hwnd, WM_KEYUP, getKeys("PageUp"), 0);
        }

        //发送停止命令
        private void PPT_PageDown()
        {
            PostMessage(hwnd, WM_KEYDOWN, getKeys("PageDown"), 0);
            PostMessage(hwnd, WM_KEYUP, getKeys("PageDown"), 0);
        }

    }
}
