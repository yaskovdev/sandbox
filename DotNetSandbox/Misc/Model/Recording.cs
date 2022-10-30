namespace Misc.Model;

public record Recording(IReadOnlyList<Chunk> Chunks, IReadOnlyList<Session> Sessions)
{
    private IReadOnlySet<int> IdsOfChunks => 
        Chunks
            .Select(it => it.Id)
            .ToHashSet();

    public IEnumerable<Session> SessionsWithChunks => 
        Sessions
            .Where(it => IdsOfChunks.Contains(it.ChunkId));
}
