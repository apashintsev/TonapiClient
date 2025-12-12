using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the credit phase of a transaction.
/// </summary>
public class CreditPhase
{
    /// <summary>
    /// Gets or sets the fees collected in nanotons.
    /// </summary>
    [JsonPropertyName("fees_collected")]
    public ulong FeesCollected { get; set; }

    /// <summary>
    /// Gets or sets the credit amount in nanotons.
    /// </summary>
    [JsonPropertyName("credit")]
    public ulong Credit { get; set; }
}

