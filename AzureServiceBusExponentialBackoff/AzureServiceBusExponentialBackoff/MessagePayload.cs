namespace AzureServiceBusExponentialBackoff;

public record MessagePayload(int Attempt, string Content);
