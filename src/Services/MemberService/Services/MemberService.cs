using Model;
using Repository.Interfaces;

namespace Services;

public class MemberService : IMemberService
{
    private readonly IPlayerRepository _playerRepository;

    public MemberService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
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
    
    public async Task<bool> HasClub(long id)
    {
        var player = await GetPlayerAsync(id);
        if (player == null)
        {
            return false;
        }

        return player.Club != null;
    }
    
    public async Task<Member> GetPlayerAsync(long id)
    {
        return await _playerRepository.GetPlayerInfo(id);
    }
}