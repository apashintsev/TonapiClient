using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an estimate for a gasless transaction.
/// </summary>
public class GaslessEstimate
{
    /// <summary>
    /// Gets or sets a value indicating whether the gasless transaction is possible.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the estimated commission in gas jettons.
    /// </summary>
    [JsonPropertyName("commission")]
    public string? Commission { get; set; }

    /// <summary>
    /// Gets or sets the error message if gasless is not available.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
