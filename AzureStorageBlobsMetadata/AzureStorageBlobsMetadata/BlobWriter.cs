namespace AzureStorageBlobsMetadata;

using Azure.Storage.Blobs.Specialized;

public class BlobWriter(AppendBlobClient blob)
{
    public async Task Start(string id)
    {
        Stream? stream = await blob.OpenWriteAsync(false, null, CancellationToken.None);
        var payload = new byte[4 * 1024 * 1024]; // 2 MB
        var random = new Random();
        try
        {
            for (var i = 0; i < 1000; i++)
            {
                random.NextBytes(payload);
                Console.WriteLine($"Writing {payload.Length} bytes to blob stream from {id}");
                var readOnlyMemory = new ReadOnlyMemory<byte>(payload);
                var cancellationToken = CancellationToken.None;
                await stream.WriteAsync(readOnlyMemory, cancellationToken);
                // var keepalive = Guid.NewGuid().ToString();
                // Console.WriteLine($"Writing keepalive {keepalive} to metadata");
                // await blob.SetMetadataAsync(ImmutableDictionary<string, string>.Empty.Add("keepalive", "123"));
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error writing to blob stream in {id}: {e}");
        }
    }
}
