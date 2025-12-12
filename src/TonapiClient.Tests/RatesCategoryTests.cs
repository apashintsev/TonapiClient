using Xunit;
using TonapiClient.Models;

namespace TonapiClient.Tests;

public class RatesCategoryTests : TestBase
{
    [Fact]
    public async Task GetAsync_WithTonTokenAndMultipleCurrencies_ReturnsRates()
    {
        // Arrange
        var tokens = new List<string> { "ton" };
        var currencies = new List<string> { "ton", "usd", "rub" };

        // Act
        var result = await Client.Rates.GetAsync(tokens, currencies);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Rates);
        
        // Verify that we have rates for TON token
        Assert.True(result.Rates.ContainsKey("TON"), "Response should contain rates for TON token");
        
        var tonRates = result.Rates["TON"];
        Assert.NotNull(tonRates);
        
        // Verify prices structure
        Assert.NotNull(tonRates.Prices);
        Assert.True(tonRates.Prices.ContainsKey("TON"), "Prices should contain TON");
        Assert.True(tonRates.Prices.ContainsKey("USD"), "Prices should contain USD");
        Assert.True(tonRates.Prices.ContainsKey("RUB"), "Prices should contain RUB");
        
        // Verify price values are reasonable
        Assert.Equal(1m, tonRates.Prices["TON"]); // TON/TON should always be 1
        Assert.True(tonRates.Prices["USD"] > 0, "USD price should be positive");
        Assert.True(tonRates.Prices["RUB"] > 0, "RUB price should be positive");
        
        // Verify diff_24h structure
        Assert.NotNull(tonRates.Diff24h);
        Assert.True(tonRates.Diff24h.ContainsKey("TON"), "diff_24h should contain TON");
        Assert.True(tonRates.Diff24h.ContainsKey("USD"), "diff_24h should contain USD");
        Assert.True(tonRates.Diff24h.ContainsKey("RUB"), "diff_24h should contain RUB");
        
        // Verify all diff_24h values are strings with % sign or contain +/− symbols
        Assert.All(tonRates.Diff24h.Values, diff => 
        {
            Assert.NotNull(diff);
            Assert.NotEmpty(diff);
            Assert.True(diff.Contains("%") || diff.Contains("+") || diff.Contains("−") || diff.Contains("-"), 
                $"diff_24h value '{diff}' should contain %, +, − or - symbol");
        });
        
        // Verify diff_7d structure
        Assert.NotNull(tonRates.Diff7d);
        Assert.True(tonRates.Diff7d.ContainsKey("TON"), "diff_7d should contain TON");
        Assert.True(tonRates.Diff7d.ContainsKey("USD"), "diff_7d should contain USD");
        Assert.True(tonRates.Diff7d.ContainsKey("RUB"), "diff_7d should contain RUB");
        
        // Verify all diff_7d values are strings
        Assert.All(tonRates.Diff7d.Values, diff => 
        {
            Assert.NotNull(diff);
            Assert.NotEmpty(diff);
        });
        
        // Verify diff_30d structure
        Assert.NotNull(tonRates.Diff30d);
        Assert.True(tonRates.Diff30d.ContainsKey("TON"), "diff_30d should contain TON");
        Assert.True(tonRates.Diff30d.ContainsKey("USD"), "diff_30d should contain USD");
        Assert.True(tonRates.Diff30d.ContainsKey("RUB"), "diff_30d should contain RUB");
        
        // Verify all diff_30d values are strings
        Assert.All(tonRates.Diff30d.Values, diff => 
        {
            Assert.NotNull(diff);
            Assert.NotEmpty(diff);
        });
        
        // Verify TON/TON diff should be "0.00%"
        Assert.Equal("0.00%", tonRates.Diff24h["TON"]);
        Assert.Equal("0.00%", tonRates.Diff7d["TON"]);
        Assert.Equal("0.00%", tonRates.Diff30d["TON"]);
    }

    [Fact]
    public async Task GetChartAsync_WithTokenAndCurrency_ReturnsChartRates()
    {
        // Arrange
        var token = "0:f418a04cf196ebc959366844a6cdf53a6fd6fff1eadafc892f05210bba31593e";
        var currency = "usd";
        var pointsCount = 200;

        // Act
        var result = await Client.Rates.GetChartAsync(token, currency, pointsCount: pointsCount);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Points);
        
        // On testnet, points may be empty, but on mainnet it would contain data
        // Each point should be an array of [timestamp, price]
        if (result.Points.Count > 0)
        {
            // Verify first point structure
            var firstPoint = result.Points[0];
            Assert.NotNull(firstPoint);
            Assert.Equal(2, firstPoint.Count); // Should have exactly 2 elements: [timestamp, price]
            
            // First element should be timestamp (large positive number)
            Assert.True(firstPoint[0] > 0, "Timestamp should be a positive number");
            
            // Second element should be price (positive number)
            Assert.True(firstPoint[1] > 0, "Price should be a positive number");
            
            // Verify all points have correct structure
            Assert.All(result.Points, point =>
            {
                Assert.NotNull(point);
                Assert.Equal(2, point.Count);
                Assert.True(point[0] > 0, "Timestamp should be positive");
                Assert.True(point[1] > 0, "Price should be positive");
            });
        }
        
        // The structure should be correct even if empty on testnet
        Assert.IsType<List<List<decimal>>>(result.Points);
    }

    [Fact]
    public async Task GetMarketsAsync_ReturnsMarketRates()
    {
        // Act
        var result = await Client.Rates.GetMarketsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Markets);
        Assert.NotEmpty(result.Markets);
        
        // Verify that we have rates from multiple markets
        Assert.True(result.Markets.Count > 0, "Should return at least one market");
        
        // Known market names that should be present
        var knownMarkets = new[] { "Bitfinex", "Bybit", "Gate.io", "Huobi", "KuCoin", "OKX" };
        var marketNames = result.Markets.Select(m => m.Market).ToList();
        
        // Verify that at least some known markets are present
        var foundMarkets = knownMarkets.Where(km => marketNames.Contains(km)).ToList();
        Assert.True(foundMarkets.Count > 0, $"Should contain at least one known market. Found: {string.Join(", ", marketNames)}");
        
        // Verify structure of each market
        foreach (var market in result.Markets)
        {
            // Verify market name is not empty
            Assert.NotNull(market.Market);
            Assert.NotEmpty(market.Market);
            
            // Verify USD price is positive
            Assert.True(market.UsdPrice > 0, $"USD price for {market.Market} should be positive, got {market.UsdPrice}");
            
            // Verify last_date_update is a valid timestamp (should be a recent Unix timestamp)
            Assert.True(market.LastDateUpdate > 0, $"Last date update for {market.Market} should be positive, got {market.LastDateUpdate}");
            
            // Timestamp should be reasonable (after 2020-01-01 and before year 2100)
            Assert.True(market.LastDateUpdate > 1577836800, $"Timestamp for {market.Market} seems too old: {market.LastDateUpdate}");
            Assert.True(market.LastDateUpdate < 4102444800, $"Timestamp for {market.Market} seems too far in future: {market.LastDateUpdate}");
        }
        
        // Verify all market names are unique
        var uniqueMarkets = result.Markets.Select(m => m.Market).Distinct().Count();
        Assert.Equal(result.Markets.Count, uniqueMarkets);
        
        // Verify all USD prices are reasonable (between $0.01 and $1000 for TON)
        Assert.All(result.Markets, market => 
        {
            Assert.True(market.UsdPrice >= 0.01m, $"{market.Market}: Price too low: {market.UsdPrice}");
            Assert.True(market.UsdPrice <= 1000m, $"{market.Market}: Price too high: {market.UsdPrice}");
        });
    }
}

