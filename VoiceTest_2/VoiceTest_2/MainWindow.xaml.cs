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

namespace VoiceTest_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += delegate { ListenForBeamChanges(); };
        }

        private KinectAudioSource CreateAudioSource()
        {
            var source = KinectSensor.KinectSensors[0].AudioSource;
            source.NoiseSuppression = true;
            source.AutomaticGainControlEnabled = true;
            source.BeamAngleMode = BeamAngleMode.Adaptive;
            return source;
        }

        private void ListenForBeamChanges()
        {
            KinectSensor.KinectSensors[0].Start();
            var audioSource = CreateAudioSource();
            audioSource.BeamAngleChanged += audioSource_BeamAngleChanged;
            audioSource.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private double _beamAngle;
        public double BeamAngle
        {
            get { return _beamAngle; }
            set
            {
                _beamAngle = value;
                OnPropertyChanged("BeamAngle");
            }
        }

        private void audioSource_BeamAngleChanged(object sender, BeamAngleChangedEventArgs e)
        {
            BeamAngle = -1 * e.Angle;
        }
    }
}
