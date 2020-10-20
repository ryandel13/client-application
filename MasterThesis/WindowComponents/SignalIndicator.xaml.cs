using MasterThesis.ExchangeObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaktionslogik für SignalIndicator.xaml
    /// </summary>
    public partial class SignalIndicator : UserControl
    {

        public SignalIndicator()
        {
            InitializeComponent();

            Indicator.Text = "";
            Thread t = new Thread(UpdateThread);
            t.IsBackground = true;
            t.Start();
        }
    

        private void UpdateThread()
        {
            while (true)
            {
                try
                {
                    Bitmap bmp = global::MasterThesis.Properties.Resources.indicator_signal;
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(Properties.Settings.Default.remoteConnection);

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

                    UpdateSignal(bmp);
                }
                catch (Exception ex)
                {
                    //Expected exception
                }
                Thread.Sleep(500);
            }
        }

        private void UpdateSignal(Bitmap signalIcon)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Indicator.Background = new ImageBrush(BitmapHelper.getBitmapSourceFromBitmap(signalIcon));
            }));
           
        }
    }
}
