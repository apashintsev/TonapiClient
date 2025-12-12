using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents wallet information in DNS record.
/// </summary>
public class WalletDns
{
    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account.
    /// </summary>
    [JsonPropertyName("account")]
    public AccountAddress Account { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether this has method.
    /// </summary>
    [JsonPropertyName("has_method")]
    public bool HasMethod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a wallet.
    /// </summary>
    [JsonPropertyName("is_wallet")]
    public bool IsWallet { get; set; }
}
