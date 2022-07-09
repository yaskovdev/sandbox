using HttpClientWithRetry;

Console.WriteLine("Calling the endpoint");

using var client = new HttpClient(new HttpRetryMessageHandler(new HttpClientHandler()));
var response = await client.GetAsync("https://google.com/abc");
var body = await response.Content.ReadAsStringAsync();

Console.WriteLine($"Response body is: {body}");
