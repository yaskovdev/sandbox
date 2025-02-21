namespace EditorConfigSandbox;

public class BoolToString
{
    public void Print()
    {
        var booleanValue = true;
        // The CA1305 rule should ignore Boolean.ToString: https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1305#cause
        Console.WriteLine(booleanValue.ToString());
    }
}