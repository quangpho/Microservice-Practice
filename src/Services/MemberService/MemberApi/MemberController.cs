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
    public async Task<IActionResult> AddMember()
    {
        long id = 123;
        await _memberService.CreateMemberAsync(id);
        return Created();
    }
}