using System.IO.Pipes;
using ProtoBuf;

namespace ProtobufWithPipeChild;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Child process started with PID {Environment.ProcessId}");
        using var parentToChildPipe =
            new AnonymousPipeClientStream(PipeDirection.In, args[0]);
        using var pipeReader = new StreamReader(parentToChildPipe);
        Console.WriteLine("Waiting for a command from the parent process...");
        try
        {
            var command = Serializer.DeserializeWithLengthPrefix<Command>(parentToChildPipe, PrefixStyle.Base128);
            Console.WriteLine($"Received command {command}");
        }
        catch (Exception e)
        {
            File.WriteAllText("/Users/yaskovdev/dev/ProtobufWithPipeChild.StackTrace.txt", e.ToString());
        }
    }
}
