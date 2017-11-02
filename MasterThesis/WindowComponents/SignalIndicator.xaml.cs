using MasterThesis.ExchangeObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace MasterThesis.WindowComponents
{
    /// <summary>
    /// Interaktionslogik für SignalIndicator.xaml
    /// </summary>
    public partial class SignalIndicator : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public SignalIndicator()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(UpdateSignal);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
    

        private void UpdateSignal(object sender, EventArgs e)
        {
            Bitmap bmp = global::MasterThesis.Properties.Resources.indicator_signal;
            Indicator.Text = "";
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(Properties.Settings.Default.remoteConnection);

                 // reply.RoundtripTime.ToString();



                if (reply.RoundtripTime < 100)
                {
                    bmp = global::MasterThesis.Properties.Resources.indicator_signal_100;
                }
                else if (reply.RoundtripTime < 500)
                {
                    bmp = global::MasterThesis.Properties.Resources.indicator_signal_75;
                }
                else if (reply.RoundtripTime < 1000)
                {
                    bmp = global::MasterThesis.Properties.Resources.indicator_signal_50;
                }
                else
                {
                    bmp = global::MasterThesis.Properties.Resources.indicator_signal_0;
                }
            }catch(Exception ex)
            {
                //Expected exception
            }
                Indicator.Background = new ImageBrush(BitmapHelper.getBitmapSourceFromBitmap(bmp));
        }
    }
}
