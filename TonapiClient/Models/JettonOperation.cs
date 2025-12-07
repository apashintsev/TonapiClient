using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a jetton transfer operation in history.
/// </summary>
public class JettonOperation
{
    /// <summary>
    /// Gets or sets the operation type (e.g., "transfer").
    /// </summary>
    [JsonPropertyName("operation")]
    public string Operation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Unix timestamp.
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
    /// Gets or sets the transfer amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the jetton information.
    /// </summary>
    [JsonPropertyName("jetton")]
    public JettonInfo Jetton { get; set; } = new();

    /// <summary>
    /// Gets or sets the trace ID.
    /// </summary>
    [JsonPropertyName("trace_id")]
    public string? TraceId { get; set; }

    /// <summary>
    /// Gets or sets the query ID.
    /// </summary>
    [JsonPropertyName("query_id")]
    public string? QueryId { get; set; }

    /// <summary>
    /// Gets or sets the payload.
    /// </summary>
    [JsonPropertyName("payload")]
    public System.Text.Json.JsonElement? Payload { get; set; }
}

