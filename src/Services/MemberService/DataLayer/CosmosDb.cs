using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace DataLayer;

public class CosmosDb : ICosmosDb
{
    public CosmosClient Client { get; }
    public CosmosDb(IConfiguration  configuration)
    {
        var connectionString = configuration.GetSection("CosmosDb:ConnectionString")?.Value;
        var databaseName = configuration.GetSection("CosmosDb:DatabaseName")?.Value;
        if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(databaseName))
        {
            Client = new CosmosClient(connectionString);
        }
    }

    public Microsoft.Azure.Cosmos.Database GetDatabase(string databaseName)
    {
        return Client.GetDatabase(databaseName);
    }
}