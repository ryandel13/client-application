using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MasterThesis.WindowComponents
{

    /// <summary>
    /// Interaktionslogik für Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();

            Thread t = new Thread(UpdateClockThread);
            t.Start();
        }

        private void UpdateClock()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                DateTime date = DateTime.Now;
                this.ClockTime.Text = date.Hour.ToString("D2") + ":" + date.Minute.ToString("D2"); ;
            }));
        }

        private void UpdateClockThread()
        {
            while(true)
            {
                UpdateClock();
                Thread.Sleep(500);
            }
        }
    }
}
