using NLog;
using Replication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Config;

namespace UserStorage.AppDomainConfig
{
   public static class SystemCreater
    {
        public static Service Master { get; set; }
        public static List<Service> Slaves { get; private set; } 
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static void CreateSystem()
        {
            ReplicationSection section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
            Slaves = new List<Service>();
            for (int i = 0; i < section.ServicesItems.Count; i++)
            {
                var newAppDomain = AppDomain.CreateDomain(section.ServicesItems[i].Path);
                Logger.Info("Domain for {0} has been created", section.ServicesItems[i].ServiceType);
                var type = typeof(DomainLoader);
                var loader = (DomainLoader)newAppDomain.CreateInstanceAndUnwrap(Assembly.GetAssembly(type).FullName, type.FullName);
                 
                var service = loader.CreateInstance(section.ServicesItems[i].ServiceType);

                if (section.ServicesItems[i].ServiceType.Contains("Slave"))
                {
                    Slaves.Add(service);
                }
                else
                {
                   Master = service;
                }
            }
        }
    }
}
