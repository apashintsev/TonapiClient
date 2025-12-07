using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents historical chart data for token rates.
/// </summary>
public class ChartRates
{
    /// <summary>
    /// Gets or sets the list of price points.
    /// </summary>
    [JsonPropertyName("points")]
    public List<ChartPoint> Points { get; set; } = new();
}

