namespace DllImportSandbox;

using System.Reflection;
using System.Runtime.InteropServices;

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

        const int outputWidth = 1920;
        const int outputHeight = 1080;
        const int outputBufferLength = outputWidth * (outputHeight + outputHeight % 2) * 3 / 2;
        var outputPtr = Marshal.AllocHGlobal(outputBufferLength);
        try
        {
            var status = CompositorDll.CompositorComposeFrames(compositorPtr, DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond, Array.Empty<CompositorDll.Source>(), 0, outputPtr, outputPtr + outputWidth * outputHeight, outputWidth, outputHeight, outputWidth, outputWidth);
            Console.WriteLine($"Composed empty sources with status {status.type}, {status.code}");
        }
        finally
        {
            Marshal.FreeHGlobal(outputPtr);
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
