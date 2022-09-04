namespace StatefulServiceProblem;

public interface ISessionEventHandler
{
    double AverageDurationOfEvents { get; }

    void HandleEvent(SessionEvent sessionEvent);
}
