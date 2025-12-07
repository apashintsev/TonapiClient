using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a paginated list of transactions.
/// </summary>
public class Transactions
{
    /// <summary>
    /// Gets or sets the list of transactions.
    /// </summary>
    [JsonPropertyName("transactions")]
    public List<Transaction> TransactionsList { get; set; } = new();

    /// <summary>
    /// Gets or sets the address filter if applied.
    /// </summary>
    [JsonPropertyName("address_book")]
    public Dictionary<string, AccountAddress>? AddressBook { get; set; }
}
