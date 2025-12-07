using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents blockchain configuration.
/// </summary>
public class BlockchainConfig
{
    /// <summary>
    /// Gets or sets the raw configuration as a dictionary.
    /// </summary>
    [JsonPropertyName("config")]
    public Dictionary<string, object> Config { get; set; } = new();
}
