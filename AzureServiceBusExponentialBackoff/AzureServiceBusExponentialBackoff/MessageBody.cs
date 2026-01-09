namespace AzureServiceBusExponentialBackoff;

public record MessageBody(int Attempt, string Content);
