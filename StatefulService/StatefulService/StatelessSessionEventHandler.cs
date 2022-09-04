namespace StatefulService;

/// <summary>
/// Contains only business logic (average duration calculation) and no state. Bad OOP.
/// Also, need to pass session ID to HandleEvent and to all the subsequent calls (if any).
/// </summary>
public class StatelessSessionEventHandler : ISessionEventHandler
{
    private readonly ISessionRepository repository;

    public StatelessSessionEventHandler(ISessionRepository repository) => this.repository = repository;

    public void HandleEvent(string sessionId, SessionEvent sessionEvent)
    {
        var session = repository.GetSession(sessionId);
        session.NumberOfEvents += 1;
        session.TotalDurationOfEvents += sessionEvent.Duration;
        session.AverageDurationOfEvents = CalculateAverageDurationOfEvents(session.TotalDurationOfEvents, session.NumberOfEvents);
    }

    private static double CalculateAverageDurationOfEvents(int totalDurationOfEvents, int numberOfEvents) => (double)totalDurationOfEvents / numberOfEvents;
}
