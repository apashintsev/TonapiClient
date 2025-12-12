using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the transfer history of a specific NFT item.
/// </summary>
public class NftItemHistory
{
    /// <summary>
    /// Gets or sets the list of events.
    /// </summary>
    [JsonPropertyName("events")]
    public List<AccountEvent> Events { get; set; } = new();

    /// <summary>
    /// Gets or sets the next offset for pagination.
    /// </summary>
    [JsonPropertyName("next_from")]
    public long? NextFrom { get; set; }
}
