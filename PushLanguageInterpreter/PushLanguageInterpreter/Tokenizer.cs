using System.Text;

namespace PushLanguageInterpreter;

public class Tokenizer
{
    private int _position;

    public IReadOnlyList<Token> Tokenize(string program)
    {
        var instructions = new List<Token>();
        while (_position < program.Length)
        {
            if (IsToken(program, _position))
            {
                var word = new StringBuilder();
                while (IsToken(program, _position))
                {
                    word.Append(program[_position]);
                    _position++;
                }

                instructions.Add(new Token(word.ToString()));
            }
            else if (program[_position] == '(' || program[_position] == ')')
            {
                instructions.Add(new Token(program[_position].ToString()));
                _position++;
            }
            else if (char.IsWhiteSpace(program[_position]))
            {
                _position++;
            }
            else
            {
                throw new Exception("Unexpected character at position " + _position + ": " + program[_position]);
            }

        }

        return instructions;
    }

    private static bool IsToken(string program, int position) => char.IsLetter(program[position]) || program[position] == '_';
}
