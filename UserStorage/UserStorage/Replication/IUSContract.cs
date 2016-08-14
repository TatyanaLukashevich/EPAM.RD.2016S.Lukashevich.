using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Replication
{
    [ServiceContract]
    public interface IUSContract
    {
        [OperationContract]
        void Add(User user);

        [OperationContract]
        void Delete(User user);

        [OperationContract]
        List<int> FindByTag(Func<User, bool> criteria);

        [OperationContract]
        void WriteToXML();

        [OperationContract]
        void ReadFromXML();
    }
}
