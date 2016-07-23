using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
    public interface IUserRepository
    {
        int Add(User user);
        void Delete(User user);
        List<int> FindByTag(Func<string, List<User>> methodTag, string tag);
        void WriteToXML();
        void ReadFromXML();
    }
}
