using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConfigLayer.AppDomainConfig;
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
            for (int i = 0; i < 10; i++)
            {
                var user = new User("Lily" + i, "Pad" + i, new DateTime(1960, 7, 20, 18, 30, 25), Gender.Male);
               
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

            RunSlaves(slaves);
            RunMaster(master);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            master.WriteToXML();
        }

        private static void RunMaster(MasterService master)
        {
            Random rand = new Random();

            ThreadStart masterSearch = () =>
            {
                while (true)
                {
                    var serachresult = master.FindByTag(u => u.Name != null);
                    Console.Write("Master search results: ");
                    foreach (var result in serachresult)
                    {
                        Console.Write(result + " ");
                    }
                       
                    Console.WriteLine();
                    Thread.Sleep(rand.Next(1000, 5000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<User>
                {
                    new User { Name = "Lily", LastName = "Pad" },
                    new User { Name = "Marsh", LastName = "Mellow" },
                };

                User userToDelete = null;

                while (true)
                {
                    foreach (var user in users)
                    {
                        int addChance = rand.Next(0, 3);
                        if (addChance == 0)
                        {
                            master.Add(user);
                        }                         

                        Thread.Sleep(rand.Next(1000, 5000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 3);
                            if (deleteChance == 0)
                            {
                                userToDelete = user;
                            }
                               
                            master.Delete(user);
                        }
                       
                        Thread.Sleep(rand.Next(1000, 5000));
                        Console.WriteLine();
                    }
                }
            };

            Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
            Thread masterAddThread = new Thread(masterAddDelete) { IsBackground = true };
            masterAddThread.Start();
            masterSearchThread.Start();
        }

        private static void RunSlaves(IEnumerable<SlaveService> slaves)
        {
            Random rand = new Random();

            foreach (var slave in slaves)
            {
                var slaveThread = new Thread(() =>
                {
                    while (true)
                    {
                        var userIds = slave.FindByTag(u => !string.IsNullOrWhiteSpace(u.Name));
                        Console.Write(" Slave search results: ");
                        foreach (var user in userIds)
                        {
                            Console.Write(user + " ");
                        }

                        Console.WriteLine();
                        Thread.Sleep((int)(rand.NextDouble() * 5000));
                    }
                });
                slaveThread.IsBackground = true;
                slaveThread.Start();
            }
        }
    }
}
