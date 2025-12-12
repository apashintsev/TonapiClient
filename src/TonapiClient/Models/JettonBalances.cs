using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of Jetton balances.
/// </summary>
public class JettonBalances
{
    /// <summary>
    /// Gets or sets the list of jetton balances.
    /// </summary>
    [JsonPropertyName("balances")]
    public List<JettonBalance> Balances { get; set; } = new();
}
