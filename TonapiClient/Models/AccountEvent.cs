using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an account event (transaction with actions).
/// </summary>
public class AccountEvent
{
    /// <summary>
    /// Gets or sets the event ID.
    /// </summary>
    [JsonPropertyName("event_id")]
    public string EventId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the actions.
    /// </summary>
    [JsonPropertyName("actions")]
    public List<Action> Actions { get; set; } = new();

    /// <summary>
    /// Gets or sets the account.
    /// </summary>
    [JsonPropertyName("account")]
    public AccountAddress Account { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether this is a scam event.
    /// </summary>
    [JsonPropertyName("is_scam")]
    public bool IsScam { get; set; }

    /// <summary>
    /// Gets or sets the logical time.
    /// </summary>
    [JsonPropertyName("lt")]
    public long Lt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the event is in progress.
    /// </summary>
    [JsonPropertyName("in_progress")]
    public bool InProgress { get; set; }
}
