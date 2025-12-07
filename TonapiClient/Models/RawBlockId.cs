using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a raw block identifier.
/// </summary>
public class RawBlockId
{
    /// <summary>
    /// Gets or sets the workchain ID.
    /// </summary>
    [JsonPropertyName("workchain")]
    public int Workchain { get; set; }

    /// <summary>
    /// Gets or sets the shard ID.
    /// </summary>
    [JsonPropertyName("shard")]
    public string Shard { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the block sequence number.
    /// </summary>
    [JsonPropertyName("seqno")]
    public long Seqno { get; set; }

    /// <summary>
    /// Gets or sets the root hash.
    /// </summary>
    [JsonPropertyName("root_hash")]
    public string RootHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file hash.
    /// </summary>
    [JsonPropertyName("file_hash")]
    public string FileHash { get; set; } = string.Empty;
}
