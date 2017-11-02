using MasterThesis.WindowComponents.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
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

namespace MasterThesis
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();

            RESTInterface.RESTInterface restService = new RESTInterface.RESTImplementation();
            WebServiceHost __serviceHost = new WebServiceHost(restService, new Uri("http://localhost:8000/RESTInterface/"));
            __serviceHost.Open();

            instance = this;
        }

        public void setViewPort(UserControl userControl)
        {
            this.ViewPort.port.Children.Add(userControl);
        }

        public static MainWindow getInstance()
        {
            return instance;
        }

        public void openPopUp(String message)
        {
            //Window popup = new PopUpMessage();
            //popup.Show();
            this.message.Show(message);
        }

        public void SetBackground()
        {
            Brush brush = new SolidColorBrush(Colors.AliceBlue);
            this.Background = brush;
        }
    }
}
