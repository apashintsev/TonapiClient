using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class SendBlockchainMessageRequest
{
    [JsonPropertyName("boc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Boc { get; set; }

    [JsonPropertyName("batch")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Batch { get; set; }

    [JsonPropertyName("meta")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? Meta { get; set; }
}

