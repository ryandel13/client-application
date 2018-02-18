using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eureka_sharpener.elements
{
    public class DataCenterInfo
    {
        [JsonProperty(PropertyName = "@class")]
        public String className { get; set; }

        public String name = "MyOwn";
    }
}
