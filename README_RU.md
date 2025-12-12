# TonapiClient

[![NuGet](https://img.shields.io/nuget/v/TonapiClient.svg)](https://www.nuget.org/packages/TonapiClient/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)

C# .NET клиент для работы с [TON API](https://tonapi.io).

**Поддерживаемые платформы:** .NET 10.0+

[English version](README.md) | Русская версия

## Установка

```bash
dotnet add package TonapiClient
```

## Возможности

✅ Полное покрытие TON API v2
✅ Типобезопасные модели для всех эндпоинтов
✅ Поддержка Dependency Injection и HttpClientFactory
✅ Retry логика и обработка ошибок
✅ Поддержка CancellationToken
✅ Асинхронное ожидание транзакций с exponential backoff
✅ Низкоуровневый доступ через LiteServer API
✅ Поддержка testnet и mainnet

## Настройка тестов

Для запуска тестов необходимо создать конфигурационный файл:

1. Скопируйте `src/TonapiClient.Tests/appsettings.example.json` в `src/TonapiClient.Tests/appsettings.json`
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

### Dependency Injection (рекомендуемый подход)

Клиент использует `HttpClientFactory` для управления HTTP-соединениями и поддерживает полную интеграцию с DI:

```csharp
using Microsoft.Extensions.DependencyInjection;
using TonapiClient;

var services = new ServiceCollection();

// Из конфигурации (appsettings.json)
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

В ASP.NET Core добавьте в `Program.cs`:

```csharp
builder.Services.AddTonApiClient(builder.Configuration);
```

И в `appsettings.json`:

```json
{
  "TonApiClient": {
    "BaseUrl": "https://tonapi.io",
    "ApiKey": "your-api-key",
    "TimeoutSeconds": 30
  }
}
```

## Архитектура

Клиент построен на основе категорий (categories), каждая из которых инкапсулирует логику работы с определённой областью TON API:

- 🔗 **CategoryBase** - базовый класс для всех категорий с общими HTTP-методами
- 📦 **TonApiClient** - главный фасад, предоставляющий доступ ко всем категориям через свойства
- 🔌 **HttpClientFactory** - используется для эффективного управления HTTP-соединениями
- ⚙️ **IOptions** - конфигурация через стандартный механизм .NET

Все категории доступны как свойства клиента: `client.Blockchain`, `client.Account`, `client.Jetton` и т.д.

## API Категории

Клиент организован по категориям для удобства использования:

### `Blockchain` - Методы блокчейна

- `GetBlockAsync()` - Получить данные блока
- `GetReducedBlocksAsync()` - Получить сокращённые данные блоков в диапазоне времени
- `GetMasterchainShardsAsync()` - Получить шарды блока masterchain
- `GetMasterchainHeadAsync()` - Получить последний блок masterchain
- `GetMasterchainBlocksAsync()` - Получить блоки между masterchain блоками
- `GetMasterchainTransactionsAsync()` - Получить транзакции между masterchain блоками
- `GetBlockTransactionsAsync()` - Получить транзакции конкретного блока
- `GetConfigAsync()` - Получить конфигурацию блокчейна по seqno
- `GetCurrentConfigAsync()` - Получить текущую конфигурацию блокчейна
- `GetRawConfigAsync()` - Получить raw конфигурацию блокчейна
- `GetTransactionAsync()` - Получить транзакцию по хешу
- `GetTransactionByMessageHashAsync()` - Получить транзакцию по хешу сообщения
- `SendBocAsync()` - Отправить BOC сообщение
- `WaitForTransactionAsync()` - Ждать появления транзакции с exponential backoff
- `GetValidatorsAsync()` - Получить список валидаторов
- `GetAccountAsync()` - Получить raw данные аккаунта
- `GetAccountTransactionsAsync()` - Получить транзакции аккаунта
- `ExecuteGetMethodAsync()` - Выполнить GET-метод на аккаунте
- `ExecuteMethodAsync()` - Выполнить POST-метод на аккаунте
- `InspectAccountAsync()` - Инспектировать аккаунт

### `Account` - Методы аккаунтов

- `GetAsync()` - Получить информацию об аккаунте
- `GetTonBalanceAsync()` - Получить TON баланс аккаунта
- `GetBulkAsync()` - Получить информацию о нескольких аккаунтах
- `GetTransactionsAsync()` - Получить транзакции аккаунта
- `GetEventsAsync()` - Получить события аккаунта
- `GetEventByIdAsync()` - Получить конкретное событие по ID
- `GetTracesAsync()` - Получить трейсы аккаунта (lightweight идентификаторы)
- `GetJettonsAsync()` - Получить Jetton балансы аккаунта
- `GetJettonBalanceAsync()` - Получить баланс конкретного Jetton по адресу master
- `GetJettonBalance()` - Получить баланс Jetton по имени или адресу
- `GetJettonsHistoryAsync()` - Получить историю Jetton переводов
- `GetNftsAsync()` - Получить NFT аккаунта
- `GetNftsHistoryAsync()` - Получить историю NFT переводов
- `GetDnsBackresolveAsync()` - Получить доменные имена аккаунта через DNS backresolve
- `GetSubscriptionsAsync()` - Получить подписки аккаунта
- `GetPublicKeyAsync()` - Получить публичный ключ аккаунта
- `GetDiffAsync()` - Получить изменение баланса между двумя метками времени
- `ExecuteGetMethodAsync()` - Выполнить GET-метод смарт-контракта
- `InspectAsync()` - Инспектировать контракт аккаунта
- `WaitForTransactionAsync()` - Ждать появления транзакции с exponential backoff

### `Jetton` - Методы Jetton токенов

- `GetAsync()` - Получить информацию о Jetton
- `GetAllAsync()` - Получить список всех Jettons
- `GetBulkAsync()` - Получить метаданные нескольких Jettons по адресам
- `GetHoldersAsync()` - Получить держателей Jetton
- `GetEventJettonsAsync()` - Получить событие с информацией о Jetton трансфере
- `GetJettonWalletAddressAsync()` - Получить адрес Jetton кошелька пользователя

### `Nft` - Методы NFT

- `GetItemAsync()` - Получить NFT по адресу
- `GetItemsBulkAsync()` - Получить несколько NFT по адресам
- `GetItemHistoryAsync()` - Получить историю NFT
- `GetCollectionAsync()` - Получить коллекцию NFT
- `GetCollectionsAsync()` - Получить список коллекций
- `GetCollectionsBulkAsync()` - Получить несколько коллекций по адресам
- `GetCollectionItemsAsync()` - Получить NFT из коллекции

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

- `GetAsync()` - Получить информацию о кошельке
- `GetSeqnoAsync()` - Получить seqno кошелька
- `GetWalletsByPublicKeyAsync()` - Получить кошельки по публичному ключу
- `EmulateAsync()` - Эмулировать отправку сообщения

### `Gasless` - Gasless транзакции

- `GetConfigAsync()` - Получить конфигурацию gasless
- `EstimateAsync()` - Оценить комиссию gasless транзакции
- `SendAsync()` - Отправить gasless транзакцию

### `Events` - События

- `GetAsync()` - Получить событие по ID
- `WaitForAsync()` - Ждать завершения события

### `LiteServer` - Методы Lite Server (низкоуровневый доступ)

- `GetMasterchainInfoAsync()` - Получить информацию о masterchain
- `GetMasterchainInfoExtAsync()` - Получить расширенную информацию о masterchain
- `GetTimeAsync()` - Получить время блокчейна
- `GetBlockAsync()` - Получить raw блок по ID
- `GetBlockHeaderAsync()` - Получить заголовок raw блока
- `GetAccountStateAsync()` - Получить raw состояние аккаунта
- `GetShardInfoAsync()` - Получить информацию о shard
- `GetAllShardsInfoAsync()` - Получить информацию о всех shards
- `GetTransactionsAsync()` - Получить raw транзакции аккаунта
- `GetListBlockTransactionsAsync()` - Получить список транзакций из блока
- `SendMessageAsync()` - Отправить raw сообщение в блокчейн
- `GetBlockProofAsync()` - Получить raw block proof
- `GetShardBlockProofAsync()` - Получить raw shard block proof
- `GetConfigAsync()` - Получить raw конфигурационные параметры
- `GetOutMsgQueueSizesAsync()` - Получить размеры очередей исходящих сообщений

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

### Выполнение get-методов смарт-контракта

```csharp
// Инспектировать аккаунт для получения списка доступных методов
var inspection = await client.Blockchain.InspectAccountAsync(contractAddress);
foreach (var method in inspection.Methods)
{
    Console.WriteLine($"Method: {method.Name}");
}

// Выполнить GET-метод (параметры в query string)
var result = await client.Blockchain.ExecuteGetMethodAsync(
    contractAddress,
    "get_wallet_address",
    new List<string> { "0:..." });

// Выполнить POST-метод (параметры в теле запроса)
var postResult = await client.Blockchain.ExecuteMethodAsync(
    contractAddress,
    "get_wallet_address",
    new List<MethodArg>
    {
        new() { Type = "slice", Value = "b5ee9c72..." }
    });

Console.WriteLine($"Success: {postResult.Success}");
```

### Работа с LiteServer (низкоуровневый доступ)

```csharp
// Получить информацию о masterchain
var info = await client.LiteServer.GetMasterchainInfoAsync();
Console.WriteLine($"Last block seqno: {info.Last.Seqno}");

// Получить время блокчейна
var time = await client.LiteServer.GetTimeAsync();
Console.WriteLine($"Blockchain time: {DateTimeOffset.FromUnixTimeSeconds(time.Time)}");

// Получить raw блок
var blockId = $"({info.Last.Workchain},{info.Last.Shard},{info.Last.Seqno},{info.Last.RootHash},{info.Last.FileHash})";
var block = await client.LiteServer.GetBlockAsync(blockId);
Console.WriteLine($"Block data length: {block.Data.Length}");

// Получить raw состояние аккаунта
var accountState = await client.LiteServer.GetAccountStateAsync(
    "0:...",
    targetBlock: blockId);
Console.WriteLine($"Account balance: {accountState.Balance}");

// Получить raw транзакции
var rawTxs = await client.LiteServer.GetTransactionsAsync(
    "0:...",
    count: 10);
Console.WriteLine($"Transactions: {rawTxs.Transactions.Count}");
```

## Структура проекта

```
TonapiClient/
├── src/
│   ├── TonapiClient/              # Основная библиотека
│   │   ├── Categories/            # API категории
│   │   │   ├── AccountCategory.cs
│   │   │   ├── BlockchainCategory.cs
│   │   │   ├── JettonCategory.cs
│   │   │   ├── NftCategory.cs
│   │   │   ├── LiteServerCategory.cs
│   │   │   └── ...
│   │   ├── Models/                # Модели данных
│   │   ├── TonApiClient.cs        # Главный класс клиента
│   │   ├── TonApiClientOptions.cs # Опции конфигурации
│   │   └── TonApiException.cs     # Исключения
│   └── TonapiClient.Tests/        # Интеграционные тесты
│       ├── AccountCategoryTests.cs
│       ├── BlockchainCategoryTests.cs
│       ├── LiteServerCategoryTests.cs
│       └── ...
├── .github/
│   └── workflows/
│       └── ci-cd.yml              # CI/CD конфигурация
└── README.md
```

## Требования для разработки

- .NET 10.0 SDK
- API ключ от [TON API](https://tonapi.io) (для тестов)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Полезные ссылки

- [TON API Documentation](https://docs.tonconsole.com/tonapi/api-v2)
- [TON API Console](https://tonapi.io)
- [TON Documentation](https://docs.ton.org)

## Donations

UQAN9eHzTT6ntU0LSIcqwLJz9GdzeUAOjeXr0x8_XWO0W9S5

## Лицензия

MIT
