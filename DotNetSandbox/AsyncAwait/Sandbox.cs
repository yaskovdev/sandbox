using System.Threading.Tasks;
using AsyncAwait;

namespace AsyncAwait;

public class Sandbox : ISandbox
{
    public async Task<int> Connect(bool create) => create ? await Create() : 0;

    private static async Task<int> Create() => 42;
}
