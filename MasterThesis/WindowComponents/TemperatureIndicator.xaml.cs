using MasterThesis.ExchangeObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaktionslogik für TemperatureIndicator.xaml
    /// </summary>
    public partial class TemperatureIndicator : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public TemperatureIndicator()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(UpdateTemperature);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void UpdateTemperature(object sender, EventArgs e)
        {
            try {
            String remoteUrl = global::MasterThesis.Properties.Settings.Default.remoteConnection;
            WebRequest request = WebRequest.Create("http://localhost:8801/vehicle/WP0ZZZ94427/temperature_inside/");
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            DataResponse dataResponse = JsonConvert.DeserializeObject<DataResponse>(responseFromServer);
            TemperatureText.Text = dataResponse.values.First().value + "°C";
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            }
            catch (Exception ex)
            {
                System.Console.Out.WriteLine("VDS:No data: " + ex.Message);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            }
        }
    }
}
