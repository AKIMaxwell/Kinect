using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using WIFIRobotCMDEngineV2;

namespace SendMassage
{
    public class Class1
    {
        public WifiRobotCMDEngine RobotEngine;//实例化引擎
        public WifiRobotCMDEngineV2 RobotEngine2;//实例化引擎
        static IPAddress ips;
        static IPEndPoint ipe;
        static Socket socket = null;


    }
}
