using System.Text.Json;
using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the result of a contract method execution.
/// </summary>
public class MethodExecutionResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the method execution was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the exit code of the method execution.
    /// </summary>
    [JsonPropertyName("exit_code")]
    public int ExitCode { get; set; }

    /// <summary>
    /// Gets or sets the stack returned by the method execution.
    /// </summary>
    [JsonPropertyName("stack")]
    public List<StackItem> Stack { get; set; } = new();

    /// <summary>
    /// Gets or sets the decoded result (optional).
    /// Can be an object with arbitrary properties or a primitive value.
    /// </summary>
    [JsonPropertyName("decoded")]
    public JsonElement? Decoded { get; set; }
}
