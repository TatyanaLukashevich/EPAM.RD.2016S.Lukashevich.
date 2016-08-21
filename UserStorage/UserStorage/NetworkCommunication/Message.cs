using System;

namespace UserStorage
{
    /// <summary>
    /// Type of called method
    /// </summary>
    [Serializable]
    public enum MethodType
    {
        /// <summary>
        /// User was added
        /// </summary>
        Add,
        /// <summary>
        /// User was deleted
        /// </summary>
        Delete
    }

    /// <summary>
    /// Message to send
    /// </summary>
    [Serializable]
   public class Message
    {
        /// <summary>
        /// Added/ deleted user
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// Type of method
        /// </summary>
        public MethodType MethodType { get; set; }
    }
}
