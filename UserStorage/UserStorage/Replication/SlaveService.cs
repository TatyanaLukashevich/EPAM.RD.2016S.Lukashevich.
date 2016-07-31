using System;
using System.Diagnostics;
using NLog;
using UserStorage;
using UserStorage.Replication;

namespace Replication
{
    public class SlaveService : Service
    {
        #region Private Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AppDomain SlaveDomain { get; private set; }
        #endregion

        #region Constructors
        public SlaveService(UserRepository repo) : base(repo)
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
    }
}