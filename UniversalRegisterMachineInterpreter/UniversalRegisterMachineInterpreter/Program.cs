using UniversalRegisterMachineInterpreter;

// var code = "a4;a5;s2;a2;s4; s3; a1;a4;s5; a5;s1.";
// var code = "(a2;s3)3.";
// var code = "((a2;s3)3)4.";
// var code = "(a4;a5;s2)2.";

// PASSES
// var code = "((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3.";
// var registers = new[] { 6, 6, 3, 0, 0, 0 };

// // PASSES
// var code = "(a4;a5;s2)2; ((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)1.";
// var registers = new[] { 0, 0, 6, 3, 0, 0 };

// PASSES
// var code = "((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3.";
// var registers = new[] { 0, 0, 6, 3, 0, 0 };

// PASSES
// var code = "((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)1.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// PASSES
// var code = "(s3; (a5;s1)1)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// PASSES
// var code = "(s3; (a1;a4;s5)5)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// PASSES
// var code = "s3; (a1;a4;s5)5; (a5;s1)1.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// PASSES
// var code = "(s3; (a1;a4;s5)5; (a5;s1)1)2.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// PASSES
// var code = "(s3; (a1;a4;s5)5; (a5;s1)1)1.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// PASSES
// var code = "(s1)2.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS (but passes for 25)
// var code = "(s2)2.";
// var registers = new[] { 0, 0, 26, 0, 0, 0 };

// FAILS (the expected goes into infinite loop, the actual returns wrong result)
// var code = "(s1)2.";
// var registers = new[] { 0, 0, 1, 0, 0, 0 };

// FAILS (the expected goes into infinite loop, the actual returns wrong result)
// var code = "(s5)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS (the expected goes into infinite loop, the actual returns wrong result)
// var code = "((s5)5)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS (the expected goes into infinite loop, the actual returns wrong result)
// var code = "((s5)5; (a5;s1)1)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS (the expected goes into infinite loop, the actual returns wrong result)
// var code = "((a1;a4;s5)5; (a5;s1)1)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS
// var code = "(s3; (a1;a4;s5)5; (a5;s1)1)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS
// var code = "((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3.";
// var registers = new[] { 0, 0, 0, 3, 6, 6 };

// FAILS
var code = "(a4;a5;s2)2; ((a2;s4)4; s3; (a1;a4;s5)5; (a5;s1)1)3.";
var registers = new[] { 0, 0, 6, 3, 0, 0 };

// new FullyFunctionalInterpreter().Interpret(code);

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
for (var j = 0; j < registers.Length; j++)
{
    memory[j] = registers[j];
}

var i = 6;
const int unknownCharacter = -100;
var instructions = code
    .Select(it => mapping.GetValueOrDefault(it, unknownCharacter))
    .Where(it => it != unknownCharacter)
    .ToList();
instructions.Reverse();
Console.WriteLine($"{54 - instructions.Count} exec.do*times ( 0 )");
Console.WriteLine(string.Join(" ", instructions));
Console.WriteLine(string.Join(" ", memory.Take(6).Reverse()));
instructions.Reverse();
instructions
    .ForEach(it => memory[i++] = it);

Console.WriteLine("\nBefore: " + string.Join(", ", memory));
new LimitedInterpreter().Interpret(memory);
Console.WriteLine("After: " + string.Join(", ", memory));