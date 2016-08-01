using System;
using System.Collections.Generic;
using System.Linq;
using ConfigLayer.AppDomainConfig;
using UserStorage;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UserStorage.NetworkCommunication;
using System.Threading;
using Replication;

namespace UserStorageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User("Lily", "Pod", new DateTime(1996, 2, 17), 0);
            User user2 = new User("Tracy", "Jordan", new DateTime(1996, 1, 18), 0);
            User user3 = new User("Liz", "Lemon", new DateTime(1995, 9, 9), 0);
            List<User> users = new List<User>();
            ////UserRepository repo = new UserRepository();

            var services= SystemCreater.CreateSystem().ToList();

            SystemCreater.Master.Add(user);
            SystemCreater.Master.Add(user2);
            SystemCreater.Master.Add(user3);
            SystemCreater.Master.WriteToXML();
            SystemCreater.Master.ReadFromXML();
            foreach (var slave in SystemCreater.Slaves)
            {
                Console.WriteLine(slave.Repo.UserCollection.Count); 
            }
        }
    }
}
