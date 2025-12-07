using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents raw account state response from the lite server.
/// </summary>
/// <remarks>
/// This DTO corresponds to /v2/liteserver/get_account_state/{accountId}.
/// Note: balance/code/data are not provided directly; the raw state BOC is in <see cref="State"/>.
/// </remarks>
public sealed class LiteServerAccountState
{
  /// <summary>
  /// Gets or sets the requested block identifier at which the state was fetched.
  /// </summary>
  [JsonPropertyName("id")]
  public BlockIdExt Id { get; set; } = new();

  /// <summary>
  /// Gets or sets the shard block identifier actually used to fetch the state.
  /// </summary>
  [JsonPropertyName("shardblk")]
  public BlockIdExt ShardBlock { get; set; } = new();

  /// <summary>
  /// Gets or sets the shard proof (base64-encoded BOC).
  /// </summary>
  [JsonPropertyName("shard_proof")]
  public string ShardProof { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the proof (base64-encoded BOC) linking the state to the specified block.
  /// </summary>
  [JsonPropertyName("proof")]
  public string Proof { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the raw account state (base64-encoded BOC).
  /// </summary>
  [JsonPropertyName("state")]
  public string State { get; set; } = string.Empty;
}
