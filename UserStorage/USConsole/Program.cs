using System;
using System.Linq;
using System.Threading;
using ConfigLayer.AppDomainConfig;
using ConfigLayer.WCF;
using Replication;
using UserStorage;
using UserStorage.Entities;

namespace UserStorageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = SystemCreater.CreateSystem();
            MasterService master = (MasterService)SystemCreater.Master;
             var slaves = SystemCreater.Services.Where(s => s is SlaveService).Select(s => (SlaveService)s);
            for (int i = 0; i < 2; i++)
            {
                var user = new User("Lily" + i, "Pad" + i, new DateTime(1990, 7, 20, 18, 30, 25), Gender.Female);
               
                master = (MasterService)SystemCreater.Master;
                master.Communicator = SystemCreater.MasterCommunicator;
                master.Add(user);

                Console.WriteLine(master.Repo.UserCollection.Count);
                foreach (var item in SystemCreater.Slaves)
                {
                    Console.WriteLine(item.Repo.UserCollection.Count);
                }

                Thread.Sleep(100);
            }

            foreach (var userService in services)
            {
                WcfCreator.CreateWcf(userService);
            }

            Console.ReadKey();
        }
    }
}
