using System;
using System.Collections.Generic;
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
using WIFIRobotCMDEngineV2;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO.Ports;


namespace SendMassageEngine
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int statLength = 15;
        private int statIndex = 0, statReady = 0;
        private int[] statCount = new int[statLength];
        private SerialPort comm = new SerialPort();
        private StringBuilder builder = new StringBuilder();
        private bool Listening = false;//是否没有执行完invoke相关操作
        public bool Send_status = true;

        private System.Windows.Forms.StatusBar statusBar = new System.Windows.Forms.StatusBar();
        private System.Windows.Forms.StatusBarPanel fpsPanel = new System.Windows.Forms.StatusBarPanel();
        private StatusBarPanel GroupINFO = new System.Windows.Forms.StatusBarPanel();
        private StatusBarPanel Systemstatus = new System.Windows.Forms.StatusBarPanel();



        string CameraIp = "";
        string ControlIp = "192.168.1.1";
        string Port = "2001";
        string CMD_Forward = "", CMD_Backward = "", CMD_TurnLeft = "", CMD_TurnRight = "", CMD_Stop = "";
        string CMD_TurnOnLight = "", CMD_TurnOffLight = "", CMD_Beep = "";
        string CMD_LeftForward = "", CMD_RightForward = "", CMD_LeftBackward = "", CMD_RightBackward = "";
        string AutoSetScreen;
        private int controlType = 3;
        private string btCom;
        private string btBaudrate;
        public WifiRobotCMDEngine RobotEngine;//实例化引擎
        public WifiRobotCMDEngineV2 RobotEngine2;//实例化引擎
        static IPAddress ips;
        static IPEndPoint ipe;
        static Socket socket = null;
        static string RootPath = System.Windows.Forms.Application.StartupPath;
        static string FileName = RootPath + "\\Config.ini";


        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);


        public MainWindow()
        {
            InitializeComponent();
            GetIni();
            SetPanle();
            RobotEngine2 = new WifiRobotCMDEngineV2((Object)this.statusBar);
            OpenWifi();
        }

        private void SetPanle()
        {
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 586);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.fpsPanel,
            this.Systemstatus,
            this.GroupINFO});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(1064, 24);
            this.statusBar.TabIndex = 1;
            this.Opacity = 1;
            // 
            // fpsPanel
            // 
            this.fpsPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.fpsPanel.MinWidth = 300;
            this.fpsPanel.Name = "fpsPanel";
            this.fpsPanel.Width = 347;
            // 
            // Systemstatus
            // 
            this.Systemstatus.MinWidth = 40;
            this.Systemstatus.Name = "Systemstatus";
            this.Systemstatus.Width = 200;
            // 
            // GroupINFO
            // 
            this.GroupINFO.Name = "GroupINFO";
            this.GroupINFO.Text = "www.wifi-robots.com WIFI机器人网·机器人创意工作室  QQ群： 145181710/196564839 ";
            this.GroupINFO.Width = 500;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DestorySocket();
        }

        private void OpenWifi()
        {
            if (0 == controlType) return;
            //开启WIFI模式
            controlType = InitWIFISocket(ControlIp, Port) ? 0 : 2;
            if (0 == controlType)
            {
                InitHeartPackage();
            }
        }

        //初始化Socket连接
        bool ret = false;
        private bool InitWIFISocket(String controlIp, String port)
        {
            //this.Systemstatus.Text = "正在尝试连接WIFI板···";

            try
            {

                ips = IPAddress.Parse(controlIp.ToString());
                ipe = new IPEndPoint(ips, Convert.ToInt32(port.ToString()));
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                RobotEngine2.SOCKET = socket;
                RobotEngine2.IPE = ipe;
                ret = RobotEngine2.SocketConnect();

            }
            catch (Exception e)
            {

                //MessageBox.Show("WIFI初始化失败：" + e.Message, "WIFI初始化失败提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ret;
        }

        //销毁socket
        private void DestorySocket()
        {
            try
            {
                socket.Close();
            }
            catch (Exception e1)
            {

            }
        }

        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
        private void GetIni()
        {
            CameraIp = ReadIni("VideoUrl", "videoUrl", "");
            ControlIp = ReadIni("ControlUrl", "controlUrl", "");
            Port = ReadIni("Port", "port", "");


            CMD_Forward = ReadIni("Forward", "forward", "");
            CMD_Backward = ReadIni("Backward", "backward", "");
            CMD_TurnLeft = ReadIni("Left", "left", "");
            CMD_TurnRight = ReadIni("Right", "right", "");
            CMD_Stop = ReadIni("Stop", "stop", "");

            CMD_LeftForward = ReadIni("LeftForward", "leftForward", "");
            CMD_RightForward = ReadIni("RightForward", "rightForward", "");
            CMD_LeftBackward = ReadIni("LeftBackward", "leftBackward", "");
            CMD_RightBackward = ReadIni("RightBackward", "rightBackward", "");


            CMD_TurnOnLight = ReadIni("TurnOnLight", "turnOnLight", "");
            CMD_TurnOffLight = ReadIni("TurnOffLight", "turnOffLight", "");
            CMD_Beep = ReadIni("Speaker", "speaker", "");

            btCom = ReadIni("BTCOM", "btcom", "");
            btBaudrate = ReadIni("BTBaudrate", "btbaudrate", "");

            AutoSetScreen = ReadIni("AutoSetScreen", "autoSetScreen", "");

        }

        private void InitHeartPackage()
        {
            Thread HThread = new Thread(HeartPackage);
            HThread.IsBackground = true;
            HThread.Start();
        }

        private void HeartPackage()
        {
            while (true)
            {
                RobotEngine2.SendHeartCMD(controlType, comm);
                Thread.Sleep(10000);
            }
        }


        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.label_SpeedR.Text = this.Speedright.Value.ToString();
            System.Windows.Forms.Application.DoEvents();
            byte[] Speedright_data = RobotEngine2.CreateData(0X02, 0X01, Convert.ToByte(Convert.ToInt32(this.textBox.Text)));//舵机数据打包第一个参数代表舵机，第二个代表哪个舵机，第三个代表转动角度值
            RobotEngine2.SendCMD(controlType, Speedright_data, comm);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            OpenWifi();
            this.textBlock.Text = controlType.ToString();

        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.label_SpeedL.Text = this.Speedleft.Value.ToString();
            System.Windows.Forms.Application.DoEvents();
            byte[] Speedleft_data = RobotEngine2.CreateData(0X02, 0X02, Convert.ToByte(Convert.ToInt32(this.textBox.Text)));//舵机数据打包第一个参数代表舵机，第二个代表哪个舵机，第三个代表转动角度值
            RobotEngine2.SendCMD(controlType, Speedleft_data, comm);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            byte[] Gear7_data = RobotEngine2.CreateData(0X01, 0X07, Convert.ToByte(Convert.ToInt32(this.textBox.Text)));//舵机数据打包
            //this.txtCommandPanel.AppendText("舵机7: " + Gear7_data[3].ToString() + "\r\n");
            RobotEngine2.SendCMD(controlType, Gear7_data, comm);


        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Forms.Application.DoEvents();
            byte[] Gear8_data = RobotEngine2.CreateData(0X01, 0X08, Convert.ToByte(Convert.ToInt32(this.textBox1.Text)));//舵机数据打包
            Console.WriteLine(Gear8_data.ToString());
           
            //socket.BeginSend(Gear8_data, 0, Gear8_data.Length, SocketFlags.None, new AsyncCallback(this.AEcZrapoyQRs8myN5), socket);

            //this.txtCommandPanel.AppendText("舵机8: " + Gear8_data[3].ToString() + "\r\n");
            RobotEngine2.SendCMD(controlType, Gear8_data, comm);
        }

        private void AEcZrapoyQRs8myN5(IAsyncResult result1)
        {
            try
            {
                Socket asyncState = (Socket)result1.AsyncState;
                //ABWIA3.Set();
            }
            catch (Exception exception)
            {
                //this.AF)gkBKD8A8qtbjdHE(true);
                //MessageBox.Show(exception.Message);
            }
        }



        private void buttonForward_Click(object sender, EventArgs e)
        {


            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_Forward, comm);
                Send_status = false;
            }

        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_TurnLeft, comm);
                Send_status = false;
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_TurnRight, comm);
                Send_status = false;
            }
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_Backward, comm);
                Send_status = false;
            }
        }


        private void buttonStop_Click(object sender, EventArgs e)
        {
            Send_status = true;
            RobotEngine2.SendCMD(controlType, CMD_Stop, comm);
        }

        private void btnLeftForward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_LeftForward, comm);
                Send_status = false;
            }
        }

        private void btnRightForward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_RightForward, comm);
                Send_status = false;
            }
        }

        private void btnLeftBack_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_LeftBackward, comm);
                Send_status = false;
            }
        }

        private void btnRightBack_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                RobotEngine2.SendCMD(controlType, CMD_RightBackward, comm);
                Send_status = false;
            }
        }

    }
}
