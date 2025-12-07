using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of reduced blockchain blocks.
/// </summary>
public class ReducedBlocks
{
    /// <summary>
    /// Gets or sets the list of reduced blocks.
    /// </summary>
    [JsonPropertyName("blocks")]
    public List<ReducedBlock> Blocks { get; set; } = new();
}
