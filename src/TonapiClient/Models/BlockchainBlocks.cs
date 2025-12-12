using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of blockchain blocks.
/// </summary>
public class BlockchainBlocks
{
    /// <summary>
    /// Gets or sets the list of blocks.
    /// </summary>
    [JsonPropertyName("blocks")]
    public List<BlockchainBlock> Blocks { get; set; } = new();
}
