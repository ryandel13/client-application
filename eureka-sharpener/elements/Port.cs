using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eureka_sharpener.elements
{
    public class Port
    {
        [JsonProperty(PropertyName = "$")]
        public int port { get; set; }

        [JsonProperty(PropertyName = "@enabled")]
        public Boolean enabled { get; set; }
    }
}
