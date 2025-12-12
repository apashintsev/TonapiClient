# TonapiClient

[![NuGet](https://img.shields.io/nuget/v/TonapiClient.svg)](https://www.nuget.org/packages/TonapiClient/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)

C# .NET client for [TON API](https://tonapi.io).

**Supported platforms:** .NET 10.0+

English version | [Русская версия](README_RU.md)

## Installation

```bash
dotnet add package TonapiClient
```

## Features

✅ Full TON API v2 coverage
✅ Type-safe models for all endpoints
✅ Dependency Injection and HttpClientFactory support
✅ Retry logic and error handling
✅ CancellationToken support
✅ Asynchronous transaction waiting with exponential backoff
✅ Low-level access via LiteServer API
✅ Testnet and mainnet support

## Test Configuration

To run tests, create a configuration file:

1. Copy `src/TonapiClient.Tests/appsettings.example.json` to `src/TonapiClient.Tests/appsettings.json`
2. Replace `YOUR_API_KEY_HERE` with your API key from [TON API](https://tonapi.io)
3. For tests, it's recommended to use testnet API: `https://testnet.tonapi.io`

```json
{
  "TonApiClient": {
    "BaseUrl": "https://testnet.tonapi.io",
    "ApiKey": "YOUR_API_KEY_HERE",
    "TimeoutSeconds": 30
  }
}
```

Run tests:

```bash
dotnet test
```

**Note**: The `appsettings.json` file is included in `.gitignore` and should not be committed to the repository.

## Usage

### Basic Usage

```csharp
using TonapiClient;

var client = new TonApiClient("https://tonapi.io", "your-api-key");

// Get blockchain information
var masterchainHead = await client.Blockchain.GetMasterchainHeadAsync();
Console.WriteLine($"Last block: {masterchainHead.Last.Seqno}");

// Get account information
var account = await client.Account.GetAsync("EQD...address");
Console.WriteLine($"Balance: {account.Balance}");

// Get account transactions
var transactions = await client.Account.GetTransactionsAsync("EQD...address", limit: 10);
foreach (var tx in transactions.Transactions)
{
    Console.WriteLine($"TX: {tx.Hash}");
}

// Get Jetton information
var jetton = await client.Jetton.GetAsync("EQD...jetton-address");
Console.WriteLine($"Jetton: {jetton.Metadata.Name}");

// Get NFT collection
var nft = await client.Nft.GetCollectionAsync("EQD...collection-address");
Console.WriteLine($"NFT Collection: {nft.Metadata.Name}");
```

### Dependency Injection (recommended approach)

The client uses `HttpClientFactory` to manage HTTP connections and supports full DI integration:

```csharp
using Microsoft.Extensions.DependencyInjection;
using TonapiClient;

var services = new ServiceCollection();

// From configuration (appsettings.json)
services.AddTonApiClient(configuration);

// Or with explicit parameters
services.AddTonApiClient(options =>
{
    options.BaseUrl = "https://tonapi.io";
    options.ApiKey = "your-api-key";
    options.TimeoutSeconds = 30;
});

var serviceProvider = services.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<TonApiClient>();

// Use client...
```

In ASP.NET Core, add to `Program.cs`:

```csharp
builder.Services.AddTonApiClient(builder.Configuration);
```

And in `appsettings.json`:

```json
{
  "TonApiClient": {
    "BaseUrl": "https://tonapi.io",
    "ApiKey": "your-api-key",
    "TimeoutSeconds": 30
  }
}
```

## Architecture

The client is built on categories, each encapsulating logic for working with a specific area of TON API:

- 🔗 **CategoryBase** - base class for all categories with common HTTP methods
- 📦 **TonApiClient** - main facade providing access to all categories via properties
- 🔌 **HttpClientFactory** - used for efficient HTTP connection management
- ⚙️ **IOptions** - configuration via standard .NET mechanism

All categories are accessible as client properties: `client.Blockchain`, `client.Account`, `client.Jetton`, etc.

## API Categories

The client is organized by categories for ease of use:

### `Blockchain` - Blockchain Methods

- `GetBlockAsync()` - Get block data
- `GetReducedBlocksAsync()` - Get reduced block data within a time range
- `GetMasterchainShardsAsync()` - Get masterchain block shards
- `GetMasterchainHeadAsync()` - Get latest masterchain block
- `GetMasterchainBlocksAsync()` - Get blocks between masterchain blocks
- `GetMasterchainTransactionsAsync()` - Get transactions between masterchain blocks
- `GetBlockTransactionsAsync()` - Get transactions from a specific block
- `GetConfigAsync()` - Get blockchain config by seqno
- `GetCurrentConfigAsync()` - Get current blockchain config
- `GetRawConfigAsync()` - Get raw blockchain config
- `GetTransactionAsync()` - Get transaction by hash
- `GetTransactionByMessageHashAsync()` - Get transaction by message hash
- `SendBocAsync()` - Send BOC message
- `WaitForTransactionAsync()` - Wait for transaction with exponential backoff
- `GetValidatorsAsync()` - Get validators list
- `GetAccountAsync()` - Get raw account data
- `GetAccountTransactionsAsync()` - Get account transactions
- `ExecuteGetMethodAsync()` - Execute GET method on account
- `ExecuteMethodAsync()` - Execute POST method on account
- `InspectAccountAsync()` - Inspect account

### `Account` - Account Methods

- `GetAsync()` - Get account information
- `GetTonBalanceAsync()` - Get account TON balance
- `GetBulkAsync()` - Get information about multiple accounts
- `GetTransactionsAsync()` - Get account transactions
- `GetEventsAsync()` - Get account events
- `GetEventByIdAsync()` - Get specific event by ID
- `GetTracesAsync()` - Get account traces (lightweight identifiers)
- `GetJettonsAsync()` - Get account Jetton balances
- `GetJettonBalanceAsync()` - Get specific Jetton balance by master address
- `GetJettonBalance()` - Get Jetton balance by name or address
- `GetJettonsHistoryAsync()` - Get Jetton transfer history
- `GetNftsAsync()` - Get account NFTs
- `GetNftsHistoryAsync()` - Get NFT transfer history
- `GetDnsBackresolveAsync()` - Get account domain names via DNS backresolve
- `GetSubscriptionsAsync()` - Get account subscriptions
- `GetPublicKeyAsync()` - Get account public key
- `GetDiffAsync()` - Get balance change between two timestamps
- `ExecuteGetMethodAsync()` - Execute smart contract GET method
- `InspectAsync()` - Inspect account contract
- `WaitForTransactionAsync()` - Wait for transaction with exponential backoff

### `Jetton` - Jetton Token Methods

- `GetAsync()` - Get Jetton information
- `GetAllAsync()` - Get list of all Jettons
- `GetBulkAsync()` - Get metadata for multiple Jettons by addresses
- `GetHoldersAsync()` - Get Jetton holders
- `GetEventJettonsAsync()` - Get event with Jetton transfer information
- `GetJettonWalletAddressAsync()` - Get user's Jetton wallet address

### `Nft` - NFT Methods

- `GetItemAsync()` - Get NFT by address
- `GetItemsBulkAsync()` - Get multiple NFTs by addresses
- `GetItemHistoryAsync()` - Get NFT history
- `GetCollectionAsync()` - Get NFT collection
- `GetCollectionsAsync()` - Get list of collections
- `GetCollectionsBulkAsync()` - Get multiple collections by addresses
- `GetCollectionItemsAsync()` - Get NFTs from collection

### `Dns` - DNS Methods

- `GetRecordAsync()` - Get DNS record
- `ResolveAsync()` - Resolve domain name
- `GetAuctionsAsync()` - Get DNS auctions
- `GetBidsAsync()` - Get bids for domain

### `Staking` - Staking Methods

- `GetPoolsAsync()` - Get list of staking pools
- `GetPoolAsync()` - Get pool information
- `GetAccountInfoAsync()` - Get account staking information

### `Rates` - Currency Rates

- `GetAsync()` - Get current token rates
- `GetChartAsync()` - Get historical rate data
- `GetMarketsAsync()` - Get TON rates from different exchanges

### `Traces` - Transaction Traces

- `GetAsync()` - Get trace by ID
- `WaitForAsync()` - Wait for trace completion
- `EmulateAsync()` - Emulate message and get trace

### `Wallet` - Wallet Methods

- `GetAsync()` - Get wallet information
- `GetSeqnoAsync()` - Get wallet seqno
- `GetWalletsByPublicKeyAsync()` - Get wallets by public key
- `EmulateAsync()` - Emulate sending message

### `Gasless` - Gasless Transactions

- `GetConfigAsync()` - Get gasless configuration
- `EstimateAsync()` - Estimate gasless transaction fee
- `SendAsync()` - Send gasless transaction

### `Events` - Events

- `GetAsync()` - Get event by ID
- `WaitForAsync()` - Wait for event completion

### `LiteServer` - Lite Server Methods (low-level access)

- `GetMasterchainInfoAsync()` - Get masterchain information
- `GetMasterchainInfoExtAsync()` - Get extended masterchain information
- `GetTimeAsync()` - Get blockchain time
- `GetBlockAsync()` - Get raw block by ID
- `GetBlockHeaderAsync()` - Get raw block header
- `GetAccountStateAsync()` - Get raw account state
- `GetShardInfoAsync()` - Get shard information
- `GetAllShardsInfoAsync()` - Get information about all shards
- `GetTransactionsAsync()` - Get raw account transactions
- `GetListBlockTransactionsAsync()` - Get list of transactions from block
- `SendMessageAsync()` - Send raw message to blockchain
- `GetBlockProofAsync()` - Get raw block proof
- `GetShardBlockProofAsync()` - Get raw shard block proof
- `GetConfigAsync()` - Get raw configuration parameters
- `GetOutMsgQueueSizesAsync()` - Get outgoing message queue sizes

### `Storage` - TON Storage

- `GetProvidersAsync()` - Get list of storage providers

### `Multisig` - Multisig Wallets

- `GetAccountAsync()` - Get multisig account information
- `GetOrdersAsync()` - Get multisig orders (proposals)

### `Emulation` - Emulation

- `DecodeMessageAsync()` - Decode message

## Examples

### Sending Transaction

```csharp
// Emulate before sending
var consequences = await client.Wallet.EmulateAsync(bocMessage);
Console.WriteLine($"Estimated fee: {consequences.Event.Fee.Total}");

// Send transaction
var response = await client.Blockchain.SendBocAsync(bocMessage);
Console.WriteLine($"Message hash: {response.Hash}");

// Wait for transaction
var transaction = await client.Account.WaitForTransactionAsync(
    accountAddress,
    response.Hash,
    maxWaitTime: 60);

if (transaction != null)
{
    Console.WriteLine($"Transaction confirmed: {transaction.Hash}");
}
```

### Working with Jettons

```csharp
// Get user's Jetton wallet address
var jettonWalletAddress = await client.GetJettonWalletAddressAsync(
    jettonMasterAddress,
    userAddress);

// Get Jetton information
var jetton = await client.Jetton.GetAsync(jettonMasterAddress);
Console.WriteLine($"{jetton.Metadata.Name} ({jetton.Metadata.Symbol})");

// Get holders
var holders = await client.Jetton.GetHoldersAsync(jettonMasterAddress, limit: 100);
Console.WriteLine($"Total holders: {holders.Total}");
```

### Working with NFT

```csharp
// Get NFT collection
var collection = await client.Nft.GetCollectionAsync(collectionAddress);
Console.WriteLine($"Collection: {collection.Metadata.Name}");

// Get NFTs from collection
var items = await client.Nft.GetCollectionItemsAsync(collectionAddress, limit: 10);
foreach (var item in items.NftItems)
{
    Console.WriteLine($"NFT: {item.Metadata.Name}");
}

// Get NFT history
var history = await client.Nft.GetItemHistoryAsync(nftAddress);
foreach (var event in history.Events)
{
    Console.WriteLine($"Event: {event.EventId}");
}
```

### Executing Smart Contract Get-Methods

```csharp
// Inspect account to get list of available methods
var inspection = await client.Blockchain.InspectAccountAsync(contractAddress);
foreach (var method in inspection.Methods)
{
    Console.WriteLine($"Method: {method.Name}");
}

// Execute GET method (parameters in query string)
var result = await client.Blockchain.ExecuteGetMethodAsync(
    contractAddress,
    "get_wallet_address",
    new List<string> { "0:..." });

// Execute POST method (parameters in request body)
var postResult = await client.Blockchain.ExecuteMethodAsync(
    contractAddress,
    "get_wallet_address",
    new List<MethodArg>
    {
        new() { Type = "slice", Value = "b5ee9c72..." }
    });

Console.WriteLine($"Success: {postResult.Success}");
```

### Working with LiteServer (low-level access)

```csharp
// Get masterchain information
var info = await client.LiteServer.GetMasterchainInfoAsync();
Console.WriteLine($"Last block seqno: {info.Last.Seqno}");

// Get blockchain time
var time = await client.LiteServer.GetTimeAsync();
Console.WriteLine($"Blockchain time: {DateTimeOffset.FromUnixTimeSeconds(time.Time)}");

// Get raw block
var blockId = $"({info.Last.Workchain},{info.Last.Shard},{info.Last.Seqno},{info.Last.RootHash},{info.Last.FileHash})";
var block = await client.LiteServer.GetBlockAsync(blockId);
Console.WriteLine($"Block data length: {block.Data.Length}");

// Get raw account state
var accountState = await client.LiteServer.GetAccountStateAsync(
    "0:...",
    targetBlock: blockId);
Console.WriteLine($"Account balance: {accountState.Balance}");

// Get raw transactions
var rawTxs = await client.LiteServer.GetTransactionsAsync(
    "0:...",
    count: 10);
Console.WriteLine($"Transactions: {rawTxs.Transactions.Count}");
```

## Project Structure

```
TonapiClient/
├── src/
│   ├── TonapiClient/              # Main library
│   │   ├── Categories/            # API categories
│   │   │   ├── AccountCategory.cs
│   │   │   ├── BlockchainCategory.cs
│   │   │   ├── JettonCategory.cs
│   │   │   ├── NftCategory.cs
│   │   │   ├── LiteServerCategory.cs
│   │   │   └── ...
│   │   ├── Models/                # Data models
│   │   ├── TonApiClient.cs        # Main client class
│   │   ├── TonApiClientOptions.cs # Configuration options
│   │   └── TonApiException.cs     # Exceptions
│   └── TonapiClient.Tests/        # Integration tests
│       ├── AccountCategoryTests.cs
│       ├── BlockchainCategoryTests.cs
│       ├── LiteServerCategoryTests.cs
│       └── ...
├── .github/
│   └── workflows/
│       └── ci-cd.yml              # CI/CD configuration
└── README.md
```

## Development Requirements

- .NET 10.0 SDK
- API key from [TON API](https://tonapi.io) (for tests)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Useful Links

- [TON API Documentation](https://docs.tonconsole.com/tonapi/api-v2)
- [TON API Console](https://tonapi.io)
- [TON Documentation](https://docs.ton.org)

## Donations

UQAN9eHzTT6ntU0LSIcqwLJz9GdzeUAOjeXr0x8_XWO0W9S5

## License

MIT
