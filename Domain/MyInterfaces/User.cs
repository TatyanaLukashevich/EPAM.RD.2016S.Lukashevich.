using System;

namespace MyInterfaces
{
    [Serializable]
    [DoSomethingAttribute]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public User() { }

    }
}
