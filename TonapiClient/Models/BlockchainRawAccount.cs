using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class BlockchainRawAccount
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("balance")]
    public long Balance { get; set; }

    [JsonPropertyName("last_transaction_lt")]
    public ulong LastTransactionLt { get; set; }

    [JsonPropertyName("last_transaction_hash")]
    public string LastTransactionHash { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("storage")]
    public StorageInfo? Storage { get; set; }
}

public class StorageInfo
{
    [JsonPropertyName("used_cells")]
    public long UsedCells { get; set; }

    [JsonPropertyName("used_bits")]
    public long UsedBits { get; set; }

    [JsonPropertyName("used_public_cells")]
    public long UsedPublicCells { get; set; }

    [JsonPropertyName("last_paid")]
    public long LastPaid { get; set; }

    [JsonPropertyName("due_payment")]
    public long DuePayment { get; set; }
}

