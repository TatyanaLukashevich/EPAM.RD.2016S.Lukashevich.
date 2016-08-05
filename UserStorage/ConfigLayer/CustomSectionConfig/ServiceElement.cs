﻿using System.Configuration;

namespace ConfigLayer.CustomSectionConfigg
{
    public class ServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("serviceType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ServiceType
        {
            get { return (string)(base["serviceType"]); }
            set { base["serviceType"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = false)]
        public string Path
        {
            get { return (string)(base["path"]); }
            set { base["path"] = value; }
        }

        [ConfigurationProperty("ip", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string IpAddress
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }

        [ConfigurationProperty("port", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Port
        {
            get { return (string)base["port"]; }
            set { base["port"] = value; }
        }
    }
}