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
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace SendMassage_1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

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


        static void Kain()

        {
            IntPtr VK_A = new IntPtr(65);
            IntPtr hwnd = FindWindow(null, "WIFI/蓝牙智能小车操纵平台 正式版V1.1 By  Liuviking      机器人创意工作室       www.wifi-robots.com");
            IntPtr htextbox = FindWindowEx(hwnd, IntPtr.Zero, "EDIT", null);
            IntPtr htextbox2 = FindWindowEx(hwnd, htextbox, "EDIT", null);
            PostMessage(hwnd, WM_KEYDOWN, 87, 0);
            //Thread.Sleep(200);
            PostMessage(hwnd, WM_KEYUP, 87, 0);
            //PostMessage(htextbox, WM_KEYUP, 65, 0);
        }

        public MainWindow()
        {
            //this.Hide();
            InitializeComponent();
            Kain();
        }
    }
}
