using Microsoft.Extensions.Logging;

namespace TonapiClient.Categories;

/// <summary>
/// Base category class with access to HTTP methods.
/// </summary>
public abstract class CategoryBase
{
    protected readonly TonApiClient _client;
    protected readonly ILogger _logger;

    internal CategoryBase(TonApiClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

    protected Task<T> GetAsync<T>(string url, CancellationToken ct = default)
    {
        return _client.GetAsync<T>(url, ct);
    }

    protected Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, CancellationToken ct = default)
    {
        return _client.PostAsync<TRequest, TResponse>(url, request, ct);
    }
}

