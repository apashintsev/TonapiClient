using TonapiClient;

// Пример использования TonApiClient с новой структурой категорий

var client = new TonApiClient("https://tonapi.io", "your-api-key");

// === Blockchain категория ===
var masterchainHead = await client.Blockchain.GetMasterchainHeadAsync();
Console.WriteLine($"Последний блок: {masterchainHead.Last.Seqno}");

var block = await client.Blockchain.GetBlockAsync("block-id");
var transaction = await client.Blockchain.GetTransactionAsync("tx-hash");

// Отправка транзакции
var sendResponse = await client.Blockchain.SendBocAsync("base64-boc");
Console.WriteLine($"Отправлено: {sendResponse.Hash}");

// === Account категория ===
var account = await client.Account.GetAsync("EQD...address");
Console.WriteLine($"Баланс: {account.Balance}");

var transactions = await client.Account.GetTransactionsAsync(
    "EQD...address",
    limit: 10,
    sortOrder: "desc");

var jettonBalances = await client.Account.GetJettonsAsync("EQD...address");
var nfts = await client.Account.GetNftsAsync("EQD...address");

// Выполнение get-метода контракта
var result = await client.Account.ExecuteGetMethodAsync(
    "EQD...contract",
    "get_jetton_data");

// === Jetton категория ===
var jetton = await client.Jetton.GetAsync("EQD...jetton");
Console.WriteLine($"Jetton: {jetton.Metadata.Name}");

var holders = await client.Jetton.GetHoldersAsync("EQD...jetton", limit: 100);
Console.WriteLine($"Держателей: {holders.Total}");

// === NFT категория ===
var nftCollection = await client.Nft.GetCollectionAsync("EQD...collection");
var nftItems = await client.Nft.GetCollectionItemsAsync("EQD...collection");
var nftItem = await client.Nft.GetItemAsync("EQD...nft");
var nftHistory = await client.Nft.GetItemHistoryAsync("EQD...nft");

// === DNS категория ===
var dnsRecord = await client.Dns.GetRecordAsync("example.ton");
var domainInfo = await client.Dns.ResolveAsync("example.ton");
var auctions = await client.Dns.GetAuctionsAsync("ton");

// === Staking категория ===
var pools = await client.Staking.GetPoolsAsync();
var pool = await client.Staking.GetPoolAsync("EQD...pool");
var stakingInfo = await client.Staking.GetAccountInfoAsync("EQD...account");

// === Rates категория ===
var rates = await client.Rates.GetAsync(
    new List<string> { "TON" },
    new List<string> { "USD", "EUR" });

var chartRates = await client.Rates.GetChartAsync("TON", "USD");
var markets = await client.Rates.GetMarketsAsync();

// === Traces категория ===
var trace = await client.Traces.GetAsync("trace-id");

// Эмуляция с трейсом
var emulatedTrace = await client.Traces.EmulateAsync("base64-boc", ignoreSignatureCheck: true);

// Ожидание завершения трейса
var finalizedTrace = await client.Traces.WaitForAsync("trace-id", maxWaitTime: 60);

// === Wallet категория ===
var seqno = await client.Wallet.GetSeqnoAsync("EQD...wallet");

// Эмуляция отправки
var consequences = await client.Wallet.EmulateAsync("base64-boc");
Console.WriteLine($"Ожидаемая комиссия: {consequences.Event.Fee.Total}");

// === Gasless категория ===
var gaslessConfig = await client.Gasless.GetConfigAsync("EQD...jetton");
var gaslessEstimate = await client.Gasless.EstimateAsync(
    "EQD...jetton",
    "EQD...wallet",
    "public-key-hex",
    new List<TonapiClient.Models.SignRawMessage>());

// === Events категория ===
var eventData = await client.Events.GetAsync("event-id");
var completedEvent = await client.Events.WaitForAsync("event-id");

// === LiteServer категория ===
var accountState = await client.LiteServer.GetAccountStateAsync("EQD...account");
var masterchainInfo = await client.LiteServer.GetMasterchainInfoAsync();
var blockchainTime = await client.LiteServer.GetTimeAsync();

// === Storage категория ===
var storageProviders = await client.Storage.GetProvidersAsync();

// === Multisig категория ===
var multisigAccount = await client.Multisig.GetAccountAsync("EQD...multisig");
var multisigOrders = await client.Multisig.GetOrdersAsync("EQD...multisig");

// === Emulation категория ===
var decodedMessage = await client.Emulation.DecodeMessageAsync("base64-boc");

// === Вспомогательные методы ===
var status = await client.GetStatusAsync();
Console.WriteLine($"API Status: {status.RestOnline}");

// Ожидание транзакции - теперь в Account
var confirmedTx = await client.Account.WaitForTransactionAsync(
    "EQD...account",
    "expected-msg-hash",
    maxWaitTime: 120);

// Получение адреса Jetton кошелька
var jettonWalletAddr = await client.GetJettonWalletAddressAsync(
    "EQD...jetton-master",
    "EQD...user-address");

Console.WriteLine("Готово!");

