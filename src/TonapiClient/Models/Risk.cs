using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents risk analysis for a transaction.
/// </summary>
public class Risk
{
    /// <summary>
    /// Gets or sets a value indicating whether this transaction is potentially dangerous.
    /// </summary>
    [JsonPropertyName("transfer_all_remaining_balance")]
    public bool TransferAllRemainingBalance { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether TON will be transferred.
    /// </summary>
    [JsonPropertyName("ton")]
    public bool Ton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Jettons will be transferred.
    /// </summary>
    [JsonPropertyName("jettons")]
    public bool Jettons { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether NFTs will be transferred.
    /// </summary>
    [JsonPropertyName("nfts")]
    public bool Nfts { get; set; }
}
