using System;
using NLog;
using UserStorage;
using UserStorage.Replication;

namespace Replication
{
    public class MasterService : Service
    {
        #region Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static MasterService instance;

        public static MasterService GetInstance => instance ?? (instance = new MasterService(new UserRepository()));

        #endregion

        #region Constructors
        public MasterService(UserRepository repo) : base(repo)
        {
            Name = "Master";
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
        }

        public override void Delete(User user)
        {
            base.Delete(user);
        }

        protected override void NotifyAdd(User user)
        {
            Repo.Add(user);
            OnUserAdded(this, new ChangedUserEventArgs { ChangedUser = user });
        }

        protected override void NotifyDelete(User user)
        {
            Repo.Delete(user);
            OnUserDeleted(this, new ChangedUserEventArgs { ChangedUser = user });
        }
    }
    #endregion
}
