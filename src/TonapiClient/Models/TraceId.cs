using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a trace identifier with timestamp.
/// </summary>
public class TraceId
{
    /// <summary>
    /// Gets or sets the trace ID.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unix timestamp.
    /// </summary>
    [JsonPropertyName("utime")]
    public long Utime { get; set; }
}

