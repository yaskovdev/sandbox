// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using System.Globalization;
using System.Numerics;
using System.Web;
using Misc;
using Misc.Model;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

new WeirdProblem().Swap(2, 5);
new WeirdProblem().Swap(3, 7);

using Frame? frame = null;
Console.WriteLine($"Used frame {frame}");

Console.WriteLine($"{new byte[] { 0, 1, 2 }.GetHashCode()} vs {new byte[] { 0, 1, 2 }.GetHashCode()}");
Console.WriteLine($"{new BigInteger(new byte[] { 0, 1, 2 }).GetHashCode()} vs {new BigInteger(new byte[] { 0, 1, 2 }).GetHashCode()}");

new NewLineTest().PrintStringLength();

// Thread.Sleep(5000);

Console.WriteLine(nameof(UserPreferences));

var recording = new Recording(new List<Chunk>() { new Chunk(0), new Chunk(1), new Chunk(2) }, new List<Session>() { new Session(1, "b"), new Session(2, "c") });
Console.WriteLine(recording);
Console.WriteLine(string.Join(", ", recording.SessionsWithChunks));

var uri = new Uri("https://microsofteur-my.sharepoint.com/personal/sergeiiaskov_microsoft_com/_layouts/15/download.aspx?UniqueId=f7b7cd45-c7f3-4f89-89c6-91078d905c6b&Translate=false&tempauth=eyJ0eXAiOiJKV1QiLCJhbGciOiJub25lIn0.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTBmZjEtY2UwMC0wMDAwMDAwMDAwMDAvbWljcm9zb2Z0ZXVyLW15LnNoYXJlcG9pbnQuY29tQDcyZjk4OGJmLTg2ZjEtNDFhZi05MWFiLTJkN2NkMDExZGI0NyIsImlzcyI6IjAwMDAwMDAzLTAwMDAtMGZmMS1jZTAwLTAwMDAwMDAwMDAwMCIsIm5iZiI6IjE2NjU0NzUyNzUiLCJleHAiOiIxNjY1NDk2ODc1IiwiZW5kcG9pbnR1cmwiOiJIcC85Z1FJenZQMi9IWWw5Q3FnSWxpbkVTNDZaeGZicFRuYm5Zekx5b0IwPSIsImVuZHBvaW50dXJsTGVuZ3RoIjoiMTYyIiwiaXNsb29wYmFjayI6IlRydWUiLCJjaWQiOiJZMkk0TXpaa1lUQXRaVEJtWWkwMU1EQXdMVFl4TVRVdE1HRTVOek5rTnpKa1lXSXgiLCJ2ZXIiOiJoYXNoZWRwcm9vZnRva2VuIiwic2l0ZWlkIjoiTWpZNE9EWmxNVGt0Tm1NM01pMDBNR00yTFdJM09HUXRORFZqTWpnM04yTTBZelZtIiwiYXBwX2Rpc3BsYXluYW1lIjoiVGVhbXMgTWVldGluZyBDbGllbnQiLCJnaXZlbl9uYW1lIjoiU2VyZ2VpIiwiZmFtaWx5X25hbWUiOiJJYXNrb3YiLCJzaWduaW5fc3RhdGUiOiJbXCJkdmNfY21wXCJdIiwiY29udHJvbHMiOiJbXCJhcHBfcmVzXCJdIiwiYXBwaWQiOiJBMDY0NzNDQi1BNDNCLTQwNUYtOUZDNC0wRTc3MkQ1NUYzRkQiLCJ0aWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJ1cG4iOiJzZXJnZWlpYXNrb3ZAbWljcm9zb2Z0LmNvbSIsInB1aWQiOiIxMDAzMjAwMTRFRDcxNjE4IiwiY2FjaGVrZXkiOiIwaC5mfG1lbWJlcnNoaXB8MTAwMzIwMDE0ZWQ3MTYxOEBsaXZlLmNvbSIsInNjcCI6InNpdGVzLnJlYWQuYWxsIiwidHQiOiIyIiwidXNlUGVyc2lzdGVudENvb2tpZSI6bnVsbCwiaXBhZGRyIjoiMTk0LjY5LjEwNC42In0.Z3NqbG5DM1dTcElMVHk1Sjk0elJwY0k0cXFVMG4zUHhwc3ZYYkVmV0EwST0&ApiVersion=2.0");
var query = HttpUtility.ParseQueryString(uri.Query);
query["tempauth"] = "pipka";
Console.WriteLine(new UriBuilder(uri) { Query = query.ToString() }.Uri);

int portOffset = 44000;
for (int i = 1; i <= 3; i++)
{
    string section = $@"
  signalling{i}:
    proto:
      https: 7777
  media{i}:
    remote_port: {portOffset + i}
    proto:
      tcp: 8445";
    Console.WriteLine(section);
}

Console.WriteLine(nameof(System.Numerics));

// List<Frame> oldWindowOfDownloadedFragments = null;
// Console.WriteLine($"Count is {oldWindowOfDownloadedFragments?.Count}");
//
// foreach (var f in oldWindowOfDownloadedFragments)
// {
//   
// }
//
// for (var indexInWindow = 0; indexInWindow < oldWindowOfDownloadedFragments?.Count; ++indexInWindow)
// {
//   
// }

var settings = new Settings();
settings.Type = MediaType.Video;
Console.WriteLine("Before " + settings.Type.GetValueOrDefault());
settings.Type = null;
Console.WriteLine("After " + settings.Type.GetValueOrDefault());

bool value = true;
string stringValue = value.ToString();
Console.WriteLine(stringValue);

// ----

IDictionary<string, string?>? dictionary = new Dictionary<string, string?>();
dictionary.Add("key", "value");

if ((dictionary?.TryGetValue("key", out var serviceFqdn) ?? false) && serviceFqdn != null)
{
    Console.WriteLine("Value exists in the dictionary");
}
else
{
    Console.WriteLine("Value does not exist in the dictionary");
}

// ----

List<string>? nullableList = null;
var contains = nullableList?.Contains("123") == true;
if (!contains)
{
    Console.WriteLine("Not contains!");
}

// ----

var emulated = bool.Parse(Environment.GetEnvironmentVariable("IS_EMULATED100500") ?? bool.FalseString);
Console.WriteLine("Emulated: " + emulated);

// ----

// throw new ArgumentNullException("param");

// ----

var classWithReadonlyStruct = new ClassWithReadonlyStruct();
classWithReadonlyStruct.PrintReadonlyStructValue();

// ----

var orderedIds = new List<OrderedId>();
var maxOrderedId = orderedIds.Max();
Console.WriteLine(maxOrderedId is null);

// ----

// var context = ExecutionContext.Capture();
// ExecutionContext.Run(null, async (context) => { Console.WriteLine("Hello from execution context!"); }, null);

// ----

new InterlockedSandbox().Run();

// ----

var eventSource = new CustomEventSource();
Console.WriteLine(eventSource.Name);

// ----

// object? arg = null;
// var state = (bool) arg;

// ----

var strings = new List<string> { "a", "b", "c" };
Console.WriteLine(string.Join(", ", strings));

// ----

const ulong unsignedLongValue = 1234567890;
Console.WriteLine(unsignedLongValue.ToString());
Console.WriteLine(unsignedLongValue.ToString(CultureInfo.InvariantCulture));

// ----

bool val = false;
Console.WriteLine(val.ToString() + " " + val.ToString(CultureInfo.InvariantCulture));

// ----


// await Task.Run(() =>
// {
//     Console.WriteLine("Hello from task!");
//     throw new NullReferenceException();
// });
//
// await Task.Delay(1000);

// ----

var deserialize = JsonConvert.DeserializeObject<Dummy>("{\"InstanceUuid\":\"newValue\",\"Title\":\"title\"}");
Console.WriteLine("Title: " + deserialize?.Title); // --> "Title: title"
Console.WriteLine("InstanceUuid: " + deserialize?.InstanceUuid); // --> "InstanceUuid: " <---- lost during deserialization

// ----

var ticks = DateTime.UtcNow.Ticks;
Console.WriteLine(ticks);

// ----

var cache = new Dictionary<Uri, string>
{
    [new Uri("http://example/foo")] = "foo",
    [new Uri("http://example/foo/")] = "bar"
};
Console.WriteLine($"The size of the cache is {cache.Count}");

// ----

// var max = Int32.MaxValue;
// var size = max + 1;
// var bytes = new byte[size];

// ----

// File.WriteAllBytes("/Users/yaskovdev/dev/test.bin", [10, 11, 12]);
// var stream = new FileStream("/Users/yaskovdev/dev/test.bin", FileMode.Open);
// var buffer = new byte[10];
// await stream.ReadExactlyAsync(buffer, 0, 1);
// await stream.ReadExactlyAsync(buffer, 0, 1);
// Console.WriteLine("Stop");

// var buffer2 = new byte[10];
// try
// {
//     await stream.ReadExactlyAsync(buffer, 0, 4);
// }
// catch (EndOfStreamException e)
// {
//     await stream.ReadExactlyAsync(buffer2, 0, 3);
// }
// ----

Util.DoStuff(2);

// ----

var dateTimeHolder = new DateTimeHolder();
dateTimeHolder.ChunkCreationDateTime = DateTime.UtcNow;
Console.WriteLine(dateTimeHolder.ChunkCreationDateTime);

// ----

Console.WriteLine(new Chunk(1));

// ---

var bufferedStream = new BufferedStream(new FileStream(@"c:\dev\output.bin", FileMode.Create), 4 * 1024 * 1024);
await bufferedStream.WriteAsync(new byte[] {0, 1, 2});
Console.WriteLine(bufferedStream.Position);