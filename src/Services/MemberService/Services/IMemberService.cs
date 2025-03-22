using Model;

namespace Services;

public interface IMemberService
{
    Task<Member> GetMemberAsync(long id);
    Task<bool> HasClub(long id);
    Task<Member> CreateMemberAsync(long id, string name);
}