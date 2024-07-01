namespace RedisImplementation.Services;

public interface ICacheService
{
    T GetData<T>(string Key);
    bool SetData<T>(string key , T value, DateTimeOffset expirationTime);
    object RemoveData(string Key);
}
