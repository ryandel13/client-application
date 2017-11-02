using System;
using System.Collections.Generic;
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
                this.Visibility = Visibility.Visible;
                dispatcherTimer.Tick += new EventHandler(CloseWindow);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
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
