using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a request to send a gasless transaction.
/// </summary>
public class GaslessSendRequest
{
    /// <summary>
    /// Gets or sets the wallet public key in hex format.
    /// </summary>
    [JsonPropertyName("wallet_public_key")]
    public string WalletPublicKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base64-encoded BOC message.
    /// </summary>
    [JsonPropertyName("boc")]
    public string Boc { get; set; } = string.Empty;
}
