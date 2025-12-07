using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Rates category methods.
/// </summary>
public class RatesCategory : CategoryBase
{
  internal RatesCategory(TonApiClient client) : base(client) { }

  /// <summary>
  /// Get current token rates.
  /// </summary>
  public async Task<TokenRates> GetAsync(
      List<string> tokens,
      List<string> currencies,
      CancellationToken ct = default)
  {
    var tokensParam = string.Join(",", tokens);
    var currenciesParam = string.Join(",", currencies);
    return await GetAsync<TokenRates>($"/v2/rates?tokens={tokensParam}&currencies={currenciesParam}", ct);
  }

  /// <summary>
  /// Get historical chart data for token rates.
  /// </summary>
  public async Task<ChartRates> GetChartAsync(
      string token,
      string currency = "usd",
      long? startDate = null,
      long? endDate = null,
      CancellationToken ct = default)
  {
    var url = $"/v2/rates/chart?token={token}&currency={currency}";
    if (startDate.HasValue)
      url += $"&start_date={startDate.Value}";

    if (endDate.HasValue)
      url += $"&end_date={endDate.Value}";

    return await GetAsync<ChartRates>(url, ct);
  }

  /// <summary>
  /// Get TON rates from different markets/exchanges.
  /// </summary>
  public async Task<MarketTonRates> GetMarketsAsync(CancellationToken ct = default)
  {
    return await GetAsync<MarketTonRates>("/v2/rates/markets", ct);
  }
}
