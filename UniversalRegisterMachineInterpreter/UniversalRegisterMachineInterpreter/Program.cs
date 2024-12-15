using System.Collections.Immutable;
using UniversalRegisterMachineInterpreter;

const string code = "(a4;a5;s2)2; ((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3."; // multiplies the 2 and 3 registers
var registers = new[] { 0, 0, 6, 3, 0, 0 };

new OriginalInterpreter().Interpret(code, [0, 6, 3, 0, 0, 0, 0, 0]);

var mapping = new Dictionary<char, int>
{
    { '.', 0 },
    { 'a', -1 },
    { 's', -2 },
    { '(', -3 },
    { ')', -4 },
    { '1', 1 },
    { '2', 2 },
    { '3', 3 },
    { '4', 4 },
    { '5', 5 }
}.ToImmutableDictionary();

var memory = new int[62];
for (var j = 0; j < registers.Length; j++)
{
    memory[j] = registers[j];
}

var i = 6;
const int unknownCharacter = -100;
var instructions = code
    .Select(it => mapping.GetValueOrDefault(it, unknownCharacter))
    .Where(it => it != unknownCharacter)
    .ToImmutableList();

Console.WriteLine("The input for the URL interpreter in Push:");
Console.WriteLine(string.Join(" ", memory.Take(6)));
Console.WriteLine(string.Join(" ", instructions));
instructions.ForEach(it => memory[i++] = it);

Console.WriteLine("\nBefore: " + string.Join(" ", memory));
new LimitedInterpreter().Interpret(memory);
Console.WriteLine("After: " + string.Join(" ", memory));