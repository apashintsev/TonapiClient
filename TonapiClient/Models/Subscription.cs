using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a subscription.
/// </summary>
public class Subscription
{
    /// <summary>
    /// Gets or sets the subscription address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the wallet address.
    /// </summary>
    [JsonPropertyName("wallet_address")]
    public string WalletAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the beneficiary address.
    /// </summary>
    [JsonPropertyName("beneficiary_address")]
    public string BeneficiaryAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the amount per invoice.
    /// </summary>
    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    /// <summary>
    /// Gets or sets the subscription period in seconds.
    /// </summary>
    [JsonPropertyName("period")]
    public long Period { get; set; }

    /// <summary>
    /// Gets or sets the start time timestamp.
    /// </summary>
    [JsonPropertyName("start_time")]
    public long StartTime { get; set; }

    /// <summary>
    /// Gets or sets the timeout for payment.
    /// </summary>
    [JsonPropertyName("timeout")]
    public long Timeout { get; set; }

    /// <summary>
    /// Gets or sets the last payment time.
    /// </summary>
    [JsonPropertyName("last_payment_time")]
    public long LastPaymentTime { get; set; }

    /// <summary>
    /// Gets or sets the last request time.
    /// </summary>
    [JsonPropertyName("last_request_time")]
    public long LastRequestTime { get; set; }

    /// <summary>
    /// Gets or sets the subscription ID.
    /// </summary>
    [JsonPropertyName("subscription_id")]
    public long SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }
}

