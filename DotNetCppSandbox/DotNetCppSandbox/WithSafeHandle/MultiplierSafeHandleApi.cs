namespace DotNetCppSandbox.WithSafeHandle;

using System.Runtime.InteropServices;

public class MultiplierSafeHandleApi
{
    public MultiplierSafeHandle CreateMultiplier(int coefficient) => CreateMultiplierExtern(coefficient);

    public int Multiply(MultiplierSafeHandle multiplier, int number) => MultiplyExtern(multiplier, number);

    [DllImport("NativeLibrary.dll", EntryPoint = "multiply")]
    private static extern int MultiplyExtern(MultiplierSafeHandle multiplier, int number);

    [DllImport("NativeLibrary.dll", EntryPoint = "create_multiplier")]
    private static extern MultiplierSafeHandle CreateMultiplierExtern(int coefficient);
}
