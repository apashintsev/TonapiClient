using Microsoft.Extensions.Configuration;

namespace TonapiClient.Tests;

/// <summary>
/// Base class for tests with configuration loaded from appsettings.json.
/// Uses real HTTP client to make actual API calls to testnet.
/// </summary>
public abstract class TestBase
{
    protected readonly IConfiguration Configuration;
    protected readonly TonApiClient Client;
    protected readonly TonApiClientOptions ClientOptions;
    protected readonly string Mnemonic;

    protected TestBase()
    {
        // Load configuration from appsettings.json
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        // Get TonApiClient options from configuration
        ClientOptions = new TonApiClientOptions();
        Configuration.GetSection("TonApiClient").Bind(ClientOptions);

        // Create real TonApiClient for actual API calls
        Client = new TonApiClient(
            ClientOptions.BaseUrl, 
            ClientOptions.ApiKey ?? string.Empty, 
            ClientOptions.TimeoutSeconds);

        Mnemonic = Configuration["Mnemonic"] ?? string.Empty;
    }
}

