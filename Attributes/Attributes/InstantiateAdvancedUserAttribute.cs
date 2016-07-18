using System;

namespace Attributes
{
    // Should be applied to assembly only.
    [AttributeUsage(AttributeTargets.Assembly)]
    public class InstantiateAdvancedUserAttribute : InstantiateUserAttribute
    {
        public int Number { get; set; }

        public InstantiateAdvancedUserAttribute(int id, string name, string lastName, int number):base(id, name, lastName)
        {
            Number = number;
        }
    }
}
