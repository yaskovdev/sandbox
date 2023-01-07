using System.Threading.Tasks.Dataflow;

namespace ActionBlockSandbox
{
    public class BuggyService : IAsyncDisposable
    {
        private readonly ActionBlock<Context> _actionBlock;

        public BuggyService()
        {
            _actionBlock = new ActionBlock<Context>(BuggyAction);
        }

        public void BuggyActionBackground(Context context)
        {
            Console.WriteLine($"Posting {context.Index}");
            bool accepted = _actionBlock.Post(context);
            Console.WriteLine(accepted ? $"Accepted {context.Index}" : $"Did not accept {context.Index}");
        }

        private void BuggyAction(Context context)
        {
            if (context.Index == 2)
            {
                throw new Exception("Unexpected exception");
            }
            Console.WriteLine($"Processed {context.Index}");
        }

        public async ValueTask DisposeAsync()
        {
            _actionBlock.Complete();
            await _actionBlock.Completion;
        }
    }
}