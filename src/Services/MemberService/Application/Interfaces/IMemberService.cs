using Domain;
using Domain.Entities;

namespace Application.Interfaces;

public interface IMemberService
{
    Task<Member> GetMemberAsync(long id);
    Task<bool> HasClub(long id);
    Task<Member> CreateMemberAsync(long id, string name);
    Task<Member> UpdateMemberAsync(long id, string name);
}