using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Replication
{
   public class ChangedUserEventArgs : EventArgs
    {
        public User ChangedUser { get; set; }
    }
}
