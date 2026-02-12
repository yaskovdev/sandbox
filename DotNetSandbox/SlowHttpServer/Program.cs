using System.Net;

namespace SlowHttpServer;

internal static class Program
{
    // Inspired by https://learn.microsoft.com/en-us/dotnet/api/system.net.httplistener?view=net-10.0.
    public static void Main(string[] args)
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
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        // Obtain a response object.
        HttpListenerResponse response = context.Response;
        // Construct a response.
        string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        // Get a response stream and write the response to it.
        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        // You must close the output stream.
        output.Close();
        listener.Stop();
    }
}