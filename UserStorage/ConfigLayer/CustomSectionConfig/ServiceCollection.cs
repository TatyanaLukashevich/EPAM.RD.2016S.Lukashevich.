using System.Configuration;
using ConfigLayer.CustomSectionConfigg;

namespace ConfigLayer.CustomSectionConfig
{
    public class ServiceCollection : ConfigurationElementCollection
    {
        public ServiceElement this[int idx]
        {
            get { return (ServiceElement)BaseGet(idx); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)(element)).Path;
        }
    }
}
