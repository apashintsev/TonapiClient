using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a contract method.
/// </summary>
public class Method
{
    /// <summary>
    /// Gets or sets the method ID.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the method name.
    /// </summary>
    [JsonPropertyName("method")]
    public string MethodName { get; set; } = string.Empty;
}

