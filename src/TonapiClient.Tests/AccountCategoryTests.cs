using System.Linq;
using System.Net;
using System.Numerics;
using Ton.Core.Addresses;
using Ton.Crypto.Mnemonic;
using TonapiClient.Models;
using TonapiClient.Tests.V5R1;
using Xunit.Sdk;

namespace TonapiClient.Tests;

/// <summary>
/// Tests for AccountCategory methods using real testnet API.
/// </summary>
public class AccountCategoryTests : TestBase
{
    // Well-known testnet addresses for testing (raw format)
    // These are valid testnet wallet addresses
    private const string TestWalletAddress1 = "0:56e51317a7d2507ef6e334fa96c8a3df88b9ce3f31c21c61897b6aa6ac2fd64f";
    private const string TestWalletAddress2 = "0:8ab7658f197f9f87470801cdf82e377f38a423a19133ebf4bd7e0c757494ff1e";
    private const string TestWalletAddress3 = "0:97264395BD65A255A429B11326C84128B7D70FFED7949ABAE3036D506BA38621";
    private const string TestWalletAddress4 = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";
    private const string DigitalRoubleJettonId = "0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457";
    private const string NftOwnerAddress = "0:21137b0bc47669b3267f1de70cbb0cef5c728b8d8c7890451e8613b2d8998270";
    private const string TonBullrunCollectionAddress = "0:4db8f94ae7fb709a35cf4307154ac73213ae7724637c309b8ff025ab9a5a3fd8";

    [Fact]
    public async Task GetAccount_WithAddress4_ReturnsAccountDetails()
    {
        // Arrange & Act
        var result = await Client.Account.GetAsync(TestWalletAddress4);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Address);
        Assert.NotEmpty(result.Address);
        Assert.Contains(TestWalletAddress4.ToLower(), result.Address.ToLower());

        // Verify required fields
        Assert.NotNull(result.Status);
        Assert.NotEmpty(result.Status);
        Assert.True(result.Balance >= 0);

        // Verify account properties
        Assert.True(result.LastActivity > 0);
    }

    [Fact]
    public async Task GetJettons_WithAddress4_ReturnsJettonBalances()
    {
        // Arrange & Act
        var result = await Client.Account.GetJettonsAsync(TestWalletAddress4);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Balances);

        // Should have jetton balances
        Assert.True(result.Balances.Count >= 2, $"Should return at least 2 jetton balances, but got {result.Balances.Count}");

        // Verify first jetton (Digital Rouble) if exists
        var digitalRouble = result.Balances.FirstOrDefault(b => b.Jetton.Symbol == "dRUB");
        if (digitalRouble != null)
        {
            Assert.NotNull(digitalRouble.Balance);
            Assert.NotEmpty(digitalRouble.Balance);
            Assert.Equal("13010000000200000100", digitalRouble.Balance);

            // Verify jetton info
            Assert.NotNull(digitalRouble.Jetton);
            Assert.Equal("Digital Rouble", digitalRouble.Jetton.Name);
            Assert.Equal("dRUB", digitalRouble.Jetton.Symbol);
            Assert.Equal(18, digitalRouble.Jetton.Decimals);

            // Verify wallet address
            Assert.NotNull(digitalRouble.WalletAddress);
            Assert.NotEmpty(digitalRouble.WalletAddress.Address);
            Assert.False(digitalRouble.WalletAddress.IsScam);
        }

        // Verify second jetton (Aiotx) if exists
        var aiotx = result.Balances.FirstOrDefault(b => b.Jetton.Symbol == "AIOTX");
        if (aiotx != null)
        {
            Assert.NotNull(aiotx.Balance);
            Assert.NotEmpty(aiotx.Balance);
            Assert.Equal("1000000000", aiotx.Balance);

            // Verify jetton info
            Assert.NotNull(aiotx.Jetton);
            Assert.Equal("Aiotx", aiotx.Jetton.Name);
            Assert.Equal("AIOTX", aiotx.Jetton.Symbol);
            Assert.Equal(9, aiotx.Jetton.Decimals);

            // Verify wallet address
            Assert.NotNull(aiotx.WalletAddress);
            Assert.NotEmpty(aiotx.WalletAddress.Address);
        }

        // Verify that all balances have required properties
        foreach (var balance in result.Balances)
        {
            Assert.NotNull(balance.Balance);
            Assert.NotEmpty(balance.Balance);
            Assert.NotNull(balance.Jetton);
            Assert.NotEmpty(balance.Jetton.Symbol);
            Assert.NotNull(balance.WalletAddress);
            Assert.NotEmpty(balance.WalletAddress.Address);
        }
    }

    [Fact]
    public async Task GetJettonBalance_WithAddress4AndDigitalRouble_ReturnsSpecificJettonBalance()
    {
        // Arrange & Act
        var result = await Client.Account.GetJettonBalanceAsync(TestWalletAddress4, DigitalRoubleJettonId);

        // Assert
        Assert.NotNull(result);

        // Verify balance
        Assert.NotNull(result.Balance);
        Assert.NotEmpty(result.Balance);
        Assert.Equal("13010000000200000100", result.Balance);

        // Verify jetton info
        Assert.NotNull(result.Jetton);
        Assert.Equal("0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457", result.Jetton.Address.ToLower());
        Assert.Equal("Digital Rouble", result.Jetton.Name);
        Assert.Equal("dRUB", result.Jetton.Symbol);
        Assert.Equal(18, result.Jetton.Decimals);
        Assert.NotNull(result.Jetton.Image);
        Assert.Contains("cache.tonapi.io", result.Jetton.Image);
        Assert.Equal("none", result.Jetton.Verification);

        // Verify wallet address
        Assert.NotNull(result.WalletAddress);
        Assert.NotEmpty(result.WalletAddress.Address);
        Assert.Equal("0:62fae4ca9c2b02c3bcfa84805f42b12043f0ba60d23af598275986ea5ae3bfd3", result.WalletAddress.Address.ToLower());
        Assert.False(result.WalletAddress.IsScam);
        Assert.False(result.WalletAddress.IsWallet);
    }

    [Fact]
    public async Task GetJettonBalance_WithNonExistentJettonWallet_ReturnsNullOrThrows()
    {
        // Arrange
        // Using TestWalletAddress3 as both account_id and jetton_id (which doesn't make sense, so should fail)
        var accountId = TestWalletAddress3;
        var jettonId = TestWalletAddress3;

        // Act
        JettonBalance? result = null;
        Exception? caughtException = null;

        try
        {
            result = await Client.Account.GetJettonBalanceAsync(accountId, jettonId);
        }
        catch (Exception ex)
        {
            caughtException = ex;
        }

        // Assert
        // API should either return null (404 handled) or throw an exception
        // When account has no jetton wallet, API returns error message
        Assert.True(result == null || caughtException != null,
            "Expected null result or exception when account has no jetton wallet");
    }

    [Fact]
    public async Task GetJettonsHistory_WithAddress4AndLimit2_ReturnsJettonTransferHistory()
    {
        // Arrange & Act
        var result = await Client.Account.GetJettonsHistoryAsync(
            TestWalletAddress4,
            limit: 2);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Operations);

        // Should return exactly 2 operations as per limit
        Assert.Equal(2, result.Operations.Count);

        // Verify first operation
        var firstOperation = result.Operations[0];

        // Verify operation has required properties
        Assert.Equal("transfer", firstOperation.Operation);
        Assert.True(firstOperation.Utime > 0);
        Assert.True(firstOperation.Lt > 0);
        Assert.NotNull(firstOperation.TransactionHash);
        Assert.NotEmpty(firstOperation.TransactionHash);

        // Verify source and destination
        Assert.NotNull(firstOperation.Source);
        Assert.NotEmpty(firstOperation.Source.Address);
        Assert.NotNull(firstOperation.Destination);
        Assert.NotEmpty(firstOperation.Destination.Address);
        Assert.Contains(TestWalletAddress4.ToLower(), firstOperation.Destination.Address.ToLower());

        // Verify amount and jetton
        Assert.NotNull(firstOperation.Amount);
        Assert.NotEmpty(firstOperation.Amount);
        Assert.Equal("1000000000000000000", firstOperation.Amount);

        // Verify jetton info (Digital Rouble)
        Assert.NotNull(firstOperation.Jetton);
        Assert.Equal("0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457", firstOperation.Jetton.Address.ToLower());
        Assert.Equal("Digital Rouble", firstOperation.Jetton.Name);
        Assert.Equal("dRUB", firstOperation.Jetton.Symbol);
        Assert.Equal(18, firstOperation.Jetton.Decimals);

        // Verify trace_id exists
        Assert.NotNull(firstOperation.TraceId);
        Assert.NotEmpty(firstOperation.TraceId);

        // Verify pagination - should have next_from
        Assert.NotNull(result.NextFrom);
        Assert.Equal(41441687000002, result.NextFrom.Value);
    }

    [Fact]
    public async Task GetNfts_WithOwnerAndCollectionAndLimit2_ReturnsNftItems()
    {
        // Arrange & Act
        var result = await Client.Account.GetNftsAsync(
            NftOwnerAddress,
            limit: 2,
            collection: TonBullrunCollectionAddress);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Items);

        // Should return exactly 2 NFT items as per limit
        Assert.Equal(2, result.Items.Count);

        // Verify first NFT item (Bull #20, index 19)
        var firstNft = result.Items[0];

        // Verify basic properties
        Assert.NotNull(firstNft.Address);
        Assert.NotEmpty(firstNft.Address);
        Assert.Equal("0:c1546d73fa3bb896c6012f3381b898b26c1cced6e855a57f1b8586ba63749da6", firstNft.Address.ToLower());
        Assert.Equal(19, firstNft.Index);
        Assert.True(firstNft.Verified);
        Assert.Equal("none", firstNft.Trust);

        // Verify owner
        Assert.NotNull(firstNft.Owner);
        Assert.Contains(NftOwnerAddress.ToLower(), firstNft.Owner.Address.ToLower());
        Assert.Equal("TON Ecosystem Reserve (OLD)", firstNft.Owner.Name);
        Assert.False(firstNft.Owner.IsScam);
        Assert.True(firstNft.Owner.IsWallet);

        // Verify collection
        Assert.NotNull(firstNft.Collection);
        Assert.Equal("0:4db8f94ae7fb709a35cf4307154ac73213ae7724637c309b8ff025ab9a5a3fd8", firstNft.Collection.Address.ToLower());
        Assert.Equal("TON Bullrun", firstNft.Collection.Name);
        Assert.Equal("Any desc", firstNft.Collection.Description);

        // Verify metadata exists
        Assert.NotNull(firstNft.Metadata);
        Assert.True(firstNft.Metadata.ContainsKey("name"));
        Assert.True(firstNft.Metadata.ContainsKey("image"));
        Assert.True(firstNft.Metadata.ContainsKey("description"));

        // Verify previews
        Assert.NotNull(firstNft.Previews);
        Assert.Equal(4, firstNft.Previews.Count);

        var preview100 = firstNft.Previews.FirstOrDefault(p => p.Resolution == "100x100");
        Assert.NotNull(preview100);
        Assert.NotNull(preview100.Url);
        Assert.Contains("cache.tonapi.io", preview100.Url);

        // Verify second NFT item (Bull #10, index 9)
        var secondNft = result.Items[1];
        Assert.NotNull(secondNft.Address);
        Assert.Equal("0:469c35071a221fd3cdcc2368668ece1b284171354c1d57f0fe63415c2405994c", secondNft.Address.ToLower());
        Assert.Equal(9, secondNft.Index);
        Assert.True(secondNft.Verified);

        // Verify approved_by and trust
        Assert.NotNull(firstNft.ApprovedBy);
        Assert.Empty(firstNft.ApprovedBy);
    }

    [Fact]
    public async Task GetEvents_WithAddress4AndLimit20_ReturnsAccountEvents()
    {
        // Arrange & Act
        var result = await Client.Account.GetEventsAsync(
            TestWalletAddress4,
            limit: 20,
            initiator: false,
            subjectOnly: false);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Events);

        // Should return events (may be less than 20)
        Assert.True(result.Events.Count > 0, "Should return at least one event");
        Assert.True(result.Events.Count <= 20, $"Should return at most 20 events, but got {result.Events.Count}");

        // Verify first event (should be a JettonTransfer event)
        var firstEvent = result.Events[0];

        // Verify basic event properties
        Assert.NotNull(firstEvent.EventId);
        Assert.NotEmpty(firstEvent.EventId);
        Assert.Equal("3eaf04d28bb18a8eb2ba2e0f32e299be320293c610a8974d04189b1b3ed986f7", firstEvent.EventId);

        // Verify account
        Assert.NotNull(firstEvent.Account);
        Assert.Contains(TestWalletAddress4.ToLower(), firstEvent.Account.Address.ToLower());
        Assert.False(firstEvent.Account.IsScam);
        Assert.True(firstEvent.Account.IsWallet);

        // Verify timestamp and lt
        Assert.Equal(1763999429, firstEvent.Timestamp);
        Assert.Equal(41673840000001, firstEvent.Lt);
        Assert.False(firstEvent.InProgress);
        Assert.False(firstEvent.IsScam);
        Assert.Equal(-2055, firstEvent.Extra);
        Assert.Equal(1, firstEvent.Progress);

        // Verify actions
        Assert.NotNull(firstEvent.Actions);
        Assert.Single(firstEvent.Actions);

        var action = firstEvent.Actions[0];
        Assert.Equal("JettonTransfer", action.Type);
        Assert.Equal("ok", action.Status);

        // Verify JettonTransfer details
        Assert.NotNull(action.JettonTransfer);
        var transfer = action.JettonTransfer;

        Assert.NotNull(transfer.Sender);
        Assert.Equal("0:ffdb1d0f6eff05ff9cbb9ac726ce4480be6ac5a0d11b4a09c2b52d9c268f1aa2", transfer.Sender.Address.ToLower());

        Assert.NotNull(transfer.Recipient);
        Assert.Contains(TestWalletAddress4.ToLower(), transfer.Recipient.Address.ToLower());

        Assert.Equal("0:89ed1c8de8f11e1c0d04ed0623156b08518eea67d89d63097eef3226f1855d02", transfer.SendersWallet.ToLower());
        Assert.Equal("0:62fae4ca9c2b02c3bcfa84805f42b12043f0ba60d23af598275986ea5ae3bfd3", transfer.RecipientsWallet.ToLower());
        Assert.Equal("1000000000000000000", transfer.Amount);
        Assert.Equal("Test jetton transfer 1", transfer.Comment);

        // Verify jetton info
        Assert.NotNull(transfer.Jetton);
        Assert.Equal("Digital Rouble", transfer.Jetton.Name);
        Assert.Equal("dRUB", transfer.Jetton.Symbol);
        Assert.Equal(18, transfer.Jetton.Decimals);

        // Verify simple preview
        Assert.NotNull(action.SimplePreview);
        Assert.Equal("Jetton Transfer", action.SimplePreview.Name);
        Assert.Contains("dRUB", action.SimplePreview.Value);

        // Verify pagination
        Assert.NotNull(result.NextFrom);
        Assert.Equal(41412472000001, result.NextFrom.Value);

        // Verify we have both JettonTransfer and TonTransfer events in the list
        var hasJettonTransfer = result.Events.Any(e =>
            e.Actions.Any(a => a.Type == "JettonTransfer"));
        var hasTonTransfer = result.Events.Any(e =>
            e.Actions.Any(a => a.Type == "TonTransfer"));

        Assert.True(hasJettonTransfer, "Should have at least one JettonTransfer event");
        Assert.True(hasTonTransfer, "Should have at least one TonTransfer event");
    }

    [Fact]
    public async Task GetEventById_WithAddress4AndEventId_ReturnsSpecificEvent()
    {
        // Arrange
        var accountId = TestWalletAddress4;
        var eventId = "3eaf04d28bb18a8eb2ba2e0f32e299be320293c610a8974d04189b1b3ed986f7";

        // Act
        var result = await Client.Account.GetEventByIdAsync(accountId, eventId, subjectOnly: false);

        // Assert
        Assert.NotNull(result);

        // Verify event ID
        Assert.Equal(eventId, result.EventId);

        // Verify account
        Assert.NotNull(result.Account);
        Assert.Contains(TestWalletAddress4.ToLower(), result.Account.Address.ToLower());
        Assert.False(result.Account.IsScam);
        Assert.True(result.Account.IsWallet);

        // Verify timestamp and lt
        Assert.Equal(1763999429, result.Timestamp);
        Assert.Equal(41673840000001, result.Lt);
        Assert.False(result.InProgress);
        Assert.False(result.IsScam);
        Assert.Equal(-2055, result.Extra);
        Assert.Equal(1, result.Progress);

        // Verify actions
        Assert.NotNull(result.Actions);
        Assert.Single(result.Actions);

        var action = result.Actions[0];
        Assert.Equal("JettonTransfer", action.Type);
        Assert.Equal("ok", action.Status);

        // Verify JettonTransfer details
        Assert.NotNull(action.JettonTransfer);
        var transfer = action.JettonTransfer;

        Assert.NotNull(transfer.Sender);
        Assert.Equal("0:ffdb1d0f6eff05ff9cbb9ac726ce4480be6ac5a0d11b4a09c2b52d9c268f1aa2", transfer.Sender.Address.ToLower());
        Assert.False(transfer.Sender.IsScam);
        Assert.True(transfer.Sender.IsWallet);

        Assert.NotNull(transfer.Recipient);
        Assert.Contains(TestWalletAddress4.ToLower(), transfer.Recipient.Address.ToLower());
        Assert.False(transfer.Recipient.IsScam);
        Assert.True(transfer.Recipient.IsWallet);

        Assert.Equal("0:89ed1c8de8f11e1c0d04ed0623156b08518eea67d89d63097eef3226f1855d02", transfer.SendersWallet.ToLower());
        Assert.Equal("0:62fae4ca9c2b02c3bcfa84805f42b12043f0ba60d23af598275986ea5ae3bfd3", transfer.RecipientsWallet.ToLower());
        Assert.Equal("1000000000000000000", transfer.Amount);
        Assert.Equal("Test jetton transfer 1", transfer.Comment);

        // Verify jetton info
        Assert.NotNull(transfer.Jetton);
        Assert.Equal("0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457", transfer.Jetton.Address.ToLower());
        Assert.Equal("Digital Rouble", transfer.Jetton.Name);
        Assert.Equal("dRUB", transfer.Jetton.Symbol);
        Assert.Equal(18, transfer.Jetton.Decimals);
        Assert.NotNull(transfer.Jetton.Image);
        Assert.Contains("cache.tonapi.io", transfer.Jetton.Image);
        Assert.Equal("none", transfer.Jetton.Verification);
        Assert.Equal(0, transfer.Jetton.Score);

        // Verify simple preview
        Assert.NotNull(action.SimplePreview);
        Assert.Equal("Jetton Transfer", action.SimplePreview.Name);
        Assert.Contains("dRUB", action.SimplePreview.Value);
        Assert.Equal("1 dRUB", action.SimplePreview.Value);
        Assert.NotNull(action.SimplePreview.ValueImage);
        Assert.Contains("cache.tonapi.io", action.SimplePreview.ValueImage);

        // Verify simple preview accounts
        Assert.NotNull(action.SimplePreview.Accounts);
        Assert.Equal(2, action.SimplePreview.Accounts.Count);

        var firstAccount = action.SimplePreview.Accounts[0];
        Assert.Equal("0:ffdb1d0f6eff05ff9cbb9ac726ce4480be6ac5a0d11b4a09c2b52d9c268f1aa2", firstAccount.Address.ToLower());
        Assert.False(firstAccount.IsScam);
        Assert.True(firstAccount.IsWallet);

        var secondAccount = action.SimplePreview.Accounts[1];
        Assert.Equal("0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457", secondAccount.Address.ToLower());
        Assert.False(secondAccount.IsScam);
        Assert.False(secondAccount.IsWallet);

        // Verify base transactions
        Assert.NotNull(action.BaseTransactions);
        Assert.Equal(4, action.BaseTransactions.Count);
        Assert.Contains("96a3bb2c1079de2096ffe46f1ecbe3b0a77d9ef1ecbec76ab2fd431df33173f1", action.BaseTransactions);
        Assert.Contains("99426d05017ef15a4781229379bc47521f5fb124961574c08a8c498144b899e5", action.BaseTransactions);
        Assert.Contains("bb0cb3e103a6fe9dc31b5bfaa0eaeaa841cc4aa0b4f47905ad9f272e470da3f0", action.BaseTransactions);
        Assert.Contains("76b46a96762bebf2671735937abdb0af824bdb23a2978267395c3bbf5d13b006", action.BaseTransactions);
    }

    [Fact]
    public async Task GetTraces_WithAddress4AndLimit2_ReturnsTraceIds()
    {
        // Arrange & Act
        var result = await Client.Account.GetTracesAsync(TestWalletAddress4, limit: 2);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Traces);

        // Should return exactly 2 traces as per limit
        Assert.Equal(2, result.Traces.Count);

        // Verify first trace
        var firstTrace = result.Traces[0];
        Assert.NotNull(firstTrace);
        Assert.NotEmpty(firstTrace.Id);
        Assert.Equal("3eaf04d28bb18a8eb2ba2e0f32e299be320293c610a8974d04189b1b3ed986f7", firstTrace.Id);
        Assert.Equal(41673840000001, firstTrace.Utime);

        // Verify second trace
        var secondTrace = result.Traces[1];
        Assert.NotNull(secondTrace);
        Assert.NotEmpty(secondTrace.Id);
        Assert.Equal("564ca1cc498d07c48c81741dea274ab92f25c0fdee5578cc0b31f9e8ac1e8897", secondTrace.Id);
        Assert.Equal(41670292000001, secondTrace.Utime);
    }

    [Fact]
    public async Task GetPublicKey_WithAddress4_ReturnsPublicKey()
    {
        // Arrange & Act
        var result = await Client.Account.GetPublicKeyAsync(TestWalletAddress4);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.PublicKeyHex);
        Assert.NotEmpty(result.PublicKeyHex);

        // Verify the exact public key value
        Assert.Equal("47ed240fb7f1c61b2f2e8113693d209ed4cd2644c73c70bac1f1464e108b34b2", result.PublicKeyHex);

        // Verify it's a valid hex string (64 characters for 32 bytes)
        Assert.Equal(64, result.PublicKeyHex.Length);
        Assert.Matches("^[0-9a-fA-F]{64}$", result.PublicKeyHex);
    }

    [Fact]
    public async Task GetBulkAsync_WithRealAddresses_ReturnsAccountsSuccessfully()
    {
        // Arrange
        var accountIds = new List<string>
        {
            TestWalletAddress1,
            TestWalletAddress2,
            TestWalletAddress3,
            TestWalletAddress4
        };

        // Act
        var result = await Client.Account.GetBulkAsync(accountIds);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.AccountsList);

        // Should return data for both addresses
        Assert.True(result.AccountsList.Count >= 1, "Should return at least one account");

        // Verify each account has required properties
        foreach (var account in result.AccountsList)
        {
            Assert.NotNull(account.Address);
            Assert.NotEmpty(account.Address);
            Assert.NotNull(account.Status);
            Assert.NotEmpty(account.Status);
        }
    }

    [Fact]
    public async Task GetBulkAsync_WithSingleAddress_ReturnsOneAccount()
    {
        // Arrange
        var accountIds = new List<string> { TestWalletAddress4 };

        // Act
        var result = await Client.Account.GetBulkAsync(accountIds);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.AccountsList);
        Assert.Single(result.AccountsList);

        var account = result.AccountsList[0];
        Assert.NotNull(account.Address);
        Assert.NotEmpty(account.Status);
        Assert.Equal("active", account!.Status);
        Assert.True(account!.IsWallet);
        Assert.True(account.Balance >= 0);
    }

    [Fact]
    public async Task GetAsync_WithValidAddress_ReturnsAccountInfo()
    {
        // Arrange & Act
        var result = await Client.Account.GetAsync(TestWalletAddress1);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Address);
        Assert.NotEmpty(result.Address);
        Assert.NotNull(result.Status);
        Assert.NotEmpty(result.Status);

        // Balance should be a valid number (can be 0)
        Assert.True(result.Balance >= 0);
    }

    [Fact]
    public async Task GetAsync_WithInvalidAddress_ThrowsException()
    {
        // Arrange
        var invalidAddress = "invalid-address-format";

        // Act & Assert
        await Assert.ThrowsAsync<TonApiException>(async () =>
            await Client.Account.GetAsync(invalidAddress));
    }

    [Fact]
    public async Task GetTonBalance_WithValidAddress_ReturnsPositiveBalance()
    {
        // Arrange
        var userAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";

        // Act
        var balance = await Client.Account.GetTonBalanceAsync(userAddress);

        // Assert
        Assert.True(balance > 0, $"Expected balance to be greater than 0, but got {balance}");
    }

    [Fact]
    public async Task GetJettonBalance_WithValidAddressAndJettonName_ReturnsPositiveBalance()
    {
        // Arrange
        var userAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";
        var jettonName = "Digital Rouble";

        // Act
        var balances = await Client.Account.GetJettonsAsync(userAddress);
        var drubBalance = balances.Balances.FirstOrDefault(x => x.Jetton.Name == jettonName);

        // Assert
        Assert.NotNull(balances);
        Assert.NotNull(balances.Balances);
        Assert.NotNull(drubBalance);
        Assert.True(BigInteger.Parse(drubBalance.Balance) > BigInteger.Zero,
            $"Expected balance to be greater than 0, but got {drubBalance.Balance}");
        Assert.Equal(jettonName, drubBalance.Jetton.Name);
    }

    [Fact]
    public async Task GetJettonBalance_WithValidAddressAndJettonName_ReturnsBalance()
    {
        // Arrange
        var userAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";
        var jettonName = "Digital Rouble";

        // Act
        var drubBalance = await Client.Account.GetJettonBalance(userAddress, jettonName);

        // Assert
        Assert.NotNull(drubBalance);
        Assert.True(BigInteger.Parse(drubBalance.Balance) > BigInteger.Zero,
            $"Expected balance to be greater than 0, but got {drubBalance.Balance}");
        Assert.Equal(jettonName, drubBalance.Jetton.Name);
    }

    [Fact]
    public async Task GetJettonBalance_WithValidAddressAndMasterAddress_ReturnsBalance()
    {
        // Arrange
        var userAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";
        var jettonMasterAddress = "0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457";

        // Act
        var balance = await Client.Account.GetJettonBalance(userAddress, jettonMasterAddress);

        // Assert
        Assert.NotNull(balance);
        Assert.True(System.Numerics.BigInteger.Parse(balance.Balance) > System.Numerics.BigInteger.Zero,
            $"Expected balance to be greater than 0, but got {balance.Balance}");
        Assert.Equal(jettonMasterAddress.ToLower(), balance.Jetton.Address.ToLower());
    }

    [Fact]
    public async Task GetAccountPublicKeyAndRecoverAddressTests()
    {
        // Arrange
        var expectedAddress = "0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e";
        var keys = Ton.Crypto.Mnemonic.Mnemonic.ToWalletKey(Mnemonic.Split(" "));
        var walletEthalon = WalletV5R1.Create(0, keys.PublicKey, false);

        // Act
        var publicKeyResult = await Client.Account.GetPublicKeyAsync(expectedAddress);
        var walletReal = WalletV5R1.Create(0, publicKeyResult.PublicKeyHex.HexToBytes(), false);
        var publicKey = publicKeyResult.PublicKeyHex.HexToBytes();
        // Create wallet from the public key
        var wallet = WalletV5R1.Create(0, publicKey, false);
        var recoveredAddress = wallet.Address.ToString();

        // Assert
        Assert.NotNull(publicKeyResult);
        Assert.NotNull(publicKeyResult.PublicKeyHex);
        Assert.Equal(keys.PublicKey.ToHex().ToUpper(), publicKeyResult.PublicKeyHex.ToUpper());
        Assert.Equal(walletEthalon.Address, walletReal.Address);

        // Convert hex public key to bytes
        Assert.Equal(32, publicKey.Length); // Ed25519 public key is always 32 bytes
        // The recovered address should match the original address
        var hexHash = wallet.Address.Hash.ToHex().ToLower();
        Assert.Equal(expectedAddress, $"{wallet.Address.Workchain}:{hexHash}");
    }
}

