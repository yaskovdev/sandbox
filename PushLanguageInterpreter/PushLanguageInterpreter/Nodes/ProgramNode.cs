namespace PushLanguageInterpreter.Nodes;

public record ProgramNode(IReadOnlyCollection<INode> Nodes) : INode
{
    public override string ToString() => $"({string.Join(", ", Nodes)})";

    public virtual bool Equals(ProgramNode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Nodes.Count == other.Nodes.Count && Nodes.Zip(other.Nodes).All(it => it.Item1.Equals(it.Item2));
    }

    public override int GetHashCode()
    {
        var hashCode = 0;
        foreach (var p in Nodes)
        {
            hashCode ^= p.GetHashCode();
            hashCode = (hashCode << 7) | (hashCode >> (32 - 7));
        }

        return hashCode;
    }
}
