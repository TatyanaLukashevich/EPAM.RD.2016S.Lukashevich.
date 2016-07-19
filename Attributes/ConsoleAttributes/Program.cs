using Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            MatchParameterWithPropertyAttribute[] matchParameter =
               (MatchParameterWithPropertyAttribute[])Attribute.GetCustomAttributes(userclass.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));

            var proper = userclass.GetProperty(matchParameter[0].Property);
            DefaultValueAttribute[] propertyAttributes =
                (DefaultValueAttribute[])Attribute.GetCustomAttributes(proper, typeof(DefaultValueAttribute));


            User[] users = new User[3];
            for (int i = 0; i < 3; i++)
            {

                users[i] = new User(instantiateUserAttributes[i].Id);
                if (users[i].Id == 0)
                {
                    users[i].Id = (int)propertyAttributes[0].Value;
                }
                users[i].FirstName = instantiateUserAttributes[i].Name;
                users[i].LastName = instantiateUserAttributes[i].LastName;
                Console.WriteLine(users[i].Id.ToString() + ' ' + users[i].FirstName + ' ' + users[i].LastName);
            }
        }
    }
}
