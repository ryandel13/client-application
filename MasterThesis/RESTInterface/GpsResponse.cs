using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.RESTInterface
{
    public class GpsResponse
    {
        public float longitude { get; set; }

        public float latitude { get; set; }

        public String marker { get; set; }

        public String name { get; set; }

        public String description { get; set; }
    }
}
