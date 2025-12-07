using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents NFT operation history for an account.
/// </summary>
public class NftHistory
{
    /// <summary>
    /// Gets or sets the list of NFT events.
    /// </summary>
    [JsonPropertyName("events")]
    public List<AccountEvent> Events { get; set; } = new();

    /// <summary>
    /// Gets or sets the next offset for pagination.
    /// </summary>
    [JsonPropertyName("next_from")]
    public long? NextFrom { get; set; }
}
