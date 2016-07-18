using System;

namespace MyInterfaces
{
    [DoSomethingAttribute]
    [Serializable]
    public class Result
    {
        public int Value { get; set; }
    }
}
