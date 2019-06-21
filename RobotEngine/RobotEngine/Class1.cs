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

namespace RobotEngine
{
    public class WifiRobotCMDEngineV2
    {
        // Fields
        private static ManualResetEvent AAqk8Hj2Tqji = new ManualResetEvent(false);
        private static ManualResetEvent _ABWIA3 = new ManualResetEvent(false);
        private readonly ManualResetEvent ACIQ6ekFpPiH350pxtrh9OSfnTVh = new ManualResetEvent(false);
        private StringBuilder ADWJZt = new StringBuilder();
        private string _AGHu45f2BEVY4PE7mr;
        private ACIQ6ekFpPiH350pxtrh9OSfnTVh _AHE2DoD = new ACIQ6ekFpPiH350pxtrh9OSfnTVh();
        private IPEndPoint AI3lj5quf16laJgcvE1HxM;
        private Socket AJ3UMv;
        private byte[] AKQ1 = new byte[3];
        private static int ALota = 0;
        private static int AMOKFQ = 0;
    private static int ANOv1WC = 0;
        private static int AO9i = 0;
        private bool APMUVTIAMp = false;
        private bool AQ3kQDjjbOU = false;

        // Events
        public event SetCallBackDataValue Setcallbackvalue;

        public event SetWIFIState Setwifistate;

        // Methods
        public WifiRobotCMDEngineV2(object cor)
        {
            this.AAqk8Hj2Tqji(cor);
        }

        private void AAqk8Hj2Tqji(object obj1)
        {
            this.AQ3kQDjjbOU = this.AHE2DoD.ADWJZt(obj1);
        }

        private void ABWIA3(IAsyncResult result1)
        {
            try
            {
                Socket asyncState = (Socket)result1.AsyncState;
                if (asyncState != null)
                {
                    asyncState.EndConnect(result1);
                    new Thread(new ThreadStart(this.ACIQ6ekFpPiH350pxtrh9OSfnTVh)).Start();
                }
            }
            catch
            {
            }
            finally
            {
                this.ACIQ6ekFpPiH350pxtrh9OSfnTVh.Set();
            }
        }

        private void ACIQ6ekFpPiH350pxtrh9OSfnTVh()
        {
            try
            {
                StateObject state = new StateObject
                {
                    workSocket = this.AJ3UMv
                };
                this.AJ3UMv.BeginReceive(state.buffer, 0, 1, SocketFlags.None, new AsyncCallback(this.ADWJZt), state);
            }
            catch (Exception exception)
            {
                this.AFgkBKD8A8qtbjdHE(true);
                MessageBox.Show(exception.Message);
            }
        }

        private void ADWJZt(IAsyncResult result1)
        {
            try
            {
                StateObject asyncState = (StateObject)result1.AsyncState;
                Socket workSocket = asyncState.workSocket;
                if (workSocket.EndReceive(result1) > 0)
                {
                    if (!this.APMUVTIAMp)
                    {
                        if (asyncState.buffer[0] == 0xff)
                        {
                            this.APMUVTIAMp = true;
                            AMOKFQ = 0;
                        }
                    }
                    else if (asyncState.buffer[0] == 0xff)
                    {
                        this.APMUVTIAMp = false;
                        if (AMOKFQ == 3)
                    {
                            if (this.AEcZrapoyQRs8myN5 != null)
                            {
                                this.AEcZrapoyQRs8myN5(this.AKQ1);
                            }
                            Array.Clear(this.AKQ1, 0, this.AKQ1.Length);
                        }
                        AMOKFQ = 0;
                    }
                    else
                    {
                        this.AKQ1[AMOKFQ] = asyncState.buffer[0];
                        AMOKFQ++;
                    }
                    Array.Clear(asyncState.buffer, 0, asyncState.buffer.Length);
                }
                else
                {
                    Array.Clear(asyncState.buffer, 0, asyncState.buffer.Length);
                }
                workSocket.BeginReceive(asyncState.buffer, 0, 1, SocketFlags.None, new AsyncCallback(this.ADWJZt), asyncState);
            }
            catch (Exception)
            {
            }
        }

        private void AEcZrapoyQRs8myN5(IAsyncResult result1)
        {
            try
            {
                Socket asyncState = (Socket)result1.AsyncState;
                ABWIA3.Set();
            }
            catch (Exception exception)
            {
                this.AFgkBKD8A8qtbjdHE(true);
                MessageBox.Show(exception.Message);
            }
        }

        private bool AFgkBKD8A8qtbjdHE(string text1)
        {
            Regex regex = new Regex(ABWIA3.AGHu45f2BEVY4PE7mr("KAAoAD8AOgAoAD8AOgAyADUAWwAwAC0ANQBdAHwAMgBbADAALQA0AF0AXABkAHwAKAAoADEAXABkAHsAMgB9ACkAfAAoAFsAMQAtADkAXQA/AFwAZAApACkAKQBcAC4AKQB7ADMAfQAoAD8AOgAyADUAWwAwAC0ANQBdAHwAMgBbADAALQA0AF0AXABkAHwAKAAoADEAXABkAHsAMgB9ACkAfAAoAFsAMQAtADkAXQA/AFwAZAApACkAKQApAA=="));
            return regex.IsMatch(text1);
        }

        public string byteToHexStr(byte[] bytes)
        {
            string str = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    str = str + bytes[i].ToString(ABWIA3.AGHu45f2BEVY4PE7mr("WAAyAA=="));
                }
            }
            return str;
        }

        public byte[] CreateData(byte type, byte cmd, byte data)
        {
            return new byte[] { 0xff, type, cmd, data, 0xff };
        }

        public string Domain2ip(string str)
        {
            if (this.AFgkBKD8A8qtbjdHE(str))
        {
                return str;
            }
            string str2 = "";
            try
            {
                str2 = Dns.GetHostByName(str).AddressList[0].ToString();
            }
            catch (Exception)
            {
                return null;
            }
            return str2;
        }

        public void DrawRectangle(Graphics g, int x, int y, int PenWidth)
        {
            Rectangle rect = new Rectangle(x - 10, y - 10, 20, 20);
            Pen pen = new Pen(Color.Blue)
            {
                Width = PenWidth
            };
            g.DrawRectangle(pen, rect);
        }

        public void DrawSignal(int signalType, Graphics g, int x, int y, int PenWidth)
        {
            Pen pen = new Pen(Color.Red)
            {
                Width = PenWidth
            };
            if (signalType != 0)
            {
                if (signalType == 1)
                {
                    g.DrawLine(pen, (int)((x / 2) - 15), (int)(y / 2), (int)((x / 2) + 15), (int)(y / 2));
                    g.DrawLine(pen, (int)(x / 2), (int)((y / 2) - 15), (int)(x / 2), (int)((y / 2) + 15));
                }
                else
                {
                    Rectangle rect = new Rectangle((x / 2) - 10, (y / 2) - 10, 20, 20);
                    g.DrawEllipse(pen, rect);
                }
            }
        }

        public void DrawStanderLine(Graphics g, int x, int y, int PenWidth)
        {
            Pen pen = new Pen(Color.Green)
            {
                Width = PenWidth,
                DashStyle = DashStyle.Dash
            };
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(pen, 0, y, x / 3, (2 * y) / 3);
            g.DrawLine(pen, x, y, (2 * x) / 3, (2 * y) / 3);
            g.DrawLine(pen, (int)(x / 3), (int)((2 * y) / 3), (int)((2 * x) / 3), (int)((2 * y) / 3));
            g.DrawLine(pen, (int)(x / 4), (int)((3 * y) / 4), (int)((3 * x) / 4), (int)((3 * y) / 4));
            g.DrawLine(pen, (int)(x / 8), (int)((7 * y) / 8), (int)((7 * x) / 8), (int)((7 * y) / 8));
        }

        public void DrawWarning(Graphics g, int x, int y, int PenWidth)
        {
            Font font = new Font(ABWIA3.AGHu45f2BEVY4PE7mr("QQByAGkAYQBsAA=="), (float)PenWidth);
            SolidBrush brush = new SolidBrush(Color.Red);
            g.DrawString(ABWIA3.AGHu45f2BEVY4PE7mr("ZotKVAH/VwBJAEYASQDhT/dTMV8M/+9T/YAiTjFZ3o+lYwH/"), font, brush, new PointF((float)(x / 2), (float)(y / 2)));
            brush.Dispose();
            font.Dispose();
        }

        public byte[] HexStringToByteArray(string s)
        {
            try
            {
                s = s.Replace(ABWIA3.AGHu45f2BEVY4PE7mr("IAA="), "");
                if ((s.Length % 2) != 0)
                {
                    MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("cGVuYwVTTU9wZQ1OCFTVbAH/94uEZyCQdlBNT3BlhHZwZW5jBVMB/w=="), ABWIA3.AGHu45f2BEVY4PE7mr("0VMBkNBjOnk="), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                byte[] buffer = new byte[s.Length / 2];
                for (int i = 0; i < s.Length; i += 2)
                {
                    buffer[i / 2] = Convert.ToByte(s.Substring(i, 2), 0x10);
                }
                return buffer;
            }
            catch
            {
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("cGVuYwVTDU4IVNVsAf/3i4RnIJB2UE1PcGWEdjEANgDbjzZScGVuYwVTAf8="), ABWIA3.AGHu45f2BEVY4PE7mr("0VMBkNBjOnk="), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return null;
            }
        }

        public void SendCMD(int controlType, string CMD_Custom, SerialPort comm)
        {
            ThreadStart start = null;
            AFgkBKD8A8qtbjdHE dhe = new AFgkBKD8A8qtbjdHE {
                ABWIA3 = CMD_Custom,
            AAqk8Hj2Tqji = this
            };
            if (this.AQ3kQDjjbOU)
            {
                if (controlType == 0)
                {
                    if (start == null)
                    {
                        start = new ThreadStart(dhe.AAqk8Hj2Tqji);
                    }
                    new Thread(start).Start();
                }
                else if (controlType == 1)
                {
                    this.SendDataInComm(comm, dhe.ABWIA3);
                }
                else
                {
                    MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("94tIUQmQ6WIATs15p2M2UrllD18B/w=="), ABWIA3.AGHu45f2BEVY4PE7mr("p2M2UtFTAZDQYzp5"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        public void SendCMD(int controlType, byte[] byteData, SerialPort comm)
        {
            ThreadStart start = null;
            AGHu45f2BEVY4PE7mr hufbevypemr = new AGHu45f2BEVY4PE7mr
            {
                ABWIA3 = byteData,
                AAqk8Hj2Tqji = this
            };
            if (this.AQ3kQDjjbOU && (null != hufbevypemr.ABWIA3))
            {
                if (controlType == 0)
                {
                    if (start == null)
                    {
                        start = new ThreadStart(hufbevypemr.AAqk8Hj2Tqji);
                    }
                    new Thread(start).Start();
                }
                else if (controlType == 1)
                {
                    this.SendDataInComm(comm, hufbevypemr.ABWIA3);
                }
                else
                {
                    MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("94tIUQmQ6WIATs15p2M2UrllD18B/w=="), ABWIA3.AGHu45f2BEVY4PE7mr("p2M2UtFTAZDQYzp5"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        public void SendDataInComm(SerialPort comm, string data)
        {
            byte[] buffer = this.HexStringToByteArray(data);
            if (null != buffer)
            {
                comm.Write(buffer, 0, buffer.Length);
            }
        }

        public void SendDataInComm(SerialPort comm, byte[] data)
        {
            byte[] buffer = data;
            if (null != buffer)
            {
                comm.Write(buffer, 0, buffer.Length);
            }
        }

        public void SendHeartCMD(int controlType, SerialPort comm)
        {
            ThreadStart start = null;
            AHE2DoD od = new AHE2DoD
            {
                ABWIA3 = this
            };
            this.AQ3kQDjjbOU = this.AHE2DoD.ACIQ6ekFpPiH350pxtrh9OSfnTVh();
            od.AAqk8Hj2Tqji = this.HexStringToByteArray(ABWIA3.AGHu45f2BEVY4PE7mr("RgBGAEUARgBFAEYARQBFAEYARgA="));
            if (this.AQ3kQDjjbOU)
            {
                if (controlType == 0)
                {
                    if (start == null)
                    {
                        start = new ThreadStart(od.AAqk8Hj2Tqji);
                    }
                    new Thread(start).Start();
                }
                else if (controlType == 1)
                {
                    this.SendDataInComm(comm, od.AAqk8Hj2Tqji);
                }
            }
        }

        public bool SocketConnect()
        {
            this.ACIQ6ekFpPiH350pxtrh9OSfnTVh.Reset();
            try
            {
                this.AJ3UMv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.AJ3UMv.Blocking = false;
                this.AJ3UMv.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 0x1388);
                this.AJ3UMv.BeginConnect(this.AI3lj5quf16laJgcvE1HxM, new AsyncCallback(this.ABWIA3), this.AJ3UMv);
                if (this.ACIQ6ekFpPiH350pxtrh9OSfnTVh.WaitOne(0xbb8, false))
                {
                    return true;
                }
                this.AJ3UMv.Dispose();
                this.AJ3UMv = null;
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("3o+lY1cASQBGAEkAf2cxWSWNAf/3i254mltTAGUAcgAyAG4AZQB0AC9mJlRjazhe0I9MiAH/"), ABWIA3.AGHu45f2BEVY4PE7mr("VwBJAEYASQAhag9fvotuf9BjOnk="), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void TakePhoto(Bitmap snapshot, string RootPath, string FileName)
        {
            try
            {
                snapshot.Save(RootPath + FileName);
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("zWJncRBin1IB/2dxR3LdT1hbKFc=") + RootPath + FileName, ABWIA3.AGHu45f2BEVY4PE7mr("zWJncRBin1LQYzp5"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception)
            {
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("zWJncfpRGZUB/6FsCWe3g9ZTMFL+Vs9QAjA="), ABWIA3.AGHu45f2BEVY4PE7mr("zWJncTFZJY3QYzp5"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void WR_DEBUG(string Modules, string debugstring)
        {
            Console.WriteLine(ABWIA3.AGHu45f2BEVY4PE7mr("WwBXAEkARgBJAC0AUgBPAEIATwBUAFMAXQArACsA") + Modules + ABWIA3.AGHu45f2BEVY4PE7mr("KwArACsAKwArACsAKwArACsAKwArACsAKwArAGwAaQB1AHYAaQBrAGkAbgBnACAARABFAEIAVQBHACsAKwArACsAKwArACsAKwArACsAKwArACsAKwArACsAKwA=") + debugstring);
        }

        // Properties
        public IPEndPoint IPE
        {
            get
            {
                return this.AI3lj5quf16laJgcvE1HxM;
            }
            set
            {
                this.AI3lj5quf16laJgcvE1HxM = value;
            }
        }

        public Socket SOCKET
        {
            get
            {
                return this.AJ3UMv;
            }
            set
            {
                this.AJ3UMv = value;
            }
        }

        // Nested Types
        [CompilerGenerated]
        private sealed class AFgkBKD8A8qtbjdHE
    {
        // Fields
        public WifiRobotCMDEngineV2 AAqk8Hj2Tqji;
        public string ABWIA3;

        // Methods
        public void AAqk8Hj2Tqji()
        {
            try
            {
                byte[] buffer = this.AAqk8Hj2Tqji.HexStringToByteArray(this.ABWIA3.ToString());
                this.AAqk8Hj2Tqji.AJ3UMv.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.AAqk8Hj2Tqji.AEcZrapoyQRs8myN5), this.AAqk8Hj2Tqji.AJ3UMv);
            }
            catch (Exception exception)
            {
                this.AAqk8Hj2Tqji.AFgkBKD8A8qtbjdHE(true);
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("0VMBkHBlbmP6URmVAf8=") + exception.Message);
            }
        }
    }

    [CompilerGenerated]
    private sealed class AGHu45f2BEVY4PE7mr
    {
        // Fields
        public WifiRobotCMDEngineV2 AAqk8Hj2Tqji;
        public byte[] ABWIA3;

        // Methods
        public void AAqk8Hj2Tqji()
        {
            try
            {
                this.AAqk8Hj2Tqji.AJ3UMv.BeginSend(this.ABWIA3, 0, this.ABWIA3.Length, SocketFlags.None, new AsyncCallback(this.AAqk8Hj2Tqji.AEcZrapoyQRs8myN5), this.AAqk8Hj2Tqji.AJ3UMv);
            }
            catch (Exception exception)
            {
                this.AAqk8Hj2Tqji.AFgkBKD8A8qtbjdHE(true);
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("0VMBkHBlbmP6URmVAf8=") + exception.Message);
            }
        }
    }

    [CompilerGenerated]
    private sealed class AHE2DoD
    {
        // Fields
        public byte[] AAqk8Hj2Tqji;
        public WifiRobotCMDEngineV2 ABWIA3;

        // Methods
        public void AAqk8Hj2Tqji()
        {
            try
            {
                this.ABWIA3.AJ3UMv.BeginSend(this.AAqk8Hj2Tqji, 0, this.AAqk8Hj2Tqji.Length, SocketFlags.None, new AsyncCallback(this.ABWIA3.AEcZrapoyQRs8myN5), this.ABWIA3.AJ3UMv);
            }
            catch (Exception exception)
            {
                MessageBox.Show(ABWIA3.AGHu45f2BEVY4PE7mr("VwBJAEYASQDej6Vj+lEZlQH/") + exception.Message);
            }
        }
    }

    public delegate void SetCallBackDataValue(byte[] callbackvalue);

    public delegate void SetWIFIState(bool wifistate);
}}
