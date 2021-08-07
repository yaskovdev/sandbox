using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;

DataSet dataSet;
HttpClient httpClient;
foreach (var r in Assembly.GetEntryAssembly().GetReferencedAssemblies())
{
    var a = Assembly.Load(new AssemblyName(r.FullName));
    var methodCount = a.DefinedTypes.Sum(t => t.GetMethods().Length);
    Console.WriteLine("{0:N0} types with {1:N0} methods in {2} assembly.", a.DefinedTypes.Count(), methodCount, r.Name);
}