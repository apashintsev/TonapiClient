using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an NFT sale information.
/// </summary>
public class Sale
{
    /// <summary>
    /// Gets or sets the sale contract address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the marketplace information.
    /// </summary>
    [JsonPropertyName("market")]
    public AccountAddress? Market { get; set; }

    /// <summary>
    /// Gets or sets the owner address.
    /// </summary>
    [JsonPropertyName("owner")]
    public AccountAddress? Owner { get; set; }

    /// <summary>
    /// Gets or sets the price in nanotons.
    /// </summary>
    [JsonPropertyName("price")]
    public TokenPrice Price { get; set; } = new();
}
