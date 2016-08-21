using System;
using UserStorage.Interface;

namespace UserStorage.SearchCriteria
{
    /// <summary>
    /// Male criteria
    /// </summary>
    [Serializable]
   public class GenderMaleCriteria : ICriteria<User>
    {
        /// <summary>
        /// Check whether user is male
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsMatch(User user)
        {
            return user.UserGender == Entities.Gender.Male;
        }
    }
}
