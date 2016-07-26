using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorage;
using System.Collections.Generic;
using UserStorage.Entities;
using Replication;

namespace UserStorageTest
{
    [TestClass]
   public class UserRepositoryTest
    {
        //[TestInitialize]
        //public void Cleanup()
        //{
        //    typeof(MasterService).GetProperty("HasRepository").SetValue(MasterService.GetInstance, false);
        //}

        [TestMethod]
        public void Add_newUser_Test()
        {
            var repo = new UserRepository();
            var user = new User("Amy", "Levis", new DateTime(1996, 2, 17), 0);
            repo.Add(user);
            Assert.AreEqual(1, repo.UserCollection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_ExistingUser_ThrowException()
        {
            var repo = new UserRepository();
            List<VisaRecord> records = new List<VisaRecord>();
            var user = new User("Sara", "Brick", new DateTime(1996, 2, 17), 0, records);
            repo.Add(user);
            repo.Add(user);
        }

        [TestMethod]
        public void Delete_User_Test()
        {
            var repo = new UserRepository();
            List<VisaRecord> records = new List<VisaRecord>();
            var user = new User("Elliot", "Smith", new DateTime(1996, 2, 17), Gender.Male, records);
            repo.Add(user);
            repo.Delete(user);
            Assert.AreEqual(0, repo.UserCollection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_UserWhoDoesNotExist_Test()
        {
            var repo = new UserRepository();
            var user = new User("Matt", null, new DateTime(1996, 2, 17), Gender.Male);
            var user2 = new User("Matty", null, new DateTime(1996, 2, 17), Gender.Male);
            repo.Add(user);
            repo.Delete(user2);
        }

        [TestMethod]
        public void FindUser_ByLastName_Test()
        {
            var repo = new UserRepository();
            List<VisaRecord> records = new List<VisaRecord>();
            var user = new User("Pol", "Larch", new DateTime(1988, 2, 17), Gender.Male, records);
            var user2 = new User("Thom", "Yorke", new DateTime(1968, 7, 10), Gender.Male, records);
            repo.Add(user);
            repo.Add(user2);
            var required = repo.FindByTag(repo.FindByLastname, "Yorke");
            Assert.AreEqual(required[0], user2.ID);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindUser_ById_NotFound_ThrowInvalidOperationException()
        {
            var repo = new UserRepository();
            List<VisaRecord> records = new List<VisaRecord>();
            var user = new User("Pol", "Larch", new DateTime(1988, 2, 17), Gender.Male, records);
            repo.Add(user);
            var required = repo.FindByTag(repo.FindById, "4");
        }

        [TestMethod]
        public void SaveToXML_ReadFromXML_Test()
        {
            var repo = new UserRepository();
            List<VisaRecord> records = new List<VisaRecord>();
            var user = new User("Pol", "Larch", new DateTime(1988, 2, 17), Gender.Male, records);
            var user2 = new User("Thom", "Yorke", new DateTime(1968, 7, 10), Gender.Male, records);
            repo.Add(user);
            repo.Add(user2);
            repo.WriteToXML();
            repo.Delete(user);
            repo.Delete(user2);
            repo.ReadFromXML();
            Assert.AreEqual(2, repo.UserCollection.Count);
        }
    }

   
}
