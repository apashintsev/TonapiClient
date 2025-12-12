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

    /// <summary>
    /// Gets or sets the original status of the account.
    /// </summary>
    [JsonPropertyName("orig_status")]
    public string? OrigStatus { get; set; }

    /// <summary>
    /// Gets or sets the end status of the account.
    /// </summary>
    [JsonPropertyName("end_status")]
    public string? EndStatus { get; set; }

    /// <summary>
    /// Gets or sets the end balance of the account.
    /// </summary>
    [JsonPropertyName("end_balance")]
    public ulong? EndBalance { get; set; }

    /// <summary>
    /// Gets or sets the transaction type.
    /// </summary>
    [JsonPropertyName("transaction_type")]
    public string? TransactionType { get; set; }

    /// <summary>
    /// Gets or sets the old state update hash.
    /// </summary>
    [JsonPropertyName("state_update_old")]
    public string? StateUpdateOld { get; set; }

    /// <summary>
    /// Gets or sets the new state update hash.
    /// </summary>
    [JsonPropertyName("state_update_new")]
    public string? StateUpdateNew { get; set; }

    /// <summary>
    /// Gets or sets the previous transaction hash.
    /// </summary>
    [JsonPropertyName("prev_trans_hash")]
    public string? PrevTransHash { get; set; }

    /// <summary>
    /// Gets or sets the previous transaction logical time.
    /// </summary>
    [JsonPropertyName("prev_trans_lt")]
    public ulong? PrevTransLt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the account was destroyed.
    /// </summary>
    [JsonPropertyName("destroyed")]
    public bool? Destroyed { get; set; }

    /// <summary>
    /// Gets or sets the compute phase of the transaction.
    /// </summary>
    [JsonPropertyName("compute_phase")]
    public ComputePhase? ComputePhase { get; set; }

    /// <summary>
    /// Gets or sets the action phase of the transaction.
    /// </summary>
    [JsonPropertyName("action_phase")]
    public ActionPhase? ActionPhase { get; set; }

    /// <summary>
    /// Gets or sets the credit phase of the transaction.
    /// </summary>
    [JsonPropertyName("credit_phase")]
    public CreditPhase? CreditPhase { get; set; }

    /// <summary>
    /// Gets or sets the storage phase of the transaction.
    /// </summary>
    [JsonPropertyName("storage_phase")]
    public StoragePhase? StoragePhase { get; set; }

    /// <summary>
    /// Gets or sets the raw transaction data.
    /// </summary>
    [JsonPropertyName("raw")]
    public string? Raw { get; set; }
}
