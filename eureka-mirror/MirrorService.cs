using eureka_sharpener;
using eureka_sharpener.elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eureka_mirror
{
   
    class MirrorService
    {
        Eureka remote;

        Eureka local;

        Instance cesLocal = null;
        public Boolean CloneServices()
        {
            System.Console.Out.WriteLine("Updating local registry");
            while (true)
            {
                

                remote = new Eureka("ryandel.selfhost.me", 8761);
                local = new Eureka("localhost", 8761);

                Registry registry = remote.ReadRegistry();

                Instance ces = null;
                if (registry != null)
                {
                    ces = registry.FindInstance("command-execution-service");
                }
                if (ces != null)
                {
                    if (cesLocal == null)
                    {
                        System.Console.Out.WriteLine("--> Adding " + ces.instanceId + " to local registry");
                        cesLocal = local.Register(ces.app, ces.port.port, ces.hostName);
                    }
                }
                else
                {
                    if (cesLocal != null)
                    {
                        System.Console.Out.WriteLine("--> Pulling down " + cesLocal.instanceId + " on local registry");
                        local.TakeOut(cesLocal);
                        cesLocal = null;
                    }
                }

                System.Threading.Thread.Sleep(3000);
            }
            return true;
        }

    }
}
