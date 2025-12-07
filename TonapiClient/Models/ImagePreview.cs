using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an image preview with different resolutions.
/// </summary>
public class ImagePreview
{
    /// <summary>
    /// Gets or sets the resolution type (e.g., "100x100", "500x500", "1500x1500").
    /// </summary>
    [JsonPropertyName("resolution")]
    public string Resolution { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the image URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
