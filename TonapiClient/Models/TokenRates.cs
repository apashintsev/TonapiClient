using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents token rates information.
/// </summary>
public class TokenRates
{
    /// <summary>
    /// Gets or sets the prices in different currencies.
    /// </summary>
    [JsonPropertyName("prices")]
    public Dictionary<string, decimal>? Prices { get; set; }

    /// <summary>
    /// Gets or sets the 24h difference percentage.
    /// </summary>
    [JsonPropertyName("diff_24h")]
    public Dictionary<string, string>? Diff24h { get; set; }

    /// <summary>
    /// Gets or sets the 7d difference percentage.
    /// </summary>
    [JsonPropertyName("diff_7d")]
    public Dictionary<string, string>? Diff7d { get; set; }

    /// <summary>
    /// Gets or sets the 30d difference percentage.
    /// </summary>
    [JsonPropertyName("diff_30d")]
    public Dictionary<string, string>? Diff30d { get; set; }
}
