namespace Misc;

public class BoxComparer : IComparer<Box>
{
    public int Compare(Box? x, Box? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Value.CompareTo(y.Value);
    }
}
