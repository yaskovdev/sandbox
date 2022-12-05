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
            _actionBlock.Post(context);
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
            // _actionBlock.Complete();
            // await _actionBlock.Completion;
        }
    }
}