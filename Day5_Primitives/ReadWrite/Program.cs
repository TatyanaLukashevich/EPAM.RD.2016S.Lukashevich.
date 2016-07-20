using System;
using System.Collections.Generic;
using System.Threading;

namespace ReadWrite
{
    class Program
    {
        // TODO: replace Object type with appropriate type for slim version of manual reset event.
        private static IList<Thread> CreateWorkers(EventWaitHandle ewh, Action action, int threadsNum, int cycles)
        {
            var threads = new Thread[threadsNum];

            for (int i = 0; i < threadsNum; i++)
            {
                Action d = () =>
                {
                    // TODO: Wait for signal.
                    ewh.WaitOne();

                    for (int j = 0; j < cycles; j++)
                    {
                        action();
                    }
                };
                // TODO: Create a new thread that will run the delegate above here.
                Thread thread = new Thread(new ThreadStart(d)); 

                threads[i] = thread;
            }

            return threads;
        }

        static void Main(string[] args)
        {
            var list = new MyList();

            // TODO: Replace Object type with slim version of manual reset event here.
            EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);

            var threads = new List<Thread>();

            threads.AddRange(CreateWorkers(ewh, () => { list.Add(1); }, 3, 5));
            threads.AddRange(CreateWorkers(ewh, () => { list.Get(); }, 3, 5));
            threads.AddRange(CreateWorkers(ewh, () => { list.Remove(); }, 3, 5));

            foreach (var thread in threads)
            {
                thread.Start();
                // TODO: Start all threads.
            }

            Console.WriteLine("Press any key to run unblock working threads.");
            Console.ReadKey();

            // NOTE: When an user presses the key all waiting worker threads should begin their work.
            // TODO: Send a signal to all worker threads that they can run.
            ewh.Set();
            foreach (var thread in threads)
            {
                thread.Join();
                // TODO: Wait for all working threads
            }

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }
    }
}
