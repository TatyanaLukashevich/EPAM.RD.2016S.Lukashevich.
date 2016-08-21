using System.Configuration;
using ConfigLayer.CustomSectionConfigg;

namespace ConfigLayer.CustomSectionConfig
{
    /// <summary>
    /// Provides interaction with collection of element inside custom section
    /// </summary>
    [ConfigurationCollection(typeof(ServiceElement))]
    public class ServiceCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Get element by index
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public ServiceElement this[int idx]
        {
            get { return (ServiceElement)BaseGet(idx); }
        }


        /// <summary>
        /// Create new element in Service section
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        /// <summary>
        /// Get Service section element key
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)(element)).Path;
        }
    }
}
