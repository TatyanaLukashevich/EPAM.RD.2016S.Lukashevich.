using System;
using UserStorage.Interface;

namespace UserStorage.SearchCriteria
{
    /// <summary>
    /// Femalr criteria
    /// </summary>
    [Serializable]
    public class GenderFemCriteria : ICriteria<User>
    {
        /// <summary>
        /// Check whether user is female
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsMatch(User user)
        {
            return user.UserGender == Entities.Gender.Female;
        }
    }
}
