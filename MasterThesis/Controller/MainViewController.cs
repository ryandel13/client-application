using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MasterThesis.Controller
{
    class MainViewController
    {
        private static MainWindow GetMainWindow()
        {
            return MainWindow.getInstance();
        }

        public static void ToggleFullscreen()
        {
            Boolean fullscreen = global::MasterThesis.Properties.Settings.Default.fullscreen;

            if (!fullscreen)
            {
                GetMainWindow().WindowState = System.Windows.WindowState.Maximized;
                GetMainWindow().WindowStyle = System.Windows.WindowStyle.None;
                global::MasterThesis.Properties.Settings.Default.fullscreen = true;
            }
            else
            {
                GetMainWindow().WindowState = System.Windows.WindowState.Normal;
                GetMainWindow().WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
                global::MasterThesis.Properties.Settings.Default.fullscreen = false;
            }
            global::MasterThesis.Properties.Settings.Default.Save();
        }

        public static void SetBackground(ImageSource image)
        {
            GetMainWindow().SetBackgroundImage(image);
        }
    }
}
