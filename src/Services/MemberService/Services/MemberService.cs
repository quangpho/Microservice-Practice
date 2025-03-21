using DataLayer.Repositories.Interfaces;
using Model;

namespace Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _memberRepository;

    public MemberService(IRepository<Member> memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task<Member> GetMemberAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasClub(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Member> CreateMemberAsync(string id, string name)
    {
        var member = new Member()
        {
            Id = id,
            Name = name
        };

        await _memberRepository.AddItemAsync(member, "member");
        return member;
    }
}