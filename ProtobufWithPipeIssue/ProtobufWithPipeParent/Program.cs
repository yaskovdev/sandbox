using System.Diagnostics;
using System.IO.Pipes;

namespace ProtobufWithPipeParent;

public static class Program
{
    private const int DelayMs = 3600 * 1000;

    public static void Main()
    {
        using var parentToChildPipe =
            new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
        using var childProcess = new Process();
        childProcess.StartInfo.FileName = "ProtobufWithPipeChild";
        childProcess.StartInfo.CreateNoWindow = true; // Note: the error will start happening if you set this to true
        childProcess.StartInfo.Arguments = parentToChildPipe.GetClientHandleAsString();
        childProcess.Start();
        parentToChildPipe.DisposeLocalCopyOfClientHandle();
        Console.WriteLine($"Going to sleep for {DelayMs} ms. Run \"kill {Environment.ProcessId}\" in the meantime");
        Thread.Sleep(DelayMs);
        childProcess.WaitForExit();
        childProcess.Close();
    }
}
