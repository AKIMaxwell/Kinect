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
using System.ComponentModel;
using Microsoft.Kinect;
using System.IO;
using System.Threading;

namespace VoiceTest_1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        bool _isPlaying;
        bool _isNoiceSuppressionOn;
        bool _isAutomaticGainOn;
        bool _isAECOn;

        string _recordingFileName;
        MediaPlayer _mplayer;
        private KinectSensor kinect;

        private bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if(_isPlaying != value)
                {
                    _isPlaying = value;
                    OnPropertyChanged("IsRecordingEnabled");                 
                }
            }
        }
        private bool IsRecording
        {
            get { return RecordHelper.Isrecording; }
            set
            {
                if(RecordHelper.Isrecording != value)
                {
                    RecordHelper.Isrecording = value;
                    OnPropertyChanged("IsPlayingEnable");
                    OnPropertyChanged("IsRecordingEnable");
                    OnPropertyChanged("IsStopEnabled");
                }
            }
        }
        public bool IsPlayingEnabled
        {
            get { return !IsRecording; }
        }
        public bool IsRecordingEnabled
        {
            get { return !IsPlaying && !IsRecording; }
        }
        public bool IsStopEnabled
        {
            get { return IsRecording; }
        }
        public bool IsNoiseSuppressionOn
        {
            get { return _isNoiceSuppressionOn; }
            set
            {
                if(_isNoiceSuppressionOn != value)
                {
                    _isNoiceSuppressionOn = value;
                    OnPropertyChanged("IsNoiseSuppressionOn");
                }
            }
        }
        public bool IsAutomaticGainOn
        {
            get { return _isAutomaticGainOn; }
            set
            {
                if (_isAutomaticGainOn != value)
                {
                    _isAutomaticGainOn = value;
                    OnPropertyChanged("IsAutomaticGainOn");
                    
                }
            }
        }
        public bool IsAECOn
        {
            get { return _isAECOn; }
            set
            {
                if(_isAECOn != value)
                {
                    _isAECOn = value;
                    OnPropertyChanged("IsAECOn");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => DiscoverKinectSensor();
            this.Unloaded += (s, e) => this.kinect = null;
            _mplayer = new MediaPlayer();
            _mplayer.MediaEnded += delegate { _mplayer.Close(); IsPlaying = false; };
            this.DataContext = this;
        }

        public KinectSensor Kinect
        {
            get { return this.kinect; }
            set
            {
                if (this.kinect != value)
                {
                    if (this.kinect != null)
                    {
                        this.kinect.Stop();
                        this.kinect = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        this.kinect = value;
                        this.kinect.Start();
                    }
                }
            }
        }

        class RecordHelper
        {
            static byte[] buffer = new byte[4096];
            static bool _isRecording;

            public static bool Isrecording
            {
                get { return _isRecording; }
                set { _isRecording = value; }
            }

            struct WAVEFORMATEX
            {
                public ushort wFormatTag;
                public ushort nChannles;
                public uint nSamplesPerSec;
                public uint nAvgBytesPerSec;
                public ushort nBlockAlign;
                public ushort wBitsPerSample;
                public ushort cbSize;
            }

            public static void WriteWavFile(KinectAudioSource source, FileStream fileStream)
            {
                var size = 0;
                //write wav header placeholder
                WriteWavHeader(fileStream, size);
                using (var audioStream = source.Start())
                {
                    //chunk audio stream to file
                    while (audioStream.Read(buffer, 0, buffer.Length) > 0 && _isRecording)
                    {
                        fileStream.Write(buffer, 0, buffer.Length);
                        size += buffer.Length;
                    }
                }

                //write real wav header
                long prePosition = fileStream.Position;
                fileStream.Seek(0, SeekOrigin.Begin);
                WriteWavHeader(fileStream, size);
                fileStream.Seek(prePosition, SeekOrigin.Begin);
                fileStream.Flush();
            }

            public static void WriteWavHeader(Stream stream, int dataLength)
            {
                using (MemoryStream memStream = new MemoryStream(64))
                {
                    int cbFormat = 18;
                    WAVEFORMATEX format = new WAVEFORMATEX()
                    {
                        wFormatTag = 1,
                        nChannles = 1,
                        nSamplesPerSec = 16000,
                        nAvgBytesPerSec = 32000,
                        nBlockAlign = 2,
                        wBitsPerSample = 16,
                        cbSize = 0
                    };

                    using (var bw = new BinaryWriter(memStream))
                    {
                        WriteString(memStream, "RIFF");
                        bw.Write(dataLength + cbFormat + 4);
                        WriteString(memStream, "WAVE");
                        WriteString(memStream, "fmt");
                        bw.Write(cbFormat);

                        bw.Write(format.wFormatTag);
                        bw.Write(format.nChannles);
                        bw.Write(format.nSamplesPerSec);
                        bw.Write(format.nAvgBytesPerSec);
                        bw.Write(format.nBlockAlign);
                        bw.Write(format.wBitsPerSample);
                        bw.Write(format.cbSize);

                        WriteString(memStream, "data");
                        bw.Write(dataLength);
                        memStream.WriteTo(stream);
                    }
                }
            }

            static void WriteString(Stream stream, string s)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        private void DiscoverKinectSensor()
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Connected:
                    if (this.kinect == null)
                        this.kinect = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (this.kinect == e.Sensor)
                    {
                        this.kinect = null;
                        this.kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (this.kinect == null)
                        {
                            //pushed
                        }
                    }
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private object lockObj = new object();
        private void RecordKinectAudio()
        {
            lock(lockObj)
            {
                IsRecording = true;

                var source = CreateAudioSource();

                var time = DateTime.Now.ToString("hhmmss");
                _recordingFileName = time + ".wav";
                using (var fileStream = new FileStream(_recordingFileName, FileMode.Create))
                {
                    RecordHelper.WriteWavFile(source, fileStream);
                }

                IsRecording = false;
            }
        }

        private KinectAudioSource CreateAudioSource()
        {
            var source = kinect.AudioSource;
            source.BeamAngleMode = BeamAngleMode.Adaptive;
            source.NoiseSuppression = _isNoiceSuppressionOn;
            source.AutomaticGainControlEnabled = _isAutomaticGainOn;

            if(IsAECOn)
            {
                source.EchoCancellationMode = EchoCancellationMode.CancellationOnly;
                source.AutomaticGainControlEnabled = false;
                IsAutomaticGainOn = false;
                source.EchoCancellationSpeakerIndex = 0;
            }

            return source;
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            if(_recordingFileName != null)
            {
                IsPlaying = true;
                _mplayer.Open(new Uri(_recordingFileName, UriKind.Relative));
                _mplayer.Play();
            }
        }

        private void Record(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(RecordKinectAudio));
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            kinect.AudioSource.Stop();
            IsRecording = false;
        }
    }
}
