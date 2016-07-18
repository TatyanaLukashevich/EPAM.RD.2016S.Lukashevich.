using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Entities
{
    [Serializable]
    public struct VisaRecord
    {
        public string Country { get; set; }
        public DateTime VisaStart { get; set; }
        public DateTime VisaEnd { get; set; }
    }
}
