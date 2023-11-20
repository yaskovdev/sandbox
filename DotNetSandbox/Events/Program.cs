namespace Events;

internal class Program
{
    public static void Main(string[] args)
    {
        new Program().Simulate();
    }

    private void Simulate()
    {
        var participant = new Participant();
        participant.PublishedStatesChanged += OnChange;
        participant.PublishedStatesChanged += OnChange;
        participant.Change();
    }

    private void OnChange()
    {
        Console.WriteLine("State changed");
    }
}
