using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NLog;
using UserStorage;
using UserStorage.Replication;
using System.Threading.Tasks;

namespace Replication
{
    public abstract class Service : MarshalByRefObject
    {
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
                    
                Repo.Add(user);
                Task.Run(() =>HandleAddEvent(this, new ChangedUserEventArgs() { ChangedUser = user }));
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

                Task.Run(() => HandleDeleteEvent(this, new ChangedUserEventArgs() { ChangedUser = user }));
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

        public virtual void Save()
        {
            Repo.WriteToXML();
        }

        public virtual void Load()
        {
            Repo.ReadFromXML();
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