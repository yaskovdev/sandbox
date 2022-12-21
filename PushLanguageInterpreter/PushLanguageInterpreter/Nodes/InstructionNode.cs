namespace PushLanguageInterpreter.Nodes;

public record InstructionNode(NodeType Type, string Name) : INode
{
    public override string ToString() => Type.ToString().ToUpperInvariant() + "_" + Name;
}
