using Model;

namespace Services;

public interface IClubService
{
    Task<bool> ClubExistsByNameAsync(string clubName);
    Task<Group> CreateClubAsync(string clubName);
    Task AddMemberToClubAsync(Member member, Group group);
    Task<Group> GetClubInfo(Guid id);
}