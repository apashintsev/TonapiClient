using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents blockchain block shards information.
/// </summary>
public class BlockchainBlockShards
{
    /// <summary>
    /// Gets or sets the list of shards.
    /// </summary>
    [JsonPropertyName("shards")]
    public List<BlockchainShard> Shards { get; set; } = new();
}
