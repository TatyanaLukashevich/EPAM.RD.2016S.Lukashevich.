using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UserStorage.Entities
{
    [Serializable]
    [DataContract]
    public struct VisaRecord 
    {
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public DateTime VisaStart { get; set; }

        [DataMember]
        public DateTime VisaEnd { get; set; }
    }
}
