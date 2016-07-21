﻿using System.Configuration;


namespace UserStorage.Config
{
    public class ServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("serviceType", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string ServiceType
        {
            get { return ((string)(base["serviceType"])); }
            set { base["serviceType"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }
}
