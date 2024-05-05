using System.Collections.Immutable;

namespace Threading;

public class Participant
{
    private IImmutableSet<int> _states = ImmutableHashSet.Create(1);

    public bool UpdatePublishedStateTypes(IImmutableSet<int> newPublishedStateTypes, out IImmutableSet<int> oldPublishedStateTypes)
    {
        do
        {
            oldPublishedStateTypes = _states;
        } while (oldPublishedStateTypes != Interlocked.CompareExchange(ref _states, newPublishedStateTypes, oldPublishedStateTypes));
        
        Thread.Sleep(1000);

        return !newPublishedStateTypes.SetEquals(oldPublishedStateTypes);
    }
}