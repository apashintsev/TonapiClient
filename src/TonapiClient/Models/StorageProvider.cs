using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a storage provider in TON Storage.
/// </summary>
public class StorageProvider
{
    /// <summary>
    /// Gets or sets the provider address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the provider accepts new contracts.
    /// </summary>
    [JsonPropertyName("accept_new_contracts")]
    public bool AcceptNewContracts { get; set; }

    /// <summary>
    /// Gets or sets the rate per MB per day in nanotons.
    /// </summary>
    [JsonPropertyName("rate_per_mb_day")]
    public string RatePerMbDay { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum span in seconds.
    /// </summary>
    [JsonPropertyName("max_span")]
    public long MaxSpan { get; set; }

    /// <summary>
    /// Gets or sets the minimal file size in bytes.
    /// </summary>
    [JsonPropertyName("minimal_file_size")]
    public long MinimalFileSize { get; set; }

    /// <summary>
    /// Gets or sets the maximal file size in bytes.
    /// </summary>
    [JsonPropertyName("maximal_file_size")]
    public long MaximalFileSize { get; set; }
}
