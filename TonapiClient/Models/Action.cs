using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents an action performed in an event.
/// </summary>
public class Action
{
    /// <summary>
    /// Gets or sets the action type (e.g., "TonTransfer", "JettonTransfer", "NftItemTransfer").
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action status.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action-specific data.
    /// </summary>
    [JsonPropertyName("TonTransfer")]
    public TonTransferAction? TonTransfer { get; set; }

    /// <summary>
    /// Gets or sets the jetton transfer action data.
    /// </summary>
    [JsonPropertyName("JettonTransfer")]
    public JettonTransferAction? JettonTransfer { get; set; }

    /// <summary>
    /// Gets or sets the NFT item transfer action data.
    /// </summary>
    [JsonPropertyName("NftItemTransfer")]
    public NftItemTransferAction? NftItemTransfer { get; set; }

    /// <summary>
    /// Gets or sets the contract deploy action data.
    /// </summary>
    [JsonPropertyName("ContractDeploy")]
    public ContractDeployAction? ContractDeploy { get; set; }

    /// <summary>
    /// Gets or sets a simple preview of the action.
    /// </summary>
    [JsonPropertyName("simple_preview")]
    public ActionSimplePreview? SimplePreview { get; set; }

    /// <summary>
    /// Gets or sets the base transactions associated with this action.
    /// </summary>
    [JsonPropertyName("base_transactions")]
    public List<string>? BaseTransactions { get; set; }
}
