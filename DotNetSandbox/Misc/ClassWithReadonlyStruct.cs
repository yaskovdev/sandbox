namespace Misc;

public class ClassWithReadonlyStruct
{
    private const SomeEnum _someEnum = SomeEnum.SomeEnumValue;
    private readonly CancellationToken _readonlyStruct;

    public void PrintReadonlyStructValue()
    {
        Console.WriteLine("The value of the readonly struct is " + _readonlyStruct);
    }
}
