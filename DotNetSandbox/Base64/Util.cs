namespace Base64;

public class Util
{
    private const int ChannelAlignment = 16;

    public static int CalculateMemorySizeInBytes(int channels, int frames)
    {
        var alignedFrames = ((frames * sizeof(float) + ChannelAlignment - 1) & ~(ChannelAlignment - 1)) / sizeof(float);
        return sizeof(float) * channels * alignedFrames;
    }
}
