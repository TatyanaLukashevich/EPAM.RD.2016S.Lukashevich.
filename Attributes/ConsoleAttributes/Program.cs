using Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAttributes
{
    class Program
    {
        static void Main(string[] args)
        {

            Type userclass = typeof(User);

            InstantiateUserAttribute[] instantiateUserAttributes =
                (InstantiateUserAttribute[])Attribute.GetCustomAttributes(userclass, typeof(InstantiateUserAttribute));


            User[] users = new User[3];
            for (int i = 0; i < 3; i++)
            {
                users[i] = new User(instantiateUserAttributes[i].Id);
                users[i].FirstName = instantiateUserAttributes[i].Name;
                users[i].LastName = instantiateUserAttributes[i].LastName;
                Console.WriteLine(users[i].Id.ToString() + ' ' + users[i].FirstName + ' ' + users[i].LastName);
            }


        }
    }
}
