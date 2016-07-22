using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace UserStorage
{
    [Serializable]
    public class UserRepository : IUserRepository
    {
        #region Private Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Autoproperties
        public List<User> UserCollection { get; set; }
        public IService Service { get; private set; }
        public GeneratorId Generator { get; set; }
        public static BooleanSwitch DataSwitch { get; private set; }
        #endregion

        #region Constructors
        public UserRepository()
        {
            Initialize(new GeneratorId(), MasterService.GetInstance);
            //MasterService.GetInstance.MasterDomain = AppDomain.CreateDomain("MasterDomain");
        }

        public UserRepository(IService service)
        {
            Initialize(new GeneratorId(), service);
        }
        #endregion

        #region Public methods
        public int Add(User user)
        {
            Logger.Trace("UserRepository.Add called");
            Service.Add(user);
            if (!this.ValidateUser(user))
            {
                throw new ArgumentException("user is too young");
            }
            if (UserCollection.Contains(user))
            {
                throw new InvalidOperationException("user already exist");
            }
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
            Service.Delete(user);
            if (!UserCollection.Contains(user))
            {
                throw new InvalidOperationException("there is no such a user");
            }
            else
            {
                UserCollection.Remove(user);
            }
        }

        public List<User> FindByTag(Func<string, List<User>> methodTag, string tag)
        {
            var required = UserCollection.FindAll(user => methodTag(tag).Contains(user));
            if (required.Count == 0)
            {
                throw new InvalidOperationException("user is not found");
            }
            else
                return required;
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

        public void ReadFromXML()
        {
            Logger.Trace("UserRepository.Load called");
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationManager.AppSettings["xmlPath"];

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                List<User> newUsers = (List<User>)formatter.Deserialize(fs);
                UserCollection = newUsers;
            }
            Logger.Info("Collection of users was sucessfully deserialized from xml file");
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
        private void Initialize(GeneratorId generator, IService service)
        {
            Generator = generator;
            Service = service;
            if (Service.HasRepository)
                throw new ArgumentException("It's impossible to register more than one repository to the role");
            Service.RegisterRepository();

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
