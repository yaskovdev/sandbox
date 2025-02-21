﻿namespace DotNetCppSandbox;

using WithIntPtr;
using WithSafeHandle;

internal static class Program
{
    public static void Main()
    {
        const int coefficient = 2;
        const int number = 5;

        UseIntPtrMultiplier(coefficient, number);
        UseSafeHandleMultiplier(coefficient, number);
    }

    private static void UseIntPtrMultiplier(int coefficient, int number)
    {
        var multiplier = MultiplierIntPtrApi.CreateMultiplierExtern(coefficient);
        try
        {
            Console.WriteLine($"Multiplier IntPtr created at address {multiplier}");
            Console.WriteLine($"{number} multiplied by {coefficient} is {MultiplierIntPtrApi.MultiplyExtern(multiplier, number)}");
        }
        finally
        {
            MultiplierIntPtrApi.DestroyMultiplierExtern(multiplier);
        }
    }

    private static void UseSafeHandleMultiplier(int coefficient, int number)
    {
        var api = new MultiplierSafeHandleApi();
        using var multiplier = api.CreateMultiplier(coefficient);
        Console.WriteLine("Multiplier SafeHandle created");
        Console.WriteLine($"{number} multiplied by {coefficient} is {api.Multiply(multiplier, number)}");
    }
}
