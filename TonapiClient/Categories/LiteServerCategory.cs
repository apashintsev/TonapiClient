using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Lite Server category methods.
/// </summary>
public class LiteServerCategory : CategoryBase
{
  internal LiteServerCategory(TonApiClient client) : base(client) { }

  /// <summary>
  /// Get raw account state from lite server.
  /// </summary>
  public async Task<LiteServerAccountState> GetAccountStateAsync(string accountId, CancellationToken ct = default)
  {
    return await GetAsync<LiteServerAccountState>($"/v2/liteserver/get_account_state/{accountId}", ct);
  }

  /// <summary>
  /// Get raw masterchain info from lite server.
  /// </summary>
  public async Task<RawMasterchainInfo> GetMasterchainInfoAsync(CancellationToken ct = default)
  {
    return await GetAsync<RawMasterchainInfo>("/v2/liteserver/get_masterchain_info", ct);
  }

  /// <summary>
  /// Get extended raw masterchain info from lite server.
  /// </summary>
  public async Task<RawMasterchainInfo> GetMasterchainInfoExtAsync(CancellationToken ct = default)
  {
    return await GetAsync<RawMasterchainInfo>("/v2/liteserver/get_masterchain_info_ext", ct);
  }

  /// <summary>
  /// Get current blockchain time from lite server.
  /// </summary>
  public async Task<long> GetTimeAsync(CancellationToken ct = default)
  {
    var response = await GetAsync<Dictionary<string, long>>("/v2/liteserver/get_time", ct);
    return response["time"];
  }

  /// <summary>
  /// Get raw blockchain block from lite server.
  /// </summary>
  public async Task<RawBlockchainBlock> GetBlockAsync(string blockId, CancellationToken ct = default)
  {
    return await GetAsync<RawBlockchainBlock>($"/v2/liteserver/get_block?block_id={blockId}", ct);
  }

  /// <summary>
  /// Get raw blockchain block header from lite server.
  /// </summary>
  public async Task<RawBlockchainBlockHeader> GetBlockHeaderAsync(string blockId, CancellationToken ct = default)
  {
    return await GetAsync<RawBlockchainBlockHeader>($"/v2/liteserver/get_block_header?block_id={blockId}", ct);
  }

  /// <summary>
  /// Send raw message to blockchain via lite server.
  /// </summary>
  public async Task SendMessageAsync(string body, CancellationToken ct = default)
  {
    var request = new { body };
    await PostAsync<object, object>("/v2/liteserver/send_message", request, ct);
  }

  /// <summary>
  /// Get raw transactions for an account from lite server.
  /// </summary>
  public async Task<RawTransactions> GetTransactionsAsync(
      string accountId,
      int count = 10,
      string? lt = null,
      string? hash = null,
      CancellationToken ct = default)
  {
    var url = $"/v2/liteserver/get_transactions?account={accountId}&count={count}";
    if (!string.IsNullOrEmpty(lt))
      url += $"&lt={lt}";

    if (!string.IsNullOrEmpty(hash))
      url += $"&hash={hash}";

    return await GetAsync<RawTransactions>(url, ct);
  }

  /// <summary>
  /// Get information about all shards from lite server.
  /// </summary>
  public async Task<RawShardInfo> GetAllShardsInfoAsync(string blockId, CancellationToken ct = default)
  {
    return await GetAsync<RawShardInfo>($"/v2/liteserver/get_all_shards_info?block_id={blockId}", ct);
  }

  /// <summary>
  /// Get raw transactions from a specific block.
  /// </summary>
  public async Task<List<RawTransactionId>> GetListBlockTransactionsAsync(
      string blockId,
      int count = 40,
      string? accountId = null,
      string? lt = null,
      CancellationToken ct = default)
  {
    var url = $"/v2/liteserver/list_block_transactions?block_id={blockId}&count={count}";
    if (!string.IsNullOrEmpty(accountId))
      url += $"&account_id={accountId}";

    if (!string.IsNullOrEmpty(lt))
      url += $"&lt={lt}";

    var response = await GetAsync<Dictionary<string, List<RawTransactionId>>>(url, ct);
    return response["ids"];
  }

  /// <summary>
  /// Get raw block proof from lite server.
  /// </summary>
  public async Task<string> GetBlockProofAsync(
      string knownBlockId,
      string? targetBlockId = null,
      CancellationToken ct = default)
  {
    var url = $"/v2/liteserver/get_block_proof?known_block={knownBlockId}";
    if (!string.IsNullOrEmpty(targetBlockId))
      url += $"&target_block={targetBlockId}";

    var response = await GetAsync<Dictionary<string, string>>(url, ct);
    return response["complete"];
  }

  /// <summary>
  /// Get raw configuration parameters from lite server.
  /// </summary>
  public async Task<string> GetConfigAsync(int? configId = null, CancellationToken ct = default)
  {
    var url = "/v2/liteserver/get_config_all";
    if (configId.HasValue)
      url = $"/v2/liteserver/get_config_params?config_id={configId.Value}";

    var response = await GetAsync<Dictionary<string, string>>(url, ct);
    return response["config_proof"];
  }

  /// <summary>
  /// Get raw shard block proof from lite server.
  /// </summary>
  public async Task<string> GetShardBlockProofAsync(string blockId, CancellationToken ct = default)
  {
    var response = await GetAsync<Dictionary<string, string>>($"/v2/liteserver/get_shard_block_proof?block_id={blockId}", ct);
    return response["proof"];
  }

  /// <summary>
  /// Get outgoing message queue sizes from lite server.
  /// </summary>
  public async Task<Dictionary<string, int>> GetOutMsgQueueSizesAsync(CancellationToken ct = default)
  {
    var response = await GetAsync<Dictionary<string, Dictionary<string, int>>>("/v2/liteserver/get_out_msg_queue_sizes", ct);
    return response["shards"];
  }
}
