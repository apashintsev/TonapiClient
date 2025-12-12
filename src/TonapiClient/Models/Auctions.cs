using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of DNS auctions.
/// </summary>
public class Auctions
{
    /// <summary>
    /// Gets or sets the list of auctions.
    /// </summary>
    [JsonPropertyName("data")]
    public List<Auction> Data { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of auctions.
    /// </summary>
    [JsonPropertyName("total")]
    public long Total { get; set; }
}

