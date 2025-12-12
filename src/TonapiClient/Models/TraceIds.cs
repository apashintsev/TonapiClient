using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of trace identifiers.
/// </summary>
public class TraceIds
{
    /// <summary>
    /// Gets or sets the list of trace identifiers.
    /// </summary>
    [JsonPropertyName("traces")]
    public List<TraceId> Traces { get; set; } = new();
}

