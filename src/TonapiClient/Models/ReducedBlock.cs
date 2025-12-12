using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a reduced blockchain block with minimal information.
/// </summary>
public class ReducedBlock
{
    /// <summary>
    /// Gets or sets the workchain ID.
    /// </summary>
    [JsonPropertyName("workchain_id")]
    public int WorkchainId { get; set; }

    /// <summary>
    /// Gets or sets the shard.
    /// </summary>
    [JsonPropertyName("shard")]
    public string Shard { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sequence number.
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
