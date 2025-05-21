using System.Net;
using Api.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Api;

[ApiController]
[Route("member")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] AddMemberRequest request)
    {
        try
        {
            await _memberService.CreateMemberAsync(request.Id, request.Name);
            return Created();
        }
        catch (CosmosException e) when(e.StatusCode == HttpStatusCode.Conflict)
        {
            return Conflict("Member is already exists");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMember(long id)
    {
        var member = await _memberService.GetMemberAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return Ok(member);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateMember(UpdateMemberRequest request)
    {
        var member = await _memberService.(request.Id);
        if (member == null)
        {
            return NotFound($"There is not member with id {request.Id}");
        }
        
        return Ok(member);
    }
}