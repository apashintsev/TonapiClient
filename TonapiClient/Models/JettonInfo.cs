using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a Jetton (token) information.
/// </summary>
public class JettonInfo
{
    /// <summary>
    /// Gets or sets the master address of the jetton.
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
    /// Gets or sets the jetton description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the total supply.
    /// </summary>
    [JsonPropertyName("total_supply")]
    public string TotalSupply { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this jetton is mintable.
    /// </summary>
    [JsonPropertyName("mintable")]
    public bool Mintable { get; set; }

    /// <summary>
    /// Gets or sets the admin address.
    /// </summary>
    [JsonPropertyName("admin")]
    public AccountAddress? Admin { get; set; }

    /// <summary>
    /// Gets or sets metadata.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether verification is available.
    /// </summary>
    [JsonPropertyName("verification")]
    public string? Verification { get; set; }

    /// <summary>
    /// Gets or sets the holders count.
    /// </summary>
    [JsonPropertyName("holders_count")]
    public int? HoldersCount { get; set; }
}
