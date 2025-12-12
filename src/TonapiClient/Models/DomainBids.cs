using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents bids information for a DNS domain auction.
/// </summary>
public class DomainBids
{
    /// <summary>
    /// Gets or sets the list of bids.
    /// </summary>
    [JsonPropertyName("data")]
    public List<DomainBid> Data { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of bids.
    /// </summary>
    [JsonPropertyName("total")]
    public long Total { get; set; }
}

