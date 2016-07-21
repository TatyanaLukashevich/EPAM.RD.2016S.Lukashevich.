﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
    public interface IUserRepository
    {
        int Add(User user);
        void Delete(User user);
        List<User> FindByTag(Func<string, List<User>> methodTag, string tag); //to do return massiv
        void WriteToXML();
        void ReadFromXML();
    }
}
