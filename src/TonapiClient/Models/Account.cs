using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a TON blockchain account with full information.
/// </summary>
public class Account
{
    /// <summary>
    /// Gets or sets the account address in any format.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account balance in nanotons.
    /// </summary>
    [JsonPropertyName("balance")]
    public ulong Balance { get; set; }

    /// <summary>
    /// Gets or sets the account status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last activity timestamp.
    /// </summary>
    [JsonPropertyName("last_activity")]
    public ulong LastActivity { get; set; }

    /// <summary>
    /// Gets or sets the account name if available.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a scam account.
    /// </summary>
    [JsonPropertyName("is_scam")]
    public bool IsScam { get; set; }

    /// <summary>
    /// Gets or sets the icon URL for the account.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a wallet.
    /// </summary>
    [JsonPropertyName("is_wallet")]
    public bool IsWallet { get; set; }

    /// <summary>
    /// Gets or sets interfaces supported by this account.
    /// </summary>
    [JsonPropertyName("interfaces")]
    public List<string>? Interfaces { get; set; }

    /// <summary>
    /// Gets or sets the memo required flag.
    /// </summary>
    [JsonPropertyName("memo_required")]
    public bool? MemoRequired { get; set; }

    /// <summary>
    /// Gets or sets the list of methods available for this account.
    /// </summary>
    [JsonPropertyName("get_methods")]
    public List<string>? GetMethods { get; set; }
}
