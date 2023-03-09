namespace ActionBlockSandbox;

using System.Threading.Tasks.Dataflow;

public class ActionBlockHolder
{
    private readonly ActionBlock<UpdateMediaStateParams> _offerAnswerActionBlock;
    private int _counter;

    public ActionBlockHolder() =>
        _offerAnswerActionBlock = new ActionBlock<UpdateMediaStateParams>(UpdateMediaStateInternal, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 2 });

    public void IncreaseCounter() => _offerAnswerActionBlock.Post(new UpdateMediaStateParams());

    public void CompleteAndWaitForCompletion()
    {
        _offerAnswerActionBlock.Complete();
        _offerAnswerActionBlock.Completion.Wait();
    }

    public void PrintCounter() => Console.WriteLine($"Counter is {_counter}");

    private Task UpdateMediaStateInternal(UpdateMediaStateParams args)
    {
        _counter++;
        return Task.CompletedTask;
    }
}
