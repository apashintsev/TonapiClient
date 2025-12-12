using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of wallet accounts.
/// </summary>
public class WalletAccounts
{
    /// <summary>
    /// Gets or sets the list of wallet accounts.
    /// </summary>
    [JsonPropertyName("accounts")]
    public List<WalletInfo> Accounts { get; set; } = new();
}

