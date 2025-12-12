using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents staking information for an account across all pools.
/// </summary>
public class AccountStakingInfo
{
    /// <summary>
    /// Gets or sets the list of pools where the account is staking.
    /// </summary>
    [JsonPropertyName("pools")]
    public List<AccountStaking> Pools { get; set; } = new();
}

