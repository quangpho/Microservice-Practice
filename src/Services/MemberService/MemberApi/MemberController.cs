using Microsoft.AspNetCore.Mvc;
using Services;

namespace MemberApi;

[ApiController]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;
    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }
    
    [Route("addMember")]
    [HttpPost]
    public async Task<IActionResult> AddMember(long id)
    {
        await _memberService.CreateMemberAsync(id);
        return Created();
    }
}