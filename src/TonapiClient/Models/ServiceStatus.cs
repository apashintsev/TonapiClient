using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the TON API service status.
/// </summary>
public class ServiceStatus
{
    /// <summary>
    /// Gets or sets a value indicating whether the indexing is in progress.
    /// </summary>
    [JsonPropertyName("indexing_latency")]
    public int IndexingLatency { get; set; }

    /// <summary>
    /// Gets or sets the last known masterchain block number.
    /// </summary>
    [JsonPropertyName("last_known_masterchain_seqno")]
    public int LastKnownMasterchainSeqno { get; set; }
}
