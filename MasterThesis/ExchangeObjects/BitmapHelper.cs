using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MasterThesis.ExchangeObjects
{
    class BitmapHelper
    {
        static public BitmapSource getBitmapSourceFromBitmap(Bitmap bmp)
        {

            BitmapSource returnSource = null;

            try
            {

                returnSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(),

                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            }

            catch { returnSource = null; }

            return returnSource;

        }
    }
}
