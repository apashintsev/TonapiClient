using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a staking pool information.
/// </summary>
public class PoolInfo
{
    /// <summary>
    /// Gets or sets the pool address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pool name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total amount staked.
    /// </summary>
    [JsonPropertyName("total_amount")]
    public long TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the implementation type.
    /// </summary>
    [JsonPropertyName("implementation")]
    public string Implementation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the APY (Annual Percentage Yield).
    /// </summary>
    [JsonPropertyName("apy")]
    public decimal Apy { get; set; }

    /// <summary>
    /// Gets or sets the minimum stake amount.
    /// </summary>
    [JsonPropertyName("min_stake")]
    public long MinStake { get; set; }

    /// <summary>
    /// Gets or sets the cycle start timestamp.
    /// </summary>
    [JsonPropertyName("cycle_start")]
    public long? CycleStart { get; set; }

    /// <summary>
    /// Gets or sets the cycle end timestamp.
    /// </summary>
    [JsonPropertyName("cycle_end")]
    public long? CycleEnd { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether verification is available.
    /// </summary>
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    /// <summary>
    /// Gets or sets the current nominators count.
    /// </summary>
    [JsonPropertyName("current_nominators")]
    public int CurrentNominators { get; set; }

    /// <summary>
    /// Gets or sets the maximum nominators count.
    /// </summary>
    [JsonPropertyName("max_nominators")]
    public int MaxNominators { get; set; }

    /// <summary>
    /// Gets or sets the liquid jetton master address.
    /// </summary>
    [JsonPropertyName("liquid_jetton_master")]
    public string? LiquidJettonMaster { get; set; }

    /// <summary>
    /// Gets or sets the nominators stake information.
    /// </summary>
    [JsonPropertyName("nominators_stake")]
    public long NominatorsStake { get; set; }

    /// <summary>
    /// Gets or sets the validator stake information.
    /// </summary>
    [JsonPropertyName("validator_stake")]
    public long ValidatorStake { get; set; }
}
