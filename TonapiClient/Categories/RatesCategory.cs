using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Rates category methods.
/// </summary>
public class RatesCategory : CategoryBase
{
    internal RatesCategory(TonApiClient client) : base(client) { }

    /// <summary>
    /// Get the token price in the chosen currency for display only. Don’t use this for financial transactions.
    /// </summary>
    public async Task<TokenRatesResponse> GetAsync(
      List<string> tokens,
      List<string> currencies,
      CancellationToken ct = default)
    {
        var tokensParam = string.Join(",", tokens);
        var currenciesParam = string.Join(",", currencies);
        return await GetAsync<TokenRatesResponse>($"/v2/rates?tokens={tokensParam}&currencies={currenciesParam}", ct);
    }

    /// <summary>
    /// Get chart by token.
    /// </summary>
    public async Task<ChartRates> GetChartAsync(
      string token,
      string currency = "usd",
      long? startDate = null,
      long? endDate = null,
      int? pointsCount = null,
      CancellationToken ct = default)
    {
        var url = $"/v2/rates/chart?token={token}&currency={currency}";
        if (startDate.HasValue)
            url += $"&start_date={startDate.Value}";

        if (endDate.HasValue)
            url += $"&end_date={endDate.Value}";

        if (pointsCount.HasValue)
            url += $"&points_count={pointsCount.Value}";

        return await GetAsync<ChartRates>(url, ct);
    }

    /// <summary>
    /// Get the TON price from markets.
    /// </summary>
    public async Task<MarketTonRates> GetMarketsAsync(CancellationToken ct = default)
    {
        return await GetAsync<MarketTonRates>("/v2/rates/markets", ct);
    }
}
