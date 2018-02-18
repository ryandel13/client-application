using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eureka_sharpener.elements
{
    public class LeaseInfo
    {
        public int renewalIntervalInSecs { get; set; }

        public int durationInSecs { get; set; }

        public long registrationTimestamp { get; set; }

        public long lastRenewalTimestamp { get; set; }

        public long evictionTimestamp { get; set; }

        public long serviceUpTimestamp { get; set; }

        public LeaseInfo()
        {
            this.renewalIntervalInSecs = 30;
            this.durationInSecs = 90;
            this.registrationTimestamp = 0;
            this.lastRenewalTimestamp = 0;
            this.evictionTimestamp = 0;
            this.serviceUpTimestamp = 0;
        }
    }
}
