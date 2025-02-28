// using Model;
// using Repository.Interfaces;
//
// namespace Services;
//
// public class ClubService : IGroupService
// {
//
//     public ClubService( clubRepository)
//     {
//         _clubRepository = clubRepository;
//     }
//
//     public async Task<bool> ClubExistsByNameAsync(string clubName)
//     {
//         return await _clubRepository.ExistsByNameAsync(clubName);
//     }
//     
//     public async Task<Group> CreateClubAsync(string clubName)
//     {
//         var club = new Group
//         {
//             Id = Guid.NewGuid(),
//             Name = clubName,
//             Members = new List<Member>()
//         };
//
//         await _clubRepository.AddAsync(club);
//         return club;
//     }
//
//     public async Task AddMemberToClubAsync(Member member, Group group)
//     {
//         group.Members.Add(member);
//         await _clubRepository.UpdateAsync(group);
//     }
//
//     public async Task<Group> GetClubInfo(Guid id)
//     {
//         return await _clubRepository.GetWithMembersAsync(id);
//     }
// }