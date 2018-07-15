using MasterThesis.Controller;
using MasterThesis.ExchangeObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace MasterThesis.WindowComponents.Views
{
    /// <summary>
    /// Interaktionslogik für Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        private static Navigation instance;

        public static Navigation getInstance()
        {
            if(instance == null)
            {
                instance = new Navigation();
                instance.StartUp();
            }
            return instance;
        }
        private static bool started;

        private static bool isConfigured = false;

        private Panel pnl;
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", SetLastError = true)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public enum MouseActionAdresses
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public Navigation()
        {
            pnl = new Panel();
            pnl.Width = 1060;
            pnl.Height = 600;
            InitializeComponent();

            pnl.Resize += new EventHandler(Resize);
        }

        private void Resize(object sender, EventArgs e)
        {
            this.Resize();
        }

        public void StartUp()
        {
            String pName = global::MasterThesis.Properties.Settings.Default.NAVApplication;
            String pParam = global::MasterThesis.Properties.Settings.Default.NAVApplication_Params;

            if (!File.Exists(pName))
            {
                return;
            }
            else
            {
                isConfigured = true;
            }

            RECT rect = new RECT();
            const short SWP_NOMOVE = 0X2;
            const short SWP_NOSIZE = 1;
            const short SWP_NOZORDER = 0X4;
            const int SWP_SHOWWINDOW = 0x0040;

            Process p;

            if (Static.GetProcess() == null)
            {


                if (Process.GetProcessesByName("PC_Navigator").Length > 0)
                {
                    //Don't trust a running service!
                    p = Process.GetProcessesByName("PC_Navigator")[0];

                    p.Kill();
                }


                p = Process.Start(pName, pParam);
                Thread.Sleep(3000);
                emulateClick(950, 1010);
                Static.SetProcess(p);
            }

            if (!started)
            {

                p = Static.GetProcess();
                //SetWindowPos(p.MainWindowHandle, 0, 0, 0, 1920, 1080, SWP_NOZORDER | SWP_SHOWWINDOW);

                GetWindowRect(p.MainWindowHandle, out rect);

                SetWindowPos(p.MainWindowHandle, 0, 0, 0, (rect.Right - rect.Left), (rect.Bottom - rect.Top), 0);


                WFH.Child = pnl;
                SetParent(p.MainWindowHandle, pnl.Handle);

                SetWindowPos(p.MainWindowHandle, 0, 0, 0, pnl.Width, pnl.Height, 0);
                started = true;
            }
        }

        private void Resize()
        {
            if (isConfigured && started)
            {
                SetWindowPos(Static.GetProcess().MainWindowHandle, 0, 0, 0, pnl.Width, pnl.Height, 0);
            }
        }

        private void emulateClick(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
            mouse_event((int)(MouseActionAdresses.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseActionAdresses.LEFTUP), 0, 0, 0, 0);
        }

        internal void HideWindow()
        {
            if (isConfigured && started)
            {
                SetWindowPos(Static.GetProcess().MainWindowHandle, 0, 0, 0, 0, 0, 0);
            }
        }

        internal void RestoreWindow()
        {
            this.Resize();
        }
    }
}
