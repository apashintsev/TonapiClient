using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents historical chart data for token rates.
/// </summary>
public class ChartRates
{
    /// <summary>
    /// Gets or sets the list of price points.
    /// Each point is an array of [timestamp, price].
    /// </summary>
    [JsonPropertyName("points")]
    public List<List<decimal>> Points { get; set; } = new();
}

