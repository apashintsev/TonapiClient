using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a TON transfer action.
/// </summary>
public class TonTransferAction
{
    /// <summary>
    /// Gets or sets the sender.
    /// </summary>
    [JsonPropertyName("sender")]
    public AccountAddress Sender { get; set; } = new();

    /// <summary>
    /// Gets or sets the recipient.
    /// </summary>
    [JsonPropertyName("recipient")]
    public AccountAddress Recipient { get; set; } = new();

    /// <summary>
    /// Gets or sets the amount transferred in nanotons.
    /// </summary>
    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    /// <summary>
    /// Gets or sets the comment if any.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the encrypted comment if any.
    /// </summary>
    [JsonPropertyName("encrypted_comment")]
    public EncryptedComment? EncryptedComment { get; set; }
}
