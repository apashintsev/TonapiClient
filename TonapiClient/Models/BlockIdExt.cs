using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an extended block identifier returned by the lite server.
/// </summary>
public sealed class BlockIdExt
{
  /// <summary>
  /// Gets or sets the workchain ID (can be negative, e.g., -1 for masterchain).
  /// </summary>
  [JsonPropertyName("workchain")]
  public int Workchain { get; set; }

  /// <summary>
  /// Gets or sets the shard identifier in hex string form (e.g., "8000000000000000").
  /// </summary>
  [JsonPropertyName("shard")]
  public string Shard { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the block sequence number.
  /// </summary>
  [JsonPropertyName("seqno")]
  public int Seqno { get; set; }

  /// <summary>
  /// Gets or sets the root hash of the block (hex string).
  /// </summary>
  [JsonPropertyName("root_hash")]
  public string RootHash { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the file hash of the block (hex string).
  /// </summary>
  [JsonPropertyName("file_hash")]
  public string FileHash { get; set; } = string.Empty;
}
