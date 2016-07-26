using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
    [Serializable]
    public enum MethodTSype
    {
        Add,
        Delete
    }

    [Serializable]
   public class Message
    {
        public User User { get; set; }
    }
}
