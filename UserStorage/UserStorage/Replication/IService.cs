using System;
using System.Collections.Generic;
using UserStorage;

namespace Replication
{
   public interface IService 
    {
        void Add(User user);
        void Delete(User user);
        void FindByTag(Func<string, List<User>> methodTag, string tag);
        void RegisterRepository();
    }
}
