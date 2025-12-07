using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a paginated collection of NFT items.
/// </summary>
public class NftItems
{
    /// <summary>
    /// Gets or sets the list of NFT items.
    /// </summary>
    [JsonPropertyName("nft_items")]
    public List<NftItem> Items { get; set; } = new();

    /// <summary>
    /// Gets or sets the next offset for pagination.
    /// </summary>
    [JsonPropertyName("next_from")]
    public long? NextFrom { get; set; }
}
