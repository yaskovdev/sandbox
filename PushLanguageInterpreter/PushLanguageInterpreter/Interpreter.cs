using System.Collections.Immutable;
using PushLanguageInterpreter.Nodes;

namespace PushLanguageInterpreter;

public class Interpreter
{
    private readonly Stack<INode> _execStack;
    private readonly Stack<int> _intStack;
    private readonly Stack<double> _floatStack;
    private readonly Stack<bool> _boolStack;

    public Interpreter(Stack<INode> execStack, Stack<int> intStack, Stack<double> floatStack, Stack<bool> boolStack)
    {
        _execStack = execStack;
        _intStack = intStack;
        _floatStack = floatStack;
        _boolStack = boolStack;
    }

    public void InterpretProgram()
    {
        while (_execStack.Any())
        {
            var node = _execStack.Pop();
            if (node is ProgramNode programNode)
            {
                programNode
                    .Nodes
                    .Reverse()
                    .ToList()
                    .ForEach(_execStack.Push);
            }
            else if (node is InstructionNode instructionNode)
            {
                if (instructionNode.Type == NodeType.Exec && instructionNode.Name == "EQUAL")
                {
                    if (_execStack.Count >= 2)
                    {
                        var x = _execStack.Pop();
                        var y = _execStack.Pop();
                        _boolStack.Push(x.Equals(y));
                    }
                }
                else if (instructionNode.Type == NodeType.Exec && instructionNode.Name == "YANKDUP")
                {
                    if (_intStack.Count >= 1)
                    {
                        var index = _intStack.Pop();
                        if (index < _execStack.Count)
                        {
                            var source = _execStack.ElementAt(index);
                            _execStack.Push(source); // TODO: no need to clone, right? Since nodes are immutable.
                        }
                    }
                }
                else if (instructionNode.Type == NodeType.Exec && instructionNode.Name == "Y")
                {
                    if (_execStack.Count >= 1)
                    {
                        var topItem = _execStack.Pop();
                        _execStack.Push(new ProgramNode(ImmutableList.Create(new InstructionNode(NodeType.Exec, "Y"), topItem)));
                        _execStack.Push(topItem);
                    }
                }
                else if (instructionNode.Type == NodeType.Integer && instructionNode.Name == "STACKDEPTH")
                {
                    _intStack.Push(_intStack.Count);
                }
                else if (instructionNode.Type == NodeType.Integer && instructionNode.Name == "YANK")
                {
                    if (_intStack.Count >= 1)
                    {
                        var index = _intStack.Pop();
                        if (index < _intStack.Count)
                        {
                        }
                    }
                }
                else if (instructionNode.Type == NodeType.Float && instructionNode.Name == "MULT")
                {
                    if (_floatStack.Count >= 2)
                    {
                        var x = _floatStack.Pop();
                        var y = _floatStack.Pop();
                        _floatStack.Push(x * y);
                    }
                }
                else if (instructionNode.Type == NodeType.Boolean && instructionNode.Name == "SWAP")
                {
                    if (_boolStack.Count >= 2)
                    {
                        var x = _boolStack.Pop();
                        var y = _boolStack.Pop();
                        _boolStack.Push(x);
                        _boolStack.Push(y);
                    }
                }
                else if (instructionNode.Type == NodeType.Code)
                {
                    Console.WriteLine("Ignoring CODE_* for now");
                }
                else
                {
                    throw new Exception($"Unknown instruction {instructionNode.Name}");
                }
            }
            else
            {
                throw new Exception("Unknown node " + node);
            }
        }
    }
}
