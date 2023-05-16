namespace Misc;

public class OrderedId : IComparable<OrderedId>
{
    private readonly string _value;

    public OrderedId(string value)
    {
        _value = value;
    }

    public int CompareTo(OrderedId? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }
}
