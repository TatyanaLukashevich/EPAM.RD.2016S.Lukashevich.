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
        #endregion

        #region Constructors
        public SlaveService(UserRepository repo) : base(repo)
        {
        }
        #endregion

        #region Public metods
        /// <summary>
        /// Slave can't add users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override int Add(User user)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        ///  Slave can't delete users
        /// </summary>
        /// <param name="user"></param>
        public override void Delete(User user)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Slave can't write to xml
        /// </summary>
        public override void WriteToXML()
        {
            throw new InvalidOperationException();
        }


        /// <summary>
        ///  Slave can't read from xml
        /// </summary>
        public override void ReadFromXML()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Communicator for slave
        /// </summary>
        /// <param name="communicator"></param>
        public override void AddCommunicator(Communicator communicator)
        {
            base.AddCommunicator(communicator);
            Communicator.UserAdded += OnAdded;
            Communicator.UserDeleted += OnDeleted;
        }

        protected override void NotifyAdd(User user)
        {
            throw new InvalidOperationException();
        }

        protected override void NotifyDelete(User user)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Notify user added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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

        /// <summary>
        /// Notify user deleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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