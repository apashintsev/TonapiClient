using Xunit;
using TonapiClient.Models;

namespace TonapiClient.Tests;

public class JettonCategoryTests : TestBase
{
    [Fact]
    public async Task GetAll_WithLimit3AndOffset0_ReturnsJettons()
    {
        // Arrange
        var limit = 3;
        var offset = 0;

        // Act
        var result = await Client.Jetton.GetAllAsync(limit, offset);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.JettonsList);
        
        // Should return jettons (may be less than limit if no more data)
        Assert.True(result.JettonsList.Count > 0, "Should return at least one jetton");
        Assert.True(result.JettonsList.Count <= limit, $"Should return at most {limit} jettons, but got {result.JettonsList.Count}");
        
        // Verify first jetton (MyJETTON)
        var firstJetton = result.JettonsList[0];
        Assert.NotNull(firstJetton);
        
        // Verify mintable and total_supply
        Assert.True(firstJetton.Mintable);
        Assert.Equal("10000000000000", firstJetton.TotalSupply);
        
        // Verify metadata
        Assert.NotNull(firstJetton.Metadata);
        Assert.True(firstJetton.Metadata.ContainsKey("address"));
        Assert.Equal("0:be52dba0f76f9fc387e31cbbdf1ed15946f111852586c4c86657207febd9abca", firstJetton.Metadata["address"].ToLower());
        Assert.True(firstJetton.Metadata.ContainsKey("name"));
        Assert.Equal("MyJETTON", firstJetton.Metadata["name"]);
        Assert.True(firstJetton.Metadata.ContainsKey("symbol"));
        Assert.Equal("MJT", firstJetton.Metadata["symbol"]);
        Assert.True(firstJetton.Metadata.ContainsKey("decimals"));
        Assert.Equal("9", firstJetton.Metadata["decimals"]);
        
        // Verify preview
        Assert.NotNull(firstJetton.Preview);
        Assert.NotEmpty(firstJetton.Preview);
        Assert.Contains("cache.tonapi.io", firstJetton.Preview);
        
        // Verify verification
        Assert.Equal("none", firstJetton.Verification);
        
        // Verify holders_count
        Assert.NotNull(firstJetton.HoldersCount);
        Assert.Equal(3, firstJetton.HoldersCount.Value);
        
        // Verify second jetton (ctUSDT) if exists
        if (result.JettonsList.Count > 1)
        {
            var secondJetton = result.JettonsList[1];
            Assert.True(secondJetton.Mintable);
            Assert.Equal("100000000000000000", secondJetton.TotalSupply);
            
            Assert.NotNull(secondJetton.Metadata);
            Assert.True(secondJetton.Metadata.ContainsKey("name"));
            Assert.Equal("ctUSDT", secondJetton.Metadata["name"]);
            Assert.True(secondJetton.Metadata.ContainsKey("symbol"));
            Assert.Equal("ctUSDT", secondJetton.Metadata["symbol"]);
            
            Assert.NotNull(secondJetton.Preview);
            Assert.Contains("cache.tonapi.io", secondJetton.Preview);
            Assert.Equal("none", secondJetton.Verification);
            Assert.NotNull(secondJetton.HoldersCount);
            Assert.Equal(2, secondJetton.HoldersCount.Value);
        }
        
        // Verify third jetton (ALCARAZ_WORLD1_2005) if exists
        if (result.JettonsList.Count > 2)
        {
            var thirdJetton = result.JettonsList[2];
            Assert.True(thirdJetton.Mintable);
            Assert.Equal("0", thirdJetton.TotalSupply);
            
            Assert.NotNull(thirdJetton.Metadata);
            Assert.True(thirdJetton.Metadata.ContainsKey("name"));
            Assert.Equal("ALCARAZ_WORLD1_2005 v12", thirdJetton.Metadata["name"]);
            Assert.True(thirdJetton.Metadata.ContainsKey("symbol"));
            Assert.Equal("ALCARAZ_WORLD1_2005", thirdJetton.Metadata["symbol"]);
            Assert.True(thirdJetton.Metadata.ContainsKey("image"));
            Assert.True(thirdJetton.Metadata.ContainsKey("description"));
            
            Assert.NotNull(thirdJetton.Preview);
            Assert.Contains("cache.tonapi.io", thirdJetton.Preview);
            Assert.Equal("none", thirdJetton.Verification);
            Assert.NotNull(thirdJetton.HoldersCount);
            Assert.Equal(0, thirdJetton.HoldersCount.Value);
        }
        
        // Verify all jettons are mintable
        Assert.All(result.JettonsList, jetton => Assert.True(jetton.Mintable));
        
        // Verify all jettons have verification="none"
        Assert.All(result.JettonsList, jetton => Assert.Equal("none", jetton.Verification));
        
        // Verify all jettons have preview
        Assert.All(result.JettonsList, jetton => 
        {
            Assert.NotNull(jetton.Preview);
            Assert.NotEmpty(jetton.Preview);
        });
    }

    [Fact]
    public async Task GetAsync_WithBlackHatCoinAddress_ReturnsJettonDetails()
    {
        // Arrange
        var jettonAddress = "0:23fc2922290944bc1e3821fa74b7a5152db347494469c579306a6a0d35345a05";

        // Act
        var result = await Client.Jetton.GetAsync(jettonAddress);

        // Assert
        Assert.NotNull(result);
        
        // Verify mintable and total_supply
        Assert.True(result.Mintable);
        Assert.Equal("12944358038", result.TotalSupply);
        
        // Verify admin
        Assert.NotNull(result.Admin);
        Assert.Equal("0:fbbabaeb782ebe41887cc2ae748f89a1c97a6fa614db5deaf8b3a4a849227a54", result.Admin.Address.ToLower());
        Assert.False(result.Admin.IsScam);
        Assert.True(result.Admin.IsWallet);
        
        // Verify metadata
        Assert.NotNull(result.Metadata);
        Assert.True(result.Metadata.ContainsKey("address"));
        Assert.Equal(jettonAddress.ToLower(), result.Metadata["address"].ToLower());
        
        Assert.True(result.Metadata.ContainsKey("name"));
        Assert.Equal("BlackHat Coin", result.Metadata["name"]);
        
        Assert.True(result.Metadata.ContainsKey("symbol"));
        Assert.Equal("BLKC", result.Metadata["symbol"]);
        
        Assert.True(result.Metadata.ContainsKey("decimals"));
        Assert.Equal("8", result.Metadata["decimals"]);
        
        Assert.True(result.Metadata.ContainsKey("image"));
        Assert.Contains("BlackHatCoin", result.Metadata["image"]);
        Assert.Contains("logo_light256x256.png", result.Metadata["image"]);
        
        Assert.True(result.Metadata.ContainsKey("description"));
        Assert.Equal("BlackHat Coin wrapped Jetton. Telegram: @blackhatcoin", result.Metadata["description"]);
        
        // Verify preview
        Assert.NotNull(result.Preview);
        Assert.NotEmpty(result.Preview);
        Assert.Contains("cache.tonapi.io", result.Preview);
        
        // Verify verification
        Assert.Equal("none", result.Verification);
        
        // Verify holders_count
        Assert.NotNull(result.HoldersCount);
        Assert.Equal(7, result.HoldersCount.Value);
    }

    [Fact]
    public async Task GetHolders_WithBlackHatCoinAndLimit10_ReturnsHolders()
    {
        // Arrange
        var jettonAddress = "0:23fc2922290944bc1e3821fa74b7a5152db347494469c579306a6a0d35345a05";
        var limit = 10;
        var offset = 0;

        // Act
        var result = await Client.Jetton.GetHoldersAsync(jettonAddress, limit, offset);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Addresses);
        
        // Total should be 7
        Assert.Equal(7, result.Total);
        
        // Should return 7 holders (less than limit because there are only 7)
        Assert.Equal(7, result.Addresses.Count);
        
        // Verify first holder (largest balance)
        var firstHolder = result.Addresses[0];
        Assert.NotNull(firstHolder);
        Assert.Equal("0:7a99ed327288bc9c6f9222b952a9baa4726a9cebcd6f1071284e1e7e509667a1", firstHolder.Address.ToLower());
        Assert.Equal("10494357982", firstHolder.Balance);
        
        // Verify first holder owner
        Assert.NotNull(firstHolder.Owner);
        Assert.Equal("0:fbbabaeb782ebe41887cc2ae748f89a1c97a6fa614db5deaf8b3a4a849227a54", firstHolder.Owner.Address.ToLower());
        Assert.False(firstHolder.Owner.IsScam);
        Assert.True(firstHolder.Owner.IsWallet);
        
        // Verify second holder
        var secondHolder = result.Addresses[1];
        Assert.NotNull(secondHolder);
        Assert.Equal("0:259fca8240997bad6c91b5183649304e5d35e60e7c3f2a662625cabaebd57f7a", secondHolder.Address.ToLower());
        Assert.Equal("1620000036", secondHolder.Balance);
        
        Assert.NotNull(secondHolder.Owner);
        Assert.Equal("0:5f52c8f074dec08b1c5497f290cbff0022aab8df563af3806366a21c468a7af0", secondHolder.Owner.Address.ToLower());
        Assert.False(secondHolder.Owner.IsScam);
        Assert.True(secondHolder.Owner.IsWallet);
        
        // Verify third holder (TON Foundation)
        var thirdHolder = result.Addresses[2];
        Assert.NotNull(thirdHolder);
        Assert.Equal("0:d53f9496eb36a5ff54afd1bd5df3703522241abfa8b99224ba707e57701cf81d", thirdHolder.Address.ToLower());
        Assert.Equal("200000008", thirdHolder.Balance);
        
        Assert.NotNull(thirdHolder.Owner);
        Assert.Equal("0:059a3528654a8a14404660510c6effa6eb5b054ad8007d447c27a8490f0514d3", thirdHolder.Owner.Address.ToLower());
        Assert.Equal("TON Foundation", thirdHolder.Owner.Name);
        Assert.False(thirdHolder.Owner.IsScam);
        Assert.True(thirdHolder.Owner.IsWallet);
        
        // Verify fourth holder (is_wallet = false)
        var fourthHolder = result.Addresses[3];
        Assert.NotNull(fourthHolder);
        Assert.Equal("0:cfc904850a8af82682e996078b8ee64299e4615562a54fd5982fe1c952b1045a", fourthHolder.Address.ToLower());
        Assert.Equal("200000006", fourthHolder.Balance);
        
        Assert.NotNull(fourthHolder.Owner);
        Assert.False(fourthHolder.Owner.IsScam);
        Assert.False(fourthHolder.Owner.IsWallet);
        
        // Verify fifth holder (TON Ecosystem Reserve)
        var fifthHolder = result.Addresses[4];
        Assert.NotNull(fifthHolder);
        Assert.Equal("0:4679371ea382807a02618cd8e3aeb51183fe090f6be5676406eabab011888787", fifthHolder.Address.ToLower());
        Assert.Equal("200000004", fifthHolder.Balance);
        
        Assert.NotNull(fifthHolder.Owner);
        Assert.Equal("0:21137b0bc47669b3267f1de70cbb0cef5c728b8d8c7890451e8613b2d8998270", fifthHolder.Owner.Address.ToLower());
        Assert.Equal("TON Ecosystem Reserve (OLD)", fifthHolder.Owner.Name);
        Assert.False(fifthHolder.Owner.IsScam);
        Assert.True(fifthHolder.Owner.IsWallet);
        
        // Verify sixth holder
        var sixthHolder = result.Addresses[5];
        Assert.NotNull(sixthHolder);
        Assert.Equal("0:05470d05c75d74e6c2c7a010a5ed24a143a313da1876cab4f12ed698d513775f", sixthHolder.Address.ToLower());
        Assert.Equal("200000002", sixthHolder.Balance);
        
        // Verify seventh holder (TON Believers Fund, is_wallet = false)
        var seventhHolder = result.Addresses[6];
        Assert.NotNull(seventhHolder);
        Assert.Equal("0:6f914467b49124ea40afe9d05770bcd8f6c04c58d15454caf6914dea1f90cd0a", seventhHolder.Address.ToLower());
        Assert.Equal("20000000", seventhHolder.Balance);
        
        Assert.NotNull(seventhHolder.Owner);
        Assert.Equal("0:ed1691307050047117b998b561d8de82d31fbf84910ced6eb5fc92e7485ef8a7", seventhHolder.Owner.Address.ToLower());
        Assert.Equal("TON Believers Fund", seventhHolder.Owner.Name);
        Assert.False(seventhHolder.Owner.IsScam);
        Assert.False(seventhHolder.Owner.IsWallet);
        
        // Verify all holders have non-empty addresses
        Assert.All(result.Addresses, holder => Assert.NotEmpty(holder.Address));
        
        // Verify all holders have balances
        Assert.All(result.Addresses, holder => Assert.NotEmpty(holder.Balance));
        
        // Verify all holders have owners
        Assert.All(result.Addresses, holder => Assert.NotNull(holder.Owner));
    }

    [Fact]
    public async Task GetEventJettons_WithEventId_ReturnsJettonTransferEvent()
    {
        // Arrange
        var eventId = "3eaf04d28bb18a8eb2ba2e0f32e299be320293c610a8974d04189b1b3ed986f7";

        // Act
        var result = await Client.Jetton.GetEventJettonsAsync(eventId);

        // Assert
        Assert.NotNull(result);
        
        // Verify event_id
        Assert.Equal(eventId, result.EventId);
        
        // Verify timestamp
        Assert.Equal(1763999429, result.Timestamp);
        
        // Verify actions
        Assert.NotNull(result.Actions);
        Assert.Single(result.Actions);
        
        var action = result.Actions[0];
        Assert.Equal("JettonTransfer", action.Type);
        Assert.Equal("ok", action.Status);
        
        // Verify JettonTransfer details
        Assert.NotNull(action.JettonTransfer);
        var transfer = action.JettonTransfer;
        
        // Verify sender
        Assert.NotNull(transfer.Sender);
        Assert.Equal("0:ffdb1d0f6eff05ff9cbb9ac726ce4480be6ac5a0d11b4a09c2b52d9c268f1aa2", transfer.Sender.Address.ToLower());
        Assert.False(transfer.Sender.IsScam);
        Assert.True(transfer.Sender.IsWallet);
        
        // Verify recipient
        Assert.NotNull(transfer.Recipient);
        Assert.Equal("0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd", transfer.Recipient.Address.ToLower());
        Assert.False(transfer.Recipient.IsScam);
        Assert.True(transfer.Recipient.IsWallet);
        
        // Verify wallets
        Assert.Equal("0:89ed1c8de8f11e1c0d04ed0623156b08518eea67d89d63097eef3226f1855d02", transfer.SendersWallet.ToLower());
        Assert.Equal("0:62fae4ca9c2b02c3bcfa84805f42b12043f0ba60d23af598275986ea5ae3bfd3", transfer.RecipientsWallet.ToLower());
        
        // Verify amount and comment
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
        
        // Verify simple_preview
        Assert.NotNull(action.SimplePreview);
        Assert.Equal("Jetton Transfer", action.SimplePreview.Name);
        Assert.Contains("dRUB", action.SimplePreview.Description);
        Assert.Equal("1 dRUB", action.SimplePreview.Value);
        Assert.NotNull(action.SimplePreview.ValueImage);
        Assert.Contains("cache.tonapi.io", action.SimplePreview.ValueImage);
        
        // Verify simple_preview accounts
        Assert.NotNull(action.SimplePreview.Accounts);
        Assert.Equal(3, action.SimplePreview.Accounts.Count);
        
        // First account (recipient)
        Assert.Equal("0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd", action.SimplePreview.Accounts[0].Address.ToLower());
        Assert.False(action.SimplePreview.Accounts[0].IsScam);
        Assert.True(action.SimplePreview.Accounts[0].IsWallet);
        
        // Second account (sender)
        Assert.Equal("0:ffdb1d0f6eff05ff9cbb9ac726ce4480be6ac5a0d11b4a09c2b52d9c268f1aa2", action.SimplePreview.Accounts[1].Address.ToLower());
        Assert.False(action.SimplePreview.Accounts[1].IsScam);
        Assert.True(action.SimplePreview.Accounts[1].IsWallet);
        
        // Third account (jetton master)
        Assert.Equal("0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457", action.SimplePreview.Accounts[2].Address.ToLower());
        Assert.False(action.SimplePreview.Accounts[2].IsScam);
        Assert.False(action.SimplePreview.Accounts[2].IsWallet);
        
        // Verify base_transactions
        Assert.NotNull(action.BaseTransactions);
        Assert.Equal(4, action.BaseTransactions.Count);
        Assert.Contains("96a3bb2c1079de2096ffe46f1ecbe3b0a77d9ef1ecbec76ab2fd431df33173f1", action.BaseTransactions);
        Assert.Contains("99426d05017ef15a4781229379bc47521f5fb124961574c08a8c498144b899e5", action.BaseTransactions);
        Assert.Contains("bb0cb3e103a6fe9dc31b5bfaa0eaeaa841cc4aa0b4f47905ad9f272e470da3f0", action.BaseTransactions);
        Assert.Contains("76b46a96762bebf2671735937abdb0af824bdb23a2978267395c3bbf5d13b006", action.BaseTransactions);
        
        // Verify value_flow
        Assert.NotNull(result.ValueFlow);
        Assert.Empty(result.ValueFlow);
        
        // Verify is_scam, lt, in_progress, progress
        Assert.False(result.IsScam);
        Assert.Equal(41673840000001, result.Lt);
        Assert.False(result.InProgress);
        Assert.Equal(1, result.Progress);
    }
}

