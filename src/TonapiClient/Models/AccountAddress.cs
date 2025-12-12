using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a TON blockchain account address.
/// </summary>
public class AccountAddress
{
    /// <summary>
    /// Gets or sets the account address in any format (raw, user-friendly, etc.).
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account name if available.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a scam address.
    /// </summary>
    [JsonPropertyName("is_scam")]
    public bool IsScam { get; set; }

    /// <summary>
    /// Gets or sets the icon URL for the account.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a wallet address.
    /// </summary>
    [JsonPropertyName("is_wallet")]
    public bool IsWallet { get; set; }
}
