namespace StreamingSandbox;

public interface IStreamableClient
{
    Task<StreamableResponse> GetDownloadStream(DocumentId documentId, long offset, CancellationToken cancellationToken);
}