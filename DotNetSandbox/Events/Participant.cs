namespace Events;

public class Participant : IParticipant
{
    public event Action? PublishedStatesChanged;

    public void Change()
    {
        PublishedStatesChanged?.Invoke();
    }
}
