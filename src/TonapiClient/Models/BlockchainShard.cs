using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a blockchain shard.
/// </summary>
public class BlockchainShard
{
    /// <summary>
    /// Gets or sets the last known block ID.
    /// </summary>
    [JsonPropertyName("last_known_block_id")]
    public string LastKnownBlockId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last known block.
    /// </summary>
    [JsonPropertyName("last_known_block")]
    public BlockchainBlock LastKnownBlock { get; set; } = new();
}
