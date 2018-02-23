using MasterThesis.ExchangeObjects;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace MasterThesis.WindowComponents.Views
{
    /// <summary>
    /// Interaktionslogik für TemperatureDiagram.xaml
    /// </summary>
    public partial class TemperatureDiagram : UserControl
    {
        Random rnd = new Random();
        ObservableCollection<LineSeries> MyData = new ObservableCollection<LineSeries>();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        LineSeries SeriesTemperatureInside = new LineSeries();

        public TemperatureDiagram()
        {
            InitializeComponent();

            MyChart.ItemsSource = MyData;

            //DrawGraphFromJson(SeriesTemperatureInside);
            MyData.Add(SeriesTemperatureInside);

            dispatcherTimer.Tick += new EventHandler(UpdateDiagram);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void UpdateDiagram(object sender, EventArgs e)
        {
            DrawGraphFromJson(SeriesTemperatureInside);
        }

        private void DrawGraph()
        {
            LineSeries MySeries = new LineSeries();

            for (int i = 0; i < 12; i++)
            {
                MySeries.MyData.Add(new DataPoint() { Frequency = (double)i, Value = rnd.Next(60) });
            }

            MyData.Add(MySeries);
        }

        private void DrawGraphFromJson(LineSeries SeriesToUpdate)
        {
            DataResponse dataResponse = null;
            try
            {
                String remoteUrl = global::MasterThesis.Properties.Settings.Default.remoteConnection;
                WebRequest request = WebRequest.Create(RemoteUrlBuilder.getUriFor(RemoteUrlBuilder.SERVICE.VDS, "vehicle", "temperature_inside/history", true).ToString());
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                dataResponse = JsonConvert.DeserializeObject<DataResponse>(responseFromServer);
            } catch(Exception e)
            {
                System.Console.Out.WriteLine("Error occured during webrequest");
            }
            int i = 0;
            if (dataResponse != null)
            {
                SeriesToUpdate.MyData.Clear();
                foreach (ResponseEntity item in dataResponse.values)
                {
                    SeriesToUpdate.MyData.Add(new DataPoint() { Frequency = (double)i++, Value = double.Parse(item.value, CultureInfo.InvariantCulture) });
                }
                if (MyData.Contains(SeriesToUpdate))
                {
                    MyData.Remove(SeriesToUpdate);
                }
                MyData.Clear();
                MyData.Add(SeriesToUpdate);
            } else
            {
                //TODO: Message that connection was not successful
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawGraphFromJson(SeriesTemperatureInside);
        }
    }
}
