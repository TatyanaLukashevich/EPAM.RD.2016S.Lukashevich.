using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage;

namespace UserStorage
{
    //Concreate strategy2
    public class SlaveService : IService
    {
        #region Private Fields
        private static int Counter { get; set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion
        private List<User> Users = new List<User>();
        public bool HasRepository { get; private set; }
        public SlaveService()
        {
            var value = Convert.ToInt32(ConfigurationManager.AppSettings["SlavesCount"]);
            if (Counter >= value)
            {
                Logger.Error("There is no way to create more than {0} instances of Slave class", value);
                throw new ArgumentException("There is no way to create more than {0} instances of Slave class",
                    value.ToString());
            }

            Counter++;

            MasterService.GetInstance.AddMethod += HandleAddEvent;
            MasterService.GetInstance.DeleteMethod += HandleDeleteEvent;
        }

        //public SlaveService(UserRepository repo)
        //{
        //    this.repo = repo;
        //}

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

        //public void AddMethodRespond()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteMethodRespond()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
