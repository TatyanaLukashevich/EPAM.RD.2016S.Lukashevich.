using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UserStorage;
using UserStorage.Replication;

namespace Replication
{
    public abstract class Service : MarshalByRefObject
    {
        public UserRepository Repo { get; set; }
        protected ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        protected bool LoggingEnabled = true;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Service() { }

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
                    Logger.Info("Invoke add event");
                HandleAddEvent(this, new ChangedUserEventArgs());
                Repo.Add(user);
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
                    Logger.Info("Invoke delete event");
                HandleDeleteEvent(this, new ChangedUserEventArgs());
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

        public void HandleAddEvent(object sender, ChangedUserEventArgs args)
        {
            Logger.Info("HandleAddEvent called");
            Debug.WriteLine("Add method notification");
        }

        public void HandleDeleteEvent(object sender, ChangedUserEventArgs args)
        {
            Logger.Info("HandleDeleteEvent called");
            Debug.WriteLine("Delete method notification");
        }

    }
}
