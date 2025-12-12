using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the action phase of a transaction.
/// </summary>
public class ActionPhase
{
    /// <summary>
    /// Gets or sets a value indicating whether the action phase was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the result code.
    /// </summary>
    [JsonPropertyName("result_code")]
    public int ResultCode { get; set; }

    /// <summary>
    /// Gets or sets the total number of actions.
    /// </summary>
    [JsonPropertyName("total_actions")]
    public int TotalActions { get; set; }

    /// <summary>
    /// Gets or sets the number of skipped actions.
    /// </summary>
    [JsonPropertyName("skipped_actions")]
    public int SkippedActions { get; set; }

    /// <summary>
    /// Gets or sets the forward fees in nanotons.
    /// </summary>
    [JsonPropertyName("fwd_fees")]
    public ulong FwdFees { get; set; }

    /// <summary>
    /// Gets or sets the total fees in nanotons.
    /// </summary>
    [JsonPropertyName("total_fees")]
    public ulong TotalFees { get; set; }
}

