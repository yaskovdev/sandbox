using System.Collections.Immutable;
using System.Net;
using System.IO.Hashing;

namespace SlowHttpServer;

internal static class Program
{
    private static readonly byte[] Magic = [0xDE, 0xAD, 0xBE, 0xEF, 0xCA, 0xFE, 0xFA, 0xCE];

    // Inspired by https://learn.microsoft.com/en-us/dotnet/api/system.net.httplistener?view=net-10.0.
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        if (!HttpListener.IsSupported)
        {
            Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
            return;
        }

        string[] prefixes = ["http://localhost:8080/index/"];
        if (prefixes == null || prefixes.Length == 0)
            throw new ArgumentException("prefixes");

        // Create a listener.
        HttpListener listener = new HttpListener();
        // Add the prefixes.
        foreach (string s in prefixes)
        {
            listener.Prefixes.Add(s);
        }
        listener.Start();
        Console.WriteLine("Listening...");
        // Note: The GetContext method blocks while waiting for a request.
        HttpListenerContext context = await listener.GetContextAsync();
        HttpListenerRequest request = context.Request;
        // Obtain a response object.
        HttpListenerResponse response = context.Response;
        // Construct a response.
        byte[] buffer = await CreateBuffer([await CreateFrame([2, 2])]);
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        var output = response.OutputStream;
        await output.WriteAsync(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
        listener.Stop();
    }

    private static async Task WriteWithFraming(this Stream stream, byte[] payload, byte[] magic, CancellationToken cancellationToken)
    {
        await stream.WriteAsync(magic, cancellationToken);

        var size = BitConverter.GetBytes(payload.Length);
        await stream.WriteAsync(size, cancellationToken);
        var sizeHash = Crc32.HashToUInt32(size);
        await stream.WriteAsync(BitConverter.GetBytes(sizeHash), cancellationToken);
        
        await stream.WriteAsync(payload, cancellationToken);
        var payloadHash = Crc32.HashToUInt32(payload);
        await stream.WriteAsync(BitConverter.GetBytes(payloadHash), cancellationToken);
    }
    
    private static async Task<byte[]> CreateFrame(byte[] payload)
    {
        using var stream = new MemoryStream();
        await stream.WriteWithFraming(payload, Magic, CancellationToken.None);
        return stream.ToArray();
    }

    private static async Task<byte[]> CreateBuffer(IImmutableList<byte[]> frames)
    {
        using var stream = new MemoryStream();
        foreach (var frame in frames)
        {
            await stream.WriteAsync(frame);
        }

        return stream.ToArray();
    }
}