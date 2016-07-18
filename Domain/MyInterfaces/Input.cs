using System;

namespace MyInterfaces
{

    [DoSomethingAttribute]
    [Serializable]
    public class Input
    {
        public User[] Users { get; set; }

        public Input() { }
    }
}
