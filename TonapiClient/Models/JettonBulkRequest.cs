using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Request model for bulk jetton retrieval.
/// </summary>
public class JettonBulkRequest
{
    /// <summary>
    /// Gets or sets the list of account (jetton) IDs.
    /// </summary>
    [JsonPropertyName("account_ids")]
    public List<string> AccountIds { get; set; } = new();
}

