using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// DNS category methods.
/// </summary>
public class DnsCategory : CategoryBase
{
    internal DnsCategory(TonApiClient client) : base(client) { }

    /// <summary>
    /// Get DNS record by domain name.
    /// </summary>
    public async Task<DnsRecord> GetRecordAsync(string domainName, CancellationToken ct = default)
    {
        return await GetAsync<DnsRecord>($"/v2/dns/{domainName}", ct);
    }

    /// <summary>
    /// Resolve domain name to wallet address.
    /// </summary>
    public async Task<DomainInfo> ResolveAsync(string domainName, CancellationToken ct = default)
    {
        return await GetAsync<DomainInfo>($"/v2/dns/{domainName}/resolve", ct);
    }

    /// <summary>
    /// Get all DNS auctions.
    /// </summary>
    public async Task<Auctions> GetAuctionsAsync(string? tld = null, CancellationToken ct = default)
    {
        var url = "/v2/dns/auctions";
        if (!string.IsNullOrEmpty(tld))
            url += $"?tld={tld}";

        return await GetAsync<Auctions>(url, ct);
    }

    /// <summary>
    /// Get bids for a specific DNS domain.
    /// </summary>
    public async Task<DomainBids> GetBidsAsync(string domainName, CancellationToken ct = default)
    {
        return await GetAsync<DomainBids>($"/v2/dns/{domainName}/bids", ct);
    }
}
