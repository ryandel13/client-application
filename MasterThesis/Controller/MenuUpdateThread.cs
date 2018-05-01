using eureka_sharpener;
using eureka_sharpener.elements;
using MasterThesis.WindowComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MasterThesis.Controller
{
    class MenuUpdateThread
    {
        public static void Start(ViewMenu instance, Dictionary<String, WindowComponents.MenuItem> services)
        {
            while (true)
            {
                try
                {
                    Eureka eureka = new Eureka("localhost", 8761);
                    Registry registry = eureka.ReadRegistry();
                    Boolean overrideFalse = false;
                    if(registry == null)
                    {
                        overrideFalse = true;
                    }
                    foreach(String serviceName in services.Keys)
                    {
                        Boolean state = false;
                        if (!overrideFalse)
                        {
                            Instance serviceInstance = registry.FindInstance(serviceName);
                            if (serviceInstance != null)
                            {
                                state = true;
                            }
                        }
                        instance.UpdateService(serviceName, state);
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(500);
            }
        }
    }
}
