using System.Collections.Generic;
using System.ServiceModel;
using UserStorage.SearchCriteria;

namespace UserStorage.Interface
{
    /// <summary>
    /// Contract-interface for WCF
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(typeof(GenderFemCriteria))]
    [ServiceKnownType(typeof(GenderMaleCriteria))]
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