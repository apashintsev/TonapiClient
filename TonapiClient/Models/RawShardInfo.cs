using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents raw shard information from lite server.
/// </summary>
public class RawShardInfo
{
    /// <summary>
    /// Gets or sets the shard block ID.
    /// </summary>
    [JsonPropertyName("id")]
    public RawBlockId Id { get; set; } = new();

    /// <summary>
    /// Gets or sets the shard data in base64.
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;
}
