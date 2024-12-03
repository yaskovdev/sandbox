using UniversalRegisterMachineInterpreter;

var code = "(a4;a5;s2)2; ((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3.";
new FullyFunctionalInterpreter().Interpret(code);

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
};

var memory = new int[62]; // [REGISTERS][CODE][VARIABLES]: [0...5][6...59][60...61]

var i = 6;
const int unknownCharacter = -100;
code
    .Select(it => mapping.GetValueOrDefault(it, unknownCharacter))
    .Where(it => it != unknownCharacter)
    .ToList()
    .ForEach(it => memory[i++] = it);

memory[2] = 6;
memory[3] = 3;
Console.WriteLine("Before: " + string.Join(", ", memory));
new LimitedInterpreter().Interpret(memory);
Console.WriteLine("After: " + string.Join(", ", memory));