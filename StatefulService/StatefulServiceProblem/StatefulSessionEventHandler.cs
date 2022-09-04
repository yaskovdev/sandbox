namespace StatefulServiceProblem;

/// <summary>
/// Contains both state and business logic (average duration calculation), just like OOP teaches us, right?
/// But cannot use DI to instantiate this class, have to instantiate by myself. If there is a tree of such classes,
/// have to create the whole tree by myself. Just imagine that the class needs 50 more dependencies to handle
/// events properly.
/// TODO: consider writing an article about this problem.
/// Note: APIs of both the services are the same. But they are implemented differently.
/// </summary>
public class StatefulSessionEventHandler : ISessionEventHandler
{
    private int TotalDurationOfEvents { get; set; }

    private int NumberOfEvents { get; set; }

    public double AverageDurationOfEvents { get; private set; }

    public void HandleEvent(SessionEvent sessionEvent)
    {
        NumberOfEvents += 1;
        TotalDurationOfEvents += sessionEvent.Duration;
        AverageDurationOfEvents = CalculateAverageDurationOfEvents(TotalDurationOfEvents, NumberOfEvents);
    }

    private static double CalculateAverageDurationOfEvents(int totalDurationOfEvents, int numberOfEvents) => (double)totalDurationOfEvents / numberOfEvents;
}
