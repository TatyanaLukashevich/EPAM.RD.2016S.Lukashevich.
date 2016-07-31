using System.Configuration;
using ConfigLayer.CustomSectionConfigg;

namespace ConfigLayer.CustomSectionConfig
{
    [ConfigurationCollection(typeof(ServiceElement))]
    public class ReplicationSection : ConfigurationSection
    {
        [ConfigurationProperty("Services")]
        public ServiceCollection ServicesItems
        {
            get
            {
                return (ServiceCollection)(base["Services"]);
            }
        }
    }
}
