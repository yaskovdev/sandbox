namespace PushLanguageInterpreter;

public record Token(string Value)
{
    public override string ToString() => Value;
}
