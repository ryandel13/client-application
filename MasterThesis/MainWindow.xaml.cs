using eureka_sharpener;
using eureka_sharpener.elements;
using MasterThesis.Controller;
using MasterThesis.ExchangeObjects;
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

        private Eureka eureka;

        private Instance eurekaInstance;

        private UserControl activeControl = null;
        public MainWindow()
        {
            InitializeComponent();

            RESTInterface.RESTInterface restService = new RESTInterface.RESTImplementation();
            WebServiceHost __serviceHost = new WebServiceHost(restService, new Uri("http://localhost:8000/ui/"));
            __serviceHost.Open();

            eureka = new Eureka("localhost", 8761);
            eurekaInstance = eureka.Register("ui-service-app", 8000);

            Application.Current.Exit += new ExitEventHandler(this.OnApplicationExit);

            instance = this;

            //ImageBrush backgroundBrush = new ImageBrush(BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.background_3));

            //MainPanel.Background = backgroundBrush;
        }

        internal void SetBackgroundImage(ImageSource image)
        {
            ImageBrush backgroundBrush = new ImageBrush(image);
            MainPanel.Background = backgroundBrush;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void setViewPort(UserControl userControl)
        {
            if (activeControl != null)
            {
                this.ViewPort.port.Children.Remove(activeControl);
            }
            this.ViewPort.port.Children.Add(userControl);
            activeControl = userControl;
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

        private void OnApplicationExit(object sender, EventArgs e)
        {
            eureka.Unregister(this.eurekaInstance);
        }
    }
}
