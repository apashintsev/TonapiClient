using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents jetton operation history for an account.
/// </summary>
public class JettonHistory
{
    /// <summary>
    /// Gets or sets the list of jetton operations.
    /// </summary>
    [JsonPropertyName("operations")]
    public List<JettonOperation> Operations { get; set; } = new();

    /// <summary>
    /// Gets or sets the next offset for pagination.
    /// </summary>
    [JsonPropertyName("next_from")]
    public long? NextFrom { get; set; }
}
