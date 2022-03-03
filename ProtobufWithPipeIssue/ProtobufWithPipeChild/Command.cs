using ProtoBuf;

namespace ProtobufWithPipeChild;

[ProtoContract]
public class Command
{
    [ProtoMember(0)]
    public CommandType Type { get; set; }
}
