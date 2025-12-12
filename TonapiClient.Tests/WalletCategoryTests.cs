using Xunit;
using TonapiClient.Models;

namespace TonapiClient.Tests;

public class WalletCategoryTests : TestBase
{
    [Fact]
    public async Task GetSeqno_WithWalletAddress_ReturnsSeqnoGreaterOrEqualTo3()
    {
        // Arrange
        var walletAddress = "0:847fcaa5b9b7dae8e4cae87390d104e8f6e1b237f3776fc328ef989354479116";

        // Act
        var result = await Client.Wallet.GetSeqnoAsync(walletAddress);

        // Assert
        Assert.NotNull(result);
        
        // Verify seqno is greater than or equal to 3
        Assert.True(result.SeqnoValue >= 3, $"Expected seqno to be >= 3, but got {result.SeqnoValue}");
    }

    [Fact]
    public async Task GetAsync_WithWalletAddress_ReturnsWalletInfo()
    {
        // Arrange
        var walletAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";
        const ulong expectedMinLastActivity = 1763999453;
        const ulong expectedMinLastLt = 41673851000001;

        // Act
        var result = await Client.Wallet.GetAsync(walletAddress);

        // Assert
        Assert.NotNull(result);
        
        // Verify address
        Assert.Equal(walletAddress.ToLower(), result.Address.ToLower());
        
        // Verify is_wallet
        Assert.True(result.IsWallet);
        
        // Verify balance is not zero
        Assert.True(result.Balance > 0, $"Expected balance to be > 0, but got {result.Balance}");
        
        // Verify stats
        Assert.NotNull(result.Stats);
        Assert.Equal(0, result.Stats.NftsCount);
        Assert.Equal(2, result.Stats.JettonsCount);
        Assert.Equal(0, result.Stats.MultisigCount);
        Assert.Equal(0, result.Stats.StakingCount);
        
        // Verify plugins
        Assert.NotNull(result.Plugins);
        Assert.Empty(result.Plugins);
        
        // Verify status
        Assert.Equal("active", result.Status);
        
        // Verify last_activity is greater than or equal to expected
        Assert.True(result.LastActivity >= expectedMinLastActivity, 
            $"Expected last_activity to be >= {expectedMinLastActivity}, but got {result.LastActivity}");
        
        // Verify get_methods
        Assert.NotNull(result.GetMethods);
        Assert.Empty(result.GetMethods);
        
        // Verify interfaces
        Assert.NotNull(result.Interfaces);
        Assert.Single(result.Interfaces);
        Assert.Equal("wallet_v4r2", result.Interfaces[0]);
        
        // Verify last_lt is greater than or equal to expected
        Assert.True(result.LastLt >= expectedMinLastLt, 
            $"Expected last_lt to be >= {expectedMinLastLt}, but got {result.LastLt}");
    }

    [Fact]
    public async Task GetWalletsByPublicKey_WithPublicKey_ReturnsWalletAccounts()
    {
        // Arrange
        var publicKey = "47ed240fb7f1c61b2f2e8113693d209ed4cd2644c73c70bac1f1464e108b34b2";

        // Act
        var result = await Client.Wallet.GetWalletsByPublicKeyAsync(publicKey);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Accounts);
        Assert.Equal(2, result.Accounts.Count);
        
        // Verify first wallet (wallet_v3r2)
        var firstWallet = result.Accounts[0];
        Assert.NotNull(firstWallet);
        Assert.Equal("0:84355589fc061dac2e2642ef7025c221df14dbf014b35eb4ff5dd902215920c3", firstWallet.Address.ToLower());
        Assert.True(firstWallet.IsWallet);
        Assert.True(firstWallet.Balance >= 0);
        
        // Verify first wallet stats
        Assert.NotNull(firstWallet.Stats);
        Assert.Equal(0, firstWallet.Stats.NftsCount);
        Assert.Equal(11, firstWallet.Stats.JettonsCount);
        Assert.Equal(0, firstWallet.Stats.MultisigCount);
        Assert.Equal(0, firstWallet.Stats.StakingCount);
        
        // Verify first wallet other properties
        Assert.NotNull(firstWallet.Plugins);
        Assert.Empty(firstWallet.Plugins);
        Assert.Equal("active", firstWallet.Status);
        Assert.True(firstWallet.LastActivity >= 0);
        Assert.NotNull(firstWallet.GetMethods);
        Assert.Empty(firstWallet.GetMethods);
        Assert.NotNull(firstWallet.Interfaces);
        Assert.Single(firstWallet.Interfaces);
        Assert.Equal("wallet_v3r2", firstWallet.Interfaces[0]);
        Assert.True(firstWallet.LastLt >= 0);
        
        // Verify second wallet (wallet_v4r2)
        var secondWallet = result.Accounts[1];
        Assert.NotNull(secondWallet);
        Assert.Equal("0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd", secondWallet.Address.ToLower());
        Assert.True(secondWallet.IsWallet);
        Assert.Equal(1620048353ul, secondWallet.Balance);
        
        // Verify second wallet stats
        Assert.NotNull(secondWallet.Stats);
        Assert.Equal(0, secondWallet.Stats.NftsCount);
        Assert.Equal(2, secondWallet.Stats.JettonsCount);
        Assert.Equal(0, secondWallet.Stats.MultisigCount);
        Assert.Equal(0, secondWallet.Stats.StakingCount);
        
        // Verify second wallet other properties
        Assert.NotNull(secondWallet.Plugins);
        Assert.Empty(secondWallet.Plugins);
        Assert.Equal("active", secondWallet.Status);
        Assert.Equal(1763999453ul, secondWallet.LastActivity);
        Assert.NotNull(secondWallet.GetMethods);
        Assert.Empty(secondWallet.GetMethods);
        Assert.NotNull(secondWallet.Interfaces);
        Assert.Single(secondWallet.Interfaces);
        Assert.Equal("wallet_v4r2", secondWallet.Interfaces[0]);
        Assert.True(secondWallet.LastLt >= 0);
        
        // Verify all wallets are active
        Assert.All(result.Accounts, wallet => Assert.Equal("active", wallet.Status));
        
        // Verify all wallets have is_wallet = true
        Assert.All(result.Accounts, wallet => Assert.True(wallet.IsWallet));
    }
}

