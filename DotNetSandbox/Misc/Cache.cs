namespace Misc;

using System.Collections.Immutable;

public class Cache
{
    private readonly ReaderWriterLockSlim lockObject = new();

    private readonly IDictionary<string, Service> _dictionary = new Dictionary<string, Service>();

    public Service GetOrAdd(string key)
    {
        lockObject.EnterUpgradeableReadLock();
        try
        {
            if (_dictionary.TryGetValue(key, out var service))
            {
                return service;
            }
            lockObject.EnterWriteLock();
            try
            {
                var value = new Service();
                _dictionary.Add(key, value);
                return service;
            }
            finally
            {
                lockObject.ExitWriteLock();
            }
        }
        finally
        {
            lockObject.ExitUpgradeableReadLock();
        }
    }

    public async Task CallServices()
    {
        var services = GetServices();
        foreach (var service in services)
        {
            await service.DoStuffAsync();
        }
    }

    private Service[] GetServices()
    {
        lockObject.EnterReadLock();
        try
        {
            return _dictionary.Values.ToArray();
        }
        finally
        {
            lockObject.ExitReadLock();
        }
    }
}
