using System;
using System.Collections;

namespace TaskConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable ht = new Hashtable();
            ht.Add(1, "heh");
            ht.Add(2, "kek");
            ht.Add(3, 4);
            foreach(dynamic h in ht)
            {
                Console.WriteLine("{0}, {1}", h.Key, h.Value);
            }

            int time = 20;

            if (time > MyClassLibrary.Helpers.WaitTime)
            {
                Console.WriteLine("time : {0} > Wait Time {1}", time, MyClassLibrary.Helpers.WaitTime);
            }
            else
            {
                Console.WriteLine("time : {0} <= Wait Time {1}", time, MyClassLibrary.Helpers.WaitTime);
            }

            Console.WriteLine("========================================================");

            var result = MyClassLibrary.GetResult.GetUserResult("Red");

            var work = new Work();
            work.DoSomething(result);

            Console.ReadLine();
        }
    }
}
