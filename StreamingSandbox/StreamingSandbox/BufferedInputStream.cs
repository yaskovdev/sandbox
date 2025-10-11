namespace StreamingSandbox;

public class BufferedInputStream(DocumentId documentId, IStreamableClient client, IStopwatch stopwatch, ILogger logger) : Stream
{
    private static readonly TimeSpan NoDataTimeout = TimeSpan.FromSeconds(60);
    private long _offset;
    private long _totalBytesCount = long.MaxValue;
    private StreamableResponse? _response;

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => _offset;
        set => throw new NotSupportedException();
    }

    public override void Flush()
    {
    }

    public override int Read(byte[] buffer, int offset, int count) => ReadAsync(buffer, offset, count).GetAwaiter().GetResult();

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (_offset >= _totalBytesCount)
        {
            logger.Info("All content has been read, returning 0 bytes");
            return 0;
        }

        if (_response is null)
        {
            logger.Info($"Current response is null, getting a new one for document ID {documentId} with offset {_offset}");
            _response = await client.GetDownloadStream(documentId, _offset, cancellationToken);
            _totalBytesCount = _response.FullContentLengthInBytes;
            stopwatch.Restart(); // stopwatch.Start() doesn't work here, because after reset it doesn't give the new request the 1 minute window to receive data 
        }

        var bytesRead = await _response.Stream.ReadAsync(buffer, cancellationToken);
        if (bytesRead > 0)
        {
            _offset += bytesRead;
            stopwatch.Restart();
        }
        else
        {
            var timeSinceLastDataReceived = stopwatch.Elapsed;
            if (timeSinceLastDataReceived >= NoDataTimeout)
            {
                logger.Info($"No data received for {timeSinceLastDataReceived}, closing the network stream");
                _response.Dispose();
                _response = null;
            }
        }

        return bytesRead;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
        await ReadAsync(buffer.AsMemory(offset, count), cancellationToken);

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _response?.Dispose();
        }

        base.Dispose(disposing);
    }
}