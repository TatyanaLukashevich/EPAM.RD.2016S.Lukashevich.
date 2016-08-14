using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Entities
{
    [Serializable]
    [DataContract]
    public enum Gender
    {
        [DataMember]
        Female,
        [DataMember]
        Male
    }
}
