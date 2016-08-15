using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.SearchCriteria
{
    [Serializable]
    public class GenderFemCriteria: ICriteria<User>
    {
        public bool MeetCriteria(User user)
        {
            Console.WriteLine("FEMALE CRITERIA!");
            return user.UserGender == Entities.Gender.Female;
        }
    }
}
