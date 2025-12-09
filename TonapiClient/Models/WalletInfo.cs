using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents detailed information about a wallet account.
/// </summary>
public class WalletInfo
{
    /// <summary>
    /// Gets or sets the wallet address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this is a wallet.
    /// </summary>
    [JsonPropertyName("is_wallet")]
    public bool IsWallet { get; set; }

    /// <summary>
    /// Gets or sets the wallet balance in nanotons.
    /// </summary>
    [JsonPropertyName("balance")]
    public ulong Balance { get; set; }

    /// <summary>
    /// Gets or sets the wallet statistics.
    /// </summary>
    [JsonPropertyName("stats")]
    public WalletStats Stats { get; set; } = new();

    /// <summary>
    /// Gets or sets the list of plugins installed in the wallet.
    /// </summary>
    [JsonPropertyName("plugins")]
    public List<string> Plugins { get; set; } = new();

    /// <summary>
    /// Gets or sets the wallet status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last activity timestamp.
    /// </summary>
    [JsonPropertyName("last_activity")]
    public ulong LastActivity { get; set; }

    /// <summary>
    /// Gets or sets the list of get methods available for this wallet.
    /// </summary>
    [JsonPropertyName("get_methods")]
    public List<string> GetMethods { get; set; } = new();

    /// <summary>
    /// Gets or sets the interfaces supported by this wallet.
    /// </summary>
    [JsonPropertyName("interfaces")]
    public List<string> Interfaces { get; set; } = new();

    /// <summary>
    /// Gets or sets the last logical time.
    /// </summary>
    [JsonPropertyName("last_lt")]
    public ulong LastLt { get; set; }
}

