using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// NFT category methods.
/// </summary>
public class NftCategory : CategoryBase
{
  internal NftCategory(TonApiClient client) : base(client) { }

  /// <summary>
  /// Get list of NFT collections.
  /// </summary>
  public async Task<NftCollections> GetCollectionsAsync(int limit = 15, int offset = 0, CancellationToken ct = default)
  {
    return await GetAsync<NftCollections>($"/v2/nfts/collections?limit={limit}&offset={offset}", ct);
  }

  /// <summary>
  /// Get NFT collection information by address.
  /// </summary>
  public async Task<NftCollection> GetCollectionAsync(string collectionAddress, CancellationToken ct = default)
  {
    return await GetAsync<NftCollection>($"/v2/nfts/collections/{collectionAddress}", ct);
  }

  /// <summary>
  /// Get multiple NFT collections information at once.
  /// </summary>
  public async Task<NftCollections> GetCollectionsBulkAsync(List<string> collectionAddresses, CancellationToken ct = default)
  {
    var request = new { account_ids = collectionAddresses };
    return await PostAsync<object, NftCollections>("/v2/nfts/collections/_bulk", request, ct);
  }

  /// <summary>
  /// Get items from NFT collection.
  /// </summary>
  public async Task<NftItems> GetCollectionItemsAsync(
      string collectionAddress,
      int limit = 1000,
      int offset = 0,
      CancellationToken ct = default)
  {
    return await GetAsync<NftItems>($"/v2/nfts/collections/{collectionAddress}/items?limit={limit}&offset={offset}", ct);
  }

  /// <summary>
  /// Get NFT item information by address.
  /// </summary>
  public async Task<NftItem> GetItemAsync(string nftAddress, CancellationToken ct = default)
  {
    return await GetAsync<NftItem>($"/v2/nfts/{nftAddress}", ct);
  }

  /// <summary>
  /// Get multiple NFT items information at once.
  /// </summary>
  public async Task<NftItems> GetItemsBulkAsync(List<string> nftAddresses, CancellationToken ct = default)
  {
    var request = new { account_ids = nftAddresses };
    return await PostAsync<object, NftItems>("/v2/nfts/_bulk", request, ct);
  }

  /// <summary>
  /// Get transfer history of a specific NFT item.
  /// </summary>
  public async Task<NftItemHistory> GetItemHistoryAsync(
      string nftAddress,
      int limit = 100,
      long? beforeLt = null,
      CancellationToken ct = default)
  {
    var url = $"/v2/nfts/{nftAddress}/history?limit={limit}";
    if (beforeLt.HasValue)
      url += $"&before_lt={beforeLt.Value}";

    return await GetAsync<NftItemHistory>(url, ct);
  }
}
