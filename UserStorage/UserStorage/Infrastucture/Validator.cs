using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
   public class Validator
    {
        public bool IsValidAge(User user)
        {
            if (DateTime.Now.Year - user.DateOfBirth.Year < 18)
            {
                return false;
            }

            return true;
        }

        public bool IsValidName(User user)
        {
            if (user.Name.Length >= 10 || user.Name.Length < 2)
            {
                return false;
            }

            return true;
        }

        public bool GenerlValidator(Func<User, bool> validator, User user)
        {
            return validator(user);
        }
    }
}
