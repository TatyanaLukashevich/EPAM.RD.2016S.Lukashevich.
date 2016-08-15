using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Entities;
using UserStorage.SearchCriteria;

namespace UserStorage.Replication
{

    [ServiceContract]
    [ServiceKnownType(typeof(GenderFemCriteria))]
    public interface IUSContract
    {
        [OperationContract]
        int Add(User user);

        [OperationContract]
        void Delete(User user);

        [OperationContract]
        List<int> FindByTag(ICriteria<User>[] criteria);

        [OperationContract]
        void WriteToXML();

        [OperationContract]
        void ReadFromXML();

       
    }
}
