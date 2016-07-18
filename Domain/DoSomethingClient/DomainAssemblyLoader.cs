using System;
using System.Reflection;
using MyInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace DoSomethingClient
{
    [DoSomethingAttribute]
    public class DomainAssemblyLoader : MarshalByRefObject, IDoSomething
    {
        public Result DoSomething(Input input)
        {
            int total = 0;

            foreach (var item in input.Users)
            {
                total += item.Age;
            }

            return new Result
            {
                Value = total / input.Users.Length
            };
        }

        // Before making this call make sure that MyInterface assembly is signed with mykey.snk file. See Signing tab in MyInterface project properties editor.
        // Usage:
        // result = loader.Load("MyLibrary, Version=1.2.3.4, Culture=neutral, PublicKeyToken=f46a87b3d9a80705", input)
        public Result Load(string assemblyString, Input data)
        {
            // LoadFile() doesn't bind through Fusion at all - the loader just goes ahead and loads exactly what the caller requested.
            // It doesn't use either the Load or the LoadFrom context.
            // LoadFile() has a catch. Since it doesn't use a binding context, its dependencies aren't automatically found in its directory. 

            var assembly = Assembly.Load(assemblyString);
            var types = assembly.GetTypes();

            // TODO: Find first type that has DoSomething attribute and implements IDoSomething.
            var typeWithAttribute = from t in types
                       where Attribute.IsDefined(t, typeof(DoSomethingAttribute)) && typeof(IDoSomething).IsAssignableFrom(t)
                                    select t;
            var firstTypeWithAttribute = typeWithAttribute.FirstOrDefault();
            // TODO: Create an instance of this type.
            object o = Activator.CreateInstance(firstTypeWithAttribute);

            IDoSomething doSomethingService = (IDoSomething)o; // TODO Save instance to variable.
            return doSomethingService.DoSomething(data);
        }

        // Usage:
        // var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"MyDomain\MyLibrary.dll");
        // result = loader.Load(path, input);
        public Result LoadFile(string path, Input data)
        {
            // LoadFrom() goes through Fusion and can be redirected to another assembly at a different path
            // but with that same identity if one is already loaded in the LoadFrom context.

            var assembly = Assembly.LoadFile(path);
            var types = assembly.GetTypes();

            // TODO: Find first type that has DoSomething attribute and don't implement IDoSomething.
            var typeWithAttribute = from t in assembly.GetTypes()
                                    where Attribute.IsDefined(t, typeof(DoSomethingAttribute)) && !typeof(IDoSomething).IsAssignableFrom(t)
                                    select t;
            var firstTypeWithAttribute = typeWithAttribute.FirstOrDefault();
            // TODO: 
            MethodInfo mi = firstTypeWithAttribute.GetMethod("DoSomething");
            //Result result = null;
            // TODO: 
            Result result = mi.Invoke(mi, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture) as Result;

            return result;
        }

        // More details: http://stackoverflow.com/questions/1477843/difference-between-loadfile-and-loadfrom-with-net-assemblies
        public Result LoadFrom(string fileName, Input data)
        {
            var assembly = Assembly.LoadFrom(fileName);
            var type = assembly.GetTypes();

            // TODO: Find first type that has DoSomething attribute and implements IDoSomething.
            // TODO: Create an instance of this type.

            var typeWithAttribute = from t in assembly.GetTypes()
                                    where Attribute.IsDefined(t, typeof(DoSomethingAttribute)) && typeof(IDoSomething).IsAssignableFrom(t)
                                    select t;
            var firstTypeWithAttribute = typeWithAttribute.FirstOrDefault();
            // TODO: Create an instance of this type.
            object o = Activator.CreateInstance(firstTypeWithAttribute);

            IDoSomething doSomethingService = (IDoSomething)o; // TODO Save instance to variable.
            return doSomethingService.DoSomething(data);
        }
    }
}
