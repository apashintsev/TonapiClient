using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents domain information.
/// </summary>
public class DomainInfo
{
    /// <summary>
    /// Gets or sets the domain name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiring timestamp.
    /// </summary>
    [JsonPropertyName("expiring_at")]
    public long? ExpiringAt { get; set; }

    /// <summary>
    /// Gets or sets the owner address.
    /// </summary>
    [JsonPropertyName("owner")]
    public AccountAddress? Owner { get; set; }

    /// <summary>
    /// Gets or sets the item information if this is an NFT.
    /// </summary>
    [JsonPropertyName("item")]
    public NftItem? Item { get; set; }
}
