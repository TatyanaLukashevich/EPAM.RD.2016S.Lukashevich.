using System.Configuration;

namespace UserStorage.Config
{
    [ConfigurationCollection(typeof(ServiceElement))]
    public class ReplicationSection : ConfigurationSection
    {
        //[ConfigurationProperty("Services")]
        //public ServiceCollection ServicesItems
        //{
        //    get { return ((ServiceCollection)(base["Services"])); }
        //}

        [ConfigurationProperty("Services")]
        public ServiceCollection ServicesItems => ((ServiceCollection)(base["Services"]));

        public static ReplicationSection GetConfig()
        {
            return (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection") ?? new ReplicationSection();
        }
    }
}
