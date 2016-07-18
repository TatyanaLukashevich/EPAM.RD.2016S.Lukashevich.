using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Entities;

namespace UserStorage
{
    [Serializable]
    public class User
    {
        #region auto-properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender UserGender { get; set; }
        public List<VisaRecord> VisaRecords { get; set; }
        #endregion

        #region constructors
        public User()
        {
            VisaRecords = new List<VisaRecord>();
        }

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
            if (ReferenceEquals(obj, null)) return false;
            var user = obj as User;
            if (user == null) return false;
            return Equals(user);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (Name != null && LastName != null && DateOfBirth != null)
                {
                    var hashCode = (Name.Length ^ 15) + (LastName.Length * 111) + DateOfBirth.GetHashCode();
                    return hashCode;
                }
            }
            return base.GetHashCode();
        }

        public bool Equals(User obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            return ((ID==obj.ID) &&
                   (string.CompareOrdinal(Name, obj.Name) == 0) &&
                   (string.CompareOrdinal(LastName, obj.LastName) == 0) &&
                   (DateOfBirth == obj.DateOfBirth) &&
                   VisaRecords.SequenceEqual(obj.VisaRecords));
        }

        public override string ToString()
        {
            string userInfo = ID + " " + Name + " " + LastName + " " + DateOfBirth + " " + UserGender;
            return userInfo;
        }
        #endregion
    }
}
