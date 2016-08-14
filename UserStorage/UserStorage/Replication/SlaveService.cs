using System;
using System.Threading.Tasks;
using NLog;
using UserStorage;
using UserStorage.NetworkCommunication;
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

        public override void WriteToXML()
        {
            throw new InvalidOperationException();
        }

        public override void ReadFromXML()
        {
            throw new InvalidOperationException();
        }

        public void RegisterRepository()
        {
            Logger.Info("Slave Repository have been registered");
        }

        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            Communicator.UserAdded += OnAdded;
            Communicator.UserDeleted += OnDeleted;
        }

        protected override void NotifyAdd(User user)
        {
            Task.Run(() => OnUserAdded(this, new ChangedUserEventArgs() { ChangedUser = user }));
        }

        protected override void NotifyDelete(User user)
        {
            throw new InvalidOperationException();
        }

        private void OnAdded(object sender, ChangedUserEventArgs args)
        {
            locker.EnterWriteLock();
            try
            {
                Repo.Add(args.ChangedUser);
                Logger.Info("Collection of users for slave was updated. Added new user.");
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
                Logger.Info("Collection of users for slave was updated. User was deleted.");
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
    }
    #endregion
}