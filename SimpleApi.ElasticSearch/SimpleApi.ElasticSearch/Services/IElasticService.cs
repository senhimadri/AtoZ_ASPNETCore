using SimpleApi.ElasticSearch.Models;

namespace SimpleApi.ElasticSearch.Services;

public interface IElasticService
{
    Task CreateIndexIfNotExistAsync(string indexName);
    Task<bool> AddOrUpdate(User user);
    Task<bool> AddOrUpdateBulk(IEnumerable<User> users, string indexName);
    Task<User> Get(string Key);
    Task<List<User>?> GetAll();
    Task<bool> Remove(string key);
    Task<bool> RemoveAll();
}
