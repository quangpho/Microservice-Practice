using Domain;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGroupService
{
    Task<bool> GroupExistsByNameAsync(string GroupName);
    Task<Group> CreateGroupAsync(string GroupName);
    Task AddMemberToGroupAsync(Member member, Group group);
    Task<Group> GetGroupInfo(Guid id);
}