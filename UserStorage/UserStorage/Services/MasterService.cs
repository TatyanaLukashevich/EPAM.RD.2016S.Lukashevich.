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
        #endregion

        #region Constructors
        public MasterService(UserRepository repo) : base(repo)
        {
        }
        #endregion

        public static MasterService GetInstance => instance ?? (instance = new MasterService(new UserRepository()));

        #region Public methods
        public void RegisterRepository()
        {
            Logger.Info("Master Repository have been registered");
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override int Add(User user)
        {
           return base.Add(user);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        public override void Delete(User user)
        {
            base.Delete(user);
        }

        /// <summary>
        /// Notify user added
        /// </summary>
        /// <param name="user"></param>
        protected override void NotifyAdd(User user)
        {
            Repo.Add(user);
            OnUserAdded(this, new ChangedUserEventArgs { ChangedUser = user });
        }

        /// <summary>
        /// Notify user deleted
        /// </summary>
        /// <param name="user"></param>
        protected override void NotifyDelete(User user)
        {
            Repo.Delete(user);
            OnUserDeleted(this, new ChangedUserEventArgs { ChangedUser = user });
        }
    }
    #endregion
}
