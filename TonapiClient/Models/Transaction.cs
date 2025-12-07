using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a TON blockchain transaction.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Gets or sets the account involved in this transaction.
    /// </summary>
    [JsonPropertyName("account")]
    public AccountAddress Account { get; set; } = new();

    /// <summary>
    /// Gets or sets the logical time.
    /// </summary>
    [JsonPropertyName("lt")]
    public ulong Lt { get; set; }

    /// <summary>
    /// Gets or sets the transaction hash.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unix timestamp of the transaction.
    /// </summary>
    [JsonPropertyName("utime")]
    public ulong Utime { get; set; }

    /// <summary>
    /// Gets or sets the transaction fee in nanotons.
    /// </summary>
    [JsonPropertyName("fee")]
    public ulong Fee { get; set; }

    /// <summary>
    /// Gets or sets the total fees for the transaction in nanotons.
    /// </summary>
    [JsonPropertyName("total_fees")]
    public ulong TotalFees { get; set; }

    /// <summary>
    /// Gets or sets the storage fee.
    /// </summary>
    [JsonPropertyName("storage_fee")]
    public ulong StorageFee { get; set; }

    /// <summary>
    /// Gets or sets the other fee.
    /// </summary>
    [JsonPropertyName("other_fee")]
    public ulong OtherFee { get; set; }

    /// <summary>
    /// Gets or sets the in message.
    /// </summary>
    [JsonPropertyName("in_msg")]
    public Message? InMsg { get; set; }

    /// <summary>
    /// Gets or sets the list of out messages.
    /// </summary>
    [JsonPropertyName("out_msgs")]
    public List<Message> OutMsgs { get; set; } = new();

    /// <summary>
    /// Gets or sets the block reference.
    /// </summary>
    [JsonPropertyName("block")]
    public string? Block { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the transaction was aborted.
    /// </summary>
    [JsonPropertyName("aborted")]
    public bool Aborted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the transaction was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }
}
