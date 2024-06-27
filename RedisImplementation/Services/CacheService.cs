using StackExchange.Redis;

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
        throw new NotImplementedException();
    }

    public object RemoveDat(string Key)
    {
        throw new NotImplementedException();
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        throw new NotImplementedException();
    }
}
