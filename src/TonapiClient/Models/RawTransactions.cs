using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents raw transactions list from lite server.
/// </summary>
public class RawTransactions
{
    /// <summary>
    /// Gets or sets the list of transaction IDs and hashes.
    /// </summary>
    [JsonPropertyName("ids")]
    public List<RawTransactionId> Ids { get; set; } = new();

    /// <summary>
    /// Gets or sets the raw transactions data in base64.
    /// </summary>
    [JsonPropertyName("transactions")]
    public string Transactions { get; set; } = string.Empty;
}

