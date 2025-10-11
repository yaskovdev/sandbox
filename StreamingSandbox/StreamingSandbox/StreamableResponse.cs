namespace StreamingSandbox;

// TODO: accept Response and extract Stream and FullContentLengthInBytes internally
public record StreamableResponse(HttpResponseMessage Response, Stream Stream, long FullContentLengthInBytes) : IDisposable
{
    public void Dispose()
    {
        Response.Dispose();
    }
}