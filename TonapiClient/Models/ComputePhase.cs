using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the compute phase of a transaction.
/// </summary>
public class ComputePhase
{
    /// <summary>
    /// Gets or sets a value indicating whether the compute phase was skipped.
    /// </summary>
    [JsonPropertyName("skipped")]
    public bool Skipped { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the compute phase was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>
    /// Gets or sets the gas fees in nanotons.
    /// </summary>
    [JsonPropertyName("gas_fees")]
    public ulong GasFees { get; set; }

    /// <summary>
    /// Gets or sets the amount of gas used.
    /// </summary>
    [JsonPropertyName("gas_used")]
    public ulong GasUsed { get; set; }

    /// <summary>
    /// Gets or sets the number of VM steps.
    /// </summary>
    [JsonPropertyName("vm_steps")]
    public int VmSteps { get; set; }

    /// <summary>
    /// Gets or sets the exit code.
    /// </summary>
    [JsonPropertyName("exit_code")]
    public int ExitCode { get; set; }

    /// <summary>
    /// Gets or sets the exit code description.
    /// </summary>
    [JsonPropertyName("exit_code_description")]
    public string? ExitCodeDescription { get; set; }
}

