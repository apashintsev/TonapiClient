using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a wallet sequence number response.
/// </summary>
public class Seqno
{
    /// <summary>
    /// Gets or sets the sequence number of the wallet.
    /// </summary>
    [JsonPropertyName("seqno")]
    public uint SeqnoValue { get; set; }
}
