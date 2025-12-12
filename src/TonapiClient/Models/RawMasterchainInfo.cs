using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents masterchain information from lite server.
/// </summary>
public class RawMasterchainInfo
{
    /// <summary>
    /// Gets or sets the last block.
    /// </summary>
    [JsonPropertyName("last")]
    public RawBlockId Last { get; set; } = new();

    /// <summary>
    /// Gets or sets the state root hash.
    /// </summary>
    [JsonPropertyName("state_root_hash")]
    public string StateRootHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the init block.
    /// </summary>
    [JsonPropertyName("init")]
    public RawBlockId Init { get; set; } = new();
}
