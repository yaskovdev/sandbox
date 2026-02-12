namespace Misc;

public record ServiceBusConsumerConfig(
    TimeSpan ExponentialBackoffInitialDelay,
    int ExponentialBackoffGrowthFactor,
    TimeSpan ExponentialBackoffMaxDelay,
    TimeSpan ExponentialBackoffMaxJitter
);