using System.Net;
using NSubstitute;
using Shouldly;

namespace StreamingSandbox.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BufferedInputStreamTests
{
    private static readonly DocumentId DocumentId = new("77a5c552fa31e573e56e48bcbfcb3f84");

    [TestMethod]
    public async Task ShouldAllowReadingBytesInPages()
    {
        var asyncMediaStorageClient = Substitute.For<IStreamableClient>();
        asyncMediaStorageClient
            .GetDownloadStream(DocumentId, 0, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new StreamableResponse(new HttpResponseMessage(HttpStatusCode.OK), new MemoryStream([0, 1, 2, 3, 4, 5, 6, 7]), long.MaxValue)));
        asyncMediaStorageClient
            .GetDownloadStream(DocumentId, 8, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new StreamableResponse(new HttpResponseMessage(HttpStatusCode.OK), new MemoryStream([8, 9]), 10)));

        var stopwatch = new FakeStopwatch();

        await using var instanceUnderTest = new BufferedInputStream(DocumentId, asyncMediaStorageClient, stopwatch, Substitute.For<ILogger>());
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, [0, 1, 2, 3, 4, 5]);
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, [6, 7]);
        stopwatch.Elapsed = TimeSpan.Zero;
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []);
        stopwatch.Elapsed = TimeSpan.FromSeconds(120);
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []);
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, [8, 9]);
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []);

        await asyncMediaStorageClient
            .Received(2)
            .GetDownloadStream(DocumentId, Arg.Any<long>(), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task ShouldHandleWhenFullContentLengthIsEqualToTotalNumberOfBytesAlreadyRead()
    {
        var asyncMediaStorageClient = Substitute.For<IStreamableClient>();
        asyncMediaStorageClient
            .GetDownloadStream(DocumentId, 0, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new StreamableResponse(new HttpResponseMessage(HttpStatusCode.OK), new MemoryStream([0, 1, 2, 3, 4, 5]), long.MaxValue)));
        asyncMediaStorageClient
            .GetDownloadStream(DocumentId, 6, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new StreamableResponse(new HttpResponseMessage(HttpStatusCode.OK), new MemoryStream(), 6)));

        var stopwatch = new FakeStopwatch();

        await using var instanceUnderTest = new BufferedInputStream(DocumentId, asyncMediaStorageClient, stopwatch, Substitute.For<ILogger>());
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, [0, 1, 2, 3, 4, 5]);
        stopwatch.Elapsed = TimeSpan.FromSeconds(120);
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []); // sets _reset to true
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []); // opens new one and updates total bytes count
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []); // returns 0 as all content has been read

        await asyncMediaStorageClient
            .Received(2)
            .GetDownloadStream(DocumentId, Arg.Any<long>(), Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task ShouldTreatNotFoundAsNoData()
    {
        var asyncMediaStorageClient = Substitute.For<IStreamableClient>();
        asyncMediaStorageClient
            .GetDownloadStream(DocumentId, 0, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new StreamableResponse(new HttpResponseMessage(HttpStatusCode.NotFound), Stream.Null, long.MaxValue)));

        await using var instanceUnderTest = new BufferedInputStream(DocumentId, asyncMediaStorageClient, new FakeStopwatch(), Substitute.For<ILogger>());
        await ReadAndAssertExpectedBytes(instanceUnderTest, bytesToRead: 6, []);

        await asyncMediaStorageClient
            .Received(1)
            .GetDownloadStream(DocumentId, Arg.Any<long>(), Arg.Any<CancellationToken>());
    }

    private static async Task ReadAndAssertExpectedBytes(Stream stream, int bytesToRead, byte[] expected)
    {
        var buffer = new Memory<byte>(new byte[bytesToRead]);
        var count = await stream.ReadAsync(buffer);
        count.ShouldBe(expected.Length);
        buffer.Span[..count].ToArray().ShouldBe(expected);
    }
}
