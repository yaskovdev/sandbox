namespace AzureStorageBlobsMetadata;

using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

public class BlobReader(AppendBlobClient blob)
{
    public async Task Start()
    {
        var stream = await blob.OpenReadAsync(new BlobOpenReadOptions(true));
        for (var i = 0; i < 1000; i++)
        {
            var buffer = new byte[1024];
            var numberOfBytesRead = await stream.ReadAsync(buffer);
            if (numberOfBytesRead == 0)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            else
            {
                Console.WriteLine($"Read {numberOfBytesRead} bytes from blob stream");
            }
        }
    }
}
