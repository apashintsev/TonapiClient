using Microsoft.Extensions.Logging;
using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Gasless category methods.
/// </summary>
public class GaslessCategory : CategoryBase
{
    internal GaslessCategory(TonApiClient client, ILogger<TonApiClient> logger) : base(client, logger) { }

    /// <summary>
    /// Get gasless configuration for a specific jetton master.
    /// </summary>
    public async Task<GaslessConfig> GetConfigAsync(string jettonMaster, CancellationToken ct = default)
    {
        return await GetAsync<GaslessConfig>($"/v2/gasless/config?master_id={jettonMaster}", ct);
    }

    /// <summary>
    /// Estimate commission for a gasless transaction.
    /// </summary>
    public async Task<GaslessEstimate> EstimateAsync(
        string jettonMaster,
        string walletAddress,
        string walletPublicKey,
        List<SignRawMessage> messages,
        CancellationToken ct = default)
    {
        var request = new
        {
            master_id = jettonMaster,
            wallet_address = walletAddress,
            wallet_public_key = walletPublicKey,
            messages,
        };
        return await PostAsync<object, GaslessEstimate>("/v2/gasless/estimate", request, ct);
    }

    /// <summary>
    /// Send a gasless transaction.
    /// </summary>
    public async Task<SendBocResponse> SendAsync(
        string walletPublicKey,
        string boc,
        CancellationToken ct = default)
    {
        var request = new GaslessSendRequest
        {
            WalletPublicKey = walletPublicKey,
            Boc = boc,
        };
        return await PostAsync<GaslessSendRequest, SendBocResponse>("/v2/gasless/send", request, ct);
    }
}
