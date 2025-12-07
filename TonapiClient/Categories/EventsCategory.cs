using TonapiClient.Models;
using Microsoft.Extensions.Logging;

namespace TonapiClient.Categories;

/// <summary>
/// Events category methods.
/// </summary>
public class EventsCategory : CategoryBase
{
  private readonly ILogger _logger;

  internal EventsCategory(TonApiClient client, ILogger logger) : base(client)
  {
    _logger = logger;
  }

  /// <summary>
  /// Get event by event ID.
  /// </summary>
  public async Task<AccountEvent> GetAsync(string eventId, CancellationToken ct = default)
  {
    return await GetAsync<AccountEvent>($"/v2/events/{eventId}", ct);
  }

  /// <summary>
  /// Wait for an event to complete (not in progress).
  /// </summary>
  public async Task<AccountEvent?> WaitForAsync(
      string eventId,
      int maxWaitTime = 120,
      int initialPollingInterval = 1,
      int maxPollingInterval = 8,
      CancellationToken ct = default)
  {
    var deadline = DateTime.UtcNow.AddSeconds(maxWaitTime);
    var currentInterval = initialPollingInterval;

    while (DateTime.UtcNow < deadline && !ct.IsCancellationRequested)
    {
      try
      {
        var eventData = await GetAsync(eventId, ct);

        if (!eventData.InProgress)
          return eventData;

        await Task.Delay(TimeSpan.FromSeconds(currentInterval), ct);
        currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
      }
      catch (HttpRequestException)
      {
        await Task.Delay(TimeSpan.FromSeconds(currentInterval), ct);
        currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
      }
    }

    return null;
  }
}
