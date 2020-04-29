using MasterThesis.SPOTIFYInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace MasterThesis.RESTInterface
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,

                ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    class SpotifyAuthImpl : SPOTIFYInterface.SPOTIFYInterface
    {
        public bool updateToken(string accesToken)
        {
            throw new NotImplementedException();
        }
    }
}
