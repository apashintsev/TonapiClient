using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a paginated collection of account events.
/// </summary>
public class AccountEvents
{
    /// <summary>
    /// Gets or sets the list of events.
    /// </summary>
    [JsonPropertyName("events")]
    public List<Event> Events { get; set; } = new();

    /// <summary>
    /// Gets or sets the next offset for pagination.
    /// </summary>
    [JsonPropertyName("next_from")]
    public long? NextFrom { get; set; }
}
