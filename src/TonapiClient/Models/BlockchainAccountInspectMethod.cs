using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class BlockchainAccountInspectMethod
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;
}

public class BlockchainAccountInspectResult
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("code_hash")]
    public string CodeHash { get; set; } = string.Empty;

    [JsonPropertyName("methods")]
    public List<BlockchainAccountInspectMethod> Methods { get; set; } = new();

    [JsonPropertyName("compiler")]
    public string? Compiler { get; set; }
}

