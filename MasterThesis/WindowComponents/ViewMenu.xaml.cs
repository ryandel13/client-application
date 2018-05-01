using eureka_sharpener;
using eureka_sharpener.elements;
using MasterThesis.Controller;
using MasterThesis.WindowComponents.Views;
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
    /// Interaktionslogik für ViewMenu.xaml
    /// </summary>
    public partial class ViewMenu : UserControl
    {
        Dictionary<String, MenuItem> menuItems = new Dictionary<String, MenuItem>();


        public ViewMenu()
        {
            InitializeComponent();

            menuItems.Add("poi-service", NaviBtn);
            menuItems.Add("music-streaming-service", MusicBtn);
            menuItems.Add("variable-data-service", SensorBtn);

            Thread t = new Thread(() => MenuUpdateThread.Start(this, menuItems));
            t.Start();
        }

        public void UpdateService(String serviceName, Boolean state)
        {
            MenuItem button;
            if(menuItems.TryGetValue(serviceName, out button))
            {
                button.Enabled(state);
            }
        }

        private void SensorBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(new TemperatureDiagram());
        }

        private void NaviBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(Navigation.getInstance());
        }

        private void MusicBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(MusicView.getInstance());
        }

        private void HomeBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(MainView.getInstance());
        }
        private void SpotifyBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(SpotifyView.getInstance());
        }
    }
}
