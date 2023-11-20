namespace HttpClient
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class FileDownloader
    {
        public async Task<long> DownloadFile(Uri uri)
        {
            using var client = new HttpClient();
            using var stream = await client.GetStreamAsync(uri);
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.Length;
        }
    }
}
