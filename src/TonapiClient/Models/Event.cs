using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an event in the TON blockchain.
/// </summary>
public class Event
{
    /// <summary>
    /// Gets or sets the event ID.
    /// </summary>
    [JsonPropertyName("event_id")]
    public string EventId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account involved.
    /// </summary>
    [JsonPropertyName("account")]
    public AccountAddress Account { get; set; } = new();

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the actions performed in this event.
    /// </summary>
    [JsonPropertyName("actions")]
    public List<Action> Actions { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether the event is scam.
    /// </summary>
    [JsonPropertyName("is_scam")]
    public bool IsScam { get; set; }

    /// <summary>
    /// Gets or sets the logical time.
    /// </summary>
    [JsonPropertyName("lt")]
    public long Lt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the transaction is in progress.
    /// </summary>
    [JsonPropertyName("in_progress")]
    public bool InProgress { get; set; }

    /// <summary>
    /// Gets or sets extra data.
    /// </summary>
    [JsonPropertyName("extra")]
    public long? Extra { get; set; }

    /// <summary>
    /// Gets or sets the progress value.
    /// </summary>
    [JsonPropertyName("progress")]
    public int? Progress { get; set; }
}
