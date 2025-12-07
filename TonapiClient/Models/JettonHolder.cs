using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a Jetton holder.
/// </summary>
public class JettonHolder
{
    /// <summary>
    /// Gets or sets the holder's account address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the owner account.
    /// </summary>
    [JsonPropertyName("owner")]
    public AccountAddress Owner { get; set; } = new();

    /// <summary>
    /// Gets or sets the balance.
    /// </summary>
    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;
}
