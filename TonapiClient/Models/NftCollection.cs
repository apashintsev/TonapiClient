using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an NFT collection.
/// </summary>
public class NftCollection
{
    /// <summary>
    /// Gets or sets the collection address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the owner address.
    /// </summary>
    [JsonPropertyName("owner")]
    public AccountAddress? Owner { get; set; }

    /// <summary>
    /// Gets or sets the next item index.
    /// </summary>
    [JsonPropertyName("next_item_index")]
    public long NextItemIndex { get; set; }

    /// <summary>
    /// Gets or sets the raw collection content.
    /// </summary>
    [JsonPropertyName("raw_collection_content")]
    public string? RawCollectionContent { get; set; }

    /// <summary>
    /// Gets or sets the metadata.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Gets or sets the previews.
    /// </summary>
    [JsonPropertyName("previews")]
    public List<ImagePreview>? Previews { get; set; }

    /// <summary>
    /// Gets or sets the approved by list.
    /// </summary>
    [JsonPropertyName("approved_by")]
    public List<string>? ApprovedBy { get; set; }
}
