using eureka_sharpener;
using eureka_sharpener.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eureka_mirror
{
   
    class MirrorService
    {
        Eureka remote;

        Eureka local;

        static Registry registry;

        Instance cesLocal = null;
        public Boolean CloneServices()
        {
            System.Console.Out.WriteLine("Updating local registry");
     

            while (true)
            {
                

                remote = new Eureka("ryandel.selfhost.me", 8761);
                local = new Eureka("localhost", 8761);

                registry = remote.ReadRegistry();

                Instance ces = null;
                if (registry != null)
                {
                    ces = registry.FindInstance("music-streaming-service");
                }
                if (ces != null)
                {
                    if (cesLocal == null)
                    {
                        System.Console.Out.WriteLine("--> Adding " + ces.instanceId + " to local registry");
                        ces.hostName = "ryandel.selfhost.me";
                        cesLocal = local.Register(ces.app, ces.port.port, ces.hostName);
                    }
                }
                else
                {
                    if (cesLocal != null)
                    {
                        System.Console.Out.WriteLine("--> Pulling down " + cesLocal.instanceId + " on local registry");
                        local.TakeOut(cesLocal);
                        
                        System.Threading.Thread.Sleep(1000);
                        local.Unregister(cesLocal);
                        cesLocal = null;
                    }
                }

                System.Threading.Thread.Sleep(3000);
            }
            return true;
        }

        private static void StartInstance(object instance)
        {

        }

    }
}
