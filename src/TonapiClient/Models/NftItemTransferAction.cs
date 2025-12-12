using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an NFT item transfer action.
/// </summary>
public class NftItemTransferAction
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
    /// Gets or sets the NFT address.
    /// </summary>
    [JsonPropertyName("nft")]
    public string Nft { get; set; } = string.Empty;

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
    /// Gets or sets the payload if any.
    /// </summary>
    [JsonPropertyName("payload")]
    public string? Payload { get; set; }
}
