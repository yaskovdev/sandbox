namespace PushLanguageInterpreter.Nodes;

public record InstructionNode(string Name) : INode
{
    public override string ToString() => Name;

    public virtual bool Equals(InstructionNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override int GetHashCode() => Name.GetHashCode();
}
