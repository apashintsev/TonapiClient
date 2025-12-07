using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a public key response.
/// </summary>
public class PublicKey
{
    /// <summary>
    /// Gets or sets the public key in hex format.
    /// </summary>
    [JsonPropertyName("public_key")]
    public string PublicKeyHex { get; set; } = string.Empty;
}
