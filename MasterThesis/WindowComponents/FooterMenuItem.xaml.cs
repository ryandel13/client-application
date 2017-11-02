using MasterThesis.ExchangeObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaktionslogik für FooterMenuItem.xaml
    /// </summary>
    public partial class FooterMenuItem : UserControl
    {
        public FooterMenuItem()
        {
            InitializeComponent();
        }

        private String bgImage;

        public string ItemTitle
        {
            get { return (string)Title.Text; }
            set { Title.Text = bgImage!=""?"":value; }
        }

        public string BackGroundImage
        {
            get { return bgImage; }
            set { SetBackgroundImage(value); }
        }

        private void SetBackgroundImage(String image)
        {
            Bitmap img = (Bitmap)global::MasterThesis.Properties.Resources.ResourceManager.GetObject(image);
            if (img != null)
            {
                this.Icon.Source = BitmapHelper.getBitmapSourceFromBitmap(img);
                this.Title.Text = "";
            } else
            {
                this.bgImage = "";
            }
        }
    }
}
