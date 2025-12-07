using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of domain names.
/// </summary>
public class DomainNames
{
    /// <summary>
    /// Gets or sets the list of domain names.
    /// </summary>
    [JsonPropertyName("domains")]
    public List<string> Domains { get; set; } = new();
}

