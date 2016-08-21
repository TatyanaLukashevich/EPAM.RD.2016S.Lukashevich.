using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using NLog;
using UserStorage;
using UserStorage.Interface;
using UserStorage.NetworkCommunication;
using UserStorage.Replication;

namespace Replication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public abstract class Service : MarshalByRefObject, IUSContract
    {
        #region Fields
        protected ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        public Service()
        {
        }

        public Service(UserRepository repo)
        {
            Repo = repo;
        }
        #endregion

        #region Autoproperties
        public static BooleanSwitch DataSwitch { get; protected set; }

        public Communicator Communicator { get; set; }

        public string Name { get; set; }

        public UserRepository Repo { get; set; }
        #endregion

        #region Public methods
        public virtual int Add(User user)
        {
            locker.EnterWriteLock();
            try
            {
                if (DataSwitch.Enabled)
                {
                    Logger.Info("Invoke add event");
                }

               NotifyAdd(user);
              return Repo.Add(user);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        public virtual void Delete(User user)
        {
            locker.EnterWriteLock();
            try
            {
                if (DataSwitch.Enabled)
                {
                    Logger.Info("Invoke delete event");
                }

                NotifyDelete(user);
                Repo.Delete(user);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        public virtual List<int> FindByTag(Func<User, bool> criteria)
        {
            List<int> listId = new List<int>();
            locker.EnterReadLock();
            try
            {
               listId = Repo.FindByTag(criteria);
            }
            finally
            {
                locker.ExitReadLock();
            }

            return listId;
        }

        public List<int> FindByTag(ICriteria<User>[] criteria)
        {
            this.locker.EnterReadLock();
            try
            {
                return this.Repo.FindByTag(criteria).ToList();
            }
            finally
            {
                this.locker.ExitReadLock();
            }
        }

        public virtual void WriteToXML()
        {
            Repo.WriteToXML();
        }

        public virtual void ReadFromXML()
        {
           var users = Repo.ReadFromXML();
        }

        public virtual void AddCommunicator(Communicator communicator)
        {
            if (communicator == null)
            {
                return;
            }
               
            Communicator = communicator;
        }
        #endregion

        #region Protected methods
        protected abstract void NotifyAdd(User user);

        protected abstract void NotifyDelete(User user);

        protected virtual void OnUserDeleted(object sender, ChangedUserEventArgs args)
        {
            Communicator?.SendDelete(args);
        }

        protected virtual void OnUserAdded(object sender, ChangedUserEventArgs args)
        {
            Communicator?.SendAdd(args);
        }
        #endregion
    }
}
