using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents blockchain account transactions response.
/// </summary>
public class BlockchainAccountTransactions
{
    /// <summary>
    /// Gets or sets the list of transactions.
    /// </summary>
    [JsonPropertyName("transactions")]
    public List<Transaction> Transactions { get; set; } = new();
}

