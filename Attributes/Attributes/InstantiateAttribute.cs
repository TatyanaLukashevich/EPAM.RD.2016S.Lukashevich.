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
            Name = name;
            LastName = lastName;
        }
    }
}
