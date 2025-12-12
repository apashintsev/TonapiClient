using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a decoded message.
/// </summary>
public class DecodedMessage
{
    /// <summary>
    /// Gets or sets the source address.
    /// </summary>
    [JsonPropertyName("source")]
    public AccountAddress? Source { get; set; }

    /// <summary>
    /// Gets or sets the destination address.
    /// </summary>
    [JsonPropertyName("destination")]
    public AccountAddress Destination { get; set; } = new();

    /// <summary>
    /// Gets or sets the amount in nanotons.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message body hash.
    /// </summary>
    [JsonPropertyName("message_hash")]
    public string MessageHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the decoded operation type.
    /// </summary>
    [JsonPropertyName("operation")]
    public string? Operation { get; set; }

    /// <summary>
    /// Gets or sets the decoded comment if present.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the state init if present.
    /// </summary>
    [JsonPropertyName("state_init")]
    public string? StateInit { get; set; }
}

