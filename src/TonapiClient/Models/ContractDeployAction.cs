using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a contract deploy action.
/// </summary>
public class ContractDeployAction
{
    /// <summary>
    /// Gets or sets the address of the deployed contract.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the interfaces implemented by the contract.
    /// </summary>
    [JsonPropertyName("interfaces")]
    public List<string> Interfaces { get; set; } = new();
}
