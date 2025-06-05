namespace MediaApp;

using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Latency;
using Polly.Contrib.Simmy.Outcomes;
using Polly.Wrap;

public static class ChaosMonkeyPolicies
{
    public static InjectOutcomePolicy FaultPolicy(Exception fault, double probability) =>
        MonkeyPolicy.InjectException(with =>
            with.Fault(fault)
                .InjectionRate(probability)
                .Enabled(true)
        );

    public static InjectLatencyPolicy LatencyPolicy(TimeSpan latency, double probability) =>
        MonkeyPolicy.InjectLatency(with =>
            with.Latency(latency)
                .InjectionRate(probability)
                .Enabled(true)
        );

    public static PolicyWrap LatencyFaultPolicy(TimeSpan latency, double latencyProbability, Exception fault, double faultProbability) =>
        Policy.Wrap(LatencyPolicy(latency, latencyProbability), FaultPolicy(fault, faultProbability));
}
