namespace StatefulService;

/// <summary>
/// Contains only state and no business logic. Bad OOP.
/// </summary>
public class Session
{
    public string Id { get; set; }

    public int TotalDurationOfEvents { get; set; }

    public int NumberOfEvents { get; set; }

    public double AverageDurationOfEvents { get; set; }
}
