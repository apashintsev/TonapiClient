using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents token price information.
/// </summary>
public class TokenPrice
{
    /// <summary>
    /// Gets or sets the price value.
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    /// <summary>
    /// Gets or sets the 24h price difference.
    /// </summary>
    [JsonPropertyName("diff_24h")]
    public string? Diff24h { get; set; }

    /// <summary>
    /// Gets or sets the 7d price difference.
    /// </summary>
    [JsonPropertyName("diff_7d")]
    public string? Diff7d { get; set; }

    /// <summary>
    /// Gets or sets the 30d price difference.
    /// </summary>
    [JsonPropertyName("diff_30d")]
    public string? Diff30d { get; set; }
}
