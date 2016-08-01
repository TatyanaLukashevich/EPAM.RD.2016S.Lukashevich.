using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NLog;
using Replication;

namespace UserStorage
{
    [Serializable]
    public class UserRepository : MarshalByRefObject, IUserRepository
    {
        #region Private Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Autoproperties
        public static BooleanSwitch DataSwitch { get; private set; }

        public List<User> UserCollection { get; set; }

        public MasterService Service { get; private set; }

        public GeneratorId Generator { get; set; }
        #endregion

        #region Constructors
        public UserRepository()
        {
            Initialize(new GeneratorId());
        }
        #endregion

        #region Public methods
        public int Add(User user)
        {
            Logger.Trace("UserRepository.Add called");
           
            if (!this.ValidateUser(user))
            {
                throw new ArgumentException("user is too young");
            }

            //if (UserCollection.Contains(user))
            //{
            //    throw new InvalidOperationException("user already exist");
            //}
            else
            {
                this.GenerateId(user);
                UserCollection.Add(user);
            }

            return user.ID;
        }

        public void Delete(User user)
        {
            Logger.Trace("UserRepository.Delete called");

            if (!UserCollection.Contains(user))
            {
                throw new InvalidOperationException("there is no such a user");
            }
            else
            {
                UserCollection.Remove(user);
            }
        }

        public List<int> FindByTag(Func<string, List<User>> methodTag, string tag)
        {
            var required = UserCollection.FindAll(user => methodTag(tag).Contains(user));
            var requiredId = new List<int>();
            if (required.Count == 0)
            {
                throw new InvalidOperationException("user is not found");
            }
            else
            {
                foreach (var item in required)
                {
                    requiredId.Add(item.ID);
                }
            }

                return requiredId;
        }

        public void WriteToXML()
        {
            Logger.Trace("UserRepository.Save called");
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationManager.AppSettings["xmlPath"];
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, UserCollection);
            }

            Logger.Info("Collection of users was sucessfully serialized into xml file");
        }

        public List<User> ReadFromXML()
        {
            Logger.Trace("UserRepository.Load called");
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationManager.AppSettings["xmlPath"];
            List<User> newUsers = new List<User>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
               newUsers = (List<User>)formatter.Deserialize(fs);
                UserCollection = newUsers;
            }

            Logger.Info("Collection of users was sucessfully deserialized from xml file");
            return newUsers;
        }

        public int GetLastId()
        {
            this.ReadFromXML();
            Logger.Info("Iterator was restored");
            return UserCollection.LastOrDefault().ID;
        }

        #region Find by tag methods
        public List<User> FindById(string id)
        {
            var required = this.UserCollection.FindAll(user => user.ID.ToString() == id);
            return required;
        }

        public List<User> FindByLastname(string lastname)
        {
            var required = this.UserCollection.FindAll(user => user.LastName == lastname);
            return required;
        }

        public List<User> FindByDoB(string dateOfBirth)
        {
            var required = this.UserCollection.FindAll(user => user.DateOfBirth.ToString() == dateOfBirth);
            return required;
        }
        #endregion
        #endregion

        #region Private Methods

        private void Initialize(GeneratorId generator)
        {
            Generator = generator;
            UserCollection = new List<User>();
            DataSwitch = new BooleanSwitch("Data", "DataAccess module");
            Logger.Info("UserRepository initialized");
        }

        private void GenerateId(User user)
        {
            GeneratorId generator = new GeneratorId();
            user.ID = generator.StrategyForGenerateId(generator.GetPrimes);
        }

        private bool ValidateUser(User user)
        {
            Validator validator = new Validator();
            return validator.GenerlValidator(validator.IsValidAge, user);
        }
        #endregion
    }
}