// See https://aka.ms/new-console-template for more information

using PushLanguageInterpreter;
using PushLanguageInterpreter.Nodes;

Console.WriteLine("Hello, World!");

// var program = "(EXEC_EQUAL BOOLEAN_SWAP FLOAT_ADD EXEC_YANKDUP EXEC_YANKDUP EXEC_Y BOOLEAN_ROT (CODE_CDR INTEGER_STACKDEPTH FLOAT_MULT) INTEGER_YANK EXEC_EQUAL INTEGER_ROT EXEC_EQUAL BOOLEAN_NOT EXEC_Y BOOLEAN_NOT)";
// var program = "(EXEC_Y FLOAT_MULT)";
var program = "(BOOLEAN_SWAP)";
var tokens = new Tokenizer().Tokenize(program);
var nodes = new Parser().ParseProgram(tokens);
if (nodes.Count != 1)
{
    throw new Exception("Illegal program");
}

Console.WriteLine(string.Join(", ", nodes));

// var execStack = new Stack<INode>();
// execStack.Push(nodes.First());
// var intStack = new Stack<int>();
// intStack.Push(4);
// new Interpreter(execStack, intStack, new Stack<double>(), new Stack<bool>()).InterpretProgram();

var execStack = new Stack<INode>();
execStack.Push(nodes.First());
var intStack = new Stack<int>();
var boolStack = new Stack<bool>();
boolStack.Push(false);
boolStack.Push(true);
new Interpreter(execStack, new Stack<int>(), new Stack<double>(), boolStack).InterpretProgram();

Console.WriteLine("Done");
