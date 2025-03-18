using Microsoft.Azure.Cosmos;

namespace DataLayer;

public class CosmosDbInitializer
{
    private readonly CosmosClient _client;

    public CosmosDbInitializer(CosmosClient client)
    {
        _client = client;
    }

    public async Task EnsureDatabaseAndContainersExistAsync()
    {
        var database = await _client.CreateDatabaseIfNotExistsAsync(_settings.DatabaseName);

        await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties(_settings.ClubContainerName, "/id"));
        await database.Database.CreateContainerIfNotExistsAsync(new ContainerProperties(_settings.PlayerContainerName, "/id"));
    }
}