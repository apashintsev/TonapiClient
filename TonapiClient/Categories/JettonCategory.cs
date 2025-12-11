using Microsoft.Extensions.Logging;
using System.Text.Json;
using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Jetton category methods.
/// </summary>
public class JettonCategory : CategoryBase
{
    internal JettonCategory(TonApiClient client, ILogger<TonApiClient> logger) : base(client, logger) { }

    /// <summary>
    /// Get list of all jettons in the system.
    /// </summary>
    public async Task<Jettons> GetAllAsync(int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        return await GetAsync<Jettons>($"/v2/jettons?limit={limit}&offset={offset}", ct);
    }

    /// <summary>
    /// Get Jetton information by address.
    /// </summary>
    public async Task<JettonInfo> GetAsync(string jettonAddress, CancellationToken ct = default)
    {
        return await GetAsync<JettonInfo>($"/v2/jettons/{jettonAddress}", ct);
    }

    /// <summary>
    /// Get list of all holders for a specific Jetton.
    /// </summary>
    public async Task<JettonHolders> GetHoldersAsync(
        string jettonAddress,
        int limit = 1000,
        int offset = 0,
        CancellationToken ct = default)
    {
        return await GetAsync<JettonHolders>($"/v2/jettons/{jettonAddress}/holders?limit={limit}&offset={offset}", ct);
    }

    /// <summary>
    /// Get event by event ID with jetton transfer information.
    /// </summary>
    public async Task<AccountEvent> GetEventJettonsAsync(string eventId, CancellationToken ct = default)
    {
        return await GetAsync<AccountEvent>($"/v2/events/{eventId}/jettons", ct);
    }

    /// <summary>
    /// Get jetton metadata items by jetton master addresses.
    /// </summary>
    public async Task<Jettons> GetBulkAsync(List<string> jettonAddresses, CancellationToken ct = default)
    {
        var request = new JettonBulkRequest { AccountIds = jettonAddresses };
        return await PostAsync<JettonBulkRequest, Jettons>("/v2/jettons/_bulk", request, ct);
    }

    /// <summary>
    /// Get jetton wallet address for a user.
    /// </summary>
    public async Task<string?> GetJettonWalletAddressAsync(
        string jettonMasterAddress,
        string userAddress,
        CancellationToken cancellationToken = default)
    {
        var args = new List<string> { userAddress };
        var result = await _client.Account.ExecuteGetMethodAsync(jettonMasterAddress, "get_wallet_address", args, cancellationToken);

        if (result.Decoded.HasValue && result.Decoded.Value.ValueKind == JsonValueKind.Object)
        {
            if (result.Decoded.Value.TryGetProperty("jetton_wallet_address", out var addressElement))
            {
                return addressElement.GetString();
            }
        }

        return null;
    }
}
