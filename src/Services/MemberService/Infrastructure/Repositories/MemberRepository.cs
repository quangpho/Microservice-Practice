using Application.Interfaces;
using Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class MemberRepository : Repository<Member>
{
    public MemberRepository(CosmosClient cosmosClient, string databaseName, string containerName,
        ILogger<IRepository<Member>> memberLogger) : base(cosmosClient, databaseName, containerName, memberLogger)

    {
    }
}