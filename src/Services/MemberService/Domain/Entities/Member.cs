namespace Domain.Entities;

public class Member : BaseCosmosModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string[] Roles { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string GroupdId { get; set; }
}