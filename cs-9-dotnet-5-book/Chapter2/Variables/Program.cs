using System.Xml;
using static System.Console;

object height = 1.88;
object name = "Amir";
WriteLine($"{name} is {height} metres tall.");
int length2 = ((string) name).Length;
WriteLine($"{name} has {length2} characters.");
dynamic anotherName = "Ahmed";
// anotherName = 1.88;
int length = anotherName.Length;
WriteLine(length);
var xml = new XmlDocument();
XmlDocument html = new();
WriteLine($"{default(string) == null}");
int? weight = 0;