using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a simple preview of an action.
/// </summary>
public class ActionSimplePreview
{
    /// <summary>
    /// Gets or sets the action name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action value if applicable.
    /// </summary>
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the value image if applicable.
    /// </summary>
    [JsonPropertyName("value_image")]
    public string? ValueImage { get; set; }

    /// <summary>
    /// Gets or sets the accounts involved.
    /// </summary>
    [JsonPropertyName("accounts")]
    public List<AccountAddress>? Accounts { get; set; }
}
