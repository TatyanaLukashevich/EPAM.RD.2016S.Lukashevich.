using NLog;
using Replication;
using System;
using System.Configuration;
using UserStorage.Config;

namespace UserStorage.AppDomainConfig
{
    public class DomainLoader :MarshalByRefObject
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Service CreateInstance(string type)
        {
           if(type.Equals("Master"))
            {
                return new MasterService(new UserRepository());
            }
            else
            {
                return new SlaveService(new UserRepository());
            }
        }
    }
}
