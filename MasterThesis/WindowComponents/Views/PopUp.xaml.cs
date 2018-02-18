using MasterThesis.ExchangeObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

namespace MasterThesis.WindowComponents.Views
{
    /// <summary>
    /// Interaktionslogik für PopUp.xaml
    /// </summary>
    public partial class PopUp : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public PopUp()
        {
            InitializeComponent();
        }

        public void Show(String message)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                content.Content = message;

                if(message.Equals("LOCK", StringComparison.InvariantCultureIgnoreCase))
                {

                    Bitmap img = (Bitmap)global::MasterThesis.Properties.Resources.ResourceManager.GetObject("car_doors_closed");
                    if (img != null)
                    {
                        this.picture.Source = BitmapHelper.getBitmapSourceFromBitmap(img);           
                    }
                    content.Content = "Car has been locked";
                } else if (message.Equals("LOCK", StringComparison.InvariantCultureIgnoreCase))
                {

                    Bitmap img = (Bitmap)global::MasterThesis.Properties.Resources.ResourceManager.GetObject("car_doors_opened");
                    if (img != null)
                    {
                        this.picture.Source = BitmapHelper.getBitmapSourceFromBitmap(img);
                    }
                    content.Content = "Car has been opened";
                }

                this.Visibility = Visibility.Visible;
                dispatcherTimer.Tick += new EventHandler(CloseWindow);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
                dispatcherTimer.Start();
            }));
           }

        private void CloseWindow(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            this.Visibility = Visibility.Hidden;
        }
    }
}
