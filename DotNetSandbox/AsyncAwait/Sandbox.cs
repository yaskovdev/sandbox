using System.Threading.Tasks;

namespace AsyncAwait;

public class Sandbox : ISandbox
{
    public async Task<int> Connect(bool create) => create ? await Create() : 0;

    private static async Task<int> Create() => 42;
}
