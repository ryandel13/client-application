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

namespace MasterThesis.WindowComponents.Views
{
    /// <summary>
    /// Interaktionslogik für Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        private static Navigation instance;

        public static Navigation getInstance()
        {
            if(instance == null)
            {
                instance = new Navigation();
            }
            return instance;
        }
        private Navigation()
        {
            InitializeComponent();

            this.MainGrid.Background = new ImageBrush(BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.navi_static));
            /*
            POI poi1 = new POI();
            poi1.Title = "Schloss";
            poi1.XAxis = 20;
            poi1.YAxis = 60;

            POI poi2 = new POI();
            poi2.Title = "BlaBla";
            poi2.XAxis = 320;
            poi2.YAxis = 260;

            POI poi3 = new POI();
            poi3.Title = "BlaBla";
            poi3.XAxis = 200;
            poi3.YAxis = 10;

            AddPOI(poi1);
            AddPOI(poi2);
            AddPOI(poi3);*/

            retrievePOI();
        }
        internal void addPoiByGps(RESTInterface.GpsResponse resp)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                POI newPOI = new POI();
                newPOI.title = resp.name;
                newPOI.latitude = (int)resp.latitude;
                newPOI.longitude = (int)resp.longitude;
                newPOI.marker = resp.marker;

                AddPOI(newPOI);
            }));

            
        }
        private void AddPOI(POI poiInfo)
        {
            Image poi = new Image();
            if (poiInfo.marker.Equals("remote"))
            {
                poi.Source = BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.navi_pin_remote);
            } else
            {
                poi.Source = BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.navi_pin);
            }
            poi.HorizontalAlignment = HorizontalAlignment.Left;
            poi.VerticalAlignment = VerticalAlignment.Top;
            poi.Height = 20;
            poi.Width = 20;
            poi.Margin = new Thickness(poiInfo.longitude, poiInfo.latitude, 0, 0);

            this.MainGrid.Children.Add(poi);
        }

        private void retrievePOI()
        {
            List<POI> dataResponse = null;
            try
            {
                String remoteUrl = global::MasterThesis.Properties.Settings.Default.remoteConnection;
                WebRequest request = WebRequest.Create(RemoteUrlBuilder.getUriFor(RemoteUrlBuilder.SERVICE.POI, "poi", "?latitude=0&longitude=0&cached=true&callback=true", true).ToString());
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                dataResponse = JsonConvert.DeserializeObject<List<POI>>(responseFromServer);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Error occured during webrequest");
            }
            if(dataResponse != null) {
                foreach (POI p in dataResponse)
                {
                    this.AddPOI(p);
                }
            }
        }
    }
}
