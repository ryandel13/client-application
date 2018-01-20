using MasterThesis.WindowComponents.Views;
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
    /// Interaktionslogik für ViewMenu.xaml
    /// </summary>
    public partial class ViewMenu : UserControl
    {
        public ViewMenu()
        {
            InitializeComponent();
        }

        private void SensorBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(new TemperatureDiagram());
        }

        private void NaviBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).setViewPort(Navigation.getInstance());
        }
    }
}
