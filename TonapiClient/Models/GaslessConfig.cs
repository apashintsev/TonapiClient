using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents gasless configuration for a specific jetton.
/// </summary>
public class GaslessConfig
{
    /// <summary>
    /// Gets or sets the gas jetton master address.
    /// </summary>
    [JsonPropertyName("gas_jetton_master")]
    public string GasJettonMaster { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relay address.
    /// </summary>
    [JsonPropertyName("relay_address")]
    public string RelayAddress { get; set; } = string.Empty;
}
