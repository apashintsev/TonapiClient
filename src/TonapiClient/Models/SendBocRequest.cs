using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a request to send a BOC (Bag of Cells) message.
/// </summary>
public class SendBocRequest
{
    /// <summary>
    /// Gets or sets the base64-encoded BOC message.
    /// </summary>
    [JsonPropertyName("boc")]
    public string Boc { get; set; } = string.Empty;
}
