using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents TON rates across different markets/exchanges.
/// </summary>
public class MarketTonRates
{
    /// <summary>
    /// Gets or sets the list of market rates.
    /// </summary>
    [JsonPropertyName("markets")]
    public List<MarketTonRate> Markets { get; set; } = new();
}
