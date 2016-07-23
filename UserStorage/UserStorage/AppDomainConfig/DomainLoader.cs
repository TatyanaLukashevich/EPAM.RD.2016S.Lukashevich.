using NLog;
using Replication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Config;

namespace UserStorage.AppDomainConfig
{
    public static class DomainLoader
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static void CreateDomain()
        {
            ReplicationSection section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
            for (int i = 0; i < section.ServicesItems.Count; i++)
            {
                var newAppDomain = AppDomain.CreateDomain(section.ServicesItems[i].Path);
                Logger.Info("Domain for {0} has been created", section.ServicesItems[i].ServiceType);
            }
        }
    }
}
