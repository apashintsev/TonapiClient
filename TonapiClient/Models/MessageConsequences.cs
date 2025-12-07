using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the consequences of a message emulation.
/// </summary>
public class MessageConsequences
{
    /// <summary>
    /// Gets or sets the trace.
    /// </summary>
    [JsonPropertyName("trace")]
    public Trace Trace { get; set; } = new();

    /// <summary>
    /// Gets or sets the risk analysis.
    /// </summary>
    [JsonPropertyName("risk")]
    public Risk? Risk { get; set; }

    /// <summary>
    /// Gets or sets the event.
    /// </summary>
    [JsonPropertyName("event")]
    public AccountEvent Event { get; set; } = new();
}
