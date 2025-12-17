using System.Numerics;
using Ton.Core.Addresses;
using Ton.Core.Boc;
using Ton.Core.Types;
using Ton.Crypto.Mnemonic;
using TonapiClient.Models;
using TonapiClient.Tests.V5R1;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TonapiClient.Tests;

public class TracesCategoryTests : TestBase
{
    [Fact]
    public async Task GetAsync_WithTraceId_ReturnsTraceWithChildren()
    {
        // Arrange
        var traceId = "d2441e142c9279dbfa67e8b812aab40ff13eac53e44bd6a8de702327c994ed60";

        // Act
        var result = await Client.Traces.GetAsync(traceId);

        // Assert
        Assert.NotNull(result);

        // Verify root transaction
        Assert.NotNull(result.Transaction);
        Assert.Equal(traceId, result.Transaction.Hash.ToLower());
        Assert.Equal(42198170000001ul, result.Transaction.Lt);

        // Verify root transaction account
        Assert.NotNull(result.Transaction.Account);
        Assert.Equal("0:7d9bd933a00838d9e7d0d3c94779689e6cc3812519a6ed523a84d45b4d896bec", result.Transaction.Account.Address.ToLower());
        Assert.False(result.Transaction.Account.IsScam);
        Assert.True(result.Transaction.Account.IsWallet);

        // Verify root transaction success
        Assert.True(result.Transaction.Success.HasValue);
        Assert.True(result.Transaction.Success.Value);
        Assert.Equal(1765205194ul, result.Transaction.Utime);
        Assert.Equal(3246798ul, result.Transaction.TotalFees);

        // Verify root transaction in_msg
        Assert.NotNull(result.Transaction.InMsg);
        Assert.Equal("ext_in_msg", result.Transaction.InMsg.MsgType);
        Assert.Equal("0x7369676e", result.Transaction.InMsg.OpCode);

        // Verify out_msgs
        Assert.NotNull(result.Transaction.OutMsgs);

        // Verify block
        Assert.NotNull(result.Transaction.Block);
        Assert.NotEmpty(result.Transaction.Block);

        // Verify interfaces
        Assert.NotNull(result.Interfaces);
        Assert.Single(result.Interfaces);
        Assert.Equal("wallet_v5r1", result.Interfaces[0]);

        // Verify children exist
        Assert.NotNull(result.Children);
        Assert.NotEmpty(result.Children);

        // Verify first child transaction
        var firstChild = result.Children[0];
        Assert.NotNull(firstChild);
        Assert.NotNull(firstChild.Transaction);
        Assert.Equal("c692cb7425fa7774a83979ca496db96c04960e1e13759ee0819b51b61566901d", firstChild.Transaction.Hash.ToLower());
        Assert.True(firstChild.Transaction.Success.HasValue);
        Assert.True(firstChild.Transaction.Success.Value);

        // Verify first child has its own children (nested structure)
        Assert.NotNull(firstChild.Children);
        Assert.NotEmpty(firstChild.Children);

        // Verify second level child
        var secondLevelChild = firstChild.Children[0];
        Assert.NotNull(secondLevelChild);
        Assert.NotNull(secondLevelChild.Transaction);
        Assert.Equal("072c3a3914dffaaba49dbd82093a299ca24c1c7e05fe34eafc5c3f9b4a7afccc", secondLevelChild.Transaction.Hash.ToLower());

        // Verify emulated flag
        Assert.False(result.Emulated);

        // Verify aborted flag
        Assert.False(result.Transaction.Aborted);

        // Test helper methods - they should execute without exceptions
        var isFinalized = result.IsFinalized(); // Can be true or false depending on trace state
        Assert.False(result.HasUnsuccessfulTransactions()); // All transactions should be successful

        var totalFees = result.GetTotalFees();
        Assert.True(totalFees > 0, $"Total fees should be positive, got {totalFees}");

        var sender = result.GetSender();
        Assert.NotNull(sender);
        Assert.Equal("0:7d9bd933a00838d9e7d0d3c94779689e6cc3812519a6ed523a84d45b4d896bec", sender.ToLower());

        var transferType = result.GetTransferType();
        Assert.NotEqual(TransferType.Unknown, transferType);
    }

    [Fact]
    public async Task GetAsync_WithSuccessfulTraceId_HasNoUnsuccessfulTransactions()
    {
        // Arrange
        var successTx = "ff54ce6fbbfc82acae46acd30fb850a2a952f14e4016787e8f9f96150a1552da";

        // Act
        var result = await Client.Traces.GetAsync(successTx);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.HasUnsuccessfulTransactions(),
            "Expected trace to have no unsuccessful transactions");
    }

    [Fact]
    public async Task GetAsync_WithUnsuccessfulTraceId_HasUnsuccessfulTransactions()
    {
        // Arrange
        var unsuccessfulTx = "5daa07502a952813831e30b572a846f4232013cb9e609f990342173ca53e5d4c";

        // Act
        var result = await Client.Traces.GetAsync(unsuccessfulTx);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasUnsuccessfulTransactions(),
            "Expected trace to have at least one unsuccessful transaction");
    }

    [Fact]
    public async Task GetAsync_CheckGetTotalFeesTests()
    {
        // Arrange
        var traceId = "1078a7a9a6e99465133c7000674c1ff3f67af1092f9c2ce087a4eb941b92d151";

        //Act
        var traceResult = await Client.Traces.GetAsync(traceId);
        var totalFees = traceResult.GetTotalFees();

        //// Assert
        Assert.Equal(12509843UL, totalFees);
    }

    [Fact]
    public async Task GetAsync_CheckJettonTransferTraceTests()
    {
        // Arrange
        var traceId = "d2441e142c9279dbfa67e8b812aab40ff13eac53e44bd6a8de702327c994ed60";
        var expectedSender = "0:7d9bd933a00838d9e7d0d3c94779689e6cc3812519a6ed523a84d45b4d896bec";
        var expectedRecipient = "0:7d9bd933a00838d9e7d0d3c94779689e6cc3812519a6ed523a84d45b4d896bec";

        // Act
        var traceResult = await Client.Traces.GetAsync(traceId);
        var transferType = traceResult.GetTransferType();
        var sender = traceResult.GetSender();
        var recipient = traceResult.GetRecipient();
        var totalFees = traceResult.GetTotalFees();

        //// Assert
        Assert.Equal(TransferType.Jetton, transferType);
        Assert.Equal(expectedSender, sender);
        Assert.Equal(expectedRecipient, recipient);
        Assert.Equal(15267740UL, totalFees);
    }

    [Fact]
    public async Task GetAsync_CheckNativeTransferTraceTests()
    {
        // Arrange
        var traceId = "8a257c2ba00b36c31a7d7c95a60a08e2dd89c02cdfdfd126e1d111082a4921b1";
        var expectedSender = "0:ffdb1d0f6eff05ff9cbb9ac726ce4480be6ac5a0d11b4a09c2b52d9c268f1aa2";
        var expectedRecipient = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";

        // Act
        var traceResult = await Client.Traces.GetAsync(traceId);
        var transferType = traceResult.GetTransferType();
        var sender = traceResult.GetSender();
        var recipient = traceResult.GetRecipient();
        var totalFees = traceResult.GetTotalFees();

        //// Assert
        Assert.Equal(TransferType.Native, transferType);
        Assert.Equal(expectedSender, sender);
        Assert.Equal(expectedRecipient, recipient);
        Assert.Equal(3260581UL, totalFees);
    }

    [Fact]
    public async Task EmulateTransferWithDummyKeyTests()
    {
        var amount = 0.05m;
        var fromAddress = "0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e";
        var toAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";

        // This test demonstrates emulation without real private key (as shown in TON API docs)
        // https://docs.tonconsole.com/tonapi/cookbook/emulation
        // emulateMessageToTrace ignores signature check by default, so we can use dummy keys

        // Get wallet seqno
        var seqnoResult = await Client.Wallet.GetSeqnoAsync(fromAddress);
        var seqno = seqnoResult.SeqnoValue;

        // Get public key
        var publicKeyResult = await Client.Account.GetPublicKeyAsync(fromAddress);
        var publicKey = publicKeyResult.PublicKeyHex.HexToBytes();

        // Create wallet instance with real public key
        var wallet = WalletV5R1.Create(0, publicKey, false);

        // Create internal message
        var message = new MessageRelaxed(
            new CommonMessageInfoRelaxed.Internal(
                true, false, false,
                null, Address.Parse(toAddress),
                new CurrencyCollection(new BigInteger(amount * 1_000_000_000)),
                0, 0, 0, 0),
            Builder.BeginCell().StoreUint(PopularOpcodes.TextComment, 32).StoreStringTail("Dummy key emulation test").EndCell());

        // Create unsigned transfer
        Cell unsignedTransfer = WalletV5R1Utils.CreateUnsignedTransfer(wallet.WalletId, seqno,
            [message],
            SendMode.SendPayFwdFeesSeparately | SendMode.SendIgnoreErrors);

        // Generate dummy key (we don't have the real private key)
        var dummyMnemonic = Ton.Crypto.Mnemonic.Mnemonic.New();
        var dummyKeys = Ton.Crypto.Mnemonic.Mnemonic.ToWalletKey(dummyMnemonic);

        // Sign with dummy key - signature will be wrong, but emulation with ignore_signature_check will work
        Cell transfer = WalletV5R1Utils.SignAndPack(unsignedTransfer, dummyKeys.SecretKey);

        // Create external message
        CommonMessageInfo.ExternalIn externalMsgInfo = new(
            null,
            Address.Parse(fromAddress),
            BigInteger.Zero
        );
        Ton.Core.Types.Message externalMsg = new(externalMsgInfo, transfer, null); // seqno > 0, so no Init

        // Serialize to BOC
        Cell messageCell = Builder.BeginCell()
          .StoreMessage(externalMsg)
          .EndCell();
        byte[] boc = messageCell.ToBoc();
        string bocBase64 = Convert.ToBase64String(boc);

        // Emulate with emulateMessageToTrace (signature check is ignored by default for emulation)
        var trace = await Client.Traces.EmulateAsync(bocBase64);

        // Assert
        Assert.NotNull(trace);
        Assert.False(trace.HasUnsuccessfulTransactions());

        // Get total fees from emulation
        var totalFees = trace.GetTotalFees();
        Assert.True(totalFees > 0, "Emulated transaction should have fees");

        // Check transfer type
        var transferType = trace.GetTransferType();
        Assert.Equal(TransferType.Native, transferType);

        // Check sender and recipient
        var sender = trace.GetSender();
        var recipient = trace.GetRecipient();
        Assert.NotNull(sender);
        Assert.NotNull(recipient);
    }


    [Fact]
    public async Task EmulateTransferTests()
    {
        var amount = 0.05m;
        var fromAddress = "0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e";
        var toAddress = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";

        // This test demonstrates emulation without real private key (as shown in TON API docs)
        // https://docs.tonconsole.com/tonapi/cookbook/emulation
        // emulateMessageToTrace ignores signature check by default, so we can use dummy keys

        // Get wallet seqno
        var seqnoResult = await Client.Wallet.GetSeqnoAsync(fromAddress);
        var seqno = seqnoResult.SeqnoValue;

        // Get public key
        var publicKeyResult = await Client.Account.GetPublicKeyAsync(fromAddress);
        var publicKey = publicKeyResult.PublicKeyHex.HexToBytes();

        // Create wallet instance with real public key
        var wallet = WalletV5R1.Create(0, publicKey, false);

        // Create internal message
        var message = new MessageRelaxed(
            new CommonMessageInfoRelaxed.Internal(
                true, false, false,
                null, Address.Parse(toAddress),
                new CurrencyCollection(new BigInteger(amount * 1_000_000_000)),
                0, 0, 0, 0),
            Builder.BeginCell().StoreUint(PopularOpcodes.TextComment, 32).StoreStringTail("Dummy key emulation test").EndCell());

        // Create unsigned transfer
        Cell unsignedTransfer = WalletV5R1Utils.CreateUnsignedTransfer(wallet.WalletId, seqno,
            [message],
            SendMode.SendPayFwdFeesSeparately | SendMode.SendIgnoreErrors);

        var keys = Ton.Crypto.Mnemonic.Mnemonic.ToWalletKey(Mnemonic.Split(" "));

        // Sign with dummy key - signature will be wrong, but emulation with ignore_signature_check will work
        Cell transfer = WalletV5R1Utils.SignAndPack(unsignedTransfer, keys.SecretKey);

        // Create external message
        CommonMessageInfo.ExternalIn externalMsgInfo = new(
            null,
            Address.Parse(fromAddress),
            BigInteger.Zero
        );
        Ton.Core.Types.Message externalMsg = new(externalMsgInfo, transfer, null); // seqno > 0, so no Init

        // Serialize to BOC
        Cell messageCell = Builder.BeginCell()
          .StoreMessage(externalMsg)
          .EndCell();
        byte[] boc = messageCell.ToBoc();
        string bocBase64 = Convert.ToBase64String(boc);

        // Emulate with emulateMessageToTrace (signature check is ignored by default for emulation)
        var trace = await Client.Traces.EmulateAsync(bocBase64, false);

        // Assert
        Assert.NotNull(trace);
        Assert.False(trace.HasUnsuccessfulTransactions());

        // Get total fees from emulation
        var totalFees = trace.GetTotalFees();
        Assert.True(totalFees > 0, "Emulated transaction should have fees");

        // Check transfer type
        var transferType = trace.GetTransferType();
        Assert.Equal(TransferType.Native, transferType);

        // Check sender and recipient
        var sender = trace.GetSender();
        var recipient = trace.GetRecipient();
        Assert.NotNull(sender);
        Assert.NotNull(recipient);
    }

    [Fact]
    public async Task TransferTests()
    {
        string toAddress = "0QAIwc1G58XyOPWkc3WyCMUy2hb4kb65RwaRbPABDIeyzYKY";
        decimal amount = 0.01m;
        var keys = Ton.Crypto.Mnemonic.Mnemonic.ToWalletKey(Mnemonic.Split(" "));

        var wallet = WalletV5R1.Create(0, keys.PublicKey, false);
        var seqnoResult = await Client.Wallet.GetSeqnoAsync(wallet.Address.ToString());
        var seqno = seqnoResult.SeqnoValue;

        var destAddress = Address.Parse(toAddress);

        // this is internal messgae
        var message = new MessageRelaxed(
            new CommonMessageInfoRelaxed.Internal(
                true, false, false,
                null, destAddress,
                new CurrencyCollection(new BigInteger(amount * 1_000_000_000)),
                0, 0, 0, 0),
            Builder.BeginCell().StoreUint(0x00000000, 32).StoreStringTail("Test transfer").EndCell());

        Cell unsignedTransfer = WalletV5R1Utils.CreateUnsignedTransfer(wallet.WalletId, seqno,
            [message],
            SendMode.SendPayFwdFeesSeparately | SendMode.SendIgnoreErrors);

        Cell transfer = WalletV5R1Utils.SignAndPack(unsignedTransfer, keys.SecretKey);

        // Create external message
        // Build external message (the receiver of this message is user`s wallet)
        CommonMessageInfo.ExternalIn externalMsgInfo = new(
            null,
            wallet.Address,
            BigInteger.Zero
        );
        Ton.Core.Types.Message externalMsg = new(externalMsgInfo, transfer, seqno > 0 ? null : wallet.Init);
        var normalizedHash = WalletV5R1Utils.NormalizeHash(externalMsg).ToHex();

        // Serialize and send
        Cell messageCell = Builder.BeginCell()
          .StoreMessage(externalMsg)
          .EndCell();
        byte[] boc = messageCell.ToBoc();
        var bocAsString = Convert.ToBase64String(boc);
        var consequences = await Client.Traces.EmulateAsync(bocAsString);
        Assert.True(!consequences.HasUnsuccessfulTransactions(), "Emulated transaction should be successful");
        Assert.True(consequences.GetTotalFees() > 0, "Emulated transaction should have fees");

        // await Client.LiteServer.SendMessageAsync(Convert.ToBase64String(boc));
        await Client.Blockchain.SendBocAsync(bocAsString);
        try
        {
            await Client.Blockchain.SendBocAsync(bocAsString);
        }
        catch (Exception ex)
        {
            Assert.Contains("duplicate message", ex.Message);
        }

        var tx = await Client.Blockchain.WaitForTransactionAsync(wallet.Address.ToString(), normalizedHash);

        Assert.True(tx != null, "Transaction should be found");
        Assert.True(tx.Success == true, "Transaction should be successful");
    }
}

