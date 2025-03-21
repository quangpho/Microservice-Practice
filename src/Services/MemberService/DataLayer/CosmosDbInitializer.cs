using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace DataLayer;

public class CosmosDbInitializer
{
    private readonly CosmosClient _client;
    private readonly IConfiguration _configuration;

    public CosmosDbInitializer(CosmosClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task EnsureDatabaseAndContainersExistAsync()
    {
        var databaseName = _configuration["CosmosDb:DatabaseName"];
        var database = await _client.CreateDatabaseIfNotExistsAsync(databaseName);

        await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties("Groups", "/partitionKey"));
        await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties("Members", "/partitionKey"));
    }
}