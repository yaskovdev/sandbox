using System.IO.Pipes;
using ProtoBuf;

namespace ProtobufWithPipeChild;

public static class Program
{
    public static void Main(string[] args)
    {
        using var parentToChildPipe = new AnonymousPipeClientStream(PipeDirection.In, args[0]);
        using var pipeReader = new StreamReader(parentToChildPipe);
        try
        {
            Serializer.DeserializeWithLengthPrefix<Command>(parentToChildPipe, PrefixStyle.Base128);
        }
        catch (Exception e)
        {
            File.WriteAllText("c:/dev/ProtobufWithPipeChild.StackTrace.txt", e.ToString());
        }
    }
}
