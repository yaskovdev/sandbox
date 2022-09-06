using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClient
{
    using HttpClient = System.Net.Http.HttpClient;

    public static class Program
    {
        public static async Task Main()
        {
            Console.WriteLine("Sending a GET request to ECS");

            var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip, CheckCertificateRevocationList = true };
            var httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromMinutes(1) };
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");

            HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://s2s.config.ecs.infra.gov.teams.microsoft.us/config/v1/ATM/1.0/Monitoring"));

            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
