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

        #region events
        public event EventHandler<ChangedUserEventArgs> AddMethod;

        public event EventHandler<ChangedUserEventArgs> DeleteMethod;
        #endregion

        #region Constructors
        public MasterService(UserRepository repo) : base(repo)
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
            ////Communicator?.SendAdd(args);
            AddMethod?.Invoke(this, new ChangedUserEventArgs { ChangedUser = user });
        }

        public override void Delete(User user)
        {
            base.Delete(user);
            ////Communicator?.SendAdd(args);
            DeleteMethod?.Invoke(this, new ChangedUserEventArgs { ChangedUser = user });
        }
    }
    #endregion
}
