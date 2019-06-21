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
using System.Diagnostics;

namespace SendMassage_2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern int FindWindowEx(IntPtr hWnd1, int hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(int hwnd, StringBuilder lpString, int cch);
        [DllImport("User32 ")]
        public static extern bool SendMessage(int hWnd, int Msg, int wParam, IntPtr lParam);

        public const int WM_GETTEXT = 0xD;
        private static void Test04()
        {
            bool running = true;
            new Thread(() =>
            {
                while (running)
                {
                    Process[] process_array = Process.GetProcesses();
                    foreach (Process p in process_array)
                    {
                        if (p.MainWindowTitle.IndexOf("记事本") != -1)//找到了
                        {
                            int hEdit = FindWindowEx(p.MainWindowHandle, 0, "Edit", "");
                            string w = " ";
                            IntPtr ptr = Marshal.StringToHGlobalAnsi(w);
                            if (SendMessage(hEdit, WM_GETTEXT, 100, ptr))

                                Console.WriteLine(Marshal.PtrToStringAnsi(ptr));
                        }
                    }
                    int tick = Environment.TickCount;
                    while (running && Environment.TickCount - tick < 10000)
                    {
                        Thread.Sleep(10);
                    }
                }
                Console.WriteLine("run over");
            }).Start();
            while (Console.ReadLine().ToLower() != "quit") ;
            running = false;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
