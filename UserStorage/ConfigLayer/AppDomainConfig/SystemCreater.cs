using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using ConfigLayer.CustomSectionConfig;
using NLog;
using Replication;
using UserStorage.NetworkCommunication;
using System.Net;
using System.Threading.Tasks;

namespace ConfigLayer.AppDomainConfig
{
    public static class SystemCreater
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static Service Master { get; set; }

        public static List<Service> Slaves { get; private set; }

        public static Communicator MasterCommunicator { get; set; }

        public static List<Communicator> SlaveCommunicators { get; set; }

        public static IEnumerable<Service> CreateSystem()
        {
            var services = new List<Service>();
            ReplicationSection section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
            Slaves = new List<Service>();
            //SlaveCommunicators = new List<Communicator>();
            List<IPEndPoint> ipEndPoints = new List<IPEndPoint>();
            for (int i = 0; i < section.ServicesItems.Count; i++)
            {
                var newAppDomain = AppDomain.CreateDomain(section.ServicesItems[i].Path);
                Logger.Info("Domain for {0} has been created", section.ServicesItems[i].ServiceType);
                var type = typeof(DomainLoader);
                var loader = (DomainLoader)newAppDomain.CreateInstanceAndUnwrap(Assembly.GetAssembly(type).FullName, type.FullName);

                var service = loader.CreateInstance(section.ServicesItems[i].ServiceType);

                if (section.ServicesItems[i].ServiceType.Equals("Slave"))
                {
                    Slaves.Add(service);
                    Receiver receiver = new Receiver(IPAddress.Parse(section.ServicesItems[i].IpAddress), Int32.Parse(section.ServicesItems[i].Port));
                    var communicator = new Communicator(receiver);
                    service.AddCommunicator(communicator);
                    receiver.AcceptConnection();
                    service.Communicator.RunReceiver();
                    ipEndPoints.Add(new IPEndPoint(IPAddress.Parse(section.ServicesItems[i].IpAddress), Int32.Parse(section.ServicesItems[i].Port)));
                    service.Repo.ReadFromXML();

                }
                else
                {
                    MasterCommunicator = new Communicator(new Sender());
                    Master = service;
                    MasterCommunicator.Connect(ipEndPoints);
                }
               
                services.Add(service);
            }

            
            return services;
        }
    }
}

