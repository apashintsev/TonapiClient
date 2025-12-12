using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a message in raw signature parameters.
/// </summary>
public class SignRawMessage
{
    /// <summary>
    /// Gets or sets the destination address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the amount to send in nanotons.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state init if deploying contract.
    /// </summary>
    [JsonPropertyName("state_init")]
    public string? StateInit { get; set; }

    /// <summary>
    /// Gets or sets the message payload.
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }
}
