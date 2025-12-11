using Microsoft.Extensions.Logging;
using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Multisig category methods.
/// </summary>
public class MultisigCategory : CategoryBase
{
    internal MultisigCategory(TonApiClient client, ILogger<TonApiClient> logger) : base(client, logger) { }

    /// <summary>
    /// Get multisig account information.
    /// </summary>
    public async Task<MultisigAccount> GetAccountAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<MultisigAccount>($"/v2/multisig/{accountId}", ct);
    }

    /// <summary>
    /// Get multisig orders (proposals).
    /// </summary>
    public async Task<List<MultisigOrder>> GetOrdersAsync(
        string accountId,
        int limit = 100,
        int offset = 0,
        CancellationToken ct = default)
    {
        var response = await GetAsync<Dictionary<string, List<MultisigOrder>>>(
            $"/v2/multisig/{accountId}/orders?limit={limit}&offset={offset}", ct);
        return response["orders"];
    }
}
