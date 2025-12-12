using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents TON rate on a specific market.
/// </summary>
public class MarketTonRate
{
    /// <summary>
    /// Gets or sets the market name.
    /// </summary>
    [JsonPropertyName("market")]
    public string Market { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the USD price.
    /// </summary>
    [JsonPropertyName("usd_price")]
    public decimal UsdPrice { get; set; }

    /// <summary>
    /// Gets or sets the last update timestamp.
    /// </summary>
    [JsonPropertyName("last_date_update")]
    public long LastDateUpdate { get; set; }
}
