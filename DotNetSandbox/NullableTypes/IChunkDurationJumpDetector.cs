namespace NullableTypes;

public interface IChunkDurationJumpDetector
{
    void LogIfChunkDurationJumpDetectedAndUpdateChunkDuration(object mediaData, object previouslyProcessedItems, Chunk chunk);
}
