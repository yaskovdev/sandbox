namespace NullableTypes;

public static class Program
{
    public static void Main()
    {
        var participant = new Participant();
        Console.WriteLine(participant.IsInLobby == true ? "The participant is in lobby" : "The participant is not in lobby");
    }
}
