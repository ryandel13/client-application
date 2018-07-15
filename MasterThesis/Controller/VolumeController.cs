using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterThesis.Controller
{
    class VolumeController
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);


        public static void VolumeUp()
        {
            keybd_event((byte)Keys.VolumeUp, 0, 0, 0); // increase volume
        }

        public static void VolumeDown()
        {
            keybd_event((byte)Keys.VolumeDown, 0, 0, 0); // decrease volume
        }

        public static void Mute()
        {
            keybd_event((byte)Keys.VolumeMute, 0, 0, 0); // decrease volume
        }
    }
}
