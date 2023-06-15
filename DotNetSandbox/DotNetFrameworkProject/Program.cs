using System;
using DotNetStandardProject;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Map((SomeEnum)2));
    }

    private static int Map(SomeEnum source) =>
        source switch
        {
            SomeEnum.SomeValue => 0,
            SomeEnum.AnotherValue => 1
        };
}
