using Api.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api;

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
    public async Task<IActionResult> AddMember([FromBody] AddMemberRequest request)
    {
        await _memberService.CreateMemberAsync(request.Id, request.Name);
        return Created();
    }
}