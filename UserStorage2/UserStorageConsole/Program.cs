using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.AppDomainConfig;

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
            //UserRepository repo = new UserRepository();
            SystemCreater.CreateSystem();
           
            SystemCreater.Master.Add(user);
            SystemCreater.Master.Add(user2);
            SystemCreater.Master.Add(user3);
            SystemCreater.Master.Repo.WriteToXML();
            
            //repo.Add(user);
            //repo.Add(user2);
            //repo.Add(user3);
            //repo.WriteToXML();
        }
    }
}
