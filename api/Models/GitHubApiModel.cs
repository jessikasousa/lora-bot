
using System.Text.Json.Serialization;

public class GitHubApiModel
{
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonIgnore]
    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonIgnore]
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }
}