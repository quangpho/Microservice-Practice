using Application.Interfaces;
using Domain;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services;

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

    public async Task<Member> CreateMemberAsync(long id, string name)
    {
        var member = new Member()
        {
            Id = id.ToString(),
            Name = name,
            PartionKey = "default"
        };

        await _memberRepository.AddItemAsync(member);
        return member;
    }
}