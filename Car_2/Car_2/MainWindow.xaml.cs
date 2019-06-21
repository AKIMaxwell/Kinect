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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO.Ports;
using Blaney;
using System.Threading;
using System.Diagnostics;
using System.IO;


namespace Car_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Send_status = true;
        //public CommandEngine RobotEngine2;//实例化引擎
        private int controlType = 3;
        string CMD_Forward = "", CMD_Backward = "", CMD_TurnLeft = "", CMD_TurnRight = "", CMD_Stop = "";
        private SerialPort comm = new SerialPort();



        //声明 API 函数 
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessage(int hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll ")]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);


        public MainWindow()
        {
            InitializeComponent();
            SendMsgToMainForm(1);
        }

        //定义消息常数 
        public const int CUSTOM_MESSAGE = 0X400 + 2;//自定义消息


        //向窗体发送消息的函数 
        public void SendMsgToMainForm(int MSG)
        {
            int WINDOW_HANDLER = FindWindow(null, "text.txt - 记事本");
            if (WINDOW_HANDLER == 0)
            {
                throw new Exception("Could not find Main window!");
            }

            long result = SendMessage(WINDOW_HANDLER, CUSTOM_MESSAGE, new IntPtr(14), IntPtr.Zero).ToInt64();
        }


        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (Send_status)
            {
                //RobotEngine2.SendCMD(controlType, CMD_Forward, comm);
                Send_status = false;
            }
        }
    }
}
