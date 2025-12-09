using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents statistics for a wallet account.
/// </summary>
public class WalletStats
{
    /// <summary>
    /// Gets or sets the number of NFTs owned by the wallet.
    /// </summary>
    [JsonPropertyName("nfts_count")]
    public int NftsCount { get; set; }

    /// <summary>
    /// Gets or sets the number of jettons owned by the wallet.
    /// </summary>
    [JsonPropertyName("jettons_count")]
    public int JettonsCount { get; set; }

    /// <summary>
    /// Gets or sets the number of multisig contracts associated with the wallet.
    /// </summary>
    [JsonPropertyName("multisig_count")]
    public int MultisigCount { get; set; }

    /// <summary>
    /// Gets or sets the number of staking contracts associated with the wallet.
    /// </summary>
    [JsonPropertyName("staking_count")]
    public int StakingCount { get; set; }
}

