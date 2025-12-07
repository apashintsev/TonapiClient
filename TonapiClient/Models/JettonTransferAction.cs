using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a Jetton transfer action.
/// </summary>
public class JettonTransferAction
{
    /// <summary>
    /// Gets or sets the sender.
    /// </summary>
    [JsonPropertyName("sender")]
    public AccountAddress? Sender { get; set; }

    /// <summary>
    /// Gets or sets the recipient.
    /// </summary>
    [JsonPropertyName("recipient")]
    public AccountAddress? Recipient { get; set; }

    /// <summary>
    /// Gets or sets the senders wallet address.
    /// </summary>
    [JsonPropertyName("senders_wallet")]
    public string SendersWallet { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipients wallet address.
    /// </summary>
    [JsonPropertyName("recipients_wallet")]
    public string RecipientsWallet { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the amount transferred.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

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

    /// <summary>
    /// Gets or sets the jetton information.
    /// </summary>
    [JsonPropertyName("jetton")]
    public JettonPreview Jetton { get; set; } = new();
}
