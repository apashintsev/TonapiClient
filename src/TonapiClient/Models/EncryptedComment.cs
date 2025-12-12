using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an encrypted comment.
/// </summary>
public class EncryptedComment
{
    /// <summary>
    /// Gets or sets the encryption type.
    /// </summary>
    [JsonPropertyName("encryption_type")]
    public string EncryptionType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cipher text.
    /// </summary>
    [JsonPropertyName("cipher_text")]
    public string CipherText { get; set; } = string.Empty;
}
