using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MasterThesis.SPOTIFYInterface
{
    [ServiceContract]
    public interface SPOTIFYInterface
    {
        [OperationContract]
        [WebGet(UriTemplate = "/{*url}")]
        Boolean updateToken(String url);

       /* [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = Routing.PutMessageRoute, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void pushMessage(Message message);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = Routing.PutGpsRoute, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void pushGps(GpsResponse message);*/
    }

    public static class Routing
    {
        public const string GetClientRoute = "/";

    }
}
