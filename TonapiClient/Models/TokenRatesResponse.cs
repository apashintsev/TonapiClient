using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the response containing token rates.
/// </summary>
public class TokenRatesResponse
{
    /// <summary>
    /// Gets or sets the rates for different tokens.
    /// Key is the token symbol (e.g., "TON"), value is the token rate information.
    /// </summary>
    [JsonPropertyName("rates")]
    public Dictionary<string, TokenRates> Rates { get; set; } = new();
}

