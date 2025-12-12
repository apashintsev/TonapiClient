using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a request to decode a message.
/// </summary>
public class DecodeMessageRequest
{
    /// <summary>
    /// Gets or sets the base64-encoded message BOC.
    /// </summary>
    [JsonPropertyName("boc")]
    public string Boc { get; set; } = string.Empty;
}

