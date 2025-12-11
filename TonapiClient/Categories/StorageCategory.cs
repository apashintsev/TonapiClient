using Microsoft.Extensions.Logging;
using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Storage category methods.
/// </summary>
public class StorageCategory : CategoryBase
{
    internal StorageCategory(TonApiClient client, ILogger<TonApiClient> logger) : base(client, logger) { }

    /// <summary>
    /// Get list of storage providers.
    /// </summary>
    public async Task<List<StorageProvider>> GetProvidersAsync(CancellationToken ct = default)
    {
        var response = await GetAsync<Dictionary<string, List<StorageProvider>>>("/v2/storage/providers", ct);
        return response["providers"];
    }
}
