namespace AzureServiceBusExponentialBackoff.Tests;

public class Counter
{
    private int _value;

    public int Value => _value;

    public void Increment()
    {
        Interlocked.Increment(ref _value);
    }
}
