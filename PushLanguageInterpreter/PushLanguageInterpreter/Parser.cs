using PushLanguageInterpreter.Nodes;

namespace PushLanguageInterpreter;

public class Parser
{
    private int _position;

    public IReadOnlyCollection<INode> ParseProgram(IReadOnlyList<Token> program)
    {
        var result = new List<INode>();
        while (_position < program.Count && (program[_position].Value == "(" || char.IsLetter(program[_position].Value[0])))
        {
            if (program[_position].Value == "(")
            {
                _position++;
                var instructions = ParseProgram(program);
                if (program[_position].Value != ")")
                {
                    throw new Exception("Expected ')', got " + program[_position].Value);
                }

                result.Add(new ProgramNode(instructions));
            }
            else
            {
                result.Add(new InstructionNode(program[_position].Value));
            }

            _position++;
        }

        return result;
    }
}
