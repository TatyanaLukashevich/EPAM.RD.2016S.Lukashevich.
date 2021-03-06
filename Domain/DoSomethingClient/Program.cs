﻿using System;
using System.IO;
using System.Reflection;
using MyInterfaces;

namespace DoSomethingClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = new Input
            {
                Users = new User[]
                {
                    new User
                    {
                        Id = 1,
                        Name = "Vasily",
                        Age = 23
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Semen",
                        Age = 35
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Pawel",
                        Age = 22
                    }
                }
            };

            Method1(input);
            Method2(input);
        }

        private static void Method1(Input input)
        {
            // TODO: Create a domain with name MyDomain.
            var myDomain = AppDomain.CreateDomain("mydomain");
            var loader = (DomainAssemblyLoader)myDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);

            try
            {
                //string path = "D:\\Laba\\Domain\\DoSomethingClient\\bin\\Debug\\DoSomethingClient.exe";
                Result result = loader.Load(Assembly.GetExecutingAssembly().FullName, input);
                // TODO: Use loader here.

                Console.WriteLine("Method1: {0}", result.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            // TODO: Unload domain
            AppDomain.Unload(myDomain);
        }

        private static void Method2(Input input)
        {
            var appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyDomain")
            };

            // TODO: Create a domain with name MyDomain and setup from appDomainSetup.
            var myDomain = AppDomain.CreateDomain("mydomain", null, appDomainSetup);

            var loader = (DomainAssemblyLoader)myDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);

            try
            {
                // TODO: Use loader here.
                Result result = loader.Load(Assembly.GetExecutingAssembly().FullName, input);

                Console.WriteLine("Method2: {0}", result.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }

            // TODO: Unload domain
            AppDomain.Unload(myDomain);
        }
    }
}
