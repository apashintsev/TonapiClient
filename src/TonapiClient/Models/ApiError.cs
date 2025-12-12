using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an API error response.
/// </summary>
public class ApiError
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error code if available.
    /// </summary>
    [JsonPropertyName("error_code")]
    public long? ErrorCode { get; set; }
}
