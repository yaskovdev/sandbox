namespace LoadBalancer;

using Yarp.ReverseProxy.LoadBalancing;
using Yarp.ReverseProxy.Model;

public class SessionAwareLoadBalancer : ILoadBalancingPolicy
{
    public string Name => "SessionAwareLoadBalancer";

    public DestinationState? PickDestination(HttpContext context, ClusterState cluster, IReadOnlyList<DestinationState> availableDestinations)
    {
        throw new NotImplementedException();
    }
}
