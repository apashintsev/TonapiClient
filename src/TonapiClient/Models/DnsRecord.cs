using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a DNS record.
/// </summary>
public class DnsRecord
{
    /// <summary>
    /// Gets or sets the wallet information if this DNS points to a wallet.
    /// </summary>
    [JsonPropertyName("wallet")]
    public WalletDns? Wallet { get; set; }

    /// <summary>
    /// Gets or sets the next resolver if any.
    /// </summary>
    [JsonPropertyName("next_resolver")]
    public string? NextResolver { get; set; }

    /// <summary>
    /// Gets or sets the site if any.
    /// </summary>
    [JsonPropertyName("site")]
    public string? Site { get; set; }

    /// <summary>
    /// Gets or sets the storage information.
    /// </summary>
    [JsonPropertyName("storage")]
    public string? Storage { get; set; }
}
