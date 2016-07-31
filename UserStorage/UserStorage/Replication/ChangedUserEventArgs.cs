using System;

namespace UserStorage.Replication
{
   public class ChangedUserEventArgs : EventArgs
    {
        public User ChangedUser { get; set; }
    }
}
