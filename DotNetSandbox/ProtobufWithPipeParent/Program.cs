using System.Diagnostics;
using System.IO.Pipes;
using ProtoBuf;
using ProtobufWithPipeChild;

namespace ProtobufWithPipeParent;

public static class Program
{
    private const int DelayMs = 60000;

    public static void Main()
    {
        Console.WriteLine($"Parent process started with PID {Environment.ProcessId}");
        using var parentToChildPipe =
            new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
        using var childProcess = new Process();
        // Note: added CreateNoWindow, UseShellExecute, RedirectStandardError and the error started to reproduce locally
        childProcess.StartInfo.FileName = "ProtobufWithPipeChild";
        childProcess.StartInfo.CreateNoWindow = true;
        childProcess.StartInfo.UseShellExecute = false;
        childProcess.StartInfo.RedirectStandardOutput = true;
        childProcess.StartInfo.RedirectStandardError = true;
        childProcess.StartInfo.Arguments = parentToChildPipe.GetClientHandleAsString();
        childProcess.Start();
        parentToChildPipe.DisposeLocalCopyOfClientHandle();
        Console.WriteLine($"Going to send command in {DelayMs} ms");
        Thread.Sleep(DelayMs);
        Serializer.SerializeWithLengthPrefix(parentToChildPipe, Command.Evaluate, PrefixStyle.Base128);
        Console.WriteLine($"Child process wrote: {childProcess.StandardOutput.ReadToEnd()}");
        childProcess.WaitForExit();
        childProcess.Close();
    }
}
