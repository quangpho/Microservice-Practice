﻿using Model;

namespace Services;

public interface IGroupService
{
    Task<bool> GroupExistsByNameAsync(string GroupName);
    Task<Group> CreateGroupAsync(string GroupName);
    Task AddMemberToGroupAsync(Member member, Group group);
    Task<Group> GetGroupInfo(Guid id);
}