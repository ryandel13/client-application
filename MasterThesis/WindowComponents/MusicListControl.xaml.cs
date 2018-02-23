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
    /// Interaktionslogik für MusicListControl.xaml
    /// </summary>
    public partial class MusicListControl : UserControl
    {
        public MusicListControl()
        {
            InitializeComponent();
            this.Icon.Source = BitmapHelper.getBitmapSourceFromBitmap((Bitmap)global::MasterThesis.Properties.Resources.menu_music);
        }
    }
}
