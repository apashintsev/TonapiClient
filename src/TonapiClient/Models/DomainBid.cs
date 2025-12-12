using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a bid on a DNS domain auction.
/// </summary>
public class DomainBid
{
    /// <summary>
    /// Gets or sets a value indicating whether the bid was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the bid value in nanotons.
    /// </summary>
    [JsonPropertyName("value")]
    public long Value { get; set; }

    /// <summary>
    /// Gets or sets the transaction hash.
    /// </summary>
    [JsonPropertyName("txHash")]
    public string TxHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the transaction timestamp.
    /// </summary>
    [JsonPropertyName("txTime")]
    public long TxTime { get; set; }

    /// <summary>
    /// Gets or sets the bidder account.
    /// </summary>
    [JsonPropertyName("bidder")]
    public AccountAddress Bidder { get; set; } = new();

    /// <summary>
    /// Gets or sets the auction info.
    /// </summary>
    [JsonPropertyName("auction")]
    public AccountAddress Auction { get; set; } = new();

    /// <summary>
    /// Gets or sets the NFT info.
    /// </summary>
    [JsonPropertyName("nft")]
    public AccountAddress? Nft { get; set; }
}

