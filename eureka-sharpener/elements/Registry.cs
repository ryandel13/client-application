using System;
using System.Collections.Generic;

namespace eureka_sharpener.elements
{
    public class Registry
    {
       public  Applications applications { get; set; }

        public Instance FindInstance(String serviceName)
        {
            Instance instance = null;
            foreach (ServiceWrapper sw in applications.application)
            {
                if(sw.name.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sw.instance[0].status.Equals("UP", StringComparison.InvariantCultureIgnoreCase))
                    {
                        instance = sw.instance[0];
                    }
                }
            }
            return instance;
    }
    }

    public class Applications
    {
        public int versions_delta { get; set; }

        public String apps_hashcode { get; set; }

        public List<ServiceWrapper> application { get; set; }
    }

    public class ServiceWrapper
    {
        public String name { get; set; }

        public List<Instance> instance { get; set; }
    }
}
