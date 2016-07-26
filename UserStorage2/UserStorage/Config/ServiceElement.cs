using System.Configuration;


namespace UserStorage.Config
{
    public class ServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("serviceType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServiceType
        {
            get { return ((string)(base["serviceType"])); }
            set { base["serviceType"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("ip", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string IpAddress
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = 0, IsKey = false, IsRequired = false)]
        public int Port
        {
            get { return (int)base["port"]; }
            set { base["port"] = value; }
        }
    }
}
