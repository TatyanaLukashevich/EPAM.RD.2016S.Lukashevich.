using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;

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
            UserRepository repo = new UserRepository();
            repo.Add(user);
            repo.Add(user2);
            repo.Add(user3);
            repo.WriteToXML();
            UserRepository repo1 = new UserRepository(new SlaveService());
            UserRepository repo2 = new UserRepository(new SlaveService());
        }
    }
}
