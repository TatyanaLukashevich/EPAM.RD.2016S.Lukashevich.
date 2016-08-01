using System;
using System.Collections.Generic;
using System.Threading;
using NLog;
using UserStorage;
using UserStorage.NetworkCommunication;
using System.Threading.Tasks;
using UserStorage.Replication;

namespace Replication
{
    public abstract class Service : MarshalByRefObject
    {
        public Communicator Communicator { get; set; }

        protected ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        protected bool LoggingEnabled = true;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public UserRepository Repo { get; set; }

        public Service()
        {
        }

        public Service(UserRepository repo)
        {
            Repo = repo;
        }

        public virtual void Add(User user)
        {
            locker.EnterWriteLock();
            try
            {
                if (LoggingEnabled)
                {
                    Logger.Info("Invoke add event");
                }

                NotifyAdd(user);
                
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
                if (LoggingEnabled)
                {
                    Logger.Info("Invoke delete event");
                }
                NotifyDelete(user);
                //Task.Run(() => HandleDeleteEvent(this, new ChangedUserEventArgs() { ChangedUser = user }));
                Repo.Delete(user);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }

        public virtual List<int> FindByTag(Func<string, List<User>> methodTag, string tag)
        {
            List<int> listId = new List<int>();
            locker.EnterReadLock();
            try
            {
               listId = Repo.FindByTag(Repo.FindById, tag);
            }
            finally
            {
                locker.ExitReadLock();
            }

            return listId;
        }

        public virtual void WriteToXML()
        {
            Repo.WriteToXML();
        }

        public virtual void ReadFromXML()
        {
           var users =  Repo.ReadFromXML();
                
        }

        public virtual void AddCommunicator(Communicator communicator)
        {
            if (communicator == null)
                return;
            Communicator = communicator;
        }

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
        //public void HandleAddEvent(object sender, ChangedUserEventArgs args)
        //{
        //    Logger.Info("HandleAddEvent called");
        //    Debug.WriteLine("Add method notification");
        //}

        //public void HandleDeleteEvent(object sender, ChangedUserEventArgs args)
        //{
        //    Logger.Info("HandleDeleteEvent called");
        //    Debug.WriteLine("Delete method notification");
        //}
    }
}