using System;
using UserStorage.Interface;

namespace UserStorage.SearchCriteria
{
    [Serializable]
   public class GenderMaleCriteria : ICriteria<User>
    {
        public bool IsMatch(User user)
        {
            return user.UserGender == Entities.Gender.Male;
        }
    }
}
