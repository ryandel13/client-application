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

namespace MasterThesis.WindowComponents
{

    /// <summary>
    /// Interaktionslogik für Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        

        public Clock()
        {
            dispatcherTimer.Tick += new EventHandler(UpdateClock);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            InitializeComponent();

            
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            this.ClockTime.Text = date.Hour.ToString("D2") + ":" + date.Minute.ToString("D2"); ;
        }
    }
}
