using System;
using NLog;
using Replication;
using UserStorage;

namespace ConfigLayer.AppDomainConfig
{
    public class DomainLoader : MarshalByRefObject
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Service CreateInstance(string type)
        {
           if (type.Equals("Master"))
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
