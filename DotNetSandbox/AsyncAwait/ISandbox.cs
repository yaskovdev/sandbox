using System.Threading.Tasks;

namespace AsyncAwait;

public interface ISandbox
{
    public Task<int> Connect(bool create);
}
