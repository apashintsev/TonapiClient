using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Staking category methods.
/// </summary>
public class StakingCategory : CategoryBase
{
  internal StakingCategory(TonApiClient client) : base(client) { }

  /// <summary>
  /// Get list of staking pools.
  /// </summary>
  public async Task<List<PoolInfo>> GetPoolsAsync(
      string? availableFor = null,
      bool includeUnverified = false,
      CancellationToken ct = default)
  {
    var url = "/v2/staking/pools";
    var parameters = new List<string>();

    if (!string.IsNullOrEmpty(availableFor))
      parameters.Add($"available_for={availableFor}");

    if (includeUnverified)
      parameters.Add("include_unverified=true");

    if (parameters.Count > 0)
      url += "?" + string.Join("&", parameters);

    var response = await GetAsync<Dictionary<string, List<PoolInfo>>>(url, ct);
    return response["pools"];
  }

  /// <summary>
  /// Get pool information by address.
  /// </summary>
  public async Task<PoolInfo> GetPoolAsync(string poolAddress, CancellationToken ct = default)
  {
    return await GetAsync<PoolInfo>($"/v2/staking/pools/{poolAddress}", ct);
  }

  /// <summary>
  /// Get staking information for a specific account (nominator).
  /// </summary>
  public async Task<AccountStakingInfo> GetAccountInfoAsync(string accountId, CancellationToken ct = default)
  {
    return await GetAsync<AccountStakingInfo>($"/v2/staking/nominator/{accountId}/pools", ct);
  }
}
