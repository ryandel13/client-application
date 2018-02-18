using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eureka_sharpener.elements
{
    public class Instance
    {
        public String instanceId { get; set; }

        public String hostName { get; set; }

        public String app { get; set; }

        public String ipAddr { get; set; }

        public String status { get; set; }

        public String overriddenstatus { get; set; }

        public Port port { get; set; }

        public Port securePort { get; set; }

        public String countryId { get; set; }

        public DataCenterInfo dataCenterInfo { get; set; }

        public LeaseInfo leaseInfo { get; set; }

        public MetaData metadata { get; set; }

        public String homePageUrl { get; set; }

        public String statusPageUrl { get; set; }

        public String healthCheckUrl { get; set; }

        public String vipAddress { get; set; }

        public String secureVipAddress { get; set; }

        public Boolean isCoordinatingDiscoveryServer { get; set; }

        [JsonIgnore]
        public Thread HeartbeatThread { get; set; }
    }
}
