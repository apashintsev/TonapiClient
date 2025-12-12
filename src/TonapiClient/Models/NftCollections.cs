using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of NFT collections.
/// </summary>
public class NftCollections
{
    /// <summary>
    /// Gets or sets the list of NFT collections.
    /// </summary>
    [JsonPropertyName("nft_collections")]
    public List<NftCollection> Collections { get; set; } = new();
}

