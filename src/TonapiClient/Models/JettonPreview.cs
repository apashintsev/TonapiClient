using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a Jetton preview with basic information.
/// </summary>
public class JettonPreview
{
    /// <summary>
    /// Gets or sets the jetton address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the jetton name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the jetton symbol.
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of decimals.
    /// </summary>
    [JsonPropertyName("decimals")]
    public int Decimals { get; set; }

    /// <summary>
    /// Gets or sets the jetton image URL.
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    /// <summary>
    /// Gets or sets the verification status.
    /// </summary>
    [JsonPropertyName("verification")]
    public string? Verification { get; set; }

    /// <summary>
    /// Gets or sets the jetton score.
    /// </summary>
    [JsonPropertyName("score")]
    public int? Score { get; set; }
}
