// using ClubApi.Dtos;
// using Microsoft.AspNetCore.Mvc;
// using Services;
//
// namespace MemberApi;
//
// [ApiController]
// [Route("api/[controller]")]
// public class GroupController : ControllerBase
// {
//     private readonly IGroupService _groupService;
//     private readonly IMemberService _memberService;
//     
//     public GroupController(IGroupService groupService, IMemberService memberService)
//     {
//         _groupService = groupService;
//         _memberService = memberService;
//     }
//     
//     [HttpPost]
//     public async Task<IActionResult> CreateClub([FromQuery(Name = "Member-ID")] long playerId, 
//         [FromBody] CreateClubRequestDto request)
//     {
//         if (await _groupService.ClubExistsByNameAsync(request.Name))
//         {
//             return Conflict("There is already a club has the same name");
//         }
//
//         // Check if player is already in another club
//         var hasClub = await _memberService.HasClub(playerId);
//         if (hasClub)
//         {
//             return Conflict("Member already belongs to a club");
//         }
//         
//         var club = await _groupService.CreateClubAsync(request.Name);
//         var player = await _memberService.GetPlayerAsync(playerId);
//         if (player == null)
//         {
//             player = await _memberService.CreatePlayerAsync(playerId);
//         }
//         
//         await _groupService.AddMemberToClubAsync(player, club);
//         
//         return CreatedAtAction(
//             nameof(CreateClub),
//             new { clubId = club.Id },
//             new { id = club.Id, members = club.Members.Select(x => x.MemberId).ToList() }
//         );
//     }
//     
//     [HttpGet("{clubId}")]
//     public async Task<IActionResult> GetClub(Guid clubId)
//     {
//         var club = await _groupService.GetClubInfo(clubId);
//         if (club == null)
//         {
//             return NotFound();
//         }
//         
//         return Ok(new { id = club.Id, members = club.Members.Select(x => x.MemberId).ToList() });
//     }
//     
//     [HttpPost("{clubId}/members")]
//     public async Task<IActionResult> AddMember(Guid clubId, 
//         [FromHeader(Name = "Member-ID")] long playerId,
//         [FromBody] AddMemberRequest request)
//     {
//         var club = await _groupService.GetClubInfo(clubId);
//         if (club == null)
//         {
//             return NotFound();
//         }
//             
//         // Check if player is already in another club
//         var hasClub = await _memberService.HasClub(playerId);
//         if (hasClub)
//         {
//             return Conflict("Member already belongs to a club");
//         }
//             
//         var player = await _memberService.GetPlayerAsync(playerId);
//         if (player == null)
//         {
//             player = await _memberService.CreatePlayerAsync(playerId);
//         }
//         
//         await _groupService.AddMemberToClubAsync(player, club);
//         
//         return NoContent();
//     }
// }