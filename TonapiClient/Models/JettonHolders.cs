using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of Jetton holders.
/// </summary>
public class JettonHolders
{
    /// <summary>
    /// Gets or sets the list of addresses holding the jetton.
    /// </summary>
    [JsonPropertyName("addresses")]
    public List<JettonHolder> Addresses { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of holders.
    /// </summary>
    [JsonPropertyName("total")]
    public long Total { get; set; }
}
