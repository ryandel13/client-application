using eureka_sharpener.elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eureka_sharpener
{
    public class Eureka
    {

        private String eurekaHost;

        public int eurekaPort { get; set; }

        public int renewalIntervalInSecs { get; set; }

        public int durationInSecs { get; set; }
        public static String host { get; }

        public Eureka(String hostName, int port)
        {
            this.eurekaHost = hostName;
            this.eurekaPort = port;
        }

        public Instance Register(String appName, int hostport)
        {
            String hostname = System.Environment.MachineName;
            return this.Register(appName, hostport, hostname);
        }

        public Instance Register(String appName, int hostport, String hostname)
        {
            String uAppName = appName.ToUpper();
            String lAppName = appName.ToLower();

            Instance instance = new Instance();
            instance.hostName = hostname;
            instance.app = uAppName;
            instance.instanceId = hostname + ":" + lAppName + ":" + hostport;
            instance.ipAddr = System.Net.IPAddress.Any.ToString();
            instance.status = "UP";
            instance.overriddenstatus = "UNKNOWN";

            Port port = new Port();
            port.port = hostport;
            port.enabled = true;

            Port securePort = new Port();
            securePort.port = 443;
            securePort.enabled = false;

            instance.port = port;
            instance.securePort = securePort;

            instance.countryId = "1";

            DataCenterInfo dataCenterInfo = new DataCenterInfo();
            dataCenterInfo.className = "com.netflix.appinfo.InstanceInfo$DefaultDataCenterInfo";
            instance.dataCenterInfo = dataCenterInfo;

            instance.leaseInfo = new LeaseInfo();
            instance.leaseInfo.renewalIntervalInSecs = this.renewalIntervalInSecs;
            instance.leaseInfo.durationInSecs = this.durationInSecs;

            instance.homePageUrl = "http://" + hostname + ":" + port.port + "/";
            instance.statusPageUrl = instance.homePageUrl + "info";
            instance.healthCheckUrl = instance.homePageUrl + "health";

            instance.vipAddress = lAppName;
            instance.secureVipAddress = lAppName;
            instance.isCoordinatingDiscoveryServer = false;

            RegisterRequest wrapper = new RegisterRequest();
            wrapper.instance = instance;

            String json = JsonConvert.SerializeObject(wrapper);
            ResponseObject resp = RestRequest(uAppName + "/", "POST", json, eurekaHost, eurekaPort);
            if (resp.StatusCode != 200) {
                return null;
            }

            HeartbeatThread heartbeat = new HeartbeatThread();
            heartbeat.instance = instance;
            heartbeat.renewalInterval = renewalIntervalInSecs;
            heartbeat.eurekaPort = this.eurekaPort;
            heartbeat.eurekaHost = this.eurekaHost;
            Thread heartbeatThread = new Thread(new ThreadStart(heartbeat.HeartBeat));
            instance.HeartbeatThread = heartbeatThread;
            heartbeatThread.Start();
            return instance;
        }

        public Boolean Unregister(Instance instance)
        {
            Thread t = instance.HeartbeatThread;
            t.Abort();

            ResponseObject response = RestRequest(instance.app + "/" + instance.instanceId, "DELETE", null, eurekaHost, eurekaPort);
            if (response.StatusCode == 200)
            {
                return true;
            }
            else return false;
        }

        public Registry ReadRegistry()
        {
            ResponseObject registry = RestRequest("", "GET", null, eurekaHost, eurekaPort);
            if(registry.StatusCode == 200)
            {
                Registry reg = JsonConvert.DeserializeObject<Registry>(registry.ResponsePayload);
                return reg;
            }
            return null;
        }

        public Boolean TakeOut(Instance instance)
        {
            ResponseObject response = RestRequest(instance.app + "/" + instance.instanceId + "/status?value=OUT_OF_SERVICE", "PUT", null, eurekaHost, eurekaPort);
            if (response.StatusCode == 200)
            {
                return true;
            }
            return false;
        }
     

        private static ResponseObject RestRequest(String ressource, String method, String payload, String hostName, int port)
        {
            try
            {
                String requestUrl = "http://" + hostName + ":" + port + "/eureka/apps/" + ressource;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Accept = "application/json";
                request.Method = method;
                System.Net.ServicePointManager.Expect100Continue = false;

                if (payload != null)
                {
                    //Process payload content
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] byteArray = encoding.GetBytes(payload);
                    request.ContentType = "application/json";
                    request.ContentLength = byteArray.Length;
                    Stream newStream = request.GetRequestStream();
                    newStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                //object dataResponse = JsonConvert.DeserializeObject<List<POI>>(responseFromServer);
                response.Close();
                request.Abort();
                return new ResponseObject(200, responseFromServer);
            }
            catch (Exception ex)
            {
                return new ResponseObject(404, null);
            }
        }

        private class ResponseObject
        {
            public int StatusCode { get; set; }

            public String ResponsePayload { get; set; }

            public ResponseObject(int StatusCode, String ResponsePayload)
            {
                this.StatusCode = StatusCode;
                this.ResponsePayload = ResponsePayload;
            }
        }

        private class HeartbeatThread
        {
            public  String eurekaHost { get; set; }

            public int eurekaPort { get; set; }
            public Instance instance { get; set; }

            public int renewalInterval { get; set; } = 30;
            public void HeartBeat()
            {
                if (instance != null)
                {
                    while (true)
                    {
                        RestRequest(instance.app + "/" + instance.instanceId, "PUT", null, eurekaHost, eurekaPort);
                        System.Threading.Thread.Sleep(renewalInterval * 1000);
                    }
                }
            }
        }
    }
}
