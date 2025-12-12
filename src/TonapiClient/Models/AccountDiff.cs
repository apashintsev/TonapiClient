using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the difference in account balances between two timestamps.
/// </summary>
public class AccountDiff
{
    /// <summary>
    /// Gets or sets the TON balance change.
    /// </summary>
    [JsonPropertyName("balance_change")]
    public long BalanceChange { get; set; }
}

