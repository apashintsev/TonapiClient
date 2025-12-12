using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a request to emulate a message.
/// </summary>
public class EmulateMessageRequest
{
    /// <summary>
    /// Gets or sets the base64-encoded BOC message.
    /// </summary>
    [JsonPropertyName("boc")]
    public string Boc { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional parameters for emulation.
    /// </summary>
    [JsonPropertyName("params")]
    public List<EmulateMessageParam>? Params { get; set; }
}

