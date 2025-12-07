using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents information about a validator.
/// </summary>
public class ValidatorInfo
{
    /// <summary>
    /// Gets or sets the validator address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ADNL address.
    /// </summary>
    [JsonPropertyName("adnl_address")]
    public string AdnlAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the stake in nanotons.
    /// </summary>
    [JsonPropertyName("stake")]
    public string Stake { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the weight.
    /// </summary>
    [JsonPropertyName("weight")]
    public long Weight { get; set; }
}
