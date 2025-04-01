using Application.Interfaces;
using Domain;

namespace Application.Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _memberRepository;
    private readonly string _defaultPartitionKey = "default";

    public MemberService(IRepository<Member> memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Member> GetMemberAsync(long id)
    {
        return await _memberRepository.GetItemAsync(id.ToString(), _defaultPartitionKey);
    }

    public Task<bool> HasClub(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Member> CreateMemberAsync(long id, string name)
    {
        var member = new Member()
        {
            Id = id.ToString(),
            Name = name,
            PartionKey = _defaultPartitionKey
        };

        await _memberRepository.AddItemAsync(member);
        return member;
    }
}