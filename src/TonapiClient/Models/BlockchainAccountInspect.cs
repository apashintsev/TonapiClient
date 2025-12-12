using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents detailed inspection of a blockchain account contract.
/// </summary>
public class BlockchainAccountInspect
{
    /// <summary>
    /// Gets or sets the account code in base64.
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the code hash.
    /// </summary>
    [JsonPropertyName("code_hash")]
    public string CodeHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the available get methods.
    /// </summary>
    [JsonPropertyName("methods")]
    public List<Method> Methods { get; set; } = new();

    /// <summary>
    /// Gets or sets the compiler type (func, fift, tact).
    /// </summary>
    [JsonPropertyName("compiler")]
    public string? Compiler { get; set; }
}

