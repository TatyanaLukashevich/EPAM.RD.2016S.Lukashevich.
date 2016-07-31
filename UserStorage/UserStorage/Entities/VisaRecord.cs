using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace UserStorage.Entities
{
    [Serializable]
    public struct VisaRecord : IXmlSerializable
    {
        public string Country { get; set; }

        public DateTime VisaStart { get; set; }

        public DateTime VisaEnd { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(nameof(VisaRecord));
            Country = reader.ReadElementContentAsString();
            VisaStart = reader.ReadElementContentAsDateTime();
            VisaEnd = reader.ReadElementContentAsDateTime();
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(nameof(VisaRecord));
            writer.WriteElementString(nameof(Country), Country);
            writer.WriteElementString(nameof(VisaStart), VisaStart.ToString("yyyy-MM-dd"));
            writer.WriteElementString(nameof(VisaEnd), VisaEnd.ToString("yyyy-MM-dd"));
            writer.WriteEndElement();
        }
    }
}
