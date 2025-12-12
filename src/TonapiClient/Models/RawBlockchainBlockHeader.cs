using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents raw blockchain block header from lite server.
/// </summary>
public class RawBlockchainBlockHeader
{
    /// <summary>
    /// Gets or sets the block ID.
    /// </summary>
    [JsonPropertyName("id")]
    public RawBlockId Id { get; set; } = new();

    /// <summary>
    /// Gets or sets the raw header data in base64.
    /// </summary>
    [JsonPropertyName("header_proof")]
    public string HeaderProof { get; set; } = string.Empty;
}
