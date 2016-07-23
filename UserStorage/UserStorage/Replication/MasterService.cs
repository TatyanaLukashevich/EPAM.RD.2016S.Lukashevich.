using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.Config;

namespace Replication
{
    public class MasterService :IService
    {
        #region Fields
        private static MasterService _instance;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private List<SlaveService> slaves = new List<SlaveService>();
        private List<User> users = new List<User>();
        public static MasterService GetInstance => _instance ?? (_instance = new MasterService());
        #endregion

        #region Constructors
        private MasterService()
        {
           
        }
        #endregion

        #region events
        public event EventHandler AddMethod;
        public event EventHandler DeleteMethod;
        #endregion

        #region Public methods
        public void Add(User user)
        {
            Logger.Info("Invoke add event");
            AddMethod?.Invoke(this, EventArgs.Empty);
            users.Add(user);
            foreach(var slave in slaves)
            {
                slave.Users.Add(user);
            }
        }

        public void Delete(User user)
        {
            Logger.Info("Invoke delete event");
            DeleteMethod?.Invoke(this, EventArgs.Empty);
            users.Remove(user);
            foreach (var slave in slaves)
            {
                slave.Users.Remove(user);
            }
        }

        public void FindByTag(Func<string, List<User>> methodTag, string tag)
        {
            var required =  users.FindAll(user => methodTag(tag).Contains(user));
        }

        public void RegisterRepository()
        {
            Logger.Info("Master Repository have been registered");
            //HasRepository = true;
        }

        public void RegisterSlave()
        {
            int amount = CheckAmountOfSlaves();
            for (int i = 0; i < amount; i++)
            {
                slaves.Add(new SlaveService());
            }
        }
        #endregion

        #region Private methods
        private int CheckAmountOfSlaves()
        {
            ReplicationSection section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
            int value = 0;
            for (int i = 0; i < section.ServicesItems.Count; i++)
            {
                if (section.ServicesItems[i].ServiceType.Contains("Slave"))
                    value++;
            }
            return value;
        }
        #endregion
    }
}
