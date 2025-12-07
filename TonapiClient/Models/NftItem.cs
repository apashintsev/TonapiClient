using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an NFT item.
/// </summary>
public class NftItem
{
    /// <summary>
    /// Gets or sets the NFT address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection address if part of a collection.
    /// </summary>
    [JsonPropertyName("collection")]
    public NftCollection? Collection { get; set; }

    /// <summary>
    /// Gets or sets the verified status.
    /// </summary>
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    /// <summary>
    /// Gets or sets the metadata.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Gets or sets the owner address.
    /// </summary>
    [JsonPropertyName("owner")]
    public AccountAddress? Owner { get; set; }

    /// <summary>
    /// Gets or sets the collection data.
    /// </summary>
    [JsonPropertyName("collection_address")]
    public string? CollectionAddress { get; set; }

    /// <summary>
    /// Gets or sets the index in the collection.
    /// </summary>
    [JsonPropertyName("index")]
    public long? Index { get; set; }

    /// <summary>
    /// Gets or sets the sale information if on sale.
    /// </summary>
    [JsonPropertyName("sale")]
    public Sale? Sale { get; set; }

    /// <summary>
    /// Gets or sets the previews.
    /// </summary>
    [JsonPropertyName("previews")]
    public List<ImagePreview>? Previews { get; set; }

    /// <summary>
    /// Gets or sets the DNS domain name if applicable.
    /// </summary>
    [JsonPropertyName("dns")]
    public string? Dns { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the NFT is approved by the collection.
    /// </summary>
    [JsonPropertyName("approved_by")]
    public List<string>? ApprovedBy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether trust is provided.
    /// </summary>
    [JsonPropertyName("trust")]
    public string? Trust { get; set; }
}
