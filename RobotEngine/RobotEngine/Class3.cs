using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace RobotEngine
{
    internal class ABWIA3
    {
        // Fields
        private static bool _AAqk8Hj2Tqji = false;
        private static bool _ABWIA3 = false;
        private static bool _ACIQ6ekFpPiH350pxtrh9OSfnTVh = false;

        // Methods
        [DllImport("nr_native_lib.dll", EntryPoint = "nr_nli", CallingConvention = (CallingConvention)0, CharSet = CharSet.Ansi)]
        public static extern bool AAqk8Hj2Tqji([MarshalAs(UnmanagedType.BStr)] string s, int i);
        [DllImport("CommandEngine_nat.dll", EntryPoint = "nr_nli", CallingConvention = (CallingConvention)0, CharSet = CharSet.Ansi)]
        public static extern bool ABWIA3_([MarshalAs(UnmanagedType.BStr)] string s, int i);
        [DllImport("CommandEngine_nat.dll", EntryPoint = "nr_startup", CallingConvention = (CallingConvention)0, CharSet = CharSet.Ansi)]
        public static extern void ACIQ6ekFpPiH350pxtrh9OSfnTVh([MarshalAs(UnmanagedType.BStr)] string s);
        internal static void ADWJZt()
        {
            if (!_ABWIA3)
            {
                _ABWIA3 = true;
                ACIQ6ekFpPiH350pxtrh9OSfnTVh(typeof(ABWIA3).Assembly.Location);
            }
        }

        internal static void AEcZrapoyQRs8myN5(bool b)
        {
        }

        internal static string AFgkBKD8A8qtbjdHE(string text1)
        {
            string str = "";
            for (int i = 0; i < text1.Length; i++)
            {
                str = str + ((char)(0xff - ((byte)text1[i])));
            }
            return str;
        }

        internal static string AGHu45f2BEVY4PE7mr(string text1)
        {
            byte[] bytes = Convert.FromBase64String(text1);
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
        }

        internal static void AHE2DoD()
        {
            if (!_AAqk8Hj2Tqji)
            {
                _AAqk8Hj2Tqji = true;
                try
                {
                    MessageBox.Show("", "Lock System");
                }
                catch
                {
                }
            }
        }

        internal static void AI3lj5quf16laJgcvE1HxM()
        {
            if (!_ACIQ6ekFpPiH350pxtrh9OSfnTVh)
            {
                _ACIQ6ekFpPiH350pxtrh9OSfnTVh = true;
                string str = typeof(ABWIA3).Assembly.Location.ToString();
                int num = 0;
                Process currentProcess = Process.GetCurrentProcess();
                for (int i = 0; i < currentProcess.Modules.Count; i++)
                {
                    try
                    {
                        if (currentProcess.Modules[i].FileName.ToString() == str.ToString())
                        {
                            num = currentProcess.Modules[i].BaseAddress.ToInt32();
                            try
                            {
                                bool flag = ABWIA3_(str, num);
                            }
                            catch (DllNotFoundException)
                            {
                                try
                                {
                                    bool flag2 = AAqk8Hj2Tqji(str, num);
                                }
                                catch (DllNotFoundException)
                                {
                                    try
                                    {
                                        MessageBox.Show("Can't find native library!  Please install the  native library to your local directory or to your system(32) directory.", "'CommandEngine_nat.dll' not found!");
                                        Application.Exit();
                                        Application.DoEvents();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            break;
                        }
                    }
                    catch (DllNotFoundException)
                    {
                    }
                }
            }
        }
    }
}
