namespace AzureServiceBusExponentialBackoff;

using System.Collections.Immutable;

public record Config(IImmutableList<Scenario> Scenarios);
