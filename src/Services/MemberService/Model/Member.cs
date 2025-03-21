using Newtonsoft.Json;

namespace Model;

public class Member
{
    [JsonProperty("id")]
    public string Id { get; set; }
    public string Name { get; set; }
    public string GroupdId { get; set; }
}