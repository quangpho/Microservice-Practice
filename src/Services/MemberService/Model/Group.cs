namespace Model;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<Member> Members { get; set; } 
}