using System.IO.Pipes;
using ProtoBuf;

namespace ProtobufWithPipeChild;

public static class Program
{
    private const string OutputPath = "c:/dev";

    public static void Main(string[] args)
    {
        using var parentToChildPipe = new AnonymousPipeClientStream(PipeDirection.In, args[0]);
        using var pipeReader = new StreamReader(parentToChildPipe);
        try
        {
            Serializer.DeserializeWithLengthPrefix<CommandType>(parentToChildPipe, PrefixStyle.Base128);
            // var value = Serializer.DeserializeWithLengthPrefix<Command>(parentToChildPipe, PrefixStyle.Base128);
            // File.WriteAllText(Path.Combine(OutputPath, "ProtobufWithPipeChild.Value.txt"), value == null ? "null" : value.Type.ToString());
        }
        catch (Exception e)
        {
            File.WriteAllText(Path.Combine(OutputPath, "ProtobufWithPipeChild.StackTrace.txt"), e.ToString());
        }
    }
}
