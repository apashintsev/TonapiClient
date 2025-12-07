using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a blockchain shard.
/// </summary>
public class BlockchainShard
{
    /// <summary>
    /// Gets or sets the last known block.
    /// </summary>
    [JsonPropertyName("last_known_block")]
    public ReducedBlock LastKnownBlock { get; set; } = new();

    /// <summary>
    /// Gets or sets the shard name.
    /// </summary>
    [JsonPropertyName("shard")]
    public string Shard { get; set; } = string.Empty;
}
