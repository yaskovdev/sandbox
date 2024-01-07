// See https://aka.ms/new-console-template for more information

using Record;

Console.WriteLine("Hello, World!");

var updates = new List<AttendeeViewModesStateUpdate> { new(AttendeeViewMode.HostMode), new(AttendeeViewMode.Default) };
Console.WriteLine(string.Join(", ", updates));
