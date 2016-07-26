using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UserStorage;
using UserStorage.AppDomainConfig;
using UserStorage.Config;
using UserStorage.Replication;

namespace Replication
{
    public class MasterService : Service
    {
        #region Fields
        private static MasterService _instance;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static MasterService GetInstance => _instance ?? (_instance = new MasterService(new UserRepository()));
        #endregion

        #region events
        public event EventHandler<ChangedUserEventArgs> AddMethod;
        public event EventHandler<ChangedUserEventArgs> DeleteMethod;
        #endregion
        #region Constructors
        internal MasterService(UserRepository repo) : base(repo)
        {

        }
        #endregion

        #region Public methods

        public void RegisterRepository()
        {
            Logger.Info("Master Repository have been registered");
        }

        public override void Add(User user)
        {
            base.Add(user);
            //Communicator?.SendAdd(args);
            AddMethod?.Invoke(this, new ChangedUserEventArgs { ChangedUser = user });
        }

        public override void Delete(User user)
        {
            base.Delete(user);
            //Communicator?.SendAdd(args);
            DeleteMethod?.Invoke(this, new ChangedUserEventArgs { ChangedUser = user });
        }

    }
    #endregion
}

