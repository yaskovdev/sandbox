// See https://aka.ms/new-console-template for more information

using AsyncLocalSandbox;

internal class Program
{
    private static readonly AsyncLocal<bool> _isFunctionalTest = new AsyncLocal<bool>();
    private static readonly AsyncLocal<ISession> _session = new AsyncLocal<ISession>();

    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World! Is functional test returns " + _isFunctionalTest.Value);
        _isFunctionalTest.Value = true;
        Console.WriteLine("And now it is " + _isFunctionalTest.Value);

        Console.WriteLine("Session returns " + _session.Value);
        Console.WriteLine("Session is null? " + (_session.Value is null));
        _session.Value = new Session();
        Console.WriteLine("And now it is " + _session.Value);
    }
}
