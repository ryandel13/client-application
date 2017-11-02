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
    class RESTImplementation : RESTInterface
    {
        public bool pushMessageAsGet(string Id)
        {
            
            return true;
        }

        public void pushMessage(Message message)
        {
            CommandMessage cmdMessage = new CommandMessage(message.message);

            Task task = new Task( () => MainWindow.getInstance().openPopUp(cmdMessage.GetCommand()));
            task.Start();
        }
    }
}
