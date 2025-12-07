# TonapiClient

C# .NET клиент для работы с TON API (https://tonapi.io).

## Установка

```bash
dotnet add package TonapiClient
```

## Настройка тестов

Для запуска тестов необходимо создать конфигурационный файл:

1. Скопируйте `TonapiClient.Tests/appsettings.example.json` в `TonapiClient.Tests/appsettings.json`
2. Замените `YOUR_API_KEY_HERE` на ваш API ключ от [TON API](https://tonapi.io)
3. Для тестов рекомендуется использовать testnet API: `https://testnet.tonapi.io`

```json
{
  "TonApiClient": {
    "BaseUrl": "https://testnet.tonapi.io",
    "ApiKey": "YOUR_API_KEY_HERE",
    "TimeoutSeconds": 30
  }
}
```

Запуск тестов:
```bash
dotnet test
```

**Примечание**: Файл `appsettings.json` включен в `.gitignore` и не должен коммититься в репозиторий.

## Использование

### Базовое использование

```csharp
using TonapiClient;

var client = new TonApiClient("https://tonapi.io", "your-api-key");

// Получить информацию о блокчейне
var masterchainHead = await client.Blockchain.GetMasterchainHeadAsync();
Console.WriteLine($"Last block: {masterchainHead.Last.Seqno}");

// Получить информацию об аккаунте
var account = await client.Account.GetAsync("EQD...address");
Console.WriteLine($"Balance: {account.Balance}");

// Получить транзакции аккаунта
var transactions = await client.Account.GetTransactionsAsync("EQD...address", limit: 10);
foreach (var tx in transactions.Transactions)
{
    Console.WriteLine($"TX: {tx.Hash}");
}

// Получить информацию о Jetton
var jetton = await client.Jetton.GetAsync("EQD...jetton-address");
Console.WriteLine($"Jetton: {jetton.Metadata.Name}");

// Получить NFT коллекцию
var nft = await client.Nft.GetCollectionAsync("EQD...collection-address");
Console.WriteLine($"NFT Collection: {nft.Metadata.Name}");
```

### Dependency Injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using TonapiClient;

var services = new ServiceCollection();

// Из конфигурации
services.AddTonApiClient(configuration);

// Или с явными параметрами
services.AddTonApiClient(options =>
{
    options.BaseUrl = "https://tonapi.io";
    options.ApiKey = "your-api-key";
    options.TimeoutSeconds = 30;
});

var serviceProvider = services.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<TonApiClient>();

// Используйте client...
```

## API Категории

Клиент организован по категориям для удобства использования:

### `Blockchain` - Методы блокчейна
- `GetBlockAsync()` - Получить данные блока
- `GetMasterchainHeadAsync()` - Получить последний блок masterchain
- `GetMasterchainBlocksAsync()` - Получить блоки между masterchain блоками
- `GetTransactionAsync()` - Получить транзакцию по хешу
- `SendBocAsync()` - Отправить BOC сообщение
- `GetValidatorsAsync()` - Получить список валидаторов

### `Account` - Методы аккаунтов
- `GetAsync()` - Получить информацию об аккаунте
- `GetTransactionsAsync()` - Получить транзакции аккаунта
- `GetEventsAsync()` - Получить события аккаунта
- `GetJettonsAsync()` - Получить Jetton балансы
- `GetNftsAsync()` - Получить NFT аккаунта
- `ExecuteGetMethodAsync()` - Выполнить get-метод смарт-контракта
- `WaitForTransactionAsync()` - Ждать появления транзакции

### `Jetton` - Методы Jetton токенов
- `GetAsync()` - Получить информацию о Jetton
- `GetAllAsync()` - Получить список всех Jettons
- `GetHoldersAsync()` - Получить держателей Jetton

### `Nft` - Методы NFT
- `GetItemAsync()` - Получить NFT по адресу
- `GetCollectionAsync()` - Получить коллекцию NFT
- `GetCollectionsAsync()` - Получить список коллекций
- `GetCollectionItemsAsync()` - Получить NFT из коллекции
- `GetItemHistoryAsync()` - Получить историю NFT

### `Dns` - Методы DNS
- `GetRecordAsync()` - Получить DNS запись
- `ResolveAsync()` - Разрешить доменное имя
- `GetAuctionsAsync()` - Получить аукционы DNS
- `GetBidsAsync()` - Получить ставки для домена

### `Staking` - Методы стейкинга
- `GetPoolsAsync()` - Получить список пулов стейкинга
- `GetPoolAsync()` - Получить информацию о пуле
- `GetAccountInfoAsync()` - Получить информацию о стейкинге аккаунта

### `Rates` - Курсы валют
- `GetAsync()` - Получить текущие курсы токенов
- `GetChartAsync()` - Получить исторические данные курсов
- `GetMarketsAsync()` - Получить курсы TON с разных бирж

### `Traces` - Трейсы транзакций
- `GetAsync()` - Получить трейс по ID
- `WaitForAsync()` - Ждать завершения трейса
- `EmulateAsync()` - Эмулировать сообщение и получить трейс

### `Wallet` - Методы кошелька
- `GetSeqnoAsync()` - Получить seqno кошелька
- `EmulateAsync()` - Эмулировать отправку сообщения

### `Gasless` - Gasless транзакции
- `GetConfigAsync()` - Получить конфигурацию gasless
- `EstimateAsync()` - Оценить комиссию gasless транзакции
- `SendAsync()` - Отправить gasless транзакцию

### `Events` - События
- `GetAsync()` - Получить событие по ID
- `WaitForAsync()` - Ждать завершения события

### `LiteServer` - Методы Lite Server
- `GetAccountStateAsync()` - Получить raw состояние аккаунта
- `GetMasterchainInfoAsync()` - Получить информацию о masterchain
- `GetTimeAsync()` - Получить время блокчейна
- `GetBlockAsync()` - Получить raw блок
- `GetTransactionsAsync()` - Получить raw транзакции

### `Storage` - TON Storage
- `GetProvidersAsync()` - Получить список провайдеров хранилища

### `Multisig` - Multisig кошельки
- `GetAccountAsync()` - Получить информацию о multisig аккаунте
- `GetOrdersAsync()` - Получить ордера (предложения) multisig

### `Emulation` - Эмуляция
- `DecodeMessageAsync()` - Декодировать сообщение

## Примеры

### Отправка транзакции

```csharp
// Эмулировать перед отправкой
var consequences = await client.Wallet.EmulateAsync(bocMessage);
Console.WriteLine($"Estimated fee: {consequences.Event.Fee.Total}");

// Отправить транзакцию
var response = await client.Blockchain.SendBocAsync(bocMessage);
Console.WriteLine($"Message hash: {response.Hash}");

// Подождать транзакцию
var transaction = await client.Account.WaitForTransactionAsync(
    accountAddress, 
    response.Hash,
    maxWaitTime: 60);

if (transaction != null)
{
    Console.WriteLine($"Transaction confirmed: {transaction.Hash}");
}
```

### Работа с Jettons

```csharp
// Получить адрес Jetton кошелька пользователя
var jettonWalletAddress = await client.GetJettonWalletAddressAsync(
    jettonMasterAddress,
    userAddress);

// Получить информацию о Jetton
var jetton = await client.Jetton.GetAsync(jettonMasterAddress);
Console.WriteLine($"{jetton.Metadata.Name} ({jetton.Metadata.Symbol})");

// Получить держателей
var holders = await client.Jetton.GetHoldersAsync(jettonMasterAddress, limit: 100);
Console.WriteLine($"Total holders: {holders.Total}");
```

### Работа с NFT

```csharp
// Получить NFT коллекцию
var collection = await client.Nft.GetCollectionAsync(collectionAddress);
Console.WriteLine($"Collection: {collection.Metadata.Name}");

// Получить NFT из коллекции
var items = await client.Nft.GetCollectionItemsAsync(collectionAddress, limit: 10);
foreach (var item in items.NftItems)
{
    Console.WriteLine($"NFT: {item.Metadata.Name}");
}

// Получить историю NFT
var history = await client.Nft.GetItemHistoryAsync(nftAddress);
foreach (var event in history.Events)
{
    Console.WriteLine($"Event: {event.EventId}");
}
```

## Лицензия

MIT

