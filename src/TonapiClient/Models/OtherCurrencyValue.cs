using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a value in an additional currency within a value flow entry.
/// </summary>
public class OtherCurrencyValue
{
  /// <summary>
  /// Gets or sets the currency identifier.
  /// </summary>
  [JsonPropertyName("id")]
  public ulong Id { get; set; }

  /// <summary>
  /// Gets or sets the value as a string.
  /// </summary>
  [JsonPropertyName("value")]
  public string Value { get; set; } = string.Empty;
}
