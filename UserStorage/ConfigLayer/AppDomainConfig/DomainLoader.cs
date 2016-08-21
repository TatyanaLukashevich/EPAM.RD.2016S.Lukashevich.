using System;
using NLog;
using Replication;
using UserStorage;

namespace ConfigLayer.AppDomainConfig
{
    /// <summary>
    /// Utility class for creating services
    /// </summary>
    public class DomainLoader : MarshalByRefObject
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Create an instance of specific class
        /// </summary>
        /// <param name="type">String type of service from app.config</param>
        /// <returns>Concreate service</returns>
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
