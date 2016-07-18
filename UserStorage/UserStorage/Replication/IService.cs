using System;
using System.Collections.Generic;
using UserStorage;

namespace UserStorage
{
    // IStrategy
   public interface IService 
    {
        bool HasRepository { get; }
        void Add(User user);
        void Delete(User user);
        void FindByTag(Func<string, List<User>> methodTag, string tag);
        void RegisterRepository();
    }
}
