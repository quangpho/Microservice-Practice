using Model;

namespace DataLayer.Repositories;

public class MemberRepository : Repository<Member>
{
    public MemberRepository(ICosmosDb cosmosDb, string databaseName, string containerName) : base(cosmosDb, databaseName, containerName)
    {
    }
}