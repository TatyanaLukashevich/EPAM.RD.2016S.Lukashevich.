using System;

namespace Attributes
{
    // Should be applied to properties and fields.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IntValidatorAttribute : Attribute
    {
        private readonly int min;
        private readonly int max;

        public IntValidatorAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

    }
}
