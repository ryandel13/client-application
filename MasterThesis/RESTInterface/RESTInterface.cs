using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MasterThesis.RESTInterface
{
    [ServiceContract(Name = "MasterREST")]
    public interface RESTInterface
    {
        [OperationContract]
        [WebGet(UriTemplate = Routing.GetClientRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        Boolean pushMessageAsGet(String id);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = Routing.PutMessageRoute, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void pushMessage(Message message);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = Routing.PutGpsRoute, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void pushGps(GpsResponse message);
    }

    public static class Routing
    {
        public const string GetClientRoute = "/Client/{id}";

        public const string PutMessageRoute = "/push";

        public const string PutGpsRoute = "/gps";
    }
}
