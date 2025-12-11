using TonapiClient.Models;
using Microsoft.Extensions.Logging;

namespace TonapiClient.Categories;

/// <summary>
/// Account category methods.
/// </summary>
public class AccountCategory : CategoryBase
{
    internal AccountCategory(TonApiClient client, ILogger<TonApiClient> logger) : base(client, logger)
    {
    }

    /// <summary>
    /// Get account information by address.
    /// </summary>
    public async Task<Account> GetAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<Account>($"/v2/accounts/{accountId}", ct);
    }

    /// <summary>
    /// Get TON balance for an account by address.
    /// </summary>
    public async Task<ulong> GetTonBalanceAsync(string accountId, CancellationToken ct = default)
    {
        var account = await GetAsync(accountId, ct);
        return account.Balance;
    }

    /// <summary>
    /// Get blockchain account transactions.
    /// </summary>
    public async Task<BlockchainAccountTransactions> GetTransactionsAsync(
        string accountId,
        long? afterLt = null,
        long? beforeLt = null,
        int limit = 100,
        string sortOrder = "desc",
        CancellationToken ct = default)
    {
        var url = $"/v2/blockchain/accounts/{accountId}/transactions?limit={limit}&sort_order={sortOrder}";

        if (afterLt.HasValue)
            url += $"&after_lt={afterLt.Value}";

        if (beforeLt.HasValue)
            url += $"&before_lt={beforeLt.Value}";

        return await GetAsync<BlockchainAccountTransactions>(url, ct);
    }

    /// <summary>
    /// Get account events.
    /// </summary>
    public async Task<AccountEvents> GetEventsAsync(
        string accountId,
        int limit = 100,
        long? beforeLt = null,
        bool? initiator = null,
        bool? subjectOnly = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/accounts/{accountId}/events?limit={limit}";
        if (beforeLt.HasValue)
            url += $"&before_lt={beforeLt.Value}";
        if (initiator.HasValue)
            url += $"&initiator={initiator.Value.ToString().ToLower()}";
        if (subjectOnly.HasValue)
            url += $"&subject_only={subjectOnly.Value.ToString().ToLower()}";

        return await GetAsync<AccountEvents>(url, ct);
    }

    /// <summary>
    /// Get a specific account event by event ID.
    /// </summary>
    public async Task<Event> GetEventByIdAsync(
        string accountId,
        string eventId,
        bool? subjectOnly = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/accounts/{accountId}/events/{eventId}";
        if (subjectOnly.HasValue)
            url += $"?subject_only={subjectOnly.Value.ToString().ToLower()}";

        return await GetAsync<Event>(url, ct);
    }

    /// <summary>
    /// Get account traces (lightweight trace identifiers).
    /// </summary>
    public async Task<TraceIds> GetTracesAsync(
        string accountId,
        int limit = 100,
        long? beforeLt = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/accounts/{accountId}/traces?limit={limit}";
        if (beforeLt.HasValue)
            url += $"&before_lt={beforeLt.Value}";

        return await GetAsync<TraceIds>(url, ct);
    }

    /// <summary>
    /// Get account Jetton balances.
    /// </summary>
    public async Task<JettonBalances> GetJettonsAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<JettonBalances>($"/v2/accounts/{accountId}/jettons", ct);
    }

    /// <summary>
    /// Get specific Jetton balance for an account by jetton master address.
    /// </summary>
    public async Task<JettonBalance> GetJettonBalanceAsync(string accountId, string jettonMasterAddress, CancellationToken ct = default)
    {
        return await GetAsync<JettonBalance>($"/v2/accounts/{accountId}/jettons/{jettonMasterAddress}", ct);
    }

    /// <summary>
    /// Get Jetton balance for an account by jetton master address or name.
    /// If the identifier starts with "0:", it's treated as a master address.
    /// Otherwise, it's treated as a jetton name and searched in the account's jettons.
    /// </summary>
    public async Task<JettonBalance?> GetJettonBalance(string accountId, string jettonIdentifier, CancellationToken ct = default)
    {
        // Check if identifier looks like an address (starts with "0:")
        if (jettonIdentifier.StartsWith("0:", StringComparison.OrdinalIgnoreCase))
        {
            // It's a master address
            return await GetJettonBalanceAsync(accountId, jettonIdentifier, ct);
        }

        // It's a jetton name - get all jettons and find by name
        var balances = await GetJettonsAsync(accountId, ct);

        // Find jetton by name (case-insensitive)
        var jettonBalance = balances.Balances.FirstOrDefault(x =>
          x.Jetton.Name.Equals(jettonIdentifier, StringComparison.OrdinalIgnoreCase));

        return jettonBalance;
    }

    /// <summary>
    /// Get account NFT items.
    /// </summary>
    public async Task<NftItems> GetNftsAsync(
        string accountId,
        int limit = 1000,
        int offset = 0,
        string? collection = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/accounts/{accountId}/nfts?limit={limit}&offset={offset}";
        if (!string.IsNullOrEmpty(collection))
            url += $"&collection={collection}";

        return await GetAsync<NftItems>(url, ct);
    }

    /// <summary>
    /// Get all domain names associated with the account via DNS backresolve.
    /// </summary>
    public async Task<DomainNames> GetDnsBackresolveAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<DomainNames>($"/v2/accounts/{accountId}/dns/backresolve", ct);
    }

    /// <summary>
    /// Get Jetton transfer history for account.
    /// </summary>
    public async Task<JettonHistory> GetJettonsHistoryAsync(
        string accountId,
        int limit = 100,
        long? beforeLt = null,
        string? jettonMaster = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/accounts/{accountId}/jettons/history?limit={limit}";
        if (beforeLt.HasValue)
            url += $"&before_lt={beforeLt.Value}";

        if (!string.IsNullOrEmpty(jettonMaster))
            url += $"&jetton_master={jettonMaster}";

        return await GetAsync<JettonHistory>(url, ct);
    }

    /// <summary>
    /// Get NFT transfer history for account.
    /// </summary>
    public async Task<NftHistory> GetNftsHistoryAsync(
        string accountId,
        int limit = 100,
        long? beforeLt = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/accounts/{accountId}/nfts/history?limit={limit}";
        if (beforeLt.HasValue)
            url += $"&before_lt={beforeLt.Value}";

        return await GetAsync<NftHistory>(url, ct);
    }

    /// <summary>
    /// Get all subscriptions of the account.
    /// </summary>
    public async Task<Subscriptions> GetSubscriptionsAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<Subscriptions>($"/v2/accounts/{accountId}/subscriptions", ct);
    }

    /// <summary>
    /// Get public key by account ID.
    /// </summary>
    public async Task<PublicKey> GetPublicKeyAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<PublicKey>($"/v2/accounts/{accountId}/publickey", ct);
    }

    /// <summary>
    /// Execute get method for blockchain account.
    /// </summary>
    public async Task<MethodExecutionResult> ExecuteGetMethodAsync(
        string accountId,
        string methodName,
        List<string>? args = null,
        CancellationToken ct = default)
    {
        var url = $"/v2/blockchain/accounts/{accountId}/methods/{methodName}";
        if (args != null && args.Count > 0)
        {
            var argsQuery = string.Join("&", args.Select(arg => $"args={Uri.EscapeDataString(arg)}"));
            url = $"{url}?{argsQuery}";
        }

        return await GetAsync<MethodExecutionResult>(url, ct);
    }

    /// <summary>
    /// Get account balance change between two timestamps.
    /// </summary>
    public async Task<AccountDiff> GetDiffAsync(
        string accountId,
        long startDate,
        long endDate,
        CancellationToken ct = default)
    {
        return await GetAsync<AccountDiff>($"/v2/accounts/{accountId}/diff?start_date={startDate}&end_date={endDate}", ct);
    }

    /// <summary>
    /// Get multiple accounts information at once.
    /// </summary>
    public async Task<Accounts> GetBulkAsync(List<string> accountIds, CancellationToken ct = default)
    {
        var request = new { account_ids = accountIds };
        return await PostAsync<object, Accounts>("/v2/accounts/_bulk", request, ct);
    }

    /// <summary>
    /// Inspect account contract code and get methods.
    /// </summary>
    public async Task<BlockchainAccountInspect> InspectAsync(string accountId, CancellationToken ct = default)
    {
        return await GetAsync<BlockchainAccountInspect>($"/v2/blockchain/accounts/{accountId}/inspect", ct);
    }

    /// <summary>
    /// Waits for a transaction to appear in the blockchain by polling account transactions.
    /// Uses exponential backoff: 1s → 2s → 4s → 8s → ... up to maxPollingInterval.
    /// </summary>
    /// <param name="accountId">Account address to monitor.</param>
    /// <param name="expectedMessageHash">Expected incoming message hash (normalized).</param>
    /// <param name="expireTime">Optional expiration time in seconds. If null, uses maxWaitTime.</param>
    /// <param name="maxWaitTime">Maximum wait time in seconds (default 120).</param>
    /// <param name="initialPollingInterval">Initial polling interval in seconds (default 1).</param>
    /// <param name="maxPollingInterval">Maximum polling interval in seconds (default 8).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The found transaction or null if timeout occurs.</returns>
    public async Task<Transaction?> WaitForTransactionAsync(
        string accountId,
        string expectedMessageHash,
        int? expireTime = null,
        int maxWaitTime = 120,
        int initialPollingInterval = 1,
        int maxPollingInterval = 8,
        CancellationToken ct = default)
    {
        var waitTime = expireTime ?? maxWaitTime;
        var deadline = DateTime.UtcNow.AddSeconds(waitTime);
        var currentInterval = initialPollingInterval;

        while (DateTime.UtcNow < deadline && !ct.IsCancellationRequested)
        {
            try
            {
                var transactions = await GetTransactionsAsync(
                    accountId,
                    limit: 100,
                    sortOrder: "desc",
                    ct: ct);

                // Search for transaction with matching incoming message hash
                foreach (var tx in transactions.Transactions)
                {
                    if (tx.InMsg != null &&
                        tx.InMsg.Hash.Equals(expectedMessageHash, StringComparison.OrdinalIgnoreCase))
                    {
                        return tx;
                    }
                }

                // Wait before next poll with exponential backoff
                var delayMs = currentInterval * 1000;
                var remainingTime = (deadline - DateTime.UtcNow).TotalMilliseconds;

                if (delayMs > remainingTime)
                {
                    delayMs = (int)Math.Max(0, remainingTime);
                }

                if (delayMs > 0)
                {
                    await Task.Delay(delayMs, ct);
                }

                currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while polling for transaction {ExpectedHash} on account {AccountId}",
                    expectedMessageHash, accountId);

                await Task.Delay(currentInterval * 1000, ct);
                currentInterval = Math.Min(currentInterval * 2, maxPollingInterval);
            }
        }

        return null;
    }
}
