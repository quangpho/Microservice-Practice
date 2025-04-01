using System.Net;
using Application.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ILogger<IRepository<T>> _logger;
    private readonly Container _container;

    protected Repository(CosmosClient cosmosClient, string databaseName, string containerName,
        ILogger<IRepository<T>> logger)
    {
        _logger = logger;
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<T?> GetItemAsync(string id, string partitionKey)
    {
        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task AddItemAsync(T item, string partitionKey)
    {
        await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task AddItemAsync(T item)
    {
        try
        {
            await _container.CreateItemAsync(item);
        }
        catch (CosmosException e) when (e.StatusCode == HttpStatusCode.Conflict)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }


    public async Task UpdateItemAsync(T item, string partitionKey)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task DeleteItemAsync(string id, string partitionKey)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }
}