using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a response from sending a BOC message.
/// </summary>
public class SendBocResponse
{
    /// <summary>
    /// Gets or sets the message hash.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;
}
