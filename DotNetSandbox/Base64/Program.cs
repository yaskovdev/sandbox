// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Reflection;
using Base64;
using Microsoft.VisualBasic.CompilerServices;

Console.WriteLine("Reading base64-encoded string");

var assembly = Assembly.GetExecutingAssembly();
var executableLocation = Path.GetDirectoryName(assembly.Location);
var source = Path.Combine(executableLocation, "Assets", "hello_base64.txt");
var base64String = File.ReadAllText(source);

var bytes = Convert.FromBase64String(base64String);
Console.WriteLine($"Number of bytes is {bytes.Length}");

File.WriteAllBytes(Path.Combine(executableLocation, "hello.ogg"), bytes);

Console.WriteLine($"Memory is {Util.CalculateMemorySizeInBytes(2, 1024)}");