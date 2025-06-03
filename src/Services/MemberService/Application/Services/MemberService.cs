using Application.Interfaces;
using Domain;
using Domain.Constants;
using Domain.Entities;

namespace Application.Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _memberRepository;
    private readonly string _defaultPartitionKey = PartitionKeys.DefaultPartitionKey;

    public MemberService(IRepository<Member> memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Member> GetMemberAsync(long id)
    {
        return await _memberRepository.GetItemAsync(id.ToString());
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
            CreatedAt = DateTime.Now,
            PartitionKey = _defaultPartitionKey
        };

        await _memberRepository.AddItemAsync(member);
        return member;
    }
    
    public async Task<Member> UpdateMemberAsync(long id, string name)
    {
        var member = new Member()
        {
            Id = id.ToString(),
            Name = name,
            UpdatedAt = DateTime.Now,
        };

        await _memberRepository.UpdateItemAsync(member);
        return member;
    }
}