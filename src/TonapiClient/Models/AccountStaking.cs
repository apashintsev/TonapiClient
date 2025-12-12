using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents account staking information.
/// </summary>
public class AccountStaking
{
    /// <summary>
    /// Gets or sets the pool address.
    /// </summary>
    [JsonPropertyName("pool")]
    public string Pool { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the amount staked.
    /// </summary>
    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    /// <summary>
    /// Gets or sets the pending deposit amount.
    /// </summary>
    [JsonPropertyName("pending_deposit")]
    public long PendingDeposit { get; set; }

    /// <summary>
    /// Gets or sets the pending withdraw amount.
    /// </summary>
    [JsonPropertyName("pending_withdraw")]
    public long PendingWithdraw { get; set; }

    /// <summary>
    /// Gets or sets the ready withdraw amount.
    /// </summary>
    [JsonPropertyName("ready_withdraw")]
    public long ReadyWithdraw { get; set; }
}
