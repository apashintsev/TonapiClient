using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a DNS domain auction.
/// </summary>
public class Auction
{
    /// <summary>
    /// Gets or sets the domain name.
    /// </summary>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the owner address.
    /// </summary>
    [JsonPropertyName("owner")]
    public string Owner { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price in nanotons.
    /// </summary>
    [JsonPropertyName("price")]
    public long Price { get; set; }

    /// <summary>
    /// Gets or sets the current bid amount.
    /// </summary>
    [JsonPropertyName("bids")]
    public long Bids { get; set; }

    /// <summary>
    /// Gets or sets the auction end date timestamp.
    /// </summary>
    [JsonPropertyName("date")]
    public long Date { get; set; }
}

