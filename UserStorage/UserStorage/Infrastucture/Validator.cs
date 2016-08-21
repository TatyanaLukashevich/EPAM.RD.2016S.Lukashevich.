using System;

namespace UserStorage
{
    /// <summary>
    /// Helper-class for users's validation
    /// </summary>
   public class Validator
    {
        /// <summary>
        /// Check user's age
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsValidAge(User user)
        {
            if (DateTime.Now.Year - user.DateOfBirth.Year < 18)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check length of username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsValidName(User user)
        {
            if (user.Name.Length >= 10 || user.Name.Length < 2)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="validator">Method-criteria of validation</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool GenerlValidator(Func<User, bool> validator, User user)
        {
            return validator(user);
        }
    }
}
