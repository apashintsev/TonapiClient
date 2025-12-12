using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents information about a pool implementation.
/// </summary>
public class PoolImplementation
{
    /// <summary>
    /// Gets or sets the implementation name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the implementation description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the implementation URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the social media links.
    /// </summary>
    [JsonPropertyName("socials")]
    public List<string>? Socials { get; set; }
}
