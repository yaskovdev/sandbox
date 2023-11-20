namespace Events;

public interface IParticipant
{
    event Action? PublishedStatesChanged;
}
