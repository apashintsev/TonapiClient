using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of Jettons.
/// </summary>
public class Jettons
{
    /// <summary>
    /// Gets or sets the list of jettons.
    /// </summary>
    [JsonPropertyName("jettons")]
    public List<JettonInfo> JettonsList { get; set; } = new();
}
