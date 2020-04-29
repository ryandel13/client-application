using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.ExchangeObjects
{
    class SpotifyDevice
    {
        private String name;

        private String deviceId;

        public SpotifyDevice(string name, string deviceId)
        {
            this.name = name;
            this.deviceId = deviceId;
        }

        public string Name { get => name; set => name = value; }
        public string DeviceId { get => deviceId; set => deviceId = value; }

        public override string ToString()
        {
            return name;
        }
    }
}
