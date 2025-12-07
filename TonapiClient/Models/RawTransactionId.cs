using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a raw transaction identifier.
/// </summary>
public class RawTransactionId
{
    /// <summary>
    /// Gets or sets the transaction logical time.
    /// </summary>
    [JsonPropertyName("lt")]
    public string Lt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the transaction hash.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;
}
