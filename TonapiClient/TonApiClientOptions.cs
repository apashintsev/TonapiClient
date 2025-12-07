namespace TonapiClient;

/// <summary>
/// Configuration options for TON API client.
/// </summary>
public class TonApiClientOptions
{
    /// <summary>
    /// Gets or sets the base URL for the TON API.
    /// Default is "https://tonapi.io" for mainnet.
    /// Use "https://testnet.tonapi.io" for testnet.
    /// </summary>
    public string BaseUrl { get; set; } = "https://tonapi.io";

    /// <summary>
    /// Gets or sets the API key for authentication.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the timeout in seconds for HTTP requests.
    /// Default is 30 seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
}
