using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.Config;

namespace UserStorage
{
    public class SlaveService : IService
    {
        #region Private Fields
        public AppDomain SlaveDomain { get; private set; }
        private static int Counter { get; set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private List<User> Users = new List<User>();
        public bool HasRepository { get; private set; }
        #endregion

        #region Constructors
        public SlaveService()
        {
            CheckAmountOfSlaves();
            MasterService.GetInstance.AddMethod += HandleAddEvent;
            MasterService.GetInstance.DeleteMethod += HandleDeleteEvent;
        }
        #endregion

        #region Public metods
        public void Add(User user)
        {
            throw new InvalidOperationException();
        }

        public void Delete(User user)
        {
            throw new InvalidOperationException();
        }

        public void FindByTag(Func<string, List<User>> methodTag, string tag)
        {
            Logger.Info("Search user by predicate");
            var required = Users.FindAll(user => methodTag(tag).Contains(user));
        }

        public void RegisterRepository()
        {
            Logger.Info("Slave Repository have been registered");
            HasRepository = true;
        }

        public void HandleAddEvent(object sender, EventArgs args)
        {
            Logger.Info("HandleAddEvent called");
            Debug.WriteLine("Add method notification");
        }

        public void HandleDeleteEvent(object sender, EventArgs args)
        {
            Logger.Info("HandleDeleteEvent called");
            Debug.WriteLine("Delete method notification");
        }
        #endregion

        #region Private methods
        private void CheckAmountOfSlaves()
        {
            ReplicationSection section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
            int value = 0;
            for (int i = 0; i < section.ServicesItems.Count; i++)
            {
                if (section.ServicesItems[i].ServiceType.Contains("Slave"))
                    value++;
            }

            if (Counter >= value)
            {
                Logger.Error("There is no way to create more than {0} instances of Slave class", value);
                throw new ArgumentException("There is no way to create more than {0} instances of Slave class",
                    value.ToString());
            }
            Counter++;
        }
        #endregion
    }
}
