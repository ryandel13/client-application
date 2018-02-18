using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eureka_sharpener;
using eureka_sharpener.elements;

namespace eureka_sharpener_test
{
    [TestClass]
    public class EurekaTest
    {
        [TestMethod]
        public void TestGetRegistry()
        {
            Eureka eureka = new Eureka("localhost", 8762);
            Registry registry = eureka.ReadRegistry();

            Assert.IsNotNull(registry);
            Assert.IsNotNull(registry.applications);
            Assert.IsNotNull(registry.applications.application);
        }

        [TestMethod]
        public void TestRegistration()
        {
            String appName = "registertest";
            Eureka eureka = new Eureka("localhost", 8762);
            Assert.IsNotNull(eureka.Register(appName, 8888));

            Registry reg = eureka.ReadRegistry();
            Boolean found = false;
            foreach(ServiceWrapper a in reg.applications.application)
            {
                if (a.name.Equals(appName, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestUnregister()
        {
            String appName = "unregistertest";
            Eureka eureka = new Eureka("localhost", 8762);
            Instance instance = eureka.Register(appName, 8888);
            Registry reg = eureka.ReadRegistry();
            Boolean found = false;
            foreach (ServiceWrapper a in reg.applications.application)
            {
                if (a.name.Equals(appName, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = true;
                    break;
                }
            }
            System.Threading.Thread.Sleep(2000);
            Assert.IsTrue(found);
            Assert.IsTrue(eureka.Unregister(instance));
         
        }

        [TestMethod]
        public void TestHeartbeat()
        {
            String appName = "heartbeattest";
            Eureka eureka = new Eureka("localhost", 8762);
            eureka.renewalIntervalInSecs = 5;
            eureka.durationInSecs = 5;
            Assert.IsNotNull(eureka.Register(appName, 8888));
            System.Threading.Thread.Sleep(90000);
            Registry reg = eureka.ReadRegistry();
            Boolean found = false;
            foreach (ServiceWrapper a in reg.applications.application)
            {
                if (a.name.Equals(appName, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }
    }
}
