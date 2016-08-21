using System.Configuration;

namespace ConfigLayer.CustomSectionConfigg
{
    /// <summary>
    /// Bind us with information in Service section
    /// </summary>
    public class ServiceElement : ConfigurationElement
    {
        /// <summary>
        /// Type of service
        /// </summary>
        [ConfigurationProperty("serviceType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServiceType
        {
            get { return (string)(base["serviceType"]); }
            set { base["serviceType"] = value; }
        }

        /// <summary>
        /// Path
        /// </summary>
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Path
        {
            get { return (string)(base["path"]); }
            set { base["path"] = value; }
        }

        /// <summary>
        /// IP address for each instance of service
        /// </summary>
        [ConfigurationProperty("ip", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string IpAddress
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }

        /// <summary>
        /// Service's port
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Port
        {
            get { return (string)base["port"]; }
            set { base["port"] = value; }
        }
    }
}
