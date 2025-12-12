using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a collection of account subscriptions.
/// </summary>
public class Subscriptions
{
    /// <summary>
    /// Gets or sets the list of subscriptions.
    /// </summary>
    [JsonPropertyName("subscriptions")]
    public List<Subscription> SubscriptionsList { get; set; } = new();
}
