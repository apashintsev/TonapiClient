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
  {
    _httpClient = new HttpClient();
    _logger = new LoggerFactory().CreateLogger<TonApiClient>();

    _httpClient.BaseAddress = new Uri(baseUrl);
    _httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);

    if (!string.IsNullOrEmpty(apiKey))
    {
      _httpClient.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue("Bearer", apiKey);
    }

    _jsonOptions = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
    };

    // Initialize categories
    Blockchain = new BlockchainCategory(this);
    Account = new AccountCategory(this, _logger);
    Jetton = new JettonCategory(this);
    Nft = new NftCategory(this);
    Dns = new DnsCategory(this);
    Staking = new StakingCategory(this);
    Rates = new RatesCategory(this);
    Traces = new TracesCategory(this, _logger);
    Wallet = new WalletCategory(this);
    Gasless = new GaslessCategory(this);
    Events = new EventsCategory(this, _logger);
    LiteServer = new LiteServerCategory(this);
    Storage = new StorageCategory(this);
    Multisig = new MultisigCategory(this);
    Emulation = new EmulationCategory(this);
  }

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
    Blockchain = new BlockchainCategory(this);
    Account = new AccountCategory(this, _logger);
    Jetton = new JettonCategory(this);
    Nft = new NftCategory(this);
    Dns = new DnsCategory(this);
    Staking = new StakingCategory(this);
    Rates = new RatesCategory(this);
    Traces = new TracesCategory(this, _logger);
    Wallet = new WalletCategory(this);
    Gasless = new GaslessCategory(this);
    Events = new EventsCategory(this, _logger);
    LiteServer = new LiteServerCategory(this);
    Storage = new StorageCategory(this);
    Multisig = new MultisigCategory(this);
    Emulation = new EmulationCategory(this);
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

  #region Helper Methods for Account Category

  /// <summary>
  /// Waits for a transaction to appear in the blockchain by polling account transactions.
  /// Uses exponential backoff: 1s → 2s → 4s → 8s → ... up to maxPollingInterval.
  /// </summary>
  public async Task<Transaction?> WaitForTransactionAsync(
      string accountId,
      string expectedMessageHash,
      int? expireTime = null,
      int maxWaitTime = 120,
      int initialPollingInterval = 1,
      int maxPollingInterval = 8,
      CancellationToken cancellationToken = default)
  {
    var waitTime = expireTime ?? maxWaitTime;
    var deadline = DateTime.UtcNow.AddSeconds(waitTime);
    var currentInterval = initialPollingInterval;

    while (DateTime.UtcNow < deadline && !cancellationToken.IsCancellationRequested)
    {
      try
      {
        var transactions = await Account.GetTransactionsAsync(
            accountId,
            limit: 100,
            sortOrder: "desc",
            ct: cancellationToken);

        // Search for transaction with matching incoming message hash
        foreach (var tx in transactions.Transactions)
        {
          if (tx.InMsg != null &&
              tx.InMsg.Hash.Equals(expectedMessageHash, StringComparison.OrdinalIgnoreCase))
          {
            return tx;
          }
        }

        // Wait before next poll with exponential backoff
        var delayMs = currentInterval * 1000;
        var remainingTime = (deadline - DateTime.UtcNow).TotalMilliseconds;

        if (delayMs > remainingTime)
        {
          delayMs = (int)Math.Max(0, remainingTime);
        }

        if (delayMs > 0)
        {
          await Task.Delay(delayMs, cancellationToken);
        }

        currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
      }
      catch (OperationCanceledException)
      {
        return null;
      }
      catch (Exception ex)
      {
        _logger.LogWarning(ex, "Error while polling for transaction {ExpectedHash} on account {AccountId}",
            expectedMessageHash, accountId);

        await Task.Delay(currentInterval * 1000, cancellationToken);
        currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
      }
    }

    return null;
  }
  #endregion

  #region Internal HTTP Methods

  internal async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken)
  {
    const int maxRetries = 5;
    int retryCount = 0;

    while (retryCount <= maxRetries)
    {
      try
      {
        _logger.LogDebug("Sending GET request to {Url} (attempt {Attempt}/{MaxAttempts})", 
            url, retryCount + 1, maxRetries + 1);

        var response = await _httpClient.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
          // Handle 404 Not Found - return null and log as Info
          if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
          {
            _logger.LogInformation(
                "Entity not found (404) for {Url}. Response: {Response}",
                url,
                content);

            return default(T)!;
          }

          // Handle 429 Rate Limit - retry after delay
          if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
          {
            if (retryCount < maxRetries)
            {
              _logger.LogWarning(
                  "Rate limit (429) for {Url}. Waiting 1 second before retry {RetryCount}/{MaxRetries}",
                  url,
                  retryCount + 1,
                  maxRetries);

              await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
              retryCount++;
              continue;
            }

            _logger.LogError(
                "Rate limit (429) for {Url} - max retries ({MaxRetries}) reached",
                url,
                maxRetries);
          }
          else
          {
            _logger.LogError(
                "API request failed with status {StatusCode} for {Url}. Response: {Response}",
                response.StatusCode,
                url,
                content);
          }

          var error = JsonSerializer.Deserialize<ApiError>(content, _jsonOptions);
          throw new TonApiException(
              error?.Error ?? "Unknown error",
              (int)response.StatusCode,
              error?.ErrorCode);
        }

        var result = JsonSerializer.Deserialize<T>(content, _jsonOptions);
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

  internal async Task<TResponse> PostAsync<TRequest, TResponse>(
      string url,
      TRequest request,
      CancellationToken cancellationToken)
  {
    const int maxRetries = 5;
    int retryCount = 0;

    while (retryCount <= maxRetries)
    {
      try
      {
        _logger.LogDebug("Sending POST request to {Url} (attempt {Attempt}/{MaxAttempts})", 
            url, retryCount + 1, maxRetries + 1);

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
          // Handle 404 Not Found
          if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
          {
            _logger.LogInformation(
                "Entity not found (404) for {Url}. Response: {Response}",
                url,
                responseContent);

            return default(TResponse)!;
          }

          // Handle 429 Rate Limit
          if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
          {
            if (retryCount < maxRetries)
            {
              _logger.LogWarning(
                  "Rate limit (429) for {Url}. Waiting 1 second before retry {RetryCount}/{MaxRetries}",
                  url,
                  retryCount + 1,
                  maxRetries);

              await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
              retryCount++;
              continue;
            }

            _logger.LogError(
                "Rate limit (429) for {Url} - max retries ({MaxRetries}) reached",
                url,
                maxRetries);
          }
          else
          {
            _logger.LogError(
                "API request failed with status {StatusCode} for {Url}. Response: {Response}",
                response.StatusCode,
                url,
                responseContent);
          }

          var error = JsonSerializer.Deserialize<ApiError>(responseContent, _jsonOptions);
          throw new TonApiException(
              error?.Error ?? "Unknown error",
              (int)response.StatusCode,
              error?.ErrorCode);
        }

        var result = JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
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

  #endregion

  /// <summary>
  /// Disposes the HTTP client.
  /// </summary>
  public void Dispose()
  {
    _httpClient?.Dispose();
  }
}
