using System;

namespace Attributes
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class StringValidatorAttribute : Attribute
    {
        private readonly int length;
        public StringValidatorAttribute(int leng)
        {
            length = leng;
        }

    }
}
