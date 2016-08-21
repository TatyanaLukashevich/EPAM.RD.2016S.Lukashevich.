using System.Configuration;
using ConfigLayer.CustomSectionConfigg;

namespace ConfigLayer.CustomSectionConfig
{
    /// <summary>
    /// Class inherited from ConfigurationSection to interact with custom section
    /// </summary>
    public class ReplicationSection : ConfigurationSection
    {
        /// <summary>
        /// To get collection of services
        /// </summary>
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
