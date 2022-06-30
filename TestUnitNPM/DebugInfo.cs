using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;

namespace TestUnitNPM
{
    class DebugInfo
    {
        public static void GetInfo(string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            MessageBox.Show(message + "\n" + "@ " + lineNumber + " (" + caller + ")");
        }
    }
}
