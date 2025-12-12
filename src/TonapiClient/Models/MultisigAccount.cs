using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a multisig wallet account.
/// </summary>
public class MultisigAccount
{
    /// <summary>
    /// Gets or sets the account address.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the balance in nanotons.
    /// </summary>
    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of signers required (threshold).
    /// </summary>
    [JsonPropertyName("threshold")]
    public int Threshold { get; set; }

    /// <summary>
    /// Gets or sets the list of signer public keys.
    /// </summary>
    [JsonPropertyName("signers")]
    public List<string> Signers { get; set; } = new();

    /// <summary>
    /// Gets or sets the proposers (who can create proposals).
    /// </summary>
    [JsonPropertyName("proposers")]
    public List<string> Proposers { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether the multisig is a simple multisig.
    /// </summary>
    [JsonPropertyName("is_simple")]
    public bool IsSimple { get; set; }
}
