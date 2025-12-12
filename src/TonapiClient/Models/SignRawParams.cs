using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents raw signature parameters for gasless transactions.
/// </summary>
public class SignRawParams
{
    /// <summary>
    /// Gets or sets the relay address.
    /// </summary>
    [JsonPropertyName("relay_address")]
    public string RelayAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiration timestamp.
    /// </summary>
    [JsonPropertyName("expire")]
    public long Expire { get; set; }

    /// <summary>
    /// Gets or sets the messages to sign.
    /// </summary>
    [JsonPropertyName("messages")]
    public List<SignRawMessage> Messages { get; set; } = new();
}
