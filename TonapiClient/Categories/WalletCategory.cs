using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Wallet category methods.
/// </summary>
public class WalletCategory : CategoryBase
{
  internal WalletCategory(TonApiClient client) : base(client) { }

  /// <summary>
  /// Get wallet sequence number.
  /// </summary>
  public async Task<Seqno> GetSeqnoAsync(string walletAddress, CancellationToken ct = default)
  {
    return await GetAsync<Seqno>($"/v2/wallet/{walletAddress}/seqno", ct);
  }

  /// <summary>
  /// Emulate sending a message to the blockchain without actually sending it.
  /// </summary>
  public async Task<MessageConsequences> EmulateAsync(
      string boc,
      List<EmulateMessageParam>? @params = null,
      CancellationToken ct = default)
  {
    var request = new EmulateMessageRequest
    {
      Boc = boc,
      Params = @params,
    };
    return await PostAsync<EmulateMessageRequest, MessageConsequences>("/v2/wallet/emulate", request, ct);
  }
}
