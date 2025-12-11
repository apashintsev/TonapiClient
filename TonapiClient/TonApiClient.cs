using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TonapiClient.Models;
using TonapiClient.Categories;

namespace TonapiClient;

/// <summary>
/// Client for interacting with the TON API (https://tonapi.io).
/// Provides access to blockchain data, accounts, transactions, NFTs, Jettons, and more.
/// </summary>
public partial class TonApiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<TonApiClient> _logger;
    private readonly TonApiClientOptions? _options;
    private readonly JsonSerializerOptions _jsonOptions;

    // Category properties
    public BlockchainCategory Blockchain { get; }
    public AccountCategory Account { get; }
    public JettonCategory Jetton { get; }
    public NftCategory Nft { get; }
    public DnsCategory Dns { get; }
    public StakingCategory Staking { get; }
    public RatesCategory Rates { get; }
    public TracesCategory Traces { get; }
    public WalletCategory Wallet { get; }
    public GaslessCategory Gasless { get; }
    public EventsCategory Events { get; }
    public LiteServerCategory LiteServer { get; }
    public StorageCategory Storage { get; }
    public MultisigCategory Multisig { get; }
    public EmulationCategory Emulation { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TonApiClient"/> class.
    /// </summary>
    /// <param name="baseUrl">Base URL of the TON API.</param>
    /// <param name="apiKey">API key for authentication.</param>
    /// <param name="timeoutSeconds">Timeout in seconds (default 30).</param>
    public TonApiClient(string baseUrl, string apiKey, int timeoutSeconds = 30)
        : this(
            new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(timeoutSeconds) },
            Options.Create(new TonApiClientOptions { BaseUrl = baseUrl, ApiKey = apiKey, TimeoutSeconds = timeoutSeconds }),
            Microsoft.Extensions.Logging.Abstractions.NullLogger<TonApiClient>.Instance)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TonApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="options">The configuration options.</param>
    /// <param name="logger">The logger.</param>
    public TonApiClient(
          HttpClient httpClient,
          IOptions<TonApiClientOptions> options,
          ILogger<TonApiClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);

        if (!string.IsNullOrEmpty(_options.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _options.ApiKey);
        }

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        // Initialize categories
        Blockchain = new BlockchainCategory(this, _logger);
        Account = new AccountCategory(this, _logger);
        Jetton = new JettonCategory(this, _logger);
        Nft = new NftCategory(this, _logger);
        Dns = new DnsCategory(this, _logger);
        Staking = new StakingCategory(this, _logger);
        Rates = new RatesCategory(this, _logger);
        Traces = new TracesCategory(this, _logger);
        Wallet = new WalletCategory(this, _logger);
        Gasless = new GaslessCategory(this, _logger);
        Events = new EventsCategory(this, _logger);
        LiteServer = new LiteServerCategory(this, _logger);
        Storage = new StorageCategory(this, _logger);
        Multisig = new MultisigCategory(this, _logger);
        Emulation = new EmulationCategory(this, _logger);
    }

    #region Utilities Methods

    /// <summary>
    /// Get service status.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The service status.</returns>
    public async Task<ServiceStatus> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<ServiceStatus>("/v2/status", cancellationToken);
    }

    #endregion

    #region Internal HTTP Methods

    private static TimeSpan GetDelay(HttpResponseMessage response, int retryCount)
    {
        if (response.Headers.RetryAfter?.Delta is { } delta)
            return delta;

        var baseDelay = TimeSpan.FromMilliseconds(200 * Math.Pow(2, retryCount));
        var jitterMs = Random.Shared.Next(0, 200);
        return baseDelay + TimeSpan.FromMilliseconds(jitterMs);
    }

    private async Task<TResponse> SendWithRetryAsync<TResponse>(
        Func<CancellationToken, Task<HttpResponseMessage>> send,
        string url,
        CancellationToken cancellationToken)
    {
        const int maxRetries = 5;
        int retryCount = 0;

        while (retryCount <= maxRetries)
        {
            try
            {
                _logger.LogDebug(
                    "Sending HTTP request to {Url} (attempt {Attempt}/{MaxAttempts})",
                    url, retryCount + 1, maxRetries + 1);

                var response = await send(cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        _logger.LogInformation(
                            "Entity not found (404) for {Url}. Response: {Response}",
                            url, content);

                        return default!;
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        var delay = GetDelay(response, retryCount);
                        if (retryCount < maxRetries)
                        {
                            _logger.LogWarning(
                                "Rate limit (429) for {Url}. Waiting {Delay} before retry {RetryCount}/{MaxRetries}",
                                url, delay, retryCount + 1, maxRetries);

                            await Task.Delay(delay, cancellationToken);
                            retryCount++;
                            continue;
                        }

                        _logger.LogError(
                            "Rate limit (429) for {Url} - max retries ({MaxRetries}) reached",
                            url, maxRetries);
                    }
                    else
                    {
                        _logger.LogError(
                            "API request failed with status {StatusCode} for {Url}. Response: {Response}",
                            response.StatusCode, url, content);
                    }

                    var error = JsonSerializer.Deserialize<ApiError>(content, _jsonOptions);
                    throw new TonApiException(
                        error?.Error ?? "Unknown error",
                        (int)response.StatusCode,
                        error?.ErrorCode);
                }

                var result = JsonSerializer.Deserialize<TResponse>(content, _jsonOptions);
                if (result == null)
                {
                    throw new TonApiException("Failed to deserialize response", 0, null);
                }

                return result;
            }
            catch (TonApiException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request failed for {Url}", url);
                throw new TonApiException("HTTP request failed", 0, null, ex);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Request timeout for {Url}", url);
                throw new TonApiException("Request timeout", 0, null, ex);
            }
        }

        throw new TonApiException("Unexpected error in retry loop", 0, null);
    }

    internal Task<T> GetAsync<T>(string url, CancellationToken ct) =>
        SendWithRetryAsync<T>(c => _httpClient.GetAsync(url, c), url, ct);

    internal Task<TResponse> PostAsync<TRequest, TResponse>(
        string url, TRequest request, CancellationToken ct)
    {
        return SendWithRetryAsync<TResponse>(async c =>
        {
            var json = JsonSerializer.Serialize(request, _jsonOptions);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, content, c);
        }, url, ct);
    }

    #endregion
}
