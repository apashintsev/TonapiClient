using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents additional parameters for message emulation.
/// </summary>
public class EmulateMessageParam
{
    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the balance.
    /// </summary>
    [JsonPropertyName("balance")]
    public long? Balance { get; set; }
}

