using Database.Repositories.Interfaces;
using Model;
using Repository.Interfaces;

namespace Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _memberRepository;

    public MemberService(IRepository<Member> memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Member> CreatePlayerAsync(long id)
    {
        var player = new Member()
        {
            MemberId = id
        };

        await _playerRepository.AddAsync(player);
        return player;
    }

    public Task<Member> GetMemberAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasClub(long id)
    {
        var player = await GetPlayerAsync(id);
        if (player == null)
        {
            return false;
        }

        return player.Club != null;
    }

    public Task<Member> CreateMemberAsync(long id)
    {
        var player = new Member()
        {
            MemberId = id
        };

        await _memberRepository.AddItemAsync(player);
        return player;
    }

    public async Task<Member> GetPlayerAsync(long id)
    {
        return await _playerRepository.GetPlayerInfo(id);
    }
}