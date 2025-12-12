using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TonapiClient;

/// <summary>
/// Extension methods for registering TON API client in DI container.
/// </summary>
public static class TonApiClientExtensions
{
    /// <summary>
    /// Adds TON API client to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="configSectionName">Configuration section name (default: "TonApiClient").</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTonApiClient(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionName = "TonApiClient")
    {
        services.Configure<TonApiClientOptions>(opts => configuration.GetSection(configSectionName).Bind(opts));

        services.AddHttpClient<TonApiClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = configuration.GetSection(configSectionName).Get<TonApiClientOptions>()
                    ?? new TonApiClientOptions();

                client.BaseAddress = new Uri(options.BaseUrl);

                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

                if (!string.IsNullOrEmpty(options.ApiKey))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiKey}");
                }
            });

        return services;
    }

    /// <summary>
    /// Adds TON API client to the service collection with explicit options.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configureOptions">Action to configure options.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddTonApiClient(
        this IServiceCollection services,
        Action<TonApiClientOptions> configureOptions)
    {
        services.Configure(configureOptions);

        services.AddHttpClient<TonApiClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = new TonApiClientOptions();
                configureOptions(options);

                client.BaseAddress = new Uri(options.BaseUrl);

                client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

                if (!string.IsNullOrEmpty(options.ApiKey))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiKey}");
                }
            });

        return services;
    }
}
