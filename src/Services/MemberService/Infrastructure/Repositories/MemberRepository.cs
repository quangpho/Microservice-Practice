using Domain;
using Microsoft.Azure.Cosmos;

namespace Infrastructure.Repositories;

public class MemberRepository : Repository<Member>
{
    public MemberRepository(CosmosClient cosmosClient, string databaseName, string containerName) : base(cosmosClient, databaseName, containerName)
    {
    }
}