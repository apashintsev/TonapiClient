using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a Jetton lock information.
/// </summary>
public class JettonLock
{
    /// <summary>
    /// Gets or sets the amount locked.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unlock timestamp.
    /// </summary>
    [JsonPropertyName("till")]
    public long Till { get; set; }
}
