using System.Threading.Tasks.Dataflow;

using ActionBlockSandbox;

// var actionBlock = new ActionBlock<Context>(c =>
// {
//     Thread.Sleep(1000);
//     Console.WriteLine($"Finished executing {c.Index}");
// });
//
// for (var i = 0; i < 5; i++)
// {
//     actionBlock.Post(new Context(i));
// }
//
// actionBlock.Complete();
// await actionBlock.Completion;
//
// Console.WriteLine("Completed everything");
//
// // ----
//
// var asyncActionBlock = new ActionBlock<Context>(c =>
// {
//     var task = Task.Delay(1000);
//     Console.WriteLine($"Finished async executing {c.Index}");
//     return task;
// });
//
// for (var i = 0; i < 5; i++)
// {
//     asyncActionBlock.Post(new Context(i));
// }
//
// asyncActionBlock.Complete();
// await asyncActionBlock.Completion;
//
// Console.WriteLine("Completed everything");

// ----


// await using var service = new BuggyService();
//
// for (var i = 0; i < 5; i++)
// {
//     service.BuggyActionBackground(new Context(i));
//     Console.WriteLine($"Posted {i}");
// }
//
// Thread.Sleep(10000);
//
// Console.WriteLine("Completed everything");

// ----

var updateMediaStateParams = new UpdateMediaStateParams();
updateMediaStateParams.sequenceNumber = null;

if (1 >= updateMediaStateParams.sequenceNumber)
{
    Console.WriteLine("true");
}

var actionBlockHolder = new ActionBlockHolder();
for (var i = 0; i < 100_000_000; i++)
{
    actionBlockHolder.IncreaseCounter();
}
actionBlockHolder.CompleteAndWaitForCompletion();
actionBlockHolder.PrintCounter();