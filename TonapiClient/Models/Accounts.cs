using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of accounts.
/// </summary>
public class Accounts
{
    /// <summary>
    /// Gets or sets the list of accounts.
    /// </summary>
    [JsonPropertyName("accounts")]
    public List<Account> AccountsList { get; set; } = new();
}

