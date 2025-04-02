using System;

namespace AsyncAwait;

public class ConsoleLogger : ILogger
{
    public void Warning(string message, string taskName, string sourceFilePath, int sourceLineNumber)
    {
        Console.WriteLine(message);
    }

    public void Error(Exception? taskException, string message, string taskName, string sourceFilePath,
        int sourceLineNumber)
    {
        Console.WriteLine(message);
    }
}