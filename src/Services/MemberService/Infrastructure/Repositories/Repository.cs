using System.Net;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Container _container;

    protected Repository(CosmosClient cosmosClient, string databaseName, string containerName)
    {
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
        await _container.CreateItemAsync(item);
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