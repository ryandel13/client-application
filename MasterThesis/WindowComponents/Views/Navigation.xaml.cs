using MasterThesis.ExchangeObjects;
using MasterThesis.RESTInterface;
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
    /// Interaktionslogik für Navigation.xaml
    /// </summary>
    public partial class Navigation : UserControl
    {
        private static Navigation instance;
        private Navigation()
        {
            InitializeComponent();

            this.MainGrid.Background = new ImageBrush(BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.navi_static));

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
            AddPOI(poi3);
        }

        internal void addPoiByGps(GpsResponse resp)
        {
            POI newPOI = new POI();
            newPOI.Title = resp.name;
            newPOI.XAxis = (int)resp.latitude;
            newPOI.YAxis = (int)resp.longitude;

            AddPOI(newPOI);
        }

        public static Navigation getInstance()
        {
            if(instance == null)
            {
                instance = new Navigation();
            }
            return instance;
        }

        private void AddPOI(POI poiInfo)
        {
            Image poi = new Image();
            poi.Source = BitmapHelper.getBitmapSourceFromBitmap(global::MasterThesis.Properties.Resources.navi_pin);
            poi.HorizontalAlignment = HorizontalAlignment.Left;
            poi.VerticalAlignment = VerticalAlignment.Top;
            poi.Height = 20;
            poi.Width = 20;
            poi.Margin = new Thickness(poiInfo.XAxis, poiInfo.YAxis, 0, 0);

            this.MainGrid.Children.Add(poi);
        }
    }
}
