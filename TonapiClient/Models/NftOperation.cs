using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a single NFT operation (transfer) in the history.
/// </summary>
public class NftOperation
{
    /// <summary>
    /// Gets or sets the operation type (e.g., "transfer").
    /// </summary>
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unix timestamp.
    /// </summary>
    [JsonPropertyName("utime")]
    public long Utime { get; set; }

    /// <summary>
    /// Gets or sets the logical time.
    /// </summary>
    [JsonPropertyName("lt")]
    public long Lt { get; set; }

    /// <summary>
    /// Gets or sets the transaction hash.
    /// </summary>
    [JsonPropertyName("transaction_hash")]
    public string TransactionHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source account.
    /// </summary>
    [JsonPropertyName("source")]
    public AccountAddress Source { get; set; } = new();

    /// <summary>
    /// Gets or sets the destination account.
    /// </summary>
    [JsonPropertyName("destination")]
    public AccountAddress Destination { get; set; } = new();

    /// <summary>
    /// Gets or sets the NFT item information.
    /// </summary>
    [JsonPropertyName("item")]
    public NftItem Item { get; set; } = new();
}

