using System;
using UserStorage.Interface;

namespace UserStorage.SearchCriteria
{
    [Serializable]
    public class GenderFemCriteria : ICriteria<User>
    {
        public bool IsMatch(User user)
        {
            return user.UserGender == Entities.Gender.Female;
        }
    }
}
