using System.Threading.Tasks.Dataflow;
using ActionBlock;

var actionBlock = new ActionBlock<Context>(c =>
{
    Thread.Sleep(1000);
    Console.WriteLine($"Finished executing {c.Index}");
});

for (var i = 0; i < 5; i++)
{
    actionBlock.Post(new Context(i));
}

actionBlock.Complete();
await actionBlock.Completion;

Console.WriteLine("Completed everything");

// ----

var asyncActionBlock = new ActionBlock<Context>(c =>
{
    var task = Task.Delay(1000);
    Console.WriteLine($"Finished async executing {c.Index}");
    return task;
});

for (var i = 0; i < 5; i++)
{
    asyncActionBlock.Post(new Context(i));
}

asyncActionBlock.Complete();
await asyncActionBlock.Completion;

Console.WriteLine("Completed everything");