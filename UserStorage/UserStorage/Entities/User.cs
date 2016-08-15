﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UserStorage.Entities;

namespace UserStorage
{
    [Serializable]
    public class User 
    {
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

        #region auto-properties
        public int ID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender UserGender { get; set; }

        public List<VisaRecord> VisaRecords { get; set; }
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
