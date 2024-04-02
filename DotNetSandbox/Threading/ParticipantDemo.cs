using System.Collections.Immutable;

namespace Threading;

public class ParticipantDemo
{
    public void RunInSequence()
    {
        var participant = new Participant();
        Console.WriteLine(participant.UpdatePublishedStateTypes(ImmutableHashSet.Create(2), out _));
        Console.WriteLine(participant.UpdatePublishedStateTypes(ImmutableHashSet.Create(2), out _));
    }

    public void RunInParallel()
    {
        var participant = new Participant();

        var t1 = new Thread(() => { Console.WriteLine(participant.UpdatePublishedStateTypes(ImmutableHashSet.Create(2), out _)); });
        var t2 = new Thread(() => { Console.WriteLine(participant.UpdatePublishedStateTypes(ImmutableHashSet.Create(2), out _)); });
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }
}