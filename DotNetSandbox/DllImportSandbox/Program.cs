namespace DllImportSandbox;

using System.Reflection;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Creating compositor");
        string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string json = File.ReadAllText(Path.Combine(executableLocation, "Layout.json"));
        Console.WriteLine($"Json is {json}");
        var compositorPtr = CompositorDll.CreateCompositor(CompositorDll.CompositorType.COMPOSITOR_TYPE_JSON, new CompositorDll.LogFunction(OnCompositorLogging), IntPtr.Zero);

        for (int i = 0; i < 100; i++)
        {
            var descriptionPtr = CompositorDll.CreateCompositorDescription(json);
            CompositorDll.CompositorSetDescription(compositorPtr, descriptionPtr);
            CompositorDll.DestroyCompositorDescription(descriptionPtr);
        }

        Console.WriteLine($"Compositor is {compositorPtr}");
        CompositorDll.DestroyCompositor(compositorPtr);
        Console.ReadKey();
    }

    private static void OnCompositorLogging(CompositorDll.LogLevel logLevel, string message, IntPtr userData)
    {
        Console.WriteLine($"Compositor: {message}");
    }
}
