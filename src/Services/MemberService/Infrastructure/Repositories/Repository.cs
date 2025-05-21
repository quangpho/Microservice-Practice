using System.Net;
using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseCosmosModel
{
    private readonly ILogger<IRepository<T>> _logger;
    private readonly Container _container;
    private readonly string _defaultPartitionKey = PartitionKeys.DefaultPartitionKey;

    protected Repository(CosmosClient cosmosClient, string databaseName, string containerName,
        ILogger<IRepository<T>> logger)
    {
        _logger = logger;
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<T> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(_defaultPartitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }
    public async Task AddItemAsync(T item)
    {
        try
        {
            await _container.CreateItemAsync(item,  new PartitionKey(item.PartitionKey));
        }
        catch (CosmosException e) when (e.StatusCode == HttpStatusCode.Conflict)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
    
    public async Task UpdateItemAsync(T item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(item.PartitionKey));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(_defaultPartitionKey));
    }
}