namespace MediaApp;

using System.Net;
using StackExchange.Redis;
using StackExchange.Redis.Maintenance;
using StackExchange.Redis.Profiling;

public class FaultyConnectionMultiplexerDecorator(IConnectionMultiplexer innerConnectionMultiplexer) : IConnectionMultiplexer
{
    public void Dispose()
    {
        innerConnectionMultiplexer.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return innerConnectionMultiplexer.DisposeAsync();
    }

    public void RegisterProfiler(Func<ProfilingSession?> profilingSessionProvider)
    {
        innerConnectionMultiplexer.RegisterProfiler(profilingSessionProvider);
    }

    public ServerCounters GetCounters()
    {
        return innerConnectionMultiplexer.GetCounters();
    }

    public EndPoint[] GetEndPoints(bool configuredOnly = false)
    {
        return innerConnectionMultiplexer.GetEndPoints(configuredOnly);
    }

    public void Wait(Task task)
    {
        innerConnectionMultiplexer.Wait(task);
    }

    public T Wait<T>(Task<T> task)
    {
        return innerConnectionMultiplexer.Wait(task);
    }

    public void WaitAll(params Task[] tasks)
    {
        innerConnectionMultiplexer.WaitAll(tasks);
    }

    public int HashSlot(RedisKey key)
    {
        return innerConnectionMultiplexer.HashSlot(key);
    }

    public ISubscriber GetSubscriber(object? asyncState = null)
    {
        return innerConnectionMultiplexer.GetSubscriber(asyncState);
    }

    public IDatabase GetDatabase(int db = -1, object? asyncState = null) =>
        new FaultyDatabaseDecorator(innerConnectionMultiplexer.GetDatabase(db, asyncState));

    public IServer GetServer(string host, int port, object? asyncState = null)
    {
        return innerConnectionMultiplexer.GetServer(host, port, asyncState);
    }

    public IServer GetServer(string hostAndPort, object? asyncState = null)
    {
        return innerConnectionMultiplexer.GetServer(hostAndPort, asyncState);
    }

    public IServer GetServer(IPAddress host, int port)
    {
        return innerConnectionMultiplexer.GetServer(host, port);
    }

    public IServer GetServer(EndPoint endpoint, object? asyncState = null)
    {
        return innerConnectionMultiplexer.GetServer(endpoint, asyncState);
    }

    public IServer[] GetServers()
    {
        return innerConnectionMultiplexer.GetServers();
    }

    public Task<bool> ConfigureAsync(TextWriter? log = null)
    {
        return innerConnectionMultiplexer.ConfigureAsync(log);
    }

    public bool Configure(TextWriter? log = null)
    {
        return innerConnectionMultiplexer.Configure(log);
    }

    public string GetStatus()
    {
        return innerConnectionMultiplexer.GetStatus();
    }

    public void GetStatus(TextWriter log)
    {
        innerConnectionMultiplexer.GetStatus(log);
    }

    public void Close(bool allowCommandsToComplete = true)
    {
        innerConnectionMultiplexer.Close(allowCommandsToComplete);
    }

    public Task CloseAsync(bool allowCommandsToComplete = true)
    {
        return innerConnectionMultiplexer.CloseAsync(allowCommandsToComplete);
    }

    public string? GetStormLog()
    {
        return innerConnectionMultiplexer.GetStormLog();
    }

    public void ResetStormLog()
    {
        innerConnectionMultiplexer.ResetStormLog();
    }

    public long PublishReconfigure(CommandFlags flags = CommandFlags.None)
    {
        return innerConnectionMultiplexer.PublishReconfigure(flags);
    }

    public Task<long> PublishReconfigureAsync(CommandFlags flags = CommandFlags.None)
    {
        return innerConnectionMultiplexer.PublishReconfigureAsync(flags);
    }

    public int GetHashSlot(RedisKey key)
    {
        return innerConnectionMultiplexer.GetHashSlot(key);
    }

    public void ExportConfiguration(Stream destination, ExportOptions options = ExportOptions.All)
    {
        innerConnectionMultiplexer.ExportConfiguration(destination, options);
    }

    public void AddLibraryNameSuffix(string suffix)
    {
        innerConnectionMultiplexer.AddLibraryNameSuffix(suffix);
    }

    public string ClientName => innerConnectionMultiplexer.ClientName;

    public string Configuration => innerConnectionMultiplexer.Configuration;

    public int TimeoutMilliseconds => innerConnectionMultiplexer.TimeoutMilliseconds;

    public long OperationCount => innerConnectionMultiplexer.OperationCount;

    public bool PreserveAsyncOrder
    {
        get => innerConnectionMultiplexer.PreserveAsyncOrder;
        set => innerConnectionMultiplexer.PreserveAsyncOrder = value;
    }

    public bool IsConnected => innerConnectionMultiplexer.IsConnected;

    public bool IsConnecting => innerConnectionMultiplexer.IsConnecting;

    public bool IncludeDetailInExceptions
    {
        get => innerConnectionMultiplexer.IncludeDetailInExceptions;
        set => innerConnectionMultiplexer.IncludeDetailInExceptions = value;
    }

    public int StormLogThreshold
    {
        get => innerConnectionMultiplexer.StormLogThreshold;
        set => innerConnectionMultiplexer.StormLogThreshold = value;
    }

    public event EventHandler<RedisErrorEventArgs>? ErrorMessage
    {
        add => innerConnectionMultiplexer.ErrorMessage += value;
        remove => innerConnectionMultiplexer.ErrorMessage -= value;
    }

    public event EventHandler<ConnectionFailedEventArgs>? ConnectionFailed
    {
        add => innerConnectionMultiplexer.ConnectionFailed += value;
        remove => innerConnectionMultiplexer.ConnectionFailed -= value;
    }

    public event EventHandler<InternalErrorEventArgs>? InternalError
    {
        add => innerConnectionMultiplexer.InternalError += value;
        remove => innerConnectionMultiplexer.InternalError -= value;
    }

    public event EventHandler<ConnectionFailedEventArgs>? ConnectionRestored
    {
        add => innerConnectionMultiplexer.ConnectionRestored += value;
        remove => innerConnectionMultiplexer.ConnectionRestored -= value;
    }

    public event EventHandler<EndPointEventArgs>? ConfigurationChanged
    {
        add => innerConnectionMultiplexer.ConfigurationChanged += value;
        remove => innerConnectionMultiplexer.ConfigurationChanged -= value;
    }

    public event EventHandler<EndPointEventArgs>? ConfigurationChangedBroadcast
    {
        add => innerConnectionMultiplexer.ConfigurationChangedBroadcast += value;
        remove => innerConnectionMultiplexer.ConfigurationChangedBroadcast -= value;
    }

    public event EventHandler<ServerMaintenanceEvent>? ServerMaintenanceEvent
    {
        add => innerConnectionMultiplexer.ServerMaintenanceEvent += value;
        remove => innerConnectionMultiplexer.ServerMaintenanceEvent -= value;
    }

    public event EventHandler<HashSlotMovedEventArgs>? HashSlotMoved
    {
        add => innerConnectionMultiplexer.HashSlotMoved += value;
        remove => innerConnectionMultiplexer.HashSlotMoved -= value;
    }
}
