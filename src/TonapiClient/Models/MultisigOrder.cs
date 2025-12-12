using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a multisig order (proposal).
/// </summary>
public class MultisigOrder
{
    /// <summary>
    /// Gets or sets the order ID.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the multisig address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation time.
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the expiration time.
    /// </summary>
    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the number of approvals received.
    /// </summary>
    [JsonPropertyName("approvals_num")]
    public int ApprovalsNum { get; set; }

    /// <summary>
    /// Gets or sets the threshold required for execution.
    /// </summary>
    [JsonPropertyName("threshold")]
    public int Threshold { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order was executed.
    /// </summary>
    [JsonPropertyName("executed")]
    public bool Executed { get; set; }

    /// <summary>
    /// Gets or sets the list of signers who approved.
    /// </summary>
    [JsonPropertyName("approvals")]
    public List<string> Approvals { get; set; } = new();

    /// <summary>
    /// Gets or sets the raw order data.
    /// </summary>
    [JsonPropertyName("order")]
    public string? Order { get; set; }
}
