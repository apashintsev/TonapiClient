using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a single value flow entry with grams and other currencies.
/// </summary>
public class ValueFlowEntry
{
  /// <summary>
  /// Gets or sets the amount in nanotons (grams).
  /// </summary>
  [JsonPropertyName("grams")]
  public ulong Grams { get; set; }

  /// <summary>
  /// Gets or sets the collection of other currencies.
  /// </summary>
  [JsonPropertyName("other")]
  public List<OtherCurrencyValue> Other { get; set; } = new();
}
