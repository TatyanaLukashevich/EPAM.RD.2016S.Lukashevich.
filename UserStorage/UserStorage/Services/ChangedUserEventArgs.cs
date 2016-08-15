using System;

namespace UserStorage.Replication
{
    [Serializable]
   public class ChangedUserEventArgs : EventArgs
    {
        public User ChangedUser { get; set; }
    }
}
