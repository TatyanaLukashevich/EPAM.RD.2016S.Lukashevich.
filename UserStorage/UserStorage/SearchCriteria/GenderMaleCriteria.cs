using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.SearchCriteria
{
    [Serializable]
   public class GenderMaleCriteria : ICriteria<User>
    {
        public bool MeetCriteria(User user)
        {
            Console.WriteLine("MALE CRITERIA!");
            return user.UserGender == Entities.Gender.Male;
        }
    }
}
