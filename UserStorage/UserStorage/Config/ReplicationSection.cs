using System.Configuration;

namespace UserStorage.Config
{
    [ConfigurationCollection(typeof(ServiceElement))]
    public class ReplicationSection : ConfigurationSection
    {
        [ConfigurationProperty("Services")]
        public ServiceCollection ServicesItems
        {
            get { return ((ServiceCollection)(base["Services"])); }
        }
    }
}
