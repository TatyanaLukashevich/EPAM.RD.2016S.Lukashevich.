using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;

namespace UserStorage
{
    //Concreate strategy1
    public class MasterService :IService
    {
        #region Fields
        private static MasterService _instance;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private List<User> users = new List<User>();
        public static MasterService GetInstance => _instance ?? (_instance = new MasterService());
        #endregion

        #region events
        public event EventHandler AddMethod;
        public event EventHandler DeleteMethod;
        #endregion
       
        #region Constructors
        private MasterService() { }
        #endregion

        public bool HasRepository { get; private set; }

        public void Add(User user)
        {
            Logger.Info("Invoke add event");
            AddMethod?.Invoke(this, EventArgs.Empty);
            users.Add(user);
        }

        public void Delete(User user)
        {
            Logger.Info("Invoke delete event");
            DeleteMethod?.Invoke(this, EventArgs.Empty);
            users.Remove(user);
        }

        public void FindByTag(Func<string, List<User>> methodTag, string tag)
        {
            var required =  users.FindAll(user => methodTag(tag).Contains(user));
        }

        public void RegisterRepository()
        {
            Logger.Info("Master Repository have been registered");
            HasRepository = true;
        }
    }
}
