using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a single point in a price chart.
/// </summary>
public class ChartPoint
{
    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the price value.
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}

