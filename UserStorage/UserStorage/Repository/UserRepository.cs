using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NLog;
using Replication;
using UserStorage.Interface;

namespace UserStorage
{
    /// <summary>
    /// Memory repository
    /// </summary>
    [Serializable]
    public class UserRepository : MarshalByRefObject, IUserRepository
    {
        #region Private Fields
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserRepository()
        {
            Initialize(new GeneratorId());
        }
        #endregion

        #region Autoproperties
        public static BooleanSwitch DataSwitch { get; private set; }

        public List<User> UserCollection { get; set; }

        public MasterService Service { get; private set; }

        public GeneratorId Generator { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User id</returns>
        public int Add(User user)
        {
            Logger.Trace("UserRepository.Add called");
           
            if (!this.ValidateUser(user))
            {
                throw new ArgumentException("user is too young");
            }
            else
            {
                this.GenerateId(user);
                UserCollection.Add(user);
            }

            return user.ID;
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            if (DataSwitch.Enabled)
            {
                Logger.Trace("UserRepository.Delete called");
            }              

            if (!UserCollection.Contains(user))
            {
                throw new InvalidOperationException("there is no such a user");
            }
            else
            {
                UserCollection.Remove(user);
            }
        }

        /// <summary>
        /// Search user
        /// </summary>
        /// <param name="criteria">Delegate</param>
        /// <returns>List user ids</returns>
        public List<int> FindByTag(Func<User, bool> criteria)
        {
            var required = UserCollection.Where(criteria).Select(u => u.ID).ToList();
                return required;
        }

        /// <summary>
        /// Search user
        /// </summary>
        /// <param name="criteria">array of criteria</param>
        /// <returns>List user ids</returns>
        public List<int> FindByTag(ICriteria<User>[] criteria)
        {
            return this.UserCollection.Where(u => criteria.All(cr => cr.IsMatch(u)))
                                    .Select(u => u.ID).ToList();
        }

        /// <summary>
        /// Write to xml
        /// </summary>
        public void WriteToXML()
        {
            if (DataSwitch.Enabled)
            {
                Logger.Trace("UserRepository.Save called");
            }
               
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            string path = ConfigurationManager.AppSettings["xmlPath"];
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, UserCollection);
            }

            if (DataSwitch.Enabled)
            {
                Logger.Info("Collection of users was sucessfully serialized into xml file");
            }               
        }

        /// <summary>
        /// Read from xml
        /// </summary>
        /// <returns>List of users</returns>
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

            if (DataSwitch.Enabled)
            {
                Logger.Info("Collection of users was sucessfully deserialized from xml file");
            }

            return newUsers;
        }

        /// <summary>
        /// Get last serialized id
        /// </summary>
        /// <returns>Last serialized id</returns>
        public int GetLastId()
        {
            this.ReadFromXML();
            if (DataSwitch.Enabled)
            {
                Logger.Info("Iterator was restored");
            }
                
            return UserCollection.LastOrDefault().ID;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initialize repository with generator id and collection of users
        /// </summary>
        /// <param name="generator"></param>
        private void Initialize(GeneratorId generator)
        {
            Generator = generator;
            UserCollection = new List<User>();
            DataSwitch = new BooleanSwitch("Data", "DataAccess module");
            if (DataSwitch.Enabled)
            {
                Logger.Info("UserRepository initialized");
            }
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