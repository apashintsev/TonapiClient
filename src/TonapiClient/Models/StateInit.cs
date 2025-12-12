using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents state initialization data for a contract.
/// </summary>
public class StateInit
{
    /// <summary>
    /// Gets or sets the BOC (Bag of Cells) representation of the state init.
    /// </summary>
    [JsonPropertyName("boc")]
    public string? Boc { get; set; }

    /// <summary>
    /// Gets or sets the list of interfaces supported by the contract.
    /// </summary>
    [JsonPropertyName("interfaces")]
    public List<string>? Interfaces { get; set; }
}

