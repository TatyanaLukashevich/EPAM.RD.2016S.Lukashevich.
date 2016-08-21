using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using ConfigLayer.CustomSectionConfig;
using NLog;
using Replication;
using UserStorage.NetworkCommunication;

namespace ConfigLayer.AppDomainConfig
{
    /// <summary>
    /// Utility class for creating replication system (master/ slaves) in separate domains and securing settings for communication via network
    /// </summary>
    public static class SystemCreater
    {
        #region Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Autoproperties
        public static List<Service> Services { get; set; }

        public static Service Master { get; set; }

        public static List<Service> Slaves { get; private set; }

        public static Communicator MasterCommunicator { get; set; }
        #endregion

        /// <summary>
        /// Create replication system
        /// </summary>
        /// <returns>Returns enumeration of all created services(master and slaves)</returns>
        public static IEnumerable<Service> CreateSystem()
        {
            Services = new List<Service>();
            Slaves = new List<Service>();
            List<IPEndPoint> slavesIPEndPoints = new List<IPEndPoint>();

            var section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
            Receiver receiver = null;

            for (int i = 0; i < section.ServicesItems.Count; i++)
            {
                AppDomain newAppDomain = AppDomain.CreateDomain(section.ServicesItems[i].ServiceType);
                Logger.Info("Domain for {0} is created", section.ServicesItems[i].ServiceType);
                var type = typeof(DomainLoader);
                var loader = (DomainLoader)newAppDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainLoader).FullName);
                var service = loader.CreateInstance(section.ServicesItems[i].ServiceType);
                Services.Add(service);
                service.Name = section.ServicesItems[i].Path;
                if (section.ServicesItems[i].ServiceType.Contains("Slave"))
                {
                    try
                    {
                        Slaves.Add(service);
                        receiver = new Receiver(IPAddress.Parse(section.ServicesItems[i].IpAddress), int.Parse(section.ServicesItems[i].Port));
                        var communicator = new Communicator(receiver);
                        service.AddCommunicator(communicator);
                        Task task = receiver.AcceptConnection();
                        service.Communicator.RunReceiver();
                        slavesIPEndPoints.Add(new IPEndPoint(IPAddress.Parse(section.ServicesItems[i].IpAddress), int.Parse(section.ServicesItems[i].Port)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                    }
                }
                else
                {
                    MasterCommunicator = new Communicator(new Sender());
                    Master = service;
                    Master.AddCommunicator(MasterCommunicator);
                }         
            }

            MasterCommunicator.Connect(slavesIPEndPoints);

            foreach (var service in Slaves)
            {
                service.Communicator.RunReceiver();
            }

            return Services;
        }
    }
}
