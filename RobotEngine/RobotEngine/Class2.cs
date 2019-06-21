using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RobotEngine
{
    internal class ACIQ6ekFpPiH350pxtrh9OSfnTVh
    {
        // Methods
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr AAqk8Hj2Tqji(string, string);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr ABWIA3(IntPtr, IntPtr, string, string);
        public bool ACIQ6ekFpPiH350pxtrh9OSfnTVh()
        {
            string str = ABWIA3.AGHu45f2BEVY4PE7mr("VwBJAEYASQAvAN2EWXJ6Zv2AD1xmj81ktX5zXvBTIABjaw9fSHJWADEALgAxACAAQgB5ACAAIABMAGkAdQB2AGkAawBpAG4AZwAgACAAIAAgACAAIAA6Z2hWuk4bUg9h5V1cT6RbIAAgACAAIAAgACAAIAB3AHcAdwAuAHcAaQBmAGkALQByAG8AYgBvAHQAcwAuAGMAbwBtAA==");
            return (AAqk8Hj2Tqji(null, str) != IntPtr.Zero);
        }

        public bool ADWJZt(object obj1)
        {
            string str = ABWIA3.AGHu45f2BEVY4PE7mr("dwB3AHcALgB3AGkAZgBpAC0AcgBvAGIAbwB0AHMALgBjAG8AbQAgAFcASQBGAEkAOmdoVrpOUX+3ADpnaFa6ThtSD2HlXVxPpFsgACAAUQBRAKR/Gv8gADEANAA1ADEAOAAxADcAMQAwAC8AMQA5ADYANQA2ADQAOAAzADkAIAA=");
            if (obj1 is StatusBar)
            {
                StatusBar bar = (StatusBar)obj1;
                return (bar.Panels[2].Text == str);
            }
            return false;
        }
    }

}
