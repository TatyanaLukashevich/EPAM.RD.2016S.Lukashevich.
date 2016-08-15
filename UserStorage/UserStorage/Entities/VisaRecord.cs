using System;

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
