using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.ExchangeObjects
{
    class RemoteUrlBuilder
    {
        private static RemoteUrlBuilder instance;

        private static RemoteUrlBuilder getInstance()
        {
            if(instance == null)
            {
                instance = new RemoteUrlBuilder();
            }
            return instance;
        }

        private RemoteUrlBuilder()
        {
            ports.Add(SERVICE.VDS, Int32.Parse(global::MasterThesis.Properties.Settings.Default.vdsPort));
            ports.Add(SERVICE.SDS, Int32.Parse(global::MasterThesis.Properties.Settings.Default.sdsPort));
            ports.Add(SERVICE.CES, Int32.Parse(global::MasterThesis.Properties.Settings.Default.cesPort));
            ports.Add(SERVICE.POI, Int32.Parse(global::MasterThesis.Properties.Settings.Default.poiPort));
            ports.Add(SERVICE.MSS, Int32.Parse(global::MasterThesis.Properties.Settings.Default.mssPort));
        }
        private IDictionary<SERVICE, int> ports = new Dictionary<SERVICE, int>();

        public enum SERVICE {
            VDS, SDS, CES, POI, MSS
        }
        public static Uri getUriFor(SERVICE service, String context, String ressource)
        {
            return getUriFor(service, context, ressource, false);
        }

        public static Uri getUriFor(SERVICE service, String context, String ressource, Boolean local)
        {
            int port = 0;
            getInstance().ports.TryGetValue(service, out port);
            String vin = global::MasterThesis.Properties.Settings.Default.vin;
            String remote;
            if(local)
            {
                remote = "localhost";
            } else
            {
                remote = global::MasterThesis.Properties.Settings.Default.remoteConnection;
            }
            Uri url = new Uri("http://" + remote + ":" + port + "/" + context + "/" + vin + "/" + ressource);

            return url;
        }
    }
}
