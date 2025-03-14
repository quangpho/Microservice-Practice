using Microsoft.Azure.Cosmos;
using Model;

namespace DataLayer.Repositories;

public class MemberRepository : Repository<Member>
{
    public MemberRepository(CosmosClient cosmosDb, string databaseName, string containerName) : base(cosmosDb, databaseName, containerName)
    {
    }
}