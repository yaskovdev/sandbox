namespace BTree;

// For internal nodes:
// BranchingFactor == Refs.Length == Keys.Length + 1
// Refs[i] is a reference to a node containing keys in [Keys[i - 1], Keys[i]) interval.
// If i is 0, then Keys[-1] is -Inf.
// If i is Keys.Length, then Keys[Keys.Length] is +Inf.
//
// For leaf nodes:
// BranchingFactor == Values.Length == Keys.Length
// Values[i] is a value associated with Keys[i].
public class Node
{
    /// <summary>
    /// How many children this node can have at most.
    /// </summary>
    private int BranchingFactor { get; }

    private int[] Keys { get; }

    private Node[] Refs { get; }

    private int[] Values { get; }

    public Node(int branchingFactor)
    {
        BranchingFactor = branchingFactor;
        Keys = new int[branchingFactor - 1];
        Refs = new Node[branchingFactor];
        Values = new int[branchingFactor - 1];
    }

    public void Add(int key, int value)
    {
        
    }

    public void Get(int key)
    {
    }
}