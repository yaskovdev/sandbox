using System;
using System.Linq;
using System.Reflection;

// #error version

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var r in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                var a = Assembly.Load(new AssemblyName(r.FullName));
                int methodCount = 0;
                foreach (var t in a.DefinedTypes)
                {
                    methodCount += t.GetMethods().Length;
                }

                Console.WriteLine("{0:NO} types with {1:NO} methods in {2} assembly", a.DefinedTypes.Count(),
                    methodCount, r.Name);
            }
        }
    }
}