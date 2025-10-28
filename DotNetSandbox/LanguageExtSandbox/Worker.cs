using LanguageExt;

namespace LanguageExtSandbox;

public class Worker
{
    public IO<Outcome> HandleSuccess(IQueueItem item) =>
        HandleSpecialCase(item).Where(it => it == Outcome.Continue)
        | HandleRegularItem(item).Map(_ => Outcome.Continue);

    private IO<Outcome> HandleSpecialCase(IQueueItem item) =>
        Prelude.liftIO(async () =>
        {
            switch (item)
            {
                case SpecialQueueItem specialItem:
                    await DoSpecialStuffAsync(specialItem);
                    return Outcome.Continue;
                default:
                    return Outcome.NotSpecialCase;
            }
        });

    private IO<Unit> HandleRegularItem(IQueueItem item) =>
        Prelude.liftIO(async () => { await DoRegularStuffAsync(item); });

    private static async Task DoSpecialStuffAsync(SpecialQueueItem item)
    {
        Console.WriteLine($"DoSpecialStuffAsync for {item.GetType().Name}");
        await Task.CompletedTask;
    }

    private static async Task DoRegularStuffAsync(IQueueItem item)
    {
        Console.WriteLine($"DoRegularStuffAsync for {item.GetType().Name}");
        await Task.CompletedTask;
    }
}