﻿using System;
using System.Threading;

namespace MutexClient
{
    class Program
    {
        static void Main(string[] args)
        {
            bool requestInitialOwnership = true;
            bool mutexWasCreated = false;
            Mutex mutex = new Mutex(requestInitialOwnership, "CustomMutex", out mutexWasCreated);

            // TODO: mutex = new Mutex(.., "MyMutex", out createdNew);

            Console.WriteLine("MutexClient. Mutex is new? " + mutexWasCreated + ". Waiting until mutex will be released.");

            // TODO: wait unit the mutex will be released
            mutex.WaitOne();

            Console.WriteLine("Press any key to release mutex.");
            Console.ReadKey();

            // TODO: release mutex
            mutex.ReleaseMutex();
            Console.WriteLine("Mutex is released. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
