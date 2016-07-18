using System;
using System.ComponentModel;

namespace Attributes
{
    // Should be applied to classes only.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InstantiateUserAttribute : Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public InstantiateUserAttribute(int id, string name, string lastName)
        {
            Id = id;
            Name = name;
            LastName = lastName;
        }

        public InstantiateUserAttribute(string name, string lastName)
        {
            Type userclass = typeof(User);
            MatchParameterWithPropertyAttribute[] matchParameter =
                (MatchParameterWithPropertyAttribute[])Attribute.GetCustomAttributes(userclass.GetConstructors()[0], typeof(MatchParameterWithPropertyAttribute));

            var proper = userclass.GetProperty(matchParameter[0].Property);
            DefaultValueAttribute[] propertyAttributes =
                (DefaultValueAttribute[])Attribute.GetCustomAttributes(proper, typeof(DefaultValueAttribute));

            
            Id = (int)propertyAttributes[0].Value;
            Name = name;
            LastName = lastName;
        }
    }
}
