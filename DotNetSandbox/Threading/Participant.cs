using System.Collections.Immutable;

namespace Threading;

public class Participant
{
    private IImmutableSet<int> _states = ImmutableHashSet.Create(2);

    public bool UpdatePublishedStateTypes(IImmutableSet<int> newPublishedStateTypes, out IImmutableSet<int> oldPublishedStateTypes)
    {
        oldPublishedStateTypes = _states;
        Thread.Sleep(2000);
        while (oldPublishedStateTypes != Interlocked.CompareExchange(ref _states, newPublishedStateTypes, oldPublishedStateTypes))
        {
        }

        return !newPublishedStateTypes.SetEquals(oldPublishedStateTypes);
    }
}