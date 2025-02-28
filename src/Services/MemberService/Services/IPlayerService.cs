using Model;

namespace Services;

public interface IPlayerService
{
    Task<Member> GetPlayerAsync(long id);
    Task<bool> HasClub(long id);
    Task<Member> CreatePlayerAsync(long id);
}