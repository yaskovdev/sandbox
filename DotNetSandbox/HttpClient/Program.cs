namespace HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class Program
    {
        private static readonly IList<string> Urls = new List<string>
        {
            "https://go.microsoft.com/fwlink/?LinkId=746572",
            "https://dot.net/v1/dotnet-install.ps1",
            "https://aka.ms/dotnet/6.0/dotnet-runtime-win-x64.zip",
            "https://dotnetcli.azureedge.net/dotnet/Runtime/6.0.21/dotnet-runtime-6.0.21-win-x64.zip",
            "https://dotnetcli.azureedge.net/dotnet/Runtime/6.0.21/runtime-productVersion.txt",
            "https://dotnetcli.azureedge.net/dotnet/Runtime/6.0.21/productVersion.txt",
            "https://aka.ms/dotnet/6.0/aspnetcore-runtime-win-x64.zip",
            "https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/6.0.21/aspnetcore-runtime-6.0.21-win-x64.zip",
            "https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/6.0.21/productVersion.txt",
            "https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/6.0.21/aspnetcore-productVersion.txt"
        };

        public static async Task Main()
        {
            // Console.WriteLine("Sending a GET request to ECS");
            //
            // var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip, CheckCertificateRevocationList = true };
            // var httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };
            // httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            //
            // HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://s2s.config.ecs.infra.gov.teams.microsoft.us/config/v1/ATM/1.0/Monitoring"));
            //
            // Console.WriteLine(await response.Content.ReadAsStringAsync());

            // ----

            var downloader = new FileDownloader();
            foreach (var url in Urls)
            {
                Console.WriteLine("URL is " + url);
                try
                {
                    Console.WriteLine("Downloaded " + await downloader.DownloadFile(new Uri(url)) + " bytes from the URL " + url);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error happened" + e);
                }
            }
        }
    }
}
