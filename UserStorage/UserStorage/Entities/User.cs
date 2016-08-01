using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UserStorage.Entities;

namespace UserStorage
{
    [Serializable]
    public class User /* : IXmlSerializable*/
    {
        #region constructors
        public User()
        {
            VisaRecords = new List<VisaRecord>();
        }

        #region auto-properties
        public int ID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender UserGender { get; set; }

        public List<VisaRecord> VisaRecords { get; set; }
        #endregion

        public User(string name, string lastname, DateTime dateOfBirth, Gender gender, List<VisaRecord> visaRecords = null)
        {
            Name = name;
            LastName = lastname;
            DateOfBirth = dateOfBirth;
            UserGender = gender;
            VisaRecords = visaRecords;
        }
        #endregion

        #region overloaded operators
        public static bool operator ==(User left, User right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(User left, User right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region override methods
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
               
            var user = obj as User;
            if (user == null)
            {
                return false;
            }

            return Equals(user);
        }

        public override int GetHashCode()
        {
            checked
            {
                if (LastName != null && DateOfBirth != null)
                {
                    var hashCode = ID.GetHashCode() + (LastName.GetHashCode() / 127) + (DateOfBirth.GetHashCode() / 21);
                    return hashCode;
                }

                return base.GetHashCode();
            }
        }

        public bool Equals(User user)
        {
            if (ReferenceEquals(user, null))
            {
                return false;
            }
                
            return ((ID == user.ID) &&
                   (string.CompareOrdinal(Name, user.Name) == 0) &&
                   (string.CompareOrdinal(LastName, user.LastName) == 0) &&
                   (DateOfBirth == user.DateOfBirth) &&
                   VisaRecords.SequenceEqual(user.VisaRecords));
        }

        public override string ToString()
        {
            string userInfo = ID + " " + Name + " " + LastName + " " + DateOfBirth + " " + UserGender;
            return userInfo;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        //public void WriteXml(XmlWriter writer)
        //{
        //    writer.WriteStartElement(nameof(User));
        //    writer.WriteStartElement("UserElement");
        //    writer.WriteElementString(nameof(ID), ID.ToString());
        //    writer.WriteElementString(nameof(Name), Name);
        //    writer.WriteElementString(nameof(LastName), LastName);
        //    writer.WriteElementString(nameof(Gender), ((int)UserGender).ToString());
        //    writer.WriteElementString(nameof(DateOfBirth), DateOfBirth.ToString("yyyy-MM-dd"));
        //    if (VisaRecords != null)
        //    {
        //        writer.WriteStartElement(nameof(VisaRecords));
        //        writer.WriteAttributeString("count", VisaRecords.Count.ToString());
        //        for (int i = 0; i < VisaRecords.Count; i++)
        //        {
        //            VisaRecords[i].WriteXml(writer);
        //        }

        //        writer.WriteEndElement();
        //    }

        //    writer.WriteEndElement();
        //}

        //public void ReadXml(XmlReader reader)
        //{
        //    reader.MoveToContent();
        //    reader.ReadStartElement(nameof(User));
        //    ID = Convert.ToInt32(reader.ReadElementString("ID"));
        //    Name = reader.ReadElementContentAsString();
        //    LastName = reader.ReadElementContentAsString();
        //    UserGender = (Gender)reader.ReadElementContentAsInt();
        //    DateOfBirth = reader.ReadElementContentAsDateTime();

        //    reader.MoveToAttribute("count");
        //    int count = int.Parse(reader.Value);
        //    VisaRecords = new List<VisaRecord>();
        //    if (VisaRecords != null)
        //    {
        //        reader.ReadStartElement(nameof(VisaRecords));
        //        var visaSer = new XmlSerializer(typeof(VisaRecord));
        //        for (int i = 0; i < count; i++)
        //        {
        //            var visa = (VisaRecord)visaSer.Deserialize(reader);
        //            VisaRecords[i] = visa;
        //        }

        //        reader.ReadEndElement();
        //    }

        //    reader.ReadEndElement();
        //}
        #endregion
    }
}
