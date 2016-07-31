using System;

namespace UserStorage
{
    [Serializable]
    public enum MethodType
    {
        Add,
        Delete
    }

    [Serializable]
   public class Message
    {
        public User User { get; set; }
        
        public MethodType MethodType { get; set; }
    }
}
