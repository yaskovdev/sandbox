// See https://aka.ms/new-console-template for more information

using System.Numerics;
using Misc;

Console.WriteLine("Hello, World!");

new WeirdProblem().Swap(2, 5);
new WeirdProblem().Swap(3, 7);

using Frame? frame = null;
Console.WriteLine($"Used frame {frame}");

Console.WriteLine($"{new byte[] { 0, 1, 2 }.GetHashCode()} vs {new byte[] { 0, 1, 2 }.GetHashCode()}");
Console.WriteLine($"{new BigInteger(new byte[] { 0, 1, 2 }).GetHashCode()} vs {new BigInteger(new byte[] { 0, 1, 2 }).GetHashCode()}");
