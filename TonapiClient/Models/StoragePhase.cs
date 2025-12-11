using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the storage phase of a transaction.
/// </summary>
public class StoragePhase
{
    /// <summary>
    /// Gets or sets the fees collected in the storage phase.
    /// </summary>
    [JsonPropertyName("fees_collected")]
    public ulong FeesCollected { get; set; }

    /// <summary>
    /// Gets or sets the status change in the storage phase.
    /// </summary>
    [JsonPropertyName("status_change")]
    public string? StatusChange { get; set; }
}

