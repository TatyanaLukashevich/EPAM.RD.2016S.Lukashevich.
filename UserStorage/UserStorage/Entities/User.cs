using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UserStorage.Entities;

namespace UserStorage
{
    [Serializable]
    [DataContract]
    public class User 
    {
        #region constructors
        public User()
        {
            VisaRecords = new List<VisaRecord>();
        }

        #region auto-properties
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public Gender UserGender { get; set; }

        [DataMember]
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
        #endregion
    }
}
