using System;
using System.Collections.Generic;

namespace UserStorage.Interface
{
    /// <summary>
    /// UserRepository interface
    /// </summary>
    public interface IUserRepository
    {
        int Add(User user);

        void Delete(User user);

        List<int> FindByTag(Func<User, bool> criteria);

        void WriteToXML();

        List<User> ReadFromXML();
    }
}
