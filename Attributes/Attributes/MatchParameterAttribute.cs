using System;

namespace Attributes
{
    // Should be applied to .ctors.
    // Matches a .ctor parameter with property. Needs to get a default value from property field, and use this value for calling .ctor.
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = true)]
    public class MatchParameterWithPropertyAttribute : Attribute
    {
        public string Value;
        public string Property;

        public MatchParameterWithPropertyAttribute(string v1, string v2)
        {
            Value = v1;
            Property = v2;
        }

        public override bool Match(object obj)
        {
            return base.Match(obj);
        }
    }
}
