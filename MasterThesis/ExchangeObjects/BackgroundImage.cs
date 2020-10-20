using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MasterThesis.ExchangeObjects
{
    class BackgroundImage
    {
        private String name;

        private ImageSource image;

        public BackgroundImage(string name, ImageSource image)
        {
            this.name = name;
            this.image = image;
        }

        public string Name { get => name; set => name = value; }
        public ImageSource Image { get => image; set => image = value; }

        public override string ToString()
        {
            return name;
        }
    }
}
