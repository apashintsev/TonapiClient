using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Blockchain category methods.
/// </summary>
public class BlockchainCategory : CategoryBase
{
    internal BlockchainCategory(TonApiClient client) : base(client) { }

    /// <summary>
    /// Get blockchain block data by block ID.
    /// </summary>
    public async Task<BlockchainBlock> GetBlockAsync(string blockId, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainBlock>($"/v2/blockchain/blocks/{blockId}", ct);
    }

    /// <summary>
    /// Get reduced blockchain blocks data within a time range.
    /// </summary>
    public async Task<ReducedBlocks> GetReducedBlocksAsync(long from, long to, CancellationToken ct = default)
    {
        return await GetAsync<ReducedBlocks>($"/v2/blockchain/reduced/blocks?from={from}&to={to}", ct);
    }

    /// <summary>
    /// Get blockchain block shards by masterchain sequence number.
    /// </summary>
    public async Task<BlockchainBlockShards> GetMasterchainShardsAsync(int masterchainSeqno, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainBlockShards>($"/v2/blockchain/masterchain/{masterchainSeqno}/shards", ct);
    }

    /// <summary>
    /// Get all blocks between target and previous masterchain block.
    /// </summary>
    public async Task<BlockchainBlocks> GetMasterchainBlocksAsync(int masterchainSeqno, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainBlocks>($"/v2/blockchain/masterchain/{masterchainSeqno}/blocks", ct);
    }

    /// <summary>
    /// Get all transactions between target and previous masterchain block.
    /// </summary>
    public async Task<Transactions> GetMasterchainTransactionsAsync(int masterchainSeqno, CancellationToken ct = default)
    {
        return await GetAsync<Transactions>($"/v2/blockchain/masterchain/{masterchainSeqno}/transactions", ct);
    }

    /// <summary>
    /// Get blockchain configuration by masterchain sequence number.
    /// </summary>
    public async Task<BlockchainConfig> GetConfigAsync(int masterchainSeqno, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainConfig>($"/v2/blockchain/masterchain/{masterchainSeqno}/config", ct);
    }

    /// <summary>
    /// Get transaction by hash.
    /// </summary>
    public async Task<Transaction> GetTransactionAsync(string transactionHash, CancellationToken ct = default)
    {
        return await GetAsync<Transaction>($"/v2/blockchain/transactions/{transactionHash}", ct);
    }

    /// <summary>
    /// Send BOC (Bag of Cells) message to the blockchain.
    /// </summary>
    public async Task<SendBocResponse> SendBocAsync(string boc, CancellationToken ct = default)
    {
        var request = new SendBocRequest { Boc = boc };
        return await PostAsync<SendBocRequest, SendBocResponse>("/v2/blockchain/message", request, ct);
    }

    /// <summary>
    /// Get transactions from a specific block.
    /// </summary>
    public async Task<Transactions> GetBlockTransactionsAsync(string blockId, CancellationToken ct = default)
    {
        return await GetAsync<Transactions>($"/v2/blockchain/blocks/{blockId}/transactions", ct);
    }

    /// <summary>
    /// Get transaction by message hash.
    /// </summary>
    public async Task<Transaction> GetTransactionByMessageHashAsync(string messageHash, CancellationToken ct = default)
    {
        return await GetAsync<Transaction>($"/v2/blockchain/messages/{messageHash}/transaction", ct);
    }

    /// <summary>
    /// Get raw blockchain account.
    /// </summary>
    public async Task<BlockchainRawAccount> GetAccountAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainRawAccount>($"/v2/blockchain/accounts/{accountId}", ct);
    }

    /// <summary>
    /// Get account transactions.
    /// </summary>
    public async Task<BlockchainAccountTransactions> GetAccountTransactionsAsync(string accountId, ulong? beforeLt = null, int? limit = null, string? sortOrder = null, CancellationToken ct = default)
    {
        var url = $"/v2/blockchain/accounts/{accountId}/transactions?";
        if (beforeLt.HasValue) url += $"before_lt={beforeLt}&";
        if (limit.HasValue) url += $"limit={limit}&";
        if (!string.IsNullOrEmpty(sortOrder)) url += $"sort_order={sortOrder}&";
        return await GetAsync<BlockchainAccountTransactions>(url.TrimEnd('&', '?'), ct);
    }

    /// <summary>
    /// Execute get method.
    /// </summary>
    public async Task<MethodExecutionResult> ExecuteGetMethodAsync(string accountId, string methodName, List<string>? args = null, CancellationToken ct = default)
    {
        var url = $"/v2/blockchain/accounts/{accountId}/methods/{methodName}";
        if (args?.Count > 0) url += "?args=" + string.Join("&args=", args);
        return await GetAsync<MethodExecutionResult>(url, ct);
    }

    /// <summary>
    /// Execute method (POST).
    /// </summary>
    public async Task<MethodExecutionResult> ExecuteMethodAsync(string accountId, string methodName, MethodExecutionRequest request, CancellationToken ct = default)
    {
        return await PostAsync<MethodExecutionRequest, MethodExecutionResult>($"/v2/blockchain/accounts/{accountId}/methods/{methodName}", request, ct);
    }

    /// <summary>
    /// Inspect account.
    /// </summary>
    public async Task<BlockchainAccountInspectResult> InspectAccountAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainAccountInspectResult>($"/v2/blockchain/accounts/{accountId}/inspect", ct);
    }

    /// <summary>
    /// Get the latest masterchain block.
    /// </summary>
    public async Task<MasterchainHead> GetMasterchainHeadAsync(CancellationToken ct = default)
    {
        return await GetAsync<MasterchainHead>("/v2/blockchain/masterchain-head", ct);
    }

    /// <summary>
    /// Get current blockchain configuration.
    /// </summary>
    public async Task<BlockchainConfig> GetCurrentConfigAsync(CancellationToken ct = default)
    {
        return await GetAsync<BlockchainConfig>("/v2/blockchain/config", ct);
    }

    /// <summary>
    /// Get blockchain raw configuration.
    /// </summary>
    public async Task<string> GetRawConfigAsync(CancellationToken ct = default)
    {
        var response = await GetAsync<Dictionary<string, string>>("/v2/blockchain/config/raw", ct);
        return response["config"];
    }

    /// <summary>
    /// Get current validators set.
    /// </summary>
    public async Task<Validators> GetValidatorsAsync(CancellationToken ct = default)
    {
        return await GetAsync<Validators>("/v2/blockchain/validators", ct);
    }
}
