using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Jetton category methods.
/// </summary>
public class JettonCategory : CategoryBase
{
  internal JettonCategory(TonApiClient client) : base(client) { }

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
}
