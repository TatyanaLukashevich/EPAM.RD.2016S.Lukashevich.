using Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

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

        public List<User> CreateUsers()
        {
            var users = new List<User>();
            var attributes = typeof(User).GetCustomAttributes<InstantiateUserAttribute>();
                foreach (var attribute in attributes)
                {

                    attribute.Id = MatchParameter(typeof(User), "id");
                    var ctor = typeof(User).GetConstructor(new[] { typeof(int) });
                    if (ctor != null)
                    {
                        var user = (User)ctor.Invoke(new object[] { attribute.Id}); //??
                        user.FirstName = attribute.Name;
                        user.LastName = attribute.LastName;
                        users.Add(user);
                    }
                }
            return users;
        }

        private int MatchParameter(Type type, string paramName)
        {
            var ctors = type.GetConstructors();
            var ctorWithAttribute =
                ctors.FirstOrDefault(ctor => ctor.GetCustomAttributes<MatchParameterWithPropertyAttribute>() != null);
            var attribute =
                    ctorWithAttribute?.GetCustomAttributes<MatchParameterWithPropertyAttribute>()
                    .FirstOrDefault(attr => attr.Property == paramName);
            if (attribute == null)
            {
                throw new InvalidOperationException();
            }
            return (int)type.GetProperties().FirstOrDefault(prop => prop.Name == attribute.Property)
                .GetCustomAttribute<DefaultValueAttribute>().Value;
        }
    }
}
