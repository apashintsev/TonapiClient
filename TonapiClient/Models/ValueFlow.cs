using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the value flow in a block.
/// </summary>
public class ValueFlow
{
  /// <summary>
  /// Gets or sets the value flow from the previous block.
  /// </summary>
  [JsonPropertyName("from_prev_blk")]
  public ValueFlowEntry FromPreviousBlock { get; set; } = new();

  /// <summary>
  /// Gets or sets the value flow to the next block.
  /// </summary>
  [JsonPropertyName("to_next_blk")]
  public ValueFlowEntry ToNextBlock { get; set; } = new();

  /// <summary>
  /// Gets or sets the imported value flow.
  /// </summary>
  [JsonPropertyName("imported")]
  public ValueFlowEntry Imported { get; set; } = new();

  /// <summary>
  /// Gets or sets the exported value flow.
  /// </summary>
  [JsonPropertyName("exported")]
  public ValueFlowEntry Exported { get; set; } = new();

  /// <summary>
  /// Gets or sets the collected fees.
  /// </summary>
  [JsonPropertyName("fees_collected")]
  public ValueFlowEntry FeesCollected { get; set; } = new();

  /// <summary>
  /// Gets or sets the burned value.
  /// </summary>
  [JsonPropertyName("burned")]
  public ValueFlowEntry Burned { get; set; } = new();

  /// <summary>
  /// Gets or sets the imported fees.
  /// </summary>
  [JsonPropertyName("fees_imported")]
  public ValueFlowEntry FeesImported { get; set; } = new();

  /// <summary>
  /// Gets or sets the recovered value.
  /// </summary>
  [JsonPropertyName("recovered")]
  public ValueFlowEntry Recovered { get; set; } = new();

  /// <summary>
  /// Gets or sets the created value.
  /// </summary>
  [JsonPropertyName("created")]
  public ValueFlowEntry Created { get; set; } = new();

  /// <summary>
  /// Gets or sets the minted value.
  /// </summary>
  [JsonPropertyName("minted")]
  public ValueFlowEntry Minted { get; set; } = new();
}
