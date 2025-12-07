using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the list of validators.
/// </summary>
public class Validators
{
    /// <summary>
    /// Gets or sets the election ID.
    /// </summary>
    [JsonPropertyName("elect_at")]
    public long ElectAt { get; set; }

    /// <summary>
    /// Gets or sets the election close timestamp.
    /// </summary>
    [JsonPropertyName("elect_close")]
    public long ElectClose { get; set; }

    /// <summary>
    /// Gets or sets the min stake.
    /// </summary>
    [JsonPropertyName("min_stake")]
    public string MinStake { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total stake.
    /// </summary>
    [JsonPropertyName("total_stake")]
    public string TotalStake { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of validators.
    /// </summary>
    [JsonPropertyName("validators")]
    public List<ValidatorInfo> ValidatorsList { get; set; } = new();
}
