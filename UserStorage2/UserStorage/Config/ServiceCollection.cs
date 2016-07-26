using System.Configuration;

namespace UserStorage.Config
{
    public class ServiceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)(element)).ServiceType;
        }

        public ServiceElement this[int idx]
        {
            get { return (ServiceElement)BaseGet(idx); }
        }
    }
}
