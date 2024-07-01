using StackExchange.Redis;
using System.Text.Json;

namespace RedisImplementation.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;
    private readonly ConnectionMultiplexer _redis;

    public CacheService()
    {
        _redis = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDb = _redis.GetDatabase();
    }
    public T GetData<T>(string Key)
    {
        var value = _cacheDb.StringGet(Key);
        if (!string.IsNullOrEmpty(value))
            return JsonSerializer.Deserialize<T>(value)!;

        return default!;
    }

    public object RemoveData(string Key)
    {
        var _exist = _cacheDb.KeyExists(Key);

        if (_exist)
            return _cacheDb.KeyDelete(Key);

        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiretime = expirationTime.DateTime.Subtract(DateTime.Now);

        return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiretime);
    }
}
