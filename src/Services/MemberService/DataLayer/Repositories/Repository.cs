using System.Net;
using Database.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Database.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Container Container;

    public Repository(ICosmosDb cosmosDb, string databaseName, string containerName)
    {
        Container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<T?> GetItemAsync(string id, string partitionKey)
    {
        try
        {
            ItemResponse<T> response = await Container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task AddItemAsync(T item, string partitionKey)
    {
        await Container.CreateItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task UpdateItemAsync(T item, string partitionKey)
    {
        await Container.UpsertItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task DeleteItemAsync(string id, string partitionKey)
    {
        await Container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }
}