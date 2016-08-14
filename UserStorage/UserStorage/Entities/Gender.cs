using System;
using System.Runtime.Serialization;

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
