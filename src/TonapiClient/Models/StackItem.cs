using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a stack item in a method execution result.
/// </summary>
public class StackItem
{
    /// <summary>
    /// Gets or sets the type of the stack item (e.g., "cell", "slice", "num", "tuple").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cell value (when type is "cell").
    /// </summary>
    [JsonPropertyName("cell")]
    public string? Cell { get; set; }

    /// <summary>
    /// Gets or sets the slice value (when type is "slice").
    /// </summary>
    [JsonPropertyName("slice")]
    public string? Slice { get; set; }

    /// <summary>
    /// Gets or sets the numeric value (when type is "num").
    /// </summary>
    [JsonPropertyName("num")]
    public string? Num { get; set; }

    /// <summary>
    /// Gets or sets the tuple value (when type is "tuple").
    /// </summary>
    [JsonPropertyName("tuple")]
    public List<StackItem>? Tuple { get; set; }
}

