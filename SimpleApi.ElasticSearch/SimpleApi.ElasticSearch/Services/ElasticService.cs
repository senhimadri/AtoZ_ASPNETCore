﻿using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;
using SimpleApi.ElasticSearch.Configurations;
using SimpleApi.ElasticSearch.Models;

namespace SimpleApi.ElasticSearch.Services;

public class ElasticService : IElasticService
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _elasticSettings;


    public ElasticService(IOptions<ElasticSettings> optionsMonitor)
    {
        _elasticSettings = optionsMonitor.Value;
        var settings = new ElasticsearchClientSettings(new Uri(_elasticSettings.Url))
            //.Authentication()
            .DefaultIndex(_elasticSettings.DefaultIndex);

        _client = new ElasticsearchClient(settings);
    }
    public async Task CreateIndexIfNotExistAsync(string indexName)
    {
        if (!_client.Indices.Exists(indexName).Exists)
            await _client.Indices.CreateAsync(indexName);
    }

    public async Task<bool> AddOrUpdate(User user)
    {
        var response =await _client
                        .IndexAsync(user, idx => 
                                        idx.Index(_elasticSettings.DefaultIndex)
                                            .OpType(OpType.Index));
        return response.IsValidResponse;
    }

    public async Task<bool> AddOrUpdateBulk(IEnumerable<User> users, string indexName)
    {

        var response = await _client.BulkAsync(b =>
                b.Index(_elasticSettings.DefaultIndex)
                .UpdateMany(users, (ud, u) => ud.Doc(u).DocAsUpsert(true)));

        return response.IsValidResponse;
    }

    public async Task<User> Get(string key)
    {
        var response = await _client.GetAsync<User>(key, g =>
                        g.Index(_elasticSettings.DefaultIndex));

        return response.Source;
    }

    public async Task<List<User>?> GetAll()
    {
        var response = await _client.SearchAsync<User>(s => s.Index(_elasticSettings.DefaultIndex));
        return response.IsValidResponse ? response.Documents.ToList() : default;
    }

    public async Task<bool> Remove(string key)
    {
        var response = await _client.DeleteAsync<User>(key, d=>
                d.Index(_elasticSettings.DefaultIndex));

        return response.IsValidResponse;
    }

    public async Task<bool> RemoveAll()
    {
        var response = await _client.DeleteByQueryAsync<User>(d =>
                            d.Indices(_elasticSettings.DefaultIndex));

        return response.IsValidResponse;
    }
}
