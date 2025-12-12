using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents blockchain configuration.
/// </summary>
public class BlockchainConfig
{
    /// <summary>
    /// Gets or sets the configuration value for parameter "0" (Config contract address).
    /// </summary>
    [JsonPropertyName("0")]
    public string? ConfigAddress { get; set; }

    /// <summary>
    /// Gets or sets the configuration value for parameter "1" (Elector contract address).
    /// </summary>
    [JsonPropertyName("1")]
    public string? ElectorAddress { get; set; }

    /// <summary>
    /// Gets or sets the configuration value for parameter "2" (Minter contract address).
    /// </summary>
    [JsonPropertyName("2")]
    public string? MinterAddress { get; set; }

    /// <summary>
    /// Gets or sets the configuration value for parameter "4" (DNS root contract address).
    /// </summary>
    [JsonPropertyName("4")]
    public string? DnsRootAddress { get; set; }

    /// <summary>
    /// Gets or sets the raw configuration data in base64.
    /// </summary>
    [JsonPropertyName("raw")]
    public string? Raw { get; set; }

    /// <summary>
    /// Gets or sets additional configuration parameters as a dictionary.
    /// This allows access to other numeric configuration parameters.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }
}
