using System;

namespace UserStorage.Entities
{
    /// <summary>
    /// Struct of records of visa
    /// </summary>
    [Serializable]
    public struct VisaRecord 
    {
        public string Country { get; set; }

        public DateTime VisaStart { get; set; }

        public DateTime VisaEnd { get; set; }
    }
}
