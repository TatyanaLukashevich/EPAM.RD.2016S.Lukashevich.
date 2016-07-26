using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using UserStorage;
using UserStorage.Config;
using UserStorage.Replication;

namespace Replication
{
    public class SlaveService : Service
    {
        #region Private Fields
        public AppDomain SlaveDomain { get; private set; }
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        public SlaveService(UserRepository repo) :base(repo)
        {
            MasterService.GetInstance.AddMethod += OnAdded;
            MasterService.GetInstance.DeleteMethod += OnDeleted;
        }
        #endregion

        #region Public metods
        public override void Add(User user)
        {
            throw new InvalidOperationException();
        }

        public override void Delete(User user)
        {
            throw new InvalidOperationException();
        }

        public void RegisterRepository()
        {
            Logger.Info("Slave Repository have been registered");
        }

        private void OnAdded(object sender, ChangedUserEventArgs args)
        {
            locker.EnterWriteLock();
            try
            {
                Debug.WriteLine("On Added! " + AppDomain.CurrentDomain.FriendlyName);
                Repo.Add(args.ChangedUser);
            }
            finally
            {
                locker.ExitWriteLock();
            }

        }

        private void OnDeleted(object sender, ChangedUserEventArgs args)
        {
            locker.EnterWriteLock();
            try
            {
                Repo.Delete(args.ChangedUser);
            }
            finally
            {
                locker.ExitWriteLock();
            }

        }
        #endregion

        #region Private methods
        //private void CheckAmountOfSlaves()
        //{
        //    ReplicationSection section = (ReplicationSection)ConfigurationManager.GetSection("ReplicationSection");
        //    int value = 0;
        //    for (int i = 0; i < section.ServicesItems.Count; i++)
        //    {
        //        if (section.ServicesItems[i].ServiceType.Contains("Slave"))
        //            value++;
        //    }

        //    if (Counter >= value)
        //    {
        //        Logger.Error("There is no way to create more than {0} instances of Slave class", value);
        //        throw new ArgumentException("There is no way to create more than {0} instances of Slave class",
        //            value.ToString());
        //    }
        //    Counter++;
        //}
        #endregion
    }
}
