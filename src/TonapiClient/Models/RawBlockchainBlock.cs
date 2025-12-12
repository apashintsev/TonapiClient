using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents raw blockchain block from lite server.
/// </summary>
public class RawBlockchainBlock
{
    /// <summary>
    /// Gets or sets the block ID.
    /// </summary>
    [JsonPropertyName("id")]
    public RawBlockId Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the raw block data in base64.
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;
}
