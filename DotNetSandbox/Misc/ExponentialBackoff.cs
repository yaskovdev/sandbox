namespace Misc;

public class ExponentialBackoff
{
    public static TimeSpan ExponentialBackoffRetryDelay(ServiceBusConsumerConfig config, int messageDeliveryCount)
    {
        var jitterMs = config.ExponentialBackoffMaxJitter > TimeSpan.Zero ? Random.Shared.NextDouble() * config.ExponentialBackoffMaxJitter.TotalMilliseconds : 0;
        var delayMs = config.ExponentialBackoffInitialDelay.TotalMilliseconds * Math.Pow(config.ExponentialBackoffGrowthFactor, messageDeliveryCount - 1);
        var cappedDelayMs = Math.Min(delayMs, config.ExponentialBackoffMaxDelay.TotalMilliseconds);
        return TimeSpan.FromMilliseconds(cappedDelayMs + jitterMs);
    }
}