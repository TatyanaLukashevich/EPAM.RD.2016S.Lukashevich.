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
            UserRepository repo = new UserRepository();
           if (type.Equals("Master"))
            {
                return new MasterService(repo);
            }
            else
            {
                return new SlaveService(repo);
            }
        }
    }
}
