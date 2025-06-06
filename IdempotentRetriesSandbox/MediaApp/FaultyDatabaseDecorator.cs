namespace MediaApp;

using System.Net;
using StackExchange.Redis;

public class FaultyDatabaseDecorator(IDatabase innerDatabase) : IDatabase
{
    public Task<TimeSpan> PingAsync(CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.PingAsync(flags);
    }

    public bool TryWait(Task task)
    {
        return innerDatabase.TryWait(task);
    }

    public void Wait(Task task)
    {
        innerDatabase.Wait(task);
    }

    public T Wait<T>(Task<T> task)
    {
        return innerDatabase.Wait(task);
    }

    public void WaitAll(params Task[] tasks)
    {
        innerDatabase.WaitAll(tasks);
    }

    public IConnectionMultiplexer Multiplexer => innerDatabase.Multiplexer;

    public TimeSpan Ping(CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.Ping(flags);
    }

    public bool IsConnected(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.IsConnected(key, flags);
    }

    public Task KeyMigrateAsync(RedisKey key, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyMigrateAsync(key, toServer, toDatabase, timeoutMilliseconds, migrateOptions, flags);
    }

    public Task<RedisValue> DebugObjectAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.DebugObjectAsync(key, flags);
    }

    public Task<bool> GeoAddAsync(RedisKey key, double longitude, double latitude, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoAddAsync(key, longitude, latitude, member, flags);
    }

    public Task<bool> GeoAddAsync(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoAddAsync(key, value, flags);
    }

    public Task<long> GeoAddAsync(RedisKey key, GeoEntry[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoAddAsync(key, values, flags);
    }

    public Task<bool> GeoRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoRemoveAsync(key, member, flags);
    }

    public Task<double?> GeoDistanceAsync(RedisKey key, RedisValue member1, RedisValue member2, GeoUnit unit = GeoUnit.Meters, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoDistanceAsync(key, member1, member2, unit, flags);
    }

    public Task<string?[]> GeoHashAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoHashAsync(key, members, flags);
    }

    public Task<string?> GeoHashAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoHashAsync(key, member, flags);
    }

    public Task<GeoPosition?[]> GeoPositionAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoPositionAsync(key, members, flags);
    }

    public Task<GeoPosition?> GeoPositionAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoPositionAsync(key, member, flags);
    }

    public Task<GeoRadiusResult[]> GeoRadiusAsync(RedisKey key, RedisValue member, double radius, GeoUnit unit = GeoUnit.Meters, int count = -1, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoRadiusAsync(key, member, radius, unit, count, order, options, flags);
    }

    public Task<GeoRadiusResult[]> GeoRadiusAsync(RedisKey key, double longitude, double latitude, double radius, GeoUnit unit = GeoUnit.Meters, int count = -1, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoRadiusAsync(key, longitude, latitude, radius, unit, count, order, options, flags);
    }

    public Task<GeoRadiusResult[]> GeoSearchAsync(RedisKey key, RedisValue member, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearchAsync(key, member, shape, count, demandClosest, order, options, flags);
    }

    public Task<GeoRadiusResult[]> GeoSearchAsync(RedisKey key, double longitude, double latitude, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearchAsync(key, longitude, latitude, shape, count, demandClosest, order, options, flags);
    }

    public Task<long> GeoSearchAndStoreAsync(RedisKey sourceKey, RedisKey destinationKey, RedisValue member, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, bool storeDistances = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearchAndStoreAsync(sourceKey, destinationKey, member, shape, count, demandClosest, order, storeDistances, flags);
    }

    public Task<long> GeoSearchAndStoreAsync(RedisKey sourceKey, RedisKey destinationKey, double longitude, double latitude, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, bool storeDistances = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearchAndStoreAsync(sourceKey, destinationKey, longitude, latitude, shape, count, demandClosest, order, storeDistances, flags);
    }

    public Task<long> HashDecrementAsync(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDecrementAsync(key, hashField, value, flags);
    }

    public Task<double> HashDecrementAsync(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDecrementAsync(key, hashField, value, flags);
    }

    public Task<bool> HashDeleteAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDeleteAsync(key, hashField, flags);
    }

    public Task<long> HashDeleteAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDeleteAsync(key, hashFields, flags);
    }

    public Task<bool> HashExistsAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashExistsAsync(key, hashField, flags);
    }

    public Task<ExpireResult[]> HashFieldExpireAsync(RedisKey key, RedisValue[] hashFields, TimeSpan expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldExpireAsync(key, hashFields, expiry, when, flags);
    }

    public Task<ExpireResult[]> HashFieldExpireAsync(RedisKey key, RedisValue[] hashFields, DateTime expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldExpireAsync(key, hashFields, expiry, when, flags);
    }

    public Task<long[]> HashFieldGetExpireDateTimeAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldGetExpireDateTimeAsync(key, hashFields, flags);
    }

    public Task<PersistResult[]> HashFieldPersistAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldPersistAsync(key, hashFields, flags);
    }

    public Task<long[]> HashFieldGetTimeToLiveAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldGetTimeToLiveAsync(key, hashFields, flags);
    }

    public Task<RedisValue> HashGetAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGetAsync(key, hashField, flags);
    }

    public Task<Lease<byte>?> HashGetLeaseAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGetLeaseAsync(key, hashField, flags);
    }

    public Task<RedisValue[]> HashGetAsync(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGetAsync(key, hashFields, flags);
    }

    public Task<HashEntry[]> HashGetAllAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGetAllAsync(key, flags);
    }

    public Task<long> HashIncrementAsync(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashIncrementAsync(key, hashField, value, flags);
    }

    public Task<double> HashIncrementAsync(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashIncrementAsync(key, hashField, value, flags);
    }

    public Task<RedisValue[]> HashKeysAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashKeysAsync(key, flags);
    }

    public Task<long> HashLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashLengthAsync(key, flags);
    }

    public Task<RedisValue> HashRandomFieldAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashRandomFieldAsync(key, flags);
    }

    public Task<RedisValue[]> HashRandomFieldsAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashRandomFieldsAsync(key, count, flags);
    }

    public Task<HashEntry[]> HashRandomFieldsWithValuesAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashRandomFieldsWithValuesAsync(key, count, flags);
    }

    public IAsyncEnumerable<HashEntry> HashScanAsync(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public IAsyncEnumerable<RedisValue> HashScanNoValuesAsync(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashScanNoValuesAsync(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public Task HashSetAsync(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashSetAsync(key, hashFields, flags);
    }

    public Task<bool> HashSetAsync(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashSetAsync(key, hashField, value, when, flags);
    }

    public Task<long> HashStringLengthAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashStringLengthAsync(key, hashField, flags);
    }

    public Task<RedisValue[]> HashValuesAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashValuesAsync(key, flags);
    }

    public Task<bool> HyperLogLogAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogAddAsync(key, value, flags);
    }

    public Task<bool> HyperLogLogAddAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogAddAsync(key, values, flags);
    }

    public Task<long> HyperLogLogLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogLengthAsync(key, flags);
    }

    public Task<long> HyperLogLogLengthAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogLengthAsync(keys, flags);
    }

    public Task HyperLogLogMergeAsync(RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogMergeAsync(destination, first, second, flags);
    }

    public Task HyperLogLogMergeAsync(RedisKey destination, RedisKey[] sourceKeys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogMergeAsync(destination, sourceKeys, flags);
    }

    public Task<EndPoint?> IdentifyEndpointAsync(RedisKey key = new RedisKey(), CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.IdentifyEndpointAsync(key, flags);
    }

    public Task<bool> KeyCopyAsync(RedisKey sourceKey, RedisKey destinationKey, int destinationDatabase = -1, bool replace = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyCopyAsync(sourceKey, destinationKey, destinationDatabase, replace, flags);
    }

    public Task<bool> KeyDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyDeleteAsync(key, flags);
    }

    public Task<long> KeyDeleteAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyDeleteAsync(keys, flags);
    }

    public Task<byte[]?> KeyDumpAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyDumpAsync(key, flags);
    }

    public Task<string?> KeyEncodingAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyEncodingAsync(key, flags);
    }

    public Task<bool> KeyExistsAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExistsAsync(key, flags);
    }

    public Task<long> KeyExistsAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExistsAsync(keys, flags);
    }

    public Task<bool> KeyExpireAsync(RedisKey key, TimeSpan? expiry, CommandFlags flags)
    {
        return innerDatabase.KeyExpireAsync(key, expiry, flags);
    }

    public Task<bool> KeyExpireAsync(RedisKey key, TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExpireAsync(key, expiry, when, flags);
    }

    public Task<bool> KeyExpireAsync(RedisKey key, DateTime? expiry, CommandFlags flags)
    {
        return innerDatabase.KeyExpireAsync(key, expiry, flags);
    }

    public Task<bool> KeyExpireAsync(RedisKey key, DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExpireAsync(key, expiry, when, flags);
    }

    public Task<DateTime?> KeyExpireTimeAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExpireTimeAsync(key, flags);
    }

    public Task<long?> KeyFrequencyAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyFrequencyAsync(key, flags);
    }

    public Task<TimeSpan?> KeyIdleTimeAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyIdleTimeAsync(key, flags);
    }

    public Task<bool> KeyMoveAsync(RedisKey key, int database, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyMoveAsync(key, database, flags);
    }

    public Task<bool> KeyPersistAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyPersistAsync(key, flags);
    }

    public Task<RedisKey> KeyRandomAsync(CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRandomAsync(flags);
    }

    public Task<long?> KeyRefCountAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRefCountAsync(key, flags);
    }

    public Task<bool> KeyRenameAsync(RedisKey key, RedisKey newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRenameAsync(key, newKey, when, flags);
    }

    public Task KeyRestoreAsync(RedisKey key, byte[] value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRestoreAsync(key, value, expiry, flags);
    }

    public Task<TimeSpan?> KeyTimeToLiveAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTimeToLiveAsync(key, flags);
    }

    public Task<bool> KeyTouchAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTouchAsync(key, flags);
    }

    public Task<long> KeyTouchAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTouchAsync(keys, flags);
    }

    public Task<RedisType> KeyTypeAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTypeAsync(key, flags);
    }

    public Task<RedisValue> ListGetByIndexAsync(RedisKey key, long index, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListGetByIndexAsync(key, index, flags);
    }

    public Task<long> ListInsertAfterAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListInsertAfterAsync(key, pivot, value, flags);
    }

    public Task<long> ListInsertBeforeAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListInsertBeforeAsync(key, pivot, value, flags);
    }

    public Task<RedisValue> ListLeftPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPopAsync(key, flags);
    }

    public Task<RedisValue[]> ListLeftPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPopAsync(key, count, flags);
    }

    public Task<ListPopResult> ListLeftPopAsync(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPopAsync(keys, count, flags);
    }

    public Task<long> ListPositionAsync(RedisKey key, RedisValue element, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListPositionAsync(key, element, rank, maxLength, flags);
    }

    public Task<long[]> ListPositionsAsync(RedisKey key, RedisValue element, long count, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListPositionsAsync(key, element, count, rank, maxLength, flags);
    }

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPushAsync(key, value, when, flags);
    }

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPushAsync(key, values, when, flags);
    }

    public Task<long> ListLeftPushAsync(RedisKey key, RedisValue[] values, CommandFlags flags)
    {
        return innerDatabase.ListLeftPushAsync(key, values, flags);
    }

    public Task<long> ListLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLengthAsync(key, flags);
    }

    public Task<RedisValue> ListMoveAsync(RedisKey sourceKey, RedisKey destinationKey, ListSide sourceSide, ListSide destinationSide, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListMoveAsync(sourceKey, destinationKey, sourceSide, destinationSide, flags);
    }

    public Task<RedisValue[]> ListRangeAsync(RedisKey key, long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRangeAsync(key, start, stop, flags);
    }

    public Task<long> ListRemoveAsync(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRemoveAsync(key, value, count, flags);
    }

    public Task<RedisValue> ListRightPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPopAsync(key, flags);
    }

    public Task<RedisValue[]> ListRightPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPopAsync(key, count, flags);
    }

    public Task<ListPopResult> ListRightPopAsync(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPopAsync(keys, count, flags);
    }

    public Task<RedisValue> ListRightPopLeftPushAsync(RedisKey source, RedisKey destination, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPopLeftPushAsync(source, destination, flags);
    }

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPushAsync(key, value, when, flags);
    }

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPushAsync(key, values, when, flags);
    }

    public Task<long> ListRightPushAsync(RedisKey key, RedisValue[] values, CommandFlags flags)
    {
        return innerDatabase.ListRightPushAsync(key, values, flags);
    }

    public Task ListSetByIndexAsync(RedisKey key, long index, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListSetByIndexAsync(key, index, value, flags);
    }

    public Task ListTrimAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListTrimAsync(key, start, stop, flags);
    }

    public Task<bool> LockExtendAsync(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockExtendAsync(key, value, expiry, flags);
    }

    public Task<RedisValue> LockQueryAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockQueryAsync(key, flags);
    }

    public Task<bool> LockReleaseAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockReleaseAsync(key, value, flags);
    }

    public Task<bool> LockTakeAsync(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockTakeAsync(key, value, expiry, flags);
    }

    public Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.PublishAsync(channel, message, flags);
    }

    public Task<RedisResult> ExecuteAsync(string command, params object[] args)
    {
        return innerDatabase.ExecuteAsync(command, args);
    }

    public Task<RedisResult> ExecuteAsync(string command, ICollection<object>? args, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ExecuteAsync(command, args, flags);
    }

    public Task<RedisResult> ScriptEvaluateAsync(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateAsync(script, keys, values, flags);
    }

    public Task<RedisResult> ScriptEvaluateAsync(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateAsync(hash, keys, values, flags);
    }

    public Task<RedisResult> ScriptEvaluateAsync(LuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateAsync(script, parameters, flags);
    }

    public Task<RedisResult> ScriptEvaluateAsync(LoadedLuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateAsync(script, parameters, flags);
    }

    public Task<RedisResult> ScriptEvaluateReadOnlyAsync(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateReadOnlyAsync(script, keys, values, flags);
    }

    public Task<RedisResult> ScriptEvaluateReadOnlyAsync(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateReadOnlyAsync(hash, keys, values, flags);
    }

    public Task<bool> SetAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetAddAsync(key, value, flags);
    }

    public Task<long> SetAddAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetAddAsync(key, values, flags);
    }

    public Task<RedisValue[]> SetCombineAsync(SetOperation operation, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombineAsync(operation, first, second, flags);
    }

    public Task<RedisValue[]> SetCombineAsync(SetOperation operation, RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombineAsync(operation, keys, flags);
    }

    public Task<long> SetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombineAndStoreAsync(operation, destination, first, second, flags);
    }

    public Task<long> SetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombineAndStoreAsync(operation, destination, keys, flags);
    }

    public Task<bool> SetContainsAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetContainsAsync(key, value, flags);
    }

    public Task<bool[]> SetContainsAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetContainsAsync(key, values, flags);
    }

    public Task<long> SetIntersectionLengthAsync(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetIntersectionLengthAsync(keys, limit, flags);
    }

    public Task<long> SetLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetLengthAsync(key, flags);
    }

    public Task<RedisValue[]> SetMembersAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetMembersAsync(key, flags);
    }

    public Task<bool> SetMoveAsync(RedisKey source, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetMoveAsync(source, destination, value, flags);
    }

    public Task<RedisValue> SetPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetPopAsync(key, flags);
    }

    public Task<RedisValue[]> SetPopAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetPopAsync(key, count, flags);
    }

    public Task<RedisValue> SetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRandomMemberAsync(key, flags);
    }

    public Task<RedisValue[]> SetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRandomMembersAsync(key, count, flags);
    }

    public Task<bool> SetRemoveAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRemoveAsync(key, value, flags);
    }

    public Task<long> SetRemoveAsync(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRemoveAsync(key, values, flags);
    }

    public IAsyncEnumerable<RedisValue> SetScanAsync(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public Task<RedisValue[]> SortAsync(RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = new RedisValue(), RedisValue[]? get = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortAsync(key, skip, take, order, sortType, by, get, flags);
    }

    public Task<long> SortAndStoreAsync(RedisKey destination, RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = new RedisValue(), RedisValue[]? get = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortAndStoreAsync(destination, key, skip, take, order, sortType, by, get, flags);
    }

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, CommandFlags flags)
    {
        return innerDatabase.SortedSetAddAsync(key, member, score, flags);
    }

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, When when, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAddAsync(key, member, score, when, flags);
    }

    public Task<bool> SortedSetAddAsync(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAddAsync(key, member, score, when, flags);
    }

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, CommandFlags flags)
    {
        return innerDatabase.SortedSetAddAsync(key, values, flags);
    }

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, When when, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAddAsync(key, values, when, flags);
    }

    public Task<long> SortedSetAddAsync(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAddAsync(key, values, when, flags);
    }

    public Task<RedisValue[]> SortedSetCombineAsync(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineAsync(operation, keys, weights, aggregate, flags);
    }

    public Task<SortedSetEntry[]> SortedSetCombineWithScoresAsync(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineWithScoresAsync(operation, keys, weights, aggregate, flags);
    }

    public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineAndStoreAsync(operation, destination, first, second, aggregate, flags);
    }

    public Task<long> SortedSetCombineAndStoreAsync(SetOperation operation, RedisKey destination, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineAndStoreAsync(operation, destination, keys, weights, aggregate, flags);
    }

    public Task<double> SortedSetDecrementAsync(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetDecrementAsync(key, member, value, flags);
    }

    public Task<double> SortedSetIncrementAsync(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetIncrementAsync(key, member, value, flags);
    }

    public Task<long> SortedSetIntersectionLengthAsync(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetIntersectionLengthAsync(keys, limit, flags);
    }

    public Task<long> SortedSetLengthAsync(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetLengthAsync(key, min, max, exclude, flags);
    }

    public Task<long> SortedSetLengthByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetLengthByValueAsync(key, min, max, exclude, flags);
    }

    public Task<RedisValue> SortedSetRandomMemberAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRandomMemberAsync(key, flags);
    }

    public Task<RedisValue[]> SortedSetRandomMembersAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRandomMembersAsync(key, count, flags);
    }

    public Task<SortedSetEntry[]> SortedSetRandomMembersWithScoresAsync(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRandomMembersWithScoresAsync(key, count, flags);
    }

    public Task<RedisValue[]> SortedSetRangeByRankAsync(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByRankAsync(key, start, stop, order, flags);
    }

    public Task<long> SortedSetRangeAndStoreAsync(RedisKey sourceKey, RedisKey destinationKey, RedisValue start, RedisValue stop, SortedSetOrder sortedSetOrder = SortedSetOrder.ByRank, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long? take = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeAndStoreAsync(sourceKey, destinationKey, start, stop, sortedSetOrder, exclude, order, skip, take, flags);
    }

    public Task<SortedSetEntry[]> SortedSetRangeByRankWithScoresAsync(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByRankWithScoresAsync(key, start, stop, order, flags);
    }

    public Task<RedisValue[]> SortedSetRangeByScoreAsync(RedisKey key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByScoreAsync(key, start, stop, exclude, order, skip, take, flags);
    }

    public Task<SortedSetEntry[]> SortedSetRangeByScoreWithScoresAsync(RedisKey key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByScoreWithScoresAsync(key, start, stop, exclude, order, skip, take, flags);
    }

    public Task<RedisValue[]> SortedSetRangeByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude, long skip, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByValueAsync(key, min, max, exclude, skip, take, flags);
    }

    public Task<RedisValue[]> SortedSetRangeByValueAsync(RedisKey key, RedisValue min = new RedisValue(), RedisValue max = new RedisValue(), Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByValueAsync(key, min, max, exclude, order, skip, take, flags);
    }

    public Task<long?> SortedSetRankAsync(RedisKey key, RedisValue member, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRankAsync(key, member, order, flags);
    }

    public Task<bool> SortedSetRemoveAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveAsync(key, member, flags);
    }

    public Task<long> SortedSetRemoveAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveAsync(key, members, flags);
    }

    public Task<long> SortedSetRemoveRangeByRankAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveRangeByRankAsync(key, start, stop, flags);
    }

    public Task<long> SortedSetRemoveRangeByScoreAsync(RedisKey key, double start, double stop, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveRangeByScoreAsync(key, start, stop, exclude, flags);
    }

    public Task<long> SortedSetRemoveRangeByValueAsync(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveRangeByValueAsync(key, min, max, exclude, flags);
    }

    public IAsyncEnumerable<SortedSetEntry> SortedSetScanAsync(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetScanAsync(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public Task<double?> SortedSetScoreAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetScoreAsync(key, member, flags);
    }

    public Task<double?[]> SortedSetScoresAsync(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetScoresAsync(key, members, flags);
    }

    public Task<bool> SortedSetUpdateAsync(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetUpdateAsync(key, member, score, when, flags);
    }

    public Task<long> SortedSetUpdateAsync(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetUpdateAsync(key, values, when, flags);
    }

    public Task<SortedSetEntry?> SortedSetPopAsync(RedisKey key, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetPopAsync(key, order, flags);
    }

    public Task<SortedSetEntry[]> SortedSetPopAsync(RedisKey key, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetPopAsync(key, count, order, flags);
    }

    public Task<SortedSetPopResult> SortedSetPopAsync(RedisKey[] keys, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetPopAsync(keys, count, order, flags);
    }

    public Task<long> StreamAcknowledgeAsync(RedisKey key, RedisValue groupName, RedisValue messageId, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAcknowledgeAsync(key, groupName, messageId, flags);
    }

    public Task<long> StreamAcknowledgeAsync(RedisKey key, RedisValue groupName, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAcknowledgeAsync(key, groupName, messageIds, flags);
    }

    public Task<RedisValue> StreamAddAsync(RedisKey key, RedisValue streamField, RedisValue streamValue, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAddAsync(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, flags);
    }

    public Task<RedisValue> StreamAddAsync(RedisKey key, NameValueEntry[] streamPairs, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAddAsync(key, streamPairs, messageId, maxLength, useApproximateMaxLength, flags);
    }

    public Task<StreamAutoClaimResult> StreamAutoClaimAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAutoClaimAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);
    }

    public Task<StreamAutoClaimIdsOnlyResult> StreamAutoClaimIdsOnlyAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAutoClaimIdsOnlyAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);
    }

    public Task<StreamEntry[]> StreamClaimAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamClaimAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);
    }

    public Task<RedisValue[]> StreamClaimIdsOnlyAsync(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamClaimIdsOnlyAsync(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);
    }

    public Task<bool> StreamConsumerGroupSetPositionAsync(RedisKey key, RedisValue groupName, RedisValue position, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamConsumerGroupSetPositionAsync(key, groupName, position, flags);
    }

    public Task<StreamConsumerInfo[]> StreamConsumerInfoAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamConsumerInfoAsync(key, groupName, flags);
    }

    public Task<bool> StreamCreateConsumerGroupAsync(RedisKey key, RedisValue groupName, RedisValue? position, CommandFlags flags)
    {
        return innerDatabase.StreamCreateConsumerGroupAsync(key, groupName, position, flags);
    }

    public Task<bool> StreamCreateConsumerGroupAsync(RedisKey key, RedisValue groupName, RedisValue? position = null, bool createStream = true, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamCreateConsumerGroupAsync(key, groupName, position, createStream, flags);
    }

    public Task<long> StreamDeleteAsync(RedisKey key, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamDeleteAsync(key, messageIds, flags);
    }

    public Task<long> StreamDeleteConsumerAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamDeleteConsumerAsync(key, groupName, consumerName, flags);
    }

    public Task<bool> StreamDeleteConsumerGroupAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamDeleteConsumerGroupAsync(key, groupName, flags);
    }

    public Task<StreamGroupInfo[]> StreamGroupInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamGroupInfoAsync(key, flags);
    }

    public Task<StreamInfo> StreamInfoAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamInfoAsync(key, flags);
    }

    public Task<long> StreamLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamLengthAsync(key, flags);
    }

    public Task<StreamPendingInfo> StreamPendingAsync(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamPendingAsync(key, groupName, flags);
    }

    public Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(RedisKey key, RedisValue groupName, int count, RedisValue consumerName, RedisValue? minId = null, RedisValue? maxId = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamPendingMessagesAsync(key, groupName, count, consumerName, minId, maxId, flags);
    }

    public Task<StreamEntry[]> StreamRangeAsync(RedisKey key, RedisValue? minId = null, RedisValue? maxId = null, int? count = null, Order messageOrder = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamRangeAsync(key, minId, maxId, count, messageOrder, flags);
    }

    public Task<StreamEntry[]> StreamReadAsync(RedisKey key, RedisValue position, int? count = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamReadAsync(key, position, count, flags);
    }

    public Task<RedisStream[]> StreamReadAsync(StreamPosition[] streamPositions, int? countPerStream = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamReadAsync(streamPositions, countPerStream, flags);
    }

    public Task<StreamEntry[]> StreamReadGroupAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position, int? count, CommandFlags flags)
    {
        return innerDatabase.StreamReadGroupAsync(key, groupName, consumerName, position, count, flags);
    }

    public Task<StreamEntry[]> StreamReadGroupAsync(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position = null, int? count = null, bool noAck = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamReadGroupAsync(key, groupName, consumerName, position, count, noAck, flags);
    }

    public Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream, CommandFlags flags)
    {
        return innerDatabase.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, flags);
    }

    public Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream = null, bool noAck = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, noAck, flags);
    }

    public Task<long> StreamTrimAsync(RedisKey key, int maxLength, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamTrimAsync(key, maxLength, useApproximateMaxLength, flags);
    }

    public Task<long> StringAppendAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringAppendAsync(key, value, flags);
    }

    public Task<long> StringBitCountAsync(RedisKey key, long start, long end, CommandFlags flags)
    {
        return innerDatabase.StringBitCountAsync(key, start, end, flags);
    }

    public Task<long> StringBitCountAsync(RedisKey key, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitCountAsync(key, start, end, indexType, flags);
    }

    public Task<long> StringBitOperationAsync(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = new RedisKey(), CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitOperationAsync(operation, destination, first, second, flags);
    }

    public Task<long> StringBitOperationAsync(Bitwise operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitOperationAsync(operation, destination, keys, flags);
    }

    public Task<long> StringBitPositionAsync(RedisKey key, bool bit, long start, long end, CommandFlags flags)
    {
        return innerDatabase.StringBitPositionAsync(key, bit, start, end, flags);
    }

    public Task<long> StringBitPositionAsync(RedisKey key, bool bit, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitPositionAsync(key, bit, start, end, indexType, flags);
    }

    public Task<long> StringDecrementAsync(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringDecrementAsync(key, value, flags);
    }

    public Task<double> StringDecrementAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringDecrementAsync(key, value, flags);
    }

    public Task<RedisValue> StringGetAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetAsync(key, flags);
    }

    public Task<RedisValue[]> StringGetAsync(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetAsync(keys, flags);
    }

    public Task<Lease<byte>?> StringGetLeaseAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetLeaseAsync(key, flags);
    }

    public Task<bool> StringGetBitAsync(RedisKey key, long offset, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetBitAsync(key, offset, flags);
    }

    public Task<RedisValue> StringGetRangeAsync(RedisKey key, long start, long end, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetRangeAsync(key, start, end, flags);
    }

    public Task<RedisValue> StringGetSetAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetSetAsync(key, value, flags);
    }

    public Task<RedisValue> StringGetSetExpiryAsync(RedisKey key, TimeSpan? expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetSetExpiryAsync(key, expiry, flags);
    }

    public Task<RedisValue> StringGetSetExpiryAsync(RedisKey key, DateTime expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetSetExpiryAsync(key, expiry, flags);
    }

    public Task<RedisValue> StringGetDeleteAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetDeleteAsync(key, flags);
    }

    public Task<RedisValueWithExpiry> StringGetWithExpiryAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetWithExpiryAsync(key, flags);
    }

    public Task<long> StringIncrementAsync(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringIncrementAsync(key, value, flags);
    }

    public Task<double> StringIncrementAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringIncrementAsync(key, value, flags);
    }

    public Task<long> StringLengthAsync(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLengthAsync(key, flags);
    }

    public Task<string?> StringLongestCommonSubsequenceAsync(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLongestCommonSubsequenceAsync(first, second, flags);
    }

    public Task<long> StringLongestCommonSubsequenceLengthAsync(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLongestCommonSubsequenceLengthAsync(first, second, flags);
    }

    public Task<LCSMatchResult> StringLongestCommonSubsequenceWithMatchesAsync(RedisKey first, RedisKey second, long minLength = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLongestCommonSubsequenceWithMatchesAsync(first, second, minLength, flags);
    }

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry, When when)
    {
        return innerDatabase.StringSetAsync(key, value, expiry, when);
    }

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags)
    {
        return innerDatabase.StringSetAsync(key, value, expiry, when, flags);
    }

    public Task<bool> StringSetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetAsync(key, value, expiry, keepTtl, when, flags);
    }

    public Task<bool> StringSetAsync(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetAsync(values, when, flags);
    }

    public Task<RedisValue> StringSetAndGetAsync(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags)
    {
        return innerDatabase.StringSetAndGetAsync(key, value, expiry, when, flags);
    }

    public Task<RedisValue> StringSetAndGetAsync(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetAndGetAsync(key, value, expiry, keepTtl, when, flags);
    }

    public Task<bool> StringSetBitAsync(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetBitAsync(key, offset, bit, flags);
    }

    public Task<RedisValue> StringSetRangeAsync(RedisKey key, long offset, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetRangeAsync(key, offset, value, flags);
    }

    public IBatch CreateBatch(object? asyncState = null)
    {
        return innerDatabase.CreateBatch(asyncState);
    }

    public ITransaction CreateTransaction(object? asyncState = null)
    {
        return innerDatabase.CreateTransaction(asyncState);
    }

    public void KeyMigrate(RedisKey key, EndPoint toServer, int toDatabase = 0, int timeoutMilliseconds = 0, MigrateOptions migrateOptions = MigrateOptions.None, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.KeyMigrate(key, toServer, toDatabase, timeoutMilliseconds, migrateOptions, flags);
    }

    public RedisValue DebugObject(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.DebugObject(key, flags);
    }

    public bool GeoAdd(RedisKey key, double longitude, double latitude, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoAdd(key, longitude, latitude, member, flags);
    }

    public bool GeoAdd(RedisKey key, GeoEntry value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoAdd(key, value, flags);
    }

    public long GeoAdd(RedisKey key, GeoEntry[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoAdd(key, values, flags);
    }

    public bool GeoRemove(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoRemove(key, member, flags);
    }

    public double? GeoDistance(RedisKey key, RedisValue member1, RedisValue member2, GeoUnit unit = GeoUnit.Meters, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoDistance(key, member1, member2, unit, flags);
    }

    public string?[] GeoHash(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoHash(key, members, flags);
    }

    public string? GeoHash(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoHash(key, member, flags);
    }

    public GeoPosition?[] GeoPosition(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoPosition(key, members, flags);
    }

    public GeoPosition? GeoPosition(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoPosition(key, member, flags);
    }

    public GeoRadiusResult[] GeoRadius(RedisKey key, RedisValue member, double radius, GeoUnit unit = GeoUnit.Meters, int count = -1, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoRadius(key, member, radius, unit, count, order, options, flags);
    }

    public GeoRadiusResult[] GeoRadius(RedisKey key, double longitude, double latitude, double radius, GeoUnit unit = GeoUnit.Meters, int count = -1, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoRadius(key, longitude, latitude, radius, unit, count, order, options, flags);
    }

    public GeoRadiusResult[] GeoSearch(RedisKey key, RedisValue member, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearch(key, member, shape, count, demandClosest, order, options, flags);
    }

    public GeoRadiusResult[] GeoSearch(RedisKey key, double longitude, double latitude, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, GeoRadiusOptions options = GeoRadiusOptions.Default, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearch(key, longitude, latitude, shape, count, demandClosest, order, options, flags);
    }

    public long GeoSearchAndStore(RedisKey sourceKey, RedisKey destinationKey, RedisValue member, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, bool storeDistances = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearchAndStore(sourceKey, destinationKey, member, shape, count, demandClosest, order, storeDistances, flags);
    }

    public long GeoSearchAndStore(RedisKey sourceKey, RedisKey destinationKey, double longitude, double latitude, GeoSearchShape shape, int count = -1, bool demandClosest = true, Order? order = null, bool storeDistances = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.GeoSearchAndStore(sourceKey, destinationKey, longitude, latitude, shape, count, demandClosest, order, storeDistances, flags);
    }

    public long HashDecrement(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDecrement(key, hashField, value, flags);
    }

    public double HashDecrement(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDecrement(key, hashField, value, flags);
    }

    public bool HashDelete(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDelete(key, hashField, flags);
    }

    public long HashDelete(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashDelete(key, hashFields, flags);
    }

    public bool HashExists(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashExists(key, hashField, flags);
    }

    public ExpireResult[] HashFieldExpire(RedisKey key, RedisValue[] hashFields, TimeSpan expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldExpire(key, hashFields, expiry, when, flags);
    }

    public ExpireResult[] HashFieldExpire(RedisKey key, RedisValue[] hashFields, DateTime expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldExpire(key, hashFields, expiry, when, flags);
    }

    public long[] HashFieldGetExpireDateTime(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldGetExpireDateTime(key, hashFields, flags);
    }

    public PersistResult[] HashFieldPersist(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldPersist(key, hashFields, flags);
    }

    public long[] HashFieldGetTimeToLive(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashFieldGetTimeToLive(key, hashFields, flags);
    }

    public RedisValue HashGet(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGet(key, hashField, flags);
    }

    public Lease<byte>? HashGetLease(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGetLease(key, hashField, flags);
    }

    public RedisValue[] HashGet(RedisKey key, RedisValue[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGet(key, hashFields, flags);
    }

    public HashEntry[] HashGetAll(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashGetAll(key, flags);
    }

    public long HashIncrement(RedisKey key, RedisValue hashField, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashIncrement(key, hashField, value, flags);
    }

    public double HashIncrement(RedisKey key, RedisValue hashField, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashIncrement(key, hashField, value, flags);
    }

    public RedisValue[] HashKeys(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashKeys(key, flags);
    }

    public long HashLength(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashLength(key, flags);
    }

    public RedisValue HashRandomField(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashRandomField(key, flags);
    }

    public RedisValue[] HashRandomFields(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashRandomFields(key, count, flags);
    }

    public HashEntry[] HashRandomFieldsWithValues(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashRandomFieldsWithValues(key, count, flags);
    }

    public IEnumerable<HashEntry> HashScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags)
    {
        return innerDatabase.HashScan(key, pattern, pageSize, flags);
    }

    public IEnumerable<HashEntry> HashScan(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashScan(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public IEnumerable<RedisValue> HashScanNoValues(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashScanNoValues(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public void HashSet(RedisKey key, HashEntry[] hashFields, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.HashSet(key, hashFields, flags);
    }

    public bool HashSet(RedisKey key, RedisValue hashField, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashSet(key, hashField, value, when, flags);
    }

    public long HashStringLength(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashStringLength(key, hashField, flags);
    }

    public RedisValue[] HashValues(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HashValues(key, flags);
    }

    public bool HyperLogLogAdd(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogAdd(key, value, flags);
    }

    public bool HyperLogLogAdd(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogAdd(key, values, flags);
    }

    public long HyperLogLogLength(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogLength(key, flags);
    }

    public long HyperLogLogLength(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.HyperLogLogLength(keys, flags);
    }

    public void HyperLogLogMerge(RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.HyperLogLogMerge(destination, first, second, flags);
    }

    public void HyperLogLogMerge(RedisKey destination, RedisKey[] sourceKeys, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.HyperLogLogMerge(destination, sourceKeys, flags);
    }

    public EndPoint? IdentifyEndpoint(RedisKey key = new RedisKey(), CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.IdentifyEndpoint(key, flags);
    }

    public bool KeyCopy(RedisKey sourceKey, RedisKey destinationKey, int destinationDatabase = -1, bool replace = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyCopy(sourceKey, destinationKey, destinationDatabase, replace, flags);
    }

    public bool KeyDelete(RedisKey key, CommandFlags flags = CommandFlags.None) =>
        ChaosMonkeyPolicies.LatencyFaultPolicy(TimeSpan.FromSeconds(4), 0.1, new TimeoutException("Cannot connect to Redis"), 0.1)
            .Execute(() => innerDatabase.KeyDelete(key, flags));

    public long KeyDelete(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyDelete(keys, flags);
    }

    public byte[]? KeyDump(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyDump(key, flags);
    }

    public string? KeyEncoding(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyEncoding(key, flags);
    }

    public bool KeyExists(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExists(key, flags);
    }

    public long KeyExists(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExists(keys, flags);
    }

    public bool KeyExpire(RedisKey key, TimeSpan? expiry, CommandFlags flags)
    {
        return innerDatabase.KeyExpire(key, expiry, flags);
    }

    public bool KeyExpire(RedisKey key, TimeSpan? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExpire(key, expiry, when, flags);
    }

    public bool KeyExpire(RedisKey key, DateTime? expiry, CommandFlags flags)
    {
        return innerDatabase.KeyExpire(key, expiry, flags);
    }

    public bool KeyExpire(RedisKey key, DateTime? expiry, ExpireWhen when = ExpireWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExpire(key, expiry, when, flags);
    }

    public DateTime? KeyExpireTime(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyExpireTime(key, flags);
    }

    public long? KeyFrequency(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyFrequency(key, flags);
    }

    public TimeSpan? KeyIdleTime(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyIdleTime(key, flags);
    }

    public bool KeyMove(RedisKey key, int database, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyMove(key, database, flags);
    }

    public bool KeyPersist(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyPersist(key, flags);
    }

    public RedisKey KeyRandom(CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRandom(flags);
    }

    public long? KeyRefCount(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRefCount(key, flags);
    }

    public bool KeyRename(RedisKey key, RedisKey newKey, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyRename(key, newKey, when, flags);
    }

    public void KeyRestore(RedisKey key, byte[] value, TimeSpan? expiry = null, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.KeyRestore(key, value, expiry, flags);
    }

    public TimeSpan? KeyTimeToLive(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTimeToLive(key, flags);
    }

    public bool KeyTouch(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTouch(key, flags);
    }

    public long KeyTouch(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyTouch(keys, flags);
    }

    public RedisType KeyType(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.KeyType(key, flags);
    }

    public RedisValue ListGetByIndex(RedisKey key, long index, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListGetByIndex(key, index, flags);
    }

    public long ListInsertAfter(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListInsertAfter(key, pivot, value, flags);
    }

    public long ListInsertBefore(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListInsertBefore(key, pivot, value, flags);
    }

    public RedisValue ListLeftPop(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPop(key, flags);
    }

    public RedisValue[] ListLeftPop(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPop(key, count, flags);
    }

    public ListPopResult ListLeftPop(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPop(keys, count, flags);
    }

    public long ListPosition(RedisKey key, RedisValue element, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListPosition(key, element, rank, maxLength, flags);
    }

    public long[] ListPositions(RedisKey key, RedisValue element, long count, long rank = 1, long maxLength = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListPositions(key, element, count, rank, maxLength, flags);
    }

    public long ListLeftPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPush(key, value, when, flags);
    }

    public long ListLeftPush(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLeftPush(key, values, when, flags);
    }

    public long ListLeftPush(RedisKey key, RedisValue[] values, CommandFlags flags)
    {
        return innerDatabase.ListLeftPush(key, values, flags);
    }

    public long ListLength(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListLength(key, flags);
    }

    public RedisValue ListMove(RedisKey sourceKey, RedisKey destinationKey, ListSide sourceSide, ListSide destinationSide, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListMove(sourceKey, destinationKey, sourceSide, destinationSide, flags);
    }

    public RedisValue[] ListRange(RedisKey key, long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRange(key, start, stop, flags);
    }

    public long ListRemove(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRemove(key, value, count, flags);
    }

    public RedisValue ListRightPop(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPop(key, flags);
    }

    public RedisValue[] ListRightPop(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPop(key, count, flags);
    }

    public ListPopResult ListRightPop(RedisKey[] keys, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPop(keys, count, flags);
    }

    public RedisValue ListRightPopLeftPush(RedisKey source, RedisKey destination, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPopLeftPush(source, destination, flags);
    }

    public long ListRightPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPush(key, value, when, flags);
    }

    public long ListRightPush(RedisKey key, RedisValue[] values, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ListRightPush(key, values, when, flags);
    }

    public long ListRightPush(RedisKey key, RedisValue[] values, CommandFlags flags)
    {
        return innerDatabase.ListRightPush(key, values, flags);
    }

    public void ListSetByIndex(RedisKey key, long index, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.ListSetByIndex(key, index, value, flags);
    }

    public void ListTrim(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None)
    {
        innerDatabase.ListTrim(key, start, stop, flags);
    }

    public bool LockExtend(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockExtend(key, value, expiry, flags);
    }

    public RedisValue LockQuery(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockQuery(key, flags);
    }

    public bool LockRelease(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockRelease(key, value, flags);
    }

    public bool LockTake(RedisKey key, RedisValue value, TimeSpan expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.LockTake(key, value, expiry, flags);
    }

    public long Publish(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.Publish(channel, message, flags);
    }

    public RedisResult Execute(string command, params object[] args)
    {
        return innerDatabase.Execute(command, args);
    }

    public RedisResult Execute(string command, ICollection<object> args, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.Execute(command, args, flags);
    }

    public RedisResult ScriptEvaluate(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None) =>
        ChaosMonkeyPolicies.LatencyFaultPolicy(TimeSpan.FromSeconds(4), 0.1, new TimeoutException("Cannot connect to Redis"), 0.1)
            .Execute(() => innerDatabase.ScriptEvaluate(script, keys, values, flags));

    public RedisResult ScriptEvaluate(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluate(hash, keys, values, flags);
    }

    public RedisResult ScriptEvaluate(LuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluate(script, parameters, flags);
    }

    public RedisResult ScriptEvaluate(LoadedLuaScript script, object? parameters = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluate(script, parameters, flags);
    }

    public RedisResult ScriptEvaluateReadOnly(string script, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateReadOnly(script, keys, values, flags);
    }

    public RedisResult ScriptEvaluateReadOnly(byte[] hash, RedisKey[]? keys = null, RedisValue[]? values = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.ScriptEvaluateReadOnly(hash, keys, values, flags);
    }

    public bool SetAdd(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetAdd(key, value, flags);
    }

    public long SetAdd(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetAdd(key, values, flags);
    }

    public RedisValue[] SetCombine(SetOperation operation, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombine(operation, first, second, flags);
    }

    public RedisValue[] SetCombine(SetOperation operation, RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombine(operation, keys, flags);
    }

    public long SetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombineAndStore(operation, destination, first, second, flags);
    }

    public long SetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetCombineAndStore(operation, destination, keys, flags);
    }

    public bool SetContains(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetContains(key, value, flags);
    }

    public bool[] SetContains(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetContains(key, values, flags);
    }

    public long SetIntersectionLength(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetIntersectionLength(keys, limit, flags);
    }

    public long SetLength(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetLength(key, flags);
    }

    public RedisValue[] SetMembers(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetMembers(key, flags);
    }

    public bool SetMove(RedisKey source, RedisKey destination, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetMove(source, destination, value, flags);
    }

    public RedisValue SetPop(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetPop(key, flags);
    }

    public RedisValue[] SetPop(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetPop(key, count, flags);
    }

    public RedisValue SetRandomMember(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRandomMember(key, flags);
    }

    public RedisValue[] SetRandomMembers(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRandomMembers(key, count, flags);
    }

    public bool SetRemove(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRemove(key, value, flags);
    }

    public long SetRemove(RedisKey key, RedisValue[] values, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetRemove(key, values, flags);
    }

    public IEnumerable<RedisValue> SetScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags)
    {
        return innerDatabase.SetScan(key, pattern, pageSize, flags);
    }

    public IEnumerable<RedisValue> SetScan(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SetScan(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public RedisValue[] Sort(RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = new RedisValue(), RedisValue[]? get = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.Sort(key, skip, take, order, sortType, by, get, flags);
    }

    public long SortAndStore(RedisKey destination, RedisKey key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = new RedisValue(), RedisValue[]? get = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortAndStore(destination, key, skip, take, order, sortType, by, get, flags);
    }

    public bool SortedSetAdd(RedisKey key, RedisValue member, double score, CommandFlags flags)
    {
        return innerDatabase.SortedSetAdd(key, member, score, flags);
    }

    public bool SortedSetAdd(RedisKey key, RedisValue member, double score, When when, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAdd(key, member, score, when, flags);
    }

    public bool SortedSetAdd(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAdd(key, member, score, when, flags);
    }

    public long SortedSetAdd(RedisKey key, SortedSetEntry[] values, CommandFlags flags)
    {
        return innerDatabase.SortedSetAdd(key, values, flags);
    }

    public long SortedSetAdd(RedisKey key, SortedSetEntry[] values, When when, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAdd(key, values, when, flags);
    }

    public long SortedSetAdd(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetAdd(key, values, when, flags);
    }

    public RedisValue[] SortedSetCombine(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombine(operation, keys, weights, aggregate, flags);
    }

    public SortedSetEntry[] SortedSetCombineWithScores(SetOperation operation, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineWithScores(operation, keys, weights, aggregate, flags);
    }

    public long SortedSetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey first, RedisKey second, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineAndStore(operation, destination, first, second, aggregate, flags);
    }

    public long SortedSetCombineAndStore(SetOperation operation, RedisKey destination, RedisKey[] keys, double[]? weights = null, Aggregate aggregate = Aggregate.Sum, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetCombineAndStore(operation, destination, keys, weights, aggregate, flags);
    }

    public double SortedSetDecrement(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetDecrement(key, member, value, flags);
    }

    public double SortedSetIncrement(RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetIncrement(key, member, value, flags);
    }

    public long SortedSetIntersectionLength(RedisKey[] keys, long limit = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetIntersectionLength(keys, limit, flags);
    }

    public long SortedSetLength(RedisKey key, double min = double.NegativeInfinity, double max = double.PositiveInfinity, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetLength(key, min, max, exclude, flags);
    }

    public long SortedSetLengthByValue(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetLengthByValue(key, min, max, exclude, flags);
    }

    public RedisValue SortedSetRandomMember(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRandomMember(key, flags);
    }

    public RedisValue[] SortedSetRandomMembers(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRandomMembers(key, count, flags);
    }

    public SortedSetEntry[] SortedSetRandomMembersWithScores(RedisKey key, long count, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRandomMembersWithScores(key, count, flags);
    }

    public RedisValue[] SortedSetRangeByRank(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByRank(key, start, stop, order, flags);
    }

    public long SortedSetRangeAndStore(RedisKey sourceKey, RedisKey destinationKey, RedisValue start, RedisValue stop, SortedSetOrder sortedSetOrder = SortedSetOrder.ByRank, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long? take = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeAndStore(sourceKey, destinationKey, start, stop, sortedSetOrder, exclude, order, skip, take, flags);
    }

    public SortedSetEntry[] SortedSetRangeByRankWithScores(RedisKey key, long start = 0, long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByRankWithScores(key, start, stop, order, flags);
    }

    public RedisValue[] SortedSetRangeByScore(RedisKey key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByScore(key, start, stop, exclude, order, skip, take, flags);
    }

    public SortedSetEntry[] SortedSetRangeByScoreWithScores(RedisKey key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity, Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByScoreWithScores(key, start, stop, exclude, order, skip, take, flags);
    }

    public RedisValue[] SortedSetRangeByValue(RedisKey key, RedisValue min, RedisValue max, Exclude exclude, long skip, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByValue(key, min, max, exclude, skip, take, flags);
    }

    public RedisValue[] SortedSetRangeByValue(RedisKey key, RedisValue min = new RedisValue(), RedisValue max = new RedisValue(), Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRangeByValue(key, min, max, exclude, order, skip, take, flags);
    }

    public long? SortedSetRank(RedisKey key, RedisValue member, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRank(key, member, order, flags);
    }

    public bool SortedSetRemove(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemove(key, member, flags);
    }

    public long SortedSetRemove(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemove(key, members, flags);
    }

    public long SortedSetRemoveRangeByRank(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveRangeByRank(key, start, stop, flags);
    }

    public long SortedSetRemoveRangeByScore(RedisKey key, double start, double stop, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveRangeByScore(key, start, stop, exclude, flags);
    }

    public long SortedSetRemoveRangeByValue(RedisKey key, RedisValue min, RedisValue max, Exclude exclude = Exclude.None, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetRemoveRangeByValue(key, min, max, exclude, flags);
    }

    public IEnumerable<SortedSetEntry> SortedSetScan(RedisKey key, RedisValue pattern, int pageSize, CommandFlags flags)
    {
        return innerDatabase.SortedSetScan(key, pattern, pageSize, flags);
    }

    public IEnumerable<SortedSetEntry> SortedSetScan(RedisKey key, RedisValue pattern = new RedisValue(), int pageSize = 250, long cursor = 0, int pageOffset = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetScan(key, pattern, pageSize, cursor, pageOffset, flags);
    }

    public double? SortedSetScore(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetScore(key, member, flags);
    }

    public double?[] SortedSetScores(RedisKey key, RedisValue[] members, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetScores(key, members, flags);
    }

    public SortedSetEntry? SortedSetPop(RedisKey key, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetPop(key, order, flags);
    }

    public SortedSetEntry[] SortedSetPop(RedisKey key, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetPop(key, count, order, flags);
    }

    public SortedSetPopResult SortedSetPop(RedisKey[] keys, long count, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetPop(keys, count, order, flags);
    }

    public bool SortedSetUpdate(RedisKey key, RedisValue member, double score, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetUpdate(key, member, score, when, flags);
    }

    public long SortedSetUpdate(RedisKey key, SortedSetEntry[] values, SortedSetWhen when = SortedSetWhen.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.SortedSetUpdate(key, values, when, flags);
    }

    public long StreamAcknowledge(RedisKey key, RedisValue groupName, RedisValue messageId, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAcknowledge(key, groupName, messageId, flags);
    }

    public long StreamAcknowledge(RedisKey key, RedisValue groupName, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAcknowledge(key, groupName, messageIds, flags);
    }

    public RedisValue StreamAdd(RedisKey key, RedisValue streamField, RedisValue streamValue, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAdd(key, streamField, streamValue, messageId, maxLength, useApproximateMaxLength, flags);
    }

    public RedisValue StreamAdd(RedisKey key, NameValueEntry[] streamPairs, RedisValue? messageId = null, int? maxLength = null, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAdd(key, streamPairs, messageId, maxLength, useApproximateMaxLength, flags);
    }

    public StreamAutoClaimResult StreamAutoClaim(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAutoClaim(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);
    }

    public StreamAutoClaimIdsOnlyResult StreamAutoClaimIdsOnly(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue startAtId, int? count = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamAutoClaimIdsOnly(key, consumerGroup, claimingConsumer, minIdleTimeInMs, startAtId, count, flags);
    }

    public StreamEntry[] StreamClaim(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamClaim(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);
    }

    public RedisValue[] StreamClaimIdsOnly(RedisKey key, RedisValue consumerGroup, RedisValue claimingConsumer, long minIdleTimeInMs, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamClaimIdsOnly(key, consumerGroup, claimingConsumer, minIdleTimeInMs, messageIds, flags);
    }

    public bool StreamConsumerGroupSetPosition(RedisKey key, RedisValue groupName, RedisValue position, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamConsumerGroupSetPosition(key, groupName, position, flags);
    }

    public StreamConsumerInfo[] StreamConsumerInfo(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamConsumerInfo(key, groupName, flags);
    }

    public bool StreamCreateConsumerGroup(RedisKey key, RedisValue groupName, RedisValue? position, CommandFlags flags)
    {
        return innerDatabase.StreamCreateConsumerGroup(key, groupName, position, flags);
    }

    public bool StreamCreateConsumerGroup(RedisKey key, RedisValue groupName, RedisValue? position = null, bool createStream = true, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamCreateConsumerGroup(key, groupName, position, createStream, flags);
    }

    public long StreamDelete(RedisKey key, RedisValue[] messageIds, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamDelete(key, messageIds, flags);
    }

    public long StreamDeleteConsumer(RedisKey key, RedisValue groupName, RedisValue consumerName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamDeleteConsumer(key, groupName, consumerName, flags);
    }

    public bool StreamDeleteConsumerGroup(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamDeleteConsumerGroup(key, groupName, flags);
    }

    public StreamGroupInfo[] StreamGroupInfo(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamGroupInfo(key, flags);
    }

    public StreamInfo StreamInfo(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamInfo(key, flags);
    }

    public long StreamLength(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamLength(key, flags);
    }

    public StreamPendingInfo StreamPending(RedisKey key, RedisValue groupName, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamPending(key, groupName, flags);
    }

    public StreamPendingMessageInfo[] StreamPendingMessages(RedisKey key, RedisValue groupName, int count, RedisValue consumerName, RedisValue? minId = null, RedisValue? maxId = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamPendingMessages(key, groupName, count, consumerName, minId, maxId, flags);
    }

    public StreamEntry[] StreamRange(RedisKey key, RedisValue? minId = null, RedisValue? maxId = null, int? count = null, Order messageOrder = Order.Ascending, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamRange(key, minId, maxId, count, messageOrder, flags);
    }

    public StreamEntry[] StreamRead(RedisKey key, RedisValue position, int? count = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamRead(key, position, count, flags);
    }

    public RedisStream[] StreamRead(StreamPosition[] streamPositions, int? countPerStream = null, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamRead(streamPositions, countPerStream, flags);
    }

    public StreamEntry[] StreamReadGroup(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position, int? count, CommandFlags flags)
    {
        return innerDatabase.StreamReadGroup(key, groupName, consumerName, position, count, flags);
    }

    public StreamEntry[] StreamReadGroup(RedisKey key, RedisValue groupName, RedisValue consumerName, RedisValue? position = null, int? count = null, bool noAck = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamReadGroup(key, groupName, consumerName, position, count, noAck, flags);
    }

    public RedisStream[] StreamReadGroup(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream, CommandFlags flags)
    {
        return innerDatabase.StreamReadGroup(streamPositions, groupName, consumerName, countPerStream, flags);
    }

    public RedisStream[] StreamReadGroup(StreamPosition[] streamPositions, RedisValue groupName, RedisValue consumerName, int? countPerStream = null, bool noAck = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamReadGroup(streamPositions, groupName, consumerName, countPerStream, noAck, flags);
    }

    public long StreamTrim(RedisKey key, int maxLength, bool useApproximateMaxLength = false, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StreamTrim(key, maxLength, useApproximateMaxLength, flags);
    }

    public long StringAppend(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringAppend(key, value, flags);
    }

    public long StringBitCount(RedisKey key, long start, long end, CommandFlags flags)
    {
        return innerDatabase.StringBitCount(key, start, end, flags);
    }

    public long StringBitCount(RedisKey key, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitCount(key, start, end, indexType, flags);
    }

    public long StringBitOperation(Bitwise operation, RedisKey destination, RedisKey first, RedisKey second = new RedisKey(), CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitOperation(operation, destination, first, second, flags);
    }

    public long StringBitOperation(Bitwise operation, RedisKey destination, RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitOperation(operation, destination, keys, flags);
    }

    public long StringBitPosition(RedisKey key, bool bit, long start, long end, CommandFlags flags)
    {
        return innerDatabase.StringBitPosition(key, bit, start, end, flags);
    }

    public long StringBitPosition(RedisKey key, bool bit, long start = 0, long end = -1, StringIndexType indexType = StringIndexType.Byte, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringBitPosition(key, bit, start, end, indexType, flags);
    }

    public long StringDecrement(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringDecrement(key, value, flags);
    }

    public double StringDecrement(RedisKey key, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringDecrement(key, value, flags);
    }

    public RedisValue StringGet(RedisKey key, CommandFlags flags = CommandFlags.None) =>
        ChaosMonkeyPolicies.LatencyFaultPolicy(TimeSpan.FromSeconds(4), 0.1, new TimeoutException("Cannot connect to Redis"), 0.1)
            .Execute(() => innerDatabase.StringGet(key, flags));

    public RedisValue[] StringGet(RedisKey[] keys, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGet(keys, flags);
    }

    public Lease<byte>? StringGetLease(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetLease(key, flags);
    }

    public bool StringGetBit(RedisKey key, long offset, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetBit(key, offset, flags);
    }

    public RedisValue StringGetRange(RedisKey key, long start, long end, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetRange(key, start, end, flags);
    }

    public RedisValue StringGetSet(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetSet(key, value, flags);
    }

    public RedisValue StringGetSetExpiry(RedisKey key, TimeSpan? expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetSetExpiry(key, expiry, flags);
    }

    public RedisValue StringGetSetExpiry(RedisKey key, DateTime expiry, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetSetExpiry(key, expiry, flags);
    }

    public RedisValue StringGetDelete(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetDelete(key, flags);
    }

    public RedisValueWithExpiry StringGetWithExpiry(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringGetWithExpiry(key, flags);
    }

    public long StringIncrement(RedisKey key, long value = 1, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringIncrement(key, value, flags);
    }

    public double StringIncrement(RedisKey key, double value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringIncrement(key, value, flags);
    }

    public long StringLength(RedisKey key, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLength(key, flags);
    }

    public string? StringLongestCommonSubsequence(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLongestCommonSubsequence(first, second, flags);
    }

    public long StringLongestCommonSubsequenceLength(RedisKey first, RedisKey second, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLongestCommonSubsequenceLength(first, second, flags);
    }

    public LCSMatchResult StringLongestCommonSubsequenceWithMatches(RedisKey first, RedisKey second, long minLength = 0, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringLongestCommonSubsequenceWithMatches(first, second, minLength, flags);
    }

    public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry, When when)
    {
        return innerDatabase.StringSet(key, value, expiry, when);
    }

    public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags)
    {
        return innerDatabase.StringSet(key, value, expiry, when, flags);
    }

    // TODO: the latency should be randomized
    public bool StringSet(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None) =>
        ChaosMonkeyPolicies.LatencyFaultPolicy(TimeSpan.FromSeconds(4), 0.1, new TimeoutException("Cannot connect to Redis"), 0.1)
            .Execute(() => innerDatabase.StringSet(key, value, expiry, keepTtl, when, flags));

    public bool StringSet(KeyValuePair<RedisKey, RedisValue>[] values, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSet(values, when, flags);
    }

    public RedisValue StringSetAndGet(RedisKey key, RedisValue value, TimeSpan? expiry, When when, CommandFlags flags)
    {
        return innerDatabase.StringSetAndGet(key, value, expiry, when, flags);
    }

    public RedisValue StringSetAndGet(RedisKey key, RedisValue value, TimeSpan? expiry = null, bool keepTtl = false, When when = When.Always, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetAndGet(key, value, expiry, keepTtl, when, flags);
    }

    public bool StringSetBit(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetBit(key, offset, bit, flags);
    }

    public RedisValue StringSetRange(RedisKey key, long offset, RedisValue value, CommandFlags flags = CommandFlags.None)
    {
        return innerDatabase.StringSetRange(key, offset, value, flags);
    }

    public int Database => innerDatabase.Database;
}
