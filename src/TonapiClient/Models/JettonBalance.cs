using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a Jetton balance for an account.
/// </summary>
public class JettonBalance
{
    /// <summary>
    /// Gets or sets the balance amount.
    /// </summary>
    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the wallet address.
    /// </summary>
    [JsonPropertyName("wallet_address")]
    public AccountAddress WalletAddress { get; set; } = new();

    /// <summary>
    /// Gets or sets the jetton information.
    /// </summary>
    [JsonPropertyName("jetton")]
    public JettonInfo Jetton { get; set; } = new();

    /// <summary>
    /// Gets or sets the lock information if available.
    /// </summary>
    [JsonPropertyName("lock")]
    public JettonLock? Lock { get; set; }

    /// <summary>
    /// Gets or sets the price in the chosen currency.
    /// </summary>
    [JsonPropertyName("price")]
    public TokenPrice? Price { get; set; }
}
