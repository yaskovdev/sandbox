namespace AzureServiceBusExponentialBackoff;

public record Scenario(string Description, int MessageCount, TimeSpan ExponentialBackoffBase, bool CrashStopAfterImmediatelyAfterSendingRefMessage, int FirstSuccessDeliveryCount);
