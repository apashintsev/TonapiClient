using TonapiClient.Models;
using Microsoft.Extensions.Logging;

namespace TonapiClient.Categories;

/// <summary>
/// Traces category methods.
/// </summary>
public class TracesCategory : CategoryBase
{
    internal TracesCategory(TonApiClient client, ILogger<TonApiClient> logger) : base(client, logger)
    {
    }

    /// <summary>
    /// Get trace by trace ID or transaction hash.
    /// </summary>
    public async Task<Trace> GetAsync(string traceId, CancellationToken ct = default)
    {
        return await GetAsync<Trace>($"/v2/traces/{traceId}", ct);
    }

    /// <summary>
    /// Waits for a trace to be finalized by polling with exponential backoff.
    /// </summary>
    public async Task<Trace?> WaitForAsync(
        string traceId,
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
                var trace = await GetAsync(traceId, ct);

                if (trace.IsFinalized())
                    return trace;

                var delayMs = currentInterval * 1000;
                var remainingTime = (deadline - DateTime.UtcNow).TotalMilliseconds;

                if (delayMs > remainingTime)
                    delayMs = (int)Math.Max(0, remainingTime);

                if (delayMs > 0)
                    await Task.Delay(delayMs, ct);

                currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while polling for trace {TraceId}", traceId);
                await Task.Delay(currentInterval * 1000, ct);
                currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
            }
        }

        return null;
    }

    /// <summary>
    /// Emulate sending a message and return the full execution trace.
    /// </summary>
    public async Task<Trace> EmulateAsync(string boc, bool ignoreSignatureCheck = true, CancellationToken ct = default)
    {
        var url = $"/v2/traces/emulate?ignore_signature_check={ignoreSignatureCheck}";
        var request = new EmulateMessageRequest { Boc = boc };
        return await PostAsync<EmulateMessageRequest, Trace>(url, request, ct);
    }
}
