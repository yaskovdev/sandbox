namespace AzureStorageBlobsMetadata;

using System.Collections.Immutable;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

internal static class Program
{
    private const long WriteBufferSizeInBytes = 100 * 1024 * 1024;

    public static async Task Main(string[] args)
    {
        
        
        var blobContainerName = $"container-{Guid.NewGuid()}";
        var blobServiceClient = new BlobServiceClient("UseDevelopmentStorage=true");
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
        var containerCreateResponse = await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.None, ImmutableDictionary<string, string>.Empty, CancellationToken.None);
        if (containerCreateResponse == null)
        {
            throw new Exception($"Unable to create blob container: container {blobContainerName} already exists");
        }
        Console.WriteLine($"Container created: {containerCreateResponse.Value}");
        var blob = blobContainerClient.GetAppendBlobClient($"blob-{Guid.NewGuid()}");

        var writerTask = Task.Run(async () =>
        {
            Console.WriteLine("Creating writer");
            var writer = new BlobWriter(blob);
            await writer.Start();
        });
        // var readerTask = Task.Run(async () =>
        // {
        //     await Task.Delay(5000);
        //     Console.WriteLine("Creating reader");
        //     var reader = new BlobReader(blob);
        //     await reader.Start();
        // });
        await Task.WhenAll(writerTask);
    }
}
