using Microsoft.Azure.Cosmos;
using Model;

namespace DataLayer.Repositories;

public class MemberRepository : Repository<Member>
{
    public MemberRepository(CosmosClient cosmosClient, string databaseName, string containerName) : base(cosmosClient, databaseName, containerName)
    {
    }
}