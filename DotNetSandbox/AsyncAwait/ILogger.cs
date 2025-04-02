using System;

namespace AsyncAwait;

public interface ILogger
{
    void Warning(string message, string taskName, string sourceFilePath, int sourceLineNumber);

    void Error(Exception? taskException, string message, string taskName, string sourceFilePath,
        int sourceLineNumber);
}