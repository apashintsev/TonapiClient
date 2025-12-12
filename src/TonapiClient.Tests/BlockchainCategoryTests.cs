using Xunit;
using TonapiClient.Models;

namespace TonapiClient.Tests;

public class BlockchainCategoryTests : TestBase
{
    [Fact]
    public async Task GetBlockAsync_WithBlockId_ReturnsBlockDetails()
    {
        // Arrange
        var blockId = "(-1,8000000000000000,4234234)";

        // Act
        var result = await Client.Blockchain.GetBlockAsync(blockId);

        // Assert
        Assert.NotNull(result);
        
        // Verify basic block properties
        Assert.Equal(3, result.TxQuantity);
        Assert.Equal(-1, result.WorkchainId);
        Assert.Equal("8000000000000000", result.Shard);
        Assert.Equal(4234234ul, result.Seqno);
        
        // Verify hashes
        Assert.Equal("cd0a43038ce574c09531c965347d6058f5dd6aa6aedaf7d336a69242a34022ea", result.RootHash);
        Assert.Equal("4e03f712f5d120d026eed7eadfb723e7327bbfcb44e35e9863d72c371dc3ef14", result.FileHash);
        
        // Verify global properties
        Assert.Equal(-3, result.GlobalId);
        Assert.Equal(0, result.Version);
        
        // Verify split/merge flags
        Assert.False(result.AfterMerge);
        Assert.False(result.BeforeSplit);
        Assert.False(result.AfterSplit);
        Assert.False(result.WantSplit);
        Assert.True(result.WantMerge);
        Assert.False(result.KeyBlock);
        
        // Verify timestamps and sequence numbers
        Assert.Equal(1666230405ul, result.GenUtime);
        Assert.Equal(5154035000000ul, result.StartLt);
        Assert.Equal(5154035000004ul, result.EndLt);
        Assert.Equal(0, result.VertSeqno);
        Assert.Equal(55624, result.GenCatchainSeqno);
        Assert.Equal(4234232, result.MinRefMcSeqno);
        Assert.Equal(4233012, result.PrevKeyBlockSeqno);
        
        // Verify software version
        Assert.NotNull(result.GenSoftwareVersion);
        Assert.Equal(3, result.GenSoftwareVersion.Value);
        Assert.NotNull(result.GenSoftwareCapabilities);
        Assert.Equal(46, result.GenSoftwareCapabilities.Value);
        
        // Verify previous references
        Assert.NotNull(result.PreviousReferences);
        Assert.Single(result.PreviousReferences);
        Assert.Equal("(-1,8000000000000000,4234233)", result.PreviousReferences[0]);
        
        // Verify message descriptors
        Assert.Equal(1, result.InMsgDescrLength);
        Assert.Equal(0, result.OutMsgDescrLength);
        
        // Verify rand_seed and created_by
        Assert.Equal("e008767f4ea828fae0841d334b7c89d2a9830fec800cf262c0f491c0b4de3c2a", result.RandSeed);
        Assert.Equal("0bc6bd9fcd46b940d59229c5ad21cb08001db7763ce4d98c54a6558c239bcc69", result.CreatedBy);
        
        // Verify value_flow exists and has data
        Assert.NotNull(result.ValueFlow);
        
        // Verify from_prev_blk
        Assert.NotNull(result.ValueFlow.FromPreviousBlock);
        Assert.True(result.ValueFlow.FromPreviousBlock.Grams > 0);
        Assert.NotNull(result.ValueFlow.FromPreviousBlock.Other);
        Assert.Equal(2, result.ValueFlow.FromPreviousBlock.Other.Count);
        Assert.Equal(239ul, result.ValueFlow.FromPreviousBlock.Other[0].Id);
        Assert.Equal("666666666666", result.ValueFlow.FromPreviousBlock.Other[0].Value);
        Assert.Equal(4294967279ul, result.ValueFlow.FromPreviousBlock.Other[1].Id);
        Assert.Equal("1000000000000", result.ValueFlow.FromPreviousBlock.Other[1].Value);
        
        // Verify to_next_blk
        Assert.NotNull(result.ValueFlow.ToNextBlock);
        Assert.True(result.ValueFlow.ToNextBlock.Grams > 0);
        Assert.NotNull(result.ValueFlow.ToNextBlock.Other);
        Assert.Equal(2, result.ValueFlow.ToNextBlock.Other.Count);
        
        // Verify fees_collected
        Assert.NotNull(result.ValueFlow.FeesCollected);
        Assert.Equal(2700000000ul, result.ValueFlow.FeesCollected.Grams);
        Assert.NotNull(result.ValueFlow.FeesCollected.Other);
        Assert.Empty(result.ValueFlow.FeesCollected.Other);
        
        // Verify fees_imported
        Assert.NotNull(result.ValueFlow.FeesImported);
        Assert.Equal(1000000000ul, result.ValueFlow.FeesImported.Grams);
        
        // Verify recovered
        Assert.NotNull(result.ValueFlow.Recovered);
        Assert.Equal(2700000000ul, result.ValueFlow.Recovered.Grams);
        
        // Verify created
        Assert.NotNull(result.ValueFlow.Created);
        Assert.Equal(1700000000ul, result.ValueFlow.Created.Grams);
        
        // Verify imported, exported, and minted are empty/zero
        Assert.NotNull(result.ValueFlow.Imported);
        Assert.Equal(0ul, result.ValueFlow.Imported.Grams);
        Assert.NotNull(result.ValueFlow.Exported);
        Assert.Equal(0ul, result.ValueFlow.Exported.Grams);
        Assert.NotNull(result.ValueFlow.Minted);
        Assert.Equal(0ul, result.ValueFlow.Minted.Grams);
    }

    [Fact]
    public async Task GetMasterchainShardsAsync_WithSeqno_ReturnsShardsInfo()
    {
        // Arrange
        var masterchainSeqno = 123456;

        // Act
        var result = await Client.Blockchain.GetMasterchainShardsAsync(masterchainSeqno);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Shards);
        Assert.NotEmpty(result.Shards);
        
        // Verify first shard
        var firstShard = result.Shards[0];
        Assert.NotNull(firstShard);
        
        // Verify last_known_block_id
        Assert.Equal("(0,8000000000000000,139034)", firstShard.LastKnownBlockId);
        
        // Verify last_known_block
        Assert.NotNull(firstShard.LastKnownBlock);
        var block = firstShard.LastKnownBlock;
        
        // Verify block basic properties
        Assert.Equal(0, block.TxQuantity);
        Assert.Equal(0, block.WorkchainId);
        Assert.Equal("8000000000000000", block.Shard);
        Assert.Equal(139034ul, block.Seqno);
        
        // Verify hashes
        Assert.Equal("08d7d6a0b9fd7ce2079779314d6d6d1074475df62845d0652a102deefaaa8cdc", block.RootHash);
        Assert.Equal("078c050bf2fed58878fb690b560cc5170fa393dd2129a481c0df8bec9ab29b2b", block.FileHash);
        
        // Verify global properties
        Assert.Equal(-3, block.GlobalId);
        Assert.Equal(0, block.Version);
        
        // Verify split/merge flags
        Assert.False(block.AfterMerge);
        Assert.False(block.BeforeSplit);
        Assert.False(block.AfterSplit);
        Assert.False(block.WantSplit);
        Assert.True(block.WantMerge);
        Assert.False(block.KeyBlock);
        
        // Verify timestamps
        Assert.Equal(1653657354ul, block.GenUtime);
        Assert.Equal(148465000000ul, block.StartLt);
        Assert.Equal(148465000001ul, block.EndLt);
        Assert.Equal(0, block.VertSeqno);
        Assert.Equal(1829, block.GenCatchainSeqno);
        Assert.Equal(123453, block.MinRefMcSeqno);
        Assert.Equal(122862, block.PrevKeyBlockSeqno);
        
        // Verify software version
        Assert.Equal(3, block.GenSoftwareVersion);
        Assert.Equal(46, block.GenSoftwareCapabilities);
        
        // Verify master_ref
        Assert.Equal("(-1,8000000000000000,123453)", block.MasterRef);
        
        // Verify prev_refs
        Assert.NotNull(block.PreviousReferences);
        Assert.Single(block.PreviousReferences);
        Assert.Equal("(0,8000000000000000,139033)", block.PreviousReferences[0]);
        
        // Verify message descriptors
        Assert.Equal(0, block.InMsgDescrLength);
        Assert.Equal(0, block.OutMsgDescrLength);
        
        // Verify rand_seed and created_by
        Assert.Equal("7e0122bc3c4024d418c061933f4a313d35f25573744e6208e142b50526735cd5", block.RandSeed);
        Assert.Equal("5a71badd45b846e992d579d3f2255456b1548d8ec2c115cf4c39d28b62e6c135", block.CreatedBy);
        
        // Verify value_flow
        Assert.NotNull(block.ValueFlow);
        
        // Verify from_prev_blk
        Assert.NotNull(block.ValueFlow.FromPreviousBlock);
        Assert.Equal(1318061373763697ul, block.ValueFlow.FromPreviousBlock.Grams);
        Assert.NotNull(block.ValueFlow.FromPreviousBlock.Other);
        Assert.Empty(block.ValueFlow.FromPreviousBlock.Other);
        
        // Verify to_next_blk
        Assert.NotNull(block.ValueFlow.ToNextBlock);
        Assert.Equal(1318061373763697ul, block.ValueFlow.ToNextBlock.Grams);
        Assert.NotNull(block.ValueFlow.ToNextBlock.Other);
        Assert.Empty(block.ValueFlow.ToNextBlock.Other);
        
        // Verify fees_collected
        Assert.NotNull(block.ValueFlow.FeesCollected);
        Assert.Equal(1000000000ul, block.ValueFlow.FeesCollected.Grams);
        Assert.Empty(block.ValueFlow.FeesCollected.Other);
        
        // Verify created
        Assert.NotNull(block.ValueFlow.Created);
        Assert.Equal(1000000000ul, block.ValueFlow.Created.Grams);
        Assert.Empty(block.ValueFlow.Created.Other);
        
        // Verify all other flows are zero/empty
        Assert.Equal(0ul, block.ValueFlow.Imported.Grams);
        Assert.Equal(0ul, block.ValueFlow.Exported.Grams);
        Assert.Equal(0ul, block.ValueFlow.FeesImported.Grams);
        Assert.Equal(0ul, block.ValueFlow.Recovered.Grams);
        Assert.Equal(0ul, block.ValueFlow.Minted.Grams);
    }

    [Fact]
    public async Task GetMasterchainBlocksAsync_WithSeqno_ReturnsBlocksList()
    {
        // Arrange
        var masterchainSeqno = 123456;

        // Act
        var result = await Client.Blockchain.GetMasterchainBlocksAsync(masterchainSeqno);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Blocks);
        Assert.Equal(2, result.Blocks.Count);
        
        // Verify first block (workchain 0, shard block)
        var firstBlock = result.Blocks[0];
        Assert.NotNull(firstBlock);
        Assert.Equal(0, firstBlock.TxQuantity);
        Assert.Equal(0, firstBlock.WorkchainId);
        Assert.Equal("8000000000000000", firstBlock.Shard);
        Assert.Equal(139034ul, firstBlock.Seqno);
        
        // Verify first block hashes
        Assert.Equal("08d7d6a0b9fd7ce2079779314d6d6d1074475df62845d0652a102deefaaa8cdc", firstBlock.RootHash);
        Assert.Equal("078c050bf2fed58878fb690b560cc5170fa393dd2129a481c0df8bec9ab29b2b", firstBlock.FileHash);
        
        // Verify first block flags
        Assert.False(firstBlock.AfterMerge);
        Assert.False(firstBlock.BeforeSplit);
        Assert.False(firstBlock.AfterSplit);
        Assert.False(firstBlock.WantSplit);
        Assert.True(firstBlock.WantMerge);
        Assert.False(firstBlock.KeyBlock);
        
        // Verify first block timestamps
        Assert.Equal(1653657354ul, firstBlock.GenUtime);
        Assert.Equal(148465000000ul, firstBlock.StartLt);
        Assert.Equal(148465000001ul, firstBlock.EndLt);
        
        // Verify first block master_ref
        Assert.Equal("(-1,8000000000000000,123453)", firstBlock.MasterRef);
        
        // Verify first block prev_refs
        Assert.NotNull(firstBlock.PreviousReferences);
        Assert.Single(firstBlock.PreviousReferences);
        Assert.Equal("(0,8000000000000000,139033)", firstBlock.PreviousReferences[0]);
        
        // Verify first block value_flow
        Assert.NotNull(firstBlock.ValueFlow);
        Assert.Equal(1318061373763697ul, firstBlock.ValueFlow.FromPreviousBlock.Grams);
        Assert.Equal(1318061373763697ul, firstBlock.ValueFlow.ToNextBlock.Grams);
        Assert.Equal(1000000000ul, firstBlock.ValueFlow.FeesCollected.Grams);
        Assert.Equal(1000000000ul, firstBlock.ValueFlow.Created.Grams);
        Assert.Empty(firstBlock.ValueFlow.FromPreviousBlock.Other);
        
        // Verify second block (workchain -1, masterchain block)
        var secondBlock = result.Blocks[1];
        Assert.NotNull(secondBlock);
        Assert.Equal(3, secondBlock.TxQuantity);
        Assert.Equal(-1, secondBlock.WorkchainId);
        Assert.Equal("8000000000000000", secondBlock.Shard);
        Assert.Equal(123456ul, secondBlock.Seqno);
        
        // Verify second block hashes
        Assert.Equal("d561c118168373c0ca74525c1148a6362ca6681bec474f8e7684c1789d41b541", secondBlock.RootHash);
        Assert.Equal("79122ab9170f5be057a997220df4cd4f06be1467a2aee61c205b1cfc5f4d6c3e", secondBlock.FileHash);
        
        // Verify second block flags
        Assert.True(secondBlock.WantMerge);
        Assert.False(secondBlock.KeyBlock);
        
        // Verify second block timestamps
        Assert.Equal(1653657358ul, secondBlock.GenUtime);
        Assert.Equal(148466000000ul, secondBlock.StartLt);
        Assert.Equal(148466000004ul, secondBlock.EndLt);
        
        // Verify second block doesn't have master_ref (it's a masterchain block)
        Assert.Null(secondBlock.MasterRef);
        
        // Verify second block prev_refs
        Assert.NotNull(secondBlock.PreviousReferences);
        Assert.Single(secondBlock.PreviousReferences);
        Assert.Equal("(-1,8000000000000000,123455)", secondBlock.PreviousReferences[0]);
        
        // Verify second block value_flow with other currencies
        Assert.NotNull(secondBlock.ValueFlow);
        Assert.NotNull(secondBlock.ValueFlow.FromPreviousBlock.Other);
        Assert.Equal(2, secondBlock.ValueFlow.FromPreviousBlock.Other.Count);
        Assert.Equal(239ul, secondBlock.ValueFlow.FromPreviousBlock.Other[0].Id);
        Assert.Equal("666666666666", secondBlock.ValueFlow.FromPreviousBlock.Other[0].Value);
        Assert.Equal(4294967279ul, secondBlock.ValueFlow.FromPreviousBlock.Other[1].Id);
        Assert.Equal("1000000000000", secondBlock.ValueFlow.FromPreviousBlock.Other[1].Value);
        
        // Verify second block fees
        Assert.Equal(2700000000ul, secondBlock.ValueFlow.FeesCollected.Grams);
        Assert.Equal(1000000000ul, secondBlock.ValueFlow.FeesImported.Grams);
        Assert.Equal(2700000000ul, secondBlock.ValueFlow.Recovered.Grams);
        Assert.Equal(1700000000ul, secondBlock.ValueFlow.Created.Grams);
        
        // Verify all blocks have valid seqno
        Assert.All(result.Blocks, block => Assert.True(block.Seqno > 0));
        
        // Verify all blocks have global_id = -3 (testnet)
        Assert.All(result.Blocks, block => Assert.Equal(-3, block.GlobalId));
    }

    [Fact]
    public async Task GetMasterchainTransactionsAsync_WithSeqno_ReturnsTransactionsList()
    {
        // Arrange
        var masterchainSeqno = 123456;

        // Act
        var result = await Client.Blockchain.GetMasterchainTransactionsAsync(masterchainSeqno);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.TransactionsList);
        Assert.Equal(3, result.TransactionsList.Count);
        
        // Verify first transaction (Elector Contract - TransTickTock)
        var firstTx = result.TransactionsList[0];
        Assert.NotNull(firstTx);
        Assert.Equal("21278d1ae7423e4cc6238604937ea76c331b05837d607a1ebd8f87268082dbd4", firstTx.Hash);
        Assert.Equal(148466000001ul, firstTx.Lt);
        Assert.NotNull(firstTx.Account);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", firstTx.Account.Address);
        Assert.Equal("Elector Contract", firstTx.Account.Name);
        Assert.False(firstTx.Account.IsScam);
        Assert.False(firstTx.Account.IsWallet);
        Assert.True(firstTx.Success);
        Assert.Equal(1653657358ul, firstTx.Utime);
        Assert.Equal("active", firstTx.OrigStatus);
        Assert.Equal("active", firstTx.EndStatus);
        Assert.Equal(0ul, firstTx.TotalFees);
        Assert.Equal(3449520159136989ul, firstTx.EndBalance);
        Assert.Equal("TransTickTock", firstTx.TransactionType);
        Assert.Equal("eeaaa3dcb67be37482e6bb6022daf0f06c809e7856a9390c7e32a34016229667", firstTx.StateUpdateOld);
        Assert.Equal("5de639c189ecb7f81e9f6576ae30196e6c9b0165e5964192bf5cc82db2972548", firstTx.StateUpdateNew);
        Assert.Empty(firstTx.OutMsgs);
        Assert.Equal("(-1,8000000000000000,123456)", firstTx.Block);
        Assert.Equal("e01dc6354e525f4cc729c5b3e323d663b3b08a98756e321d962ef88c88be9649", firstTx.PrevTransHash);
        Assert.Equal(148465000002ul, firstTx.PrevTransLt);
        Assert.False(firstTx.Aborted);
        Assert.False(firstTx.Destroyed);
        
        // Verify first transaction compute_phase
        Assert.NotNull(firstTx.ComputePhase);
        Assert.False(firstTx.ComputePhase.Skipped);
        Assert.True(firstTx.ComputePhase.Success);
        Assert.Equal(0ul, firstTx.ComputePhase.GasFees);
        Assert.Equal(7752ul, firstTx.ComputePhase.GasUsed);
        Assert.Equal(159, firstTx.ComputePhase.VmSteps);
        Assert.Equal(0, firstTx.ComputePhase.ExitCode);
        
        // Verify first transaction action_phase
        Assert.NotNull(firstTx.ActionPhase);
        Assert.True(firstTx.ActionPhase.Success);
        Assert.Equal(0, firstTx.ActionPhase.ResultCode);
        Assert.Equal(0, firstTx.ActionPhase.TotalActions);
        Assert.Equal(0, firstTx.ActionPhase.SkippedActions);
        Assert.Equal(0ul, firstTx.ActionPhase.FwdFees);
        Assert.Equal(0ul, firstTx.ActionPhase.TotalFees);
        
        // Verify second transaction (Elector Contract - TransOrd with in_msg)
        var secondTx = result.TransactionsList[1];
        Assert.NotNull(secondTx);
        Assert.Equal("88c163c244d12bb583c0e84660cf0f65a6631d78ac71a5f41c3ca01397254962", secondTx.Hash);
        Assert.Equal(148466000002ul, secondTx.Lt);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", secondTx.Account.Address);
        Assert.Equal("Elector Contract", secondTx.Account.Name);
        Assert.True(secondTx.Success);
        Assert.Equal("TransOrd", secondTx.TransactionType);
        Assert.Equal(3449522859136989ul, secondTx.EndBalance);
        
        // Verify second transaction has in_msg
        Assert.NotNull(secondTx.InMsg);
        Assert.Equal("int_msg", secondTx.InMsg.MessageType);
        Assert.Equal(148466000000L, secondTx.InMsg.CreatedLt);
        Assert.True(secondTx.InMsg.IhrDisabled == true);
        Assert.True(secondTx.InMsg.Bounce == true);
        Assert.False(secondTx.InMsg.Bounced == true);
        Assert.Equal(2700000000L, secondTx.InMsg.Value);
        Assert.Equal(0ul, secondTx.InMsg.FwdFee);
        Assert.Equal(0ul, secondTx.InMsg.IhrFee);
        
        // Verify in_msg destination
        Assert.NotNull(secondTx.InMsg.Destination);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", secondTx.InMsg.Destination.Address);
        Assert.Equal("Elector Contract", secondTx.InMsg.Destination.Name);
        
        // Verify in_msg source
        Assert.NotNull(secondTx.InMsg.Source);
        Assert.Equal("-1:0000000000000000000000000000000000000000000000000000000000000000", secondTx.InMsg.Source.Address);
        Assert.Equal("System", secondTx.InMsg.Source.Name);
        
        Assert.Equal(0ul, secondTx.InMsg.ImportFee);
        Assert.Equal(1653657358ul, secondTx.InMsg.CreatedAt);
        Assert.Equal("a1edb13cfebd875d2a41b4c8536e7262b3e288355f2a7e267f268f3caa76b9e8", secondTx.InMsg.Hash);
        
        // Verify second transaction has no out_msgs
        Assert.Empty(secondTx.OutMsgs);
        
        // Verify second transaction compute_phase
        Assert.NotNull(secondTx.ComputePhase);
        Assert.False(secondTx.ComputePhase.Skipped);
        Assert.True(secondTx.ComputePhase.Success);
        Assert.Equal(0ul, secondTx.ComputePhase.GasFees);
        Assert.Equal(4874ul, secondTx.ComputePhase.GasUsed);
        Assert.Equal(100, secondTx.ComputePhase.VmSteps);
        Assert.Equal(0, secondTx.ComputePhase.ExitCode);
        
        // Verify second transaction credit_phase
        Assert.NotNull(secondTx.CreditPhase);
        Assert.Equal(0ul, secondTx.CreditPhase.FeesCollected);
        Assert.Equal(2700000000ul, secondTx.CreditPhase.Credit);
        
        // Verify second transaction action_phase
        Assert.NotNull(secondTx.ActionPhase);
        Assert.True(secondTx.ActionPhase.Success);
        Assert.Equal(0, secondTx.ActionPhase.ResultCode);
        
        // Verify third transaction (Config Contract - TransTickTock)
        var thirdTx = result.TransactionsList[2];
        Assert.NotNull(thirdTx);
        Assert.Equal("a80de2e5155ee06863878fc2439b5c0cd83e82a9bcd832b2bfe19cb20d096dbe", thirdTx.Hash);
        Assert.Equal(148466000003ul, thirdTx.Lt);
        Assert.NotNull(thirdTx.Account);
        Assert.Equal("-1:5555555555555555555555555555555555555555555555555555555555555555", thirdTx.Account.Address);
        Assert.Equal("Config Contract", thirdTx.Account.Name);
        Assert.False(thirdTx.Account.IsScam);
        Assert.False(thirdTx.Account.IsWallet);
        Assert.True(thirdTx.Success);
        Assert.Equal(1653657358ul, thirdTx.Utime);
        Assert.Equal("active", thirdTx.OrigStatus);
        Assert.Equal("active", thirdTx.EndStatus);
        Assert.Equal(0ul, thirdTx.TotalFees);
        Assert.Equal(10000000000ul, thirdTx.EndBalance);
        Assert.Equal("TransTickTock", thirdTx.TransactionType);
        Assert.Equal("6a435aa5daa0e573db31bc4437a1cf36a20d1adf663ca0ddb68944b6dac28437", thirdTx.StateUpdateOld);
        Assert.Equal("c542e2636528606782340d4c04acc63796040d5b068eec3155a0f6877f44f996", thirdTx.StateUpdateNew);
        Assert.Empty(thirdTx.OutMsgs);
        Assert.Equal("(-1,8000000000000000,123456)", thirdTx.Block);
        Assert.Equal("127627e6338f259b8fc3c330392679723ea1a1ee8843f381ef2bf327a255644f", thirdTx.PrevTransHash);
        Assert.Equal(148465000003ul, thirdTx.PrevTransLt);
        Assert.False(thirdTx.Aborted);
        Assert.False(thirdTx.Destroyed);
        
        // Verify third transaction compute_phase
        Assert.NotNull(thirdTx.ComputePhase);
        Assert.False(thirdTx.ComputePhase.Skipped);
        Assert.True(thirdTx.ComputePhase.Success);
        Assert.Equal(0ul, thirdTx.ComputePhase.GasFees);
        Assert.Equal(2179ul, thirdTx.ComputePhase.GasUsed);
        Assert.Equal(46, thirdTx.ComputePhase.VmSteps);
        Assert.Equal(0, thirdTx.ComputePhase.ExitCode);
        
        // Verify third transaction action_phase
        Assert.NotNull(thirdTx.ActionPhase);
        Assert.True(thirdTx.ActionPhase.Success);
        Assert.Equal(0, thirdTx.ActionPhase.ResultCode);
        Assert.Equal(0, thirdTx.ActionPhase.TotalActions);
        
        // Verify all transactions are successful
        Assert.All(result.TransactionsList, tx => Assert.True(tx.Success));
        
        // Verify all transactions are from the same block
        Assert.All(result.TransactionsList, tx => Assert.Equal("(-1,8000000000000000,123456)", tx.Block));
        
        // Verify all transactions have same utime
        Assert.All(result.TransactionsList, tx => Assert.Equal(1653657358ul, tx.Utime));
    }

    [Fact]
    public async Task GetConfigAsync_WithMasterchainSeqno_ReturnsBlockchainConfig()
    {
        // Arrange
        var masterchainSeqno = 38358951;

        // Act
        var result = await Client.Blockchain.GetConfigAsync(masterchainSeqno);

        // Assert
        Assert.NotNull(result);
        
        // Verify main contract addresses
        Assert.Equal("-1:5555555555555555555555555555555555555555555555555555555555555555", result.ConfigAddress);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", result.ElectorAddress);
        Assert.Equal("-1:c13e44a8368271bcc03590cf609dc8a43e874ba2fde3489cb850727822cba5ad", result.MinterAddress);
        Assert.Equal("-1:efe71d13860afaa6aeaeaf636f9168487f80f1031b0bf8d939ae49d3ea7f7da0", result.DnsRootAddress);
        
        // Verify raw configuration data is present
        Assert.NotNull(result.Raw);
        Assert.NotEmpty(result.Raw);
        Assert.True(result.Raw.Length > 1000); // Raw config should be a large base64 string
        
        // Verify additional configuration parameters are present
        Assert.NotNull(result.AdditionalData);
        Assert.True(result.AdditionalData.Count > 0);
        
        // Verify some key configuration parameters exist in additional data
        Assert.True(result.AdditionalData.ContainsKey("8")); // Version and capabilities
        Assert.True(result.AdditionalData.ContainsKey("15")); // Validator election params
        Assert.True(result.AdditionalData.ContainsKey("16")); // Validator count params
        Assert.True(result.AdditionalData.ContainsKey("17")); // Stake params
        Assert.True(result.AdditionalData.ContainsKey("32")); // Current validators (utime_since: 1765282761)
        Assert.True(result.AdditionalData.ContainsKey("34")); // Next validators (utime_since: 1765297161)
        Assert.True(result.AdditionalData.ContainsKey("36")); // Future validators (utime_since: 1765311561)
    }

    [Fact]
    public async Task GetBlockTransactionsAsync_WithBlockId_ReturnsBlockTransactions()
    {
        // Arrange
        var blockId = "(-1,8000000000000000,4234234)";

        // Act
        var result = await Client.Blockchain.GetBlockTransactionsAsync(blockId);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.TransactionsList);
        Assert.Equal(3, result.TransactionsList.Count);
        
        // Verify first transaction (Elector Contract - TransTickTock)
        var firstTx = result.TransactionsList[0];
        Assert.NotNull(firstTx);
        Assert.Equal("dc31ff965ff5296142f51ff7cdeef1a9b1a0635c45d2c0dbc2da0db7eae0c254", firstTx.Hash);
        Assert.Equal(5154035000001ul, firstTx.Lt);
        Assert.NotNull(firstTx.Account);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", firstTx.Account.Address);
        Assert.Equal("Elector Contract", firstTx.Account.Name);
        Assert.False(firstTx.Account.IsScam);
        Assert.False(firstTx.Account.IsWallet);
        Assert.True(firstTx.Success == true);
        Assert.Equal(1666230405ul, firstTx.Utime);
        Assert.Equal("active", firstTx.OrigStatus);
        Assert.Equal("active", firstTx.EndStatus);
        Assert.Equal(0ul, firstTx.TotalFees);
        Assert.Equal(3327930204687821ul, firstTx.EndBalance);
        Assert.Equal("TransTickTock", firstTx.TransactionType);
        Assert.Equal("c0712c6904dd3ff8e5cf325334f5fccc9013a51f06f816556f6d877c87343451", firstTx.StateUpdateOld);
        Assert.Equal("49ed509478848a99679bf6a812b34493dcd087e0d996dfb7ae3fc521fa2092c3", firstTx.StateUpdateNew);
        Assert.Empty(firstTx.OutMsgs);
        Assert.Equal("(-1,8000000000000000,4234234)", firstTx.Block);
        Assert.Equal("ece84a1e95c25bebd47509c8900ed8fc9c243f47cd1278b92a2cdbe2fceea4ff", firstTx.PrevTransHash);
        Assert.Equal(5154034000002ul, firstTx.PrevTransLt);
        Assert.False(firstTx.Aborted);
        Assert.False(firstTx.Destroyed == true);
        
        // Verify first transaction compute_phase
        Assert.NotNull(firstTx.ComputePhase);
        Assert.False(firstTx.ComputePhase.Skipped);
        Assert.True(firstTx.ComputePhase.Success == true);
        Assert.Equal(0ul, firstTx.ComputePhase.GasFees);
        Assert.Equal(7952ul, firstTx.ComputePhase.GasUsed);
        Assert.Equal(159, firstTx.ComputePhase.VmSteps);
        Assert.Equal(0, firstTx.ComputePhase.ExitCode);
        Assert.Equal("Ok", firstTx.ComputePhase.ExitCodeDescription);
        
        // Verify first transaction action_phase
        Assert.NotNull(firstTx.ActionPhase);
        Assert.True(firstTx.ActionPhase.Success);
        Assert.Equal(0, firstTx.ActionPhase.ResultCode);
        Assert.Equal(0, firstTx.ActionPhase.TotalActions);
        Assert.Equal(0, firstTx.ActionPhase.SkippedActions);
        Assert.Equal(0ul, firstTx.ActionPhase.FwdFees);
        Assert.Equal(0ul, firstTx.ActionPhase.TotalFees);
        
        // Verify second transaction (Elector Contract - TransOrd with in_msg)
        var secondTx = result.TransactionsList[1];
        Assert.NotNull(secondTx);
        Assert.Equal("a0877a5dfce8d4a1ad2a4faf7255924d1ecf4e6c4af772e95ad10357b4410f77", secondTx.Hash);
        Assert.Equal(5154035000002ul, secondTx.Lt);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", secondTx.Account.Address);
        Assert.Equal("Elector Contract", secondTx.Account.Name);
        Assert.True(secondTx.Success == true);
        Assert.Equal("TransOrd", secondTx.TransactionType);
        Assert.Equal(3327932904687821ul, secondTx.EndBalance);
        
        // Verify second transaction has in_msg
        Assert.NotNull(secondTx.InMsg);
        Assert.Equal("int_msg", secondTx.InMsg.MessageType);
        Assert.Equal(5154035000000L, secondTx.InMsg.CreatedLt);
        Assert.True(secondTx.InMsg.IhrDisabled == true);
        Assert.True(secondTx.InMsg.Bounce == true);
        Assert.False(secondTx.InMsg.Bounced == true);
        Assert.Equal(2700000000L, secondTx.InMsg.Value);
        Assert.Equal(0ul, secondTx.InMsg.FwdFee);
        Assert.Equal(0ul, secondTx.InMsg.IhrFee);
        
        // Verify in_msg destination
        Assert.NotNull(secondTx.InMsg.Destination);
        Assert.Equal("-1:3333333333333333333333333333333333333333333333333333333333333333", secondTx.InMsg.Destination.Address);
        Assert.Equal("Elector Contract", secondTx.InMsg.Destination.Name);
        
        // Verify in_msg source
        Assert.NotNull(secondTx.InMsg.Source);
        Assert.Equal("-1:0000000000000000000000000000000000000000000000000000000000000000", secondTx.InMsg.Source.Address);
        Assert.Equal("System", secondTx.InMsg.Source.Name);
        
        Assert.Equal(0ul, secondTx.InMsg.ImportFee);
        Assert.Equal(1666230405ul, secondTx.InMsg.CreatedAt);
        Assert.Equal("f44a815383248ba7f6f3426c9bfcc876a5340878886c85d6c847ba096bf05b0a", secondTx.InMsg.Hash);
        
        // Verify second transaction has no out_msgs
        Assert.Empty(secondTx.OutMsgs);
        
        // Verify second transaction compute_phase
        Assert.NotNull(secondTx.ComputePhase);
        Assert.False(secondTx.ComputePhase.Skipped);
        Assert.True(secondTx.ComputePhase.Success == true);
        Assert.Equal(0ul, secondTx.ComputePhase.GasFees);
        Assert.Equal(4874ul, secondTx.ComputePhase.GasUsed);
        Assert.Equal(100, secondTx.ComputePhase.VmSteps);
        Assert.Equal(0, secondTx.ComputePhase.ExitCode);
        
        // Verify second transaction credit_phase
        Assert.NotNull(secondTx.CreditPhase);
        Assert.Equal(0ul, secondTx.CreditPhase.FeesCollected);
        Assert.Equal(2700000000ul, secondTx.CreditPhase.Credit);
        
        // Verify second transaction action_phase
        Assert.NotNull(secondTx.ActionPhase);
        Assert.True(secondTx.ActionPhase.Success);
        Assert.Equal(0, secondTx.ActionPhase.ResultCode);
        
        // Verify third transaction (Config Contract - TransTickTock)
        var thirdTx = result.TransactionsList[2];
        Assert.NotNull(thirdTx);
        Assert.Equal("3fa6f0123211b240345fada49d361267caee3b564fdb4380981caffd652fde6a", thirdTx.Hash);
        Assert.Equal(5154035000003ul, thirdTx.Lt);
        Assert.NotNull(thirdTx.Account);
        Assert.Equal("-1:5555555555555555555555555555555555555555555555555555555555555555", thirdTx.Account.Address);
        Assert.Equal("Config Contract", thirdTx.Account.Name);
        Assert.False(thirdTx.Account.IsScam);
        Assert.False(thirdTx.Account.IsWallet);
        Assert.True(thirdTx.Success == true);
        Assert.Equal(1666230405ul, thirdTx.Utime);
        Assert.Equal("active", thirdTx.OrigStatus);
        Assert.Equal("active", thirdTx.EndStatus);
        Assert.Equal(0ul, thirdTx.TotalFees);
        Assert.Equal(20000000000ul, thirdTx.EndBalance);
        Assert.Equal("TransTickTock", thirdTx.TransactionType);
        Assert.Equal("34a91f45d78704de80cfd93a572d0916da7491e2fb7399f55b5fbb48b9caaa76", thirdTx.StateUpdateOld);
        Assert.Equal("d96e3e1277a9db6a65e1eb54f0e8488eddbf083dc529579cbf012f77a70fa631", thirdTx.StateUpdateNew);
        Assert.Empty(thirdTx.OutMsgs);
        Assert.Equal("(-1,8000000000000000,4234234)", thirdTx.Block);
        Assert.Equal("eb95a3635a22dd6aa5d31afd7c6f8a13904a30ef8ffe33700e4dc7132ffa7e22", thirdTx.PrevTransHash);
        Assert.Equal(5154034000003ul, thirdTx.PrevTransLt);
        Assert.False(thirdTx.Aborted);
        Assert.False(thirdTx.Destroyed == true);
        
        // Verify third transaction compute_phase
        Assert.NotNull(thirdTx.ComputePhase);
        Assert.False(thirdTx.ComputePhase.Skipped);
        Assert.True(thirdTx.ComputePhase.Success == true);
        Assert.Equal(0ul, thirdTx.ComputePhase.GasFees);
        Assert.Equal(2279ul, thirdTx.ComputePhase.GasUsed);
        Assert.Equal(46, thirdTx.ComputePhase.VmSteps);
        Assert.Equal(0, thirdTx.ComputePhase.ExitCode);
        
        // Verify third transaction action_phase
        Assert.NotNull(thirdTx.ActionPhase);
        Assert.True(thirdTx.ActionPhase.Success);
        Assert.Equal(0, thirdTx.ActionPhase.ResultCode);
        Assert.Equal(0, thirdTx.ActionPhase.TotalActions);
        
        // Verify all transactions are successful
        Assert.All(result.TransactionsList, tx => Assert.True(tx.Success == true));
        
        // Verify all transactions are from the same block
        Assert.All(result.TransactionsList, tx => Assert.Equal("(-1,8000000000000000,4234234)", tx.Block));
        
        // Verify all transactions have same utime
        Assert.All(result.TransactionsList, tx => Assert.Equal(1666230405ul, tx.Utime));
    }

    [Fact]
    public async Task GetTransactionAsync_WithTransactionHash_ReturnsTransactionDetails()
    {
        // Arrange
        var transactionHash = "6bc9d4a39a5e101b8661c570d1c2b79de4f731838aca27fde84de7de7025d5c0";

        // Act
        var tx = await Client.Blockchain.GetTransactionAsync(transactionHash);

        // Assert
        Assert.NotNull(tx);
        
        // Verify transaction basic info
        Assert.Equal("6bc9d4a39a5e101b8661c570d1c2b79de4f731838aca27fde84de7de7025d5c0", tx.Hash);
        Assert.Equal(42243750000001ul, tx.Lt);
        Assert.NotNull(tx.Account);
        Assert.Equal("0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e", tx.Account.Address);
        Assert.False(tx.Account.IsScam);
        Assert.True(tx.Account.IsWallet);
        Assert.True(tx.Success == true);
        Assert.Equal(1765309460ul, tx.Utime);
        
        // Verify status change (wallet initialization)
        Assert.Equal("uninit", tx.OrigStatus);
        Assert.Equal("active", tx.EndStatus);
        
        // Verify transaction details
        Assert.Equal(5638932ul, tx.TotalFees);
        Assert.Equal(0ul, tx.EndBalance);
        Assert.Equal("TransOrd", tx.TransactionType);
        Assert.Equal("91596f42f44bb047ed000164182e8c43f985edccf2fac75bce3b8db4e4464ba8", tx.StateUpdateOld);
        Assert.Equal("11c1197174e7660d3af0d449c7a5ca61bdd7b949a26ddfcf448e14167c8f6847", tx.StateUpdateNew);
        
        // Verify in_msg (external message with init)
        Assert.NotNull(tx.InMsg);
        Assert.Equal("ext_in_msg", tx.InMsg.MessageType);
        Assert.Equal(0L, tx.InMsg.CreatedLt);
        Assert.False(tx.InMsg.IhrDisabled == true);
        Assert.False(tx.InMsg.Bounce == true);
        Assert.False(tx.InMsg.Bounced == true);
        Assert.Equal(0L, tx.InMsg.Value);
        Assert.Equal(0ul, tx.InMsg.FwdFee);
        Assert.Equal(0ul, tx.InMsg.IhrFee);
        
        // Verify in_msg destination
        Assert.NotNull(tx.InMsg.Destination);
        Assert.Equal("0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e", tx.InMsg.Destination.Address);
        Assert.False(tx.InMsg.Destination.IsScam);
        Assert.True(tx.InMsg.Destination.IsWallet);
        
        // Verify in_msg has no source (external message)
        Assert.Null(tx.InMsg.Source);
        
        // Verify in_msg details
        Assert.Equal(0ul, tx.InMsg.ImportFee);
        Assert.Equal(0ul, tx.InMsg.CreatedAt);
        Assert.Equal("0x7369676e", tx.InMsg.OpCode);
        Assert.Equal("d967cc46e3f9bec47df4e7416ae0c5cadb47acba71a8d4c1f83ebddf771afdf3", tx.InMsg.Hash);
        Assert.NotNull(tx.InMsg.RawBody);
        Assert.NotEmpty(tx.InMsg.RawBody);
        Assert.Equal("wallet_signed_external_v5_r1", tx.InMsg.DecodedOpName);
        Assert.NotNull(tx.InMsg.DecodedBody);
        
        // Verify in_msg has init (wallet initialization)
        Assert.NotNull(tx.InMsg.Init);
        Assert.NotNull(tx.InMsg.Init.Boc);
        Assert.NotEmpty(tx.InMsg.Init.Boc);
        Assert.True(tx.InMsg.Init.Boc!.Length > 100); // Large BOC data
        Assert.NotNull(tx.InMsg.Init.Interfaces);
        Assert.Single(tx.InMsg.Init.Interfaces);
        Assert.Equal("wallet_v5r1", tx.InMsg.Init.Interfaces[0]);
        
        // Verify out_msgs (internal message)
        Assert.NotNull(tx.OutMsgs);
        Assert.Single(tx.OutMsgs);
        
        var outMsg = tx.OutMsgs[0];
        Assert.NotNull(outMsg);
        Assert.Equal("int_msg", outMsg.MessageType);
        Assert.Equal(42243750000002L, outMsg.CreatedLt);
        Assert.True(outMsg.IhrDisabled == true);
        Assert.False(outMsg.Bounce == true);
        Assert.False(outMsg.Bounced == true);
        Assert.Equal(221218183L, outMsg.Value);
        Assert.Equal(266669ul, outMsg.FwdFee);
        Assert.Equal(0ul, outMsg.IhrFee);
        
        // Verify out_msg destination and source
        Assert.NotNull(outMsg.Destination);
        Assert.Equal("0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e", outMsg.Destination.Address);
        Assert.NotNull(outMsg.Source);
        Assert.Equal("0:166ee3201c967f2dbd85ed916da9eeb4b2cfea0afa7f7b4f0302597461f7eb5e", outMsg.Source.Address);
        
        Assert.Equal(0ul, outMsg.ImportFee);
        Assert.Equal(1765309460ul, outMsg.CreatedAt);
        Assert.Equal("0x00000000", outMsg.OpCode);
        Assert.Equal("1ec889694711b52281b76a2ac649afab84c911048428f086cb80e4bbfcdf59bb", outMsg.Hash);
        Assert.NotNull(outMsg.RawBody);
        Assert.Equal("text_comment", outMsg.DecodedOpName);
        Assert.NotNull(outMsg.DecodedBody);
        
        // Verify block and previous transaction
        Assert.Equal("(0,2000000000000000,40215134)", tx.Block);
        Assert.Equal("25c1033571cc8738383e49a4a2ae7c06d76c1495fc919454db76aae0eb73bf2a", tx.PrevTransHash);
        Assert.Equal(42243735000001ul, tx.PrevTransLt);
        Assert.False(tx.Aborted);
        Assert.False(tx.Destroyed == true);
        
        // Verify compute_phase
        Assert.NotNull(tx.ComputePhase);
        Assert.False(tx.ComputePhase.Skipped);
        Assert.True(tx.ComputePhase.Success == true);
        Assert.Equal(1975600ul, tx.ComputePhase.GasFees);
        Assert.Equal(4939ul, tx.ComputePhase.GasUsed);
        Assert.Equal(143, tx.ComputePhase.VmSteps);
        Assert.Equal(0, tx.ComputePhase.ExitCode);
        Assert.Equal("Ok", tx.ComputePhase.ExitCodeDescription);
        
        // Verify storage_phase (wallet initialization phase)
        Assert.NotNull(tx.StoragePhase);
        Assert.Equal(1ul, tx.StoragePhase.FeesCollected);
        Assert.Equal("acst_unchanged", tx.StoragePhase.StatusChange);
        
        // Verify action_phase
        Assert.NotNull(tx.ActionPhase);
        Assert.True(tx.ActionPhase.Success);
        Assert.Equal(0, tx.ActionPhase.ResultCode);
        Assert.Equal(1, tx.ActionPhase.TotalActions);
        Assert.Equal(0, tx.ActionPhase.SkippedActions);
        Assert.Equal(400000ul, tx.ActionPhase.FwdFees);
        Assert.Equal(133331ul, tx.ActionPhase.TotalFees);
        
        // Verify raw transaction data is present
        Assert.NotNull(tx.Raw);
        Assert.NotEmpty(tx.Raw);
        Assert.True(tx.Raw.Length > 100); // Raw should be a large base64 string
    }

    [Fact]
    public async Task GetTransactionByMessageHashAsync_WithMessageHash_ReturnsTransactionDetails()
    {
        // Arrange
        var messageHash = "98077afb68937de0566ae443966288be7a7fc197f91094fa394af362bb044cea";

        // Act
        var tx = await Client.Blockchain.GetTransactionByMessageHashAsync(messageHash);

        // Assert
        Assert.NotNull(tx);
        
        // Verify transaction basic info
        Assert.Equal("6ba63411fd1c3e6b87318a2050ee2cf2743ec4d70416529039bd65465169a41d", tx.Hash);
        Assert.Equal(26199401000001ul, tx.Lt);
        Assert.NotNull(tx.Account);
        Assert.Equal("0:e41b40882a6df40ebbe7ada243fd289f8be77901861acd882f97e9441f4460ff", tx.Account.Address);
        Assert.False(tx.Account.IsScam);
        Assert.False(tx.Account.IsWallet);
        Assert.True(tx.Success);
        Assert.Equal(1727098516ul, tx.Utime);
        Assert.Equal("active", tx.OrigStatus);
        Assert.Equal("active", tx.EndStatus);
        Assert.Equal(4924954ul, tx.TotalFees);
        Assert.Equal(349984825ul, tx.EndBalance);
        Assert.Equal("TransOrd", tx.TransactionType);
        Assert.Equal("55846c90468d913b2a611dec6a62caeb26595c8fe5cf7ee41f54174031194781", tx.StateUpdateOld);
        Assert.Equal("622eaeb2234ba8cbae7f7e99ac40488166fe670ef6c4d856955866561641eddf", tx.StateUpdateNew);
        
        // Verify in_msg (jetton transfer message)
        Assert.NotNull(tx.InMsg);
        Assert.Equal("int_msg", tx.InMsg.MessageType);
        Assert.Equal(26199398000002L, tx.InMsg.CreatedLt);
        Assert.True(tx.InMsg.IhrDisabled);
        Assert.True(tx.InMsg.Bounce);
        Assert.False(tx.InMsg.Bounced);
        Assert.Equal(50000000L, tx.InMsg.Value);
        Assert.Equal(642139ul, tx.InMsg.FwdFee);
        Assert.Equal(0ul, tx.InMsg.IhrFee);
        
        // Verify in_msg destination
        Assert.NotNull(tx.InMsg.Destination);
        Assert.Equal("0:e41b40882a6df40ebbe7ada243fd289f8be77901861acd882f97e9441f4460ff", tx.InMsg.Destination.Address);
        Assert.False(tx.InMsg.Destination.IsScam);
        Assert.False(tx.InMsg.Destination.IsWallet);
        
        // Verify in_msg source
        Assert.NotNull(tx.InMsg.Source);
        Assert.Equal("0:94aa09fe231de4bb384f02428a8aaa9741acec27df0add54828b8409dd94c60b", tx.InMsg.Source.Address);
        Assert.False(tx.InMsg.Source.IsScam);
        Assert.True(tx.InMsg.Source.IsWallet);
        
        Assert.Equal(0ul, tx.InMsg.ImportFee);
        Assert.Equal(1727098507ul, tx.InMsg.CreatedAt);
        Assert.Equal("0x0f8a7ea5", tx.InMsg.OpCode);
        Assert.Equal("98077afb68937de0566ae443966288be7a7fc197f91094fa394af362bb044cea", tx.InMsg.Hash);
        Assert.NotNull(tx.InMsg.RawBody);
        Assert.NotEmpty(tx.InMsg.RawBody);
        Assert.Equal("jetton_transfer", tx.InMsg.DecodedOpName);
        Assert.NotNull(tx.InMsg.DecodedBody);
        
        // Verify out_msgs (jetton internal transfer)
        Assert.NotNull(tx.OutMsgs);
        Assert.Single(tx.OutMsgs);
        
        var outMsg = tx.OutMsgs[0];
        Assert.NotNull(outMsg);
        Assert.Equal("int_msg", outMsg.MessageType);
        Assert.Equal(26199401000002L, outMsg.CreatedLt);
        Assert.True(outMsg.IhrDisabled);
        Assert.True(outMsg.Bounce);
        Assert.False(outMsg.Bounced);
        Assert.Equal(42190000L, outMsg.Value);
        Assert.Equal(2885089ul, outMsg.FwdFee);
        Assert.Equal(0ul, outMsg.IhrFee);
        
        // Verify out_msg destination and source
        Assert.NotNull(outMsg.Destination);
        Assert.Equal("0:6415bf41c9412b10665c77ec744eeca2d20dfc1f2477dc3397ae583edf3ca3fc", outMsg.Destination.Address);
        Assert.False(outMsg.Destination.IsScam);
        Assert.False(outMsg.Destination.IsWallet);
        
        Assert.NotNull(outMsg.Source);
        Assert.Equal("0:e41b40882a6df40ebbe7ada243fd289f8be77901861acd882f97e9441f4460ff", outMsg.Source.Address);
        Assert.False(outMsg.Source.IsScam);
        Assert.False(outMsg.Source.IsWallet);
        
        Assert.Equal(0ul, outMsg.ImportFee);
        Assert.Equal(1727098516ul, outMsg.CreatedAt);
        Assert.Equal("0x178d4519", outMsg.OpCode);
        Assert.Equal("jetton_internal_transfer", outMsg.DecodedOpName);
        Assert.NotNull(outMsg.DecodedBody);
        
        // Verify out_msg has init (jetton wallet initialization)
        Assert.NotNull(outMsg.Init);
        Assert.NotNull(outMsg.Init.Boc);
        Assert.NotEmpty(outMsg.Init.Boc);
        Assert.NotNull(outMsg.Init.Interfaces);
        Assert.Single(outMsg.Init.Interfaces);
        Assert.Equal("jetton_wallet_v1", outMsg.Init.Interfaces[0]);
        
        // Verify block and previous transaction
        Assert.Equal("(0,e000000000000000,24965068)", tx.Block);
        Assert.Equal("f2afaf969f39b9cfc57278371d08dd8720b370c49083581d4393c903c9508016", tx.PrevTransHash);
        Assert.Equal(26199332000001ul, tx.PrevTransLt);
        Assert.False(tx.Aborted);
        Assert.False(tx.Destroyed);
        
        // Verify compute_phase
        Assert.NotNull(tx.ComputePhase);
        Assert.False(tx.ComputePhase.Skipped);
        Assert.True(tx.ComputePhase.Success);
        Assert.Equal(3482400ul, tx.ComputePhase.GasFees);
        Assert.Equal(8706ul, tx.ComputePhase.GasUsed);
        Assert.Equal(175, tx.ComputePhase.VmSteps);
        Assert.Equal(0, tx.ComputePhase.ExitCode);
        Assert.Equal("Ok", tx.ComputePhase.ExitCodeDescription);
        
        // Verify storage_phase
        Assert.NotNull(tx.StoragePhase);
        Assert.Equal(43ul, tx.StoragePhase.FeesCollected);
        Assert.Equal("acst_unchanged", tx.StoragePhase.StatusChange);
        
        // Verify credit_phase
        Assert.NotNull(tx.CreditPhase);
        Assert.Equal(0ul, tx.CreditPhase.FeesCollected);
        Assert.Equal(50000000ul, tx.CreditPhase.Credit);
        
        // Verify action_phase
        Assert.NotNull(tx.ActionPhase);
        Assert.True(tx.ActionPhase.Success);
        Assert.Equal(0, tx.ActionPhase.ResultCode);
        Assert.Equal(1, tx.ActionPhase.TotalActions);
        Assert.Equal(0, tx.ActionPhase.SkippedActions);
        Assert.Equal(4327600ul, tx.ActionPhase.FwdFees);
        Assert.Equal(1442511ul, tx.ActionPhase.TotalFees);
        
        // Verify raw transaction data is present
        Assert.NotNull(tx.Raw);
        Assert.NotEmpty(tx.Raw);
        Assert.True(tx.Raw.Length > 100); // Raw should be a large base64 string
    }

    [Fact]
    public async Task GetMasterchainHead()
    {
        // Arrange & Act
        var head = await Client.Blockchain.GetMasterchainHeadAsync();

        //// Assert
        Assert.NotNull(head);
        Assert.True(head.Seqno > 0);
    }

    [Fact]
    public async Task GetAccountAsync_WithUninitAddress_ReturnsAccountInfo()
    {
        // Arrange
        var address = "0:97264395bd65a255a429b11326c84128b7d70ffed7949abae3036d506ba38621";

        // Act
        var account = await Client.Blockchain.GetAccountAsync(address);

        // Assert
        Assert.NotNull(account);
        Assert.Equal("0:97264395bd65a255a429b11326c84128b7d70ffed7949abae3036d506ba38621", account.Address);
        Assert.Equal(1L, account.Balance);
        Assert.Equal(30283411000001ul, account.LastTransactionLt);
        Assert.Equal("5961f02bff926c999fb6fcdcee9556fb0bb1f80abb4e5bbba37556e745c9d27b", account.LastTransactionHash);
        Assert.Equal("uninit", account.Status);
        
        Assert.NotNull(account.Storage);
        Assert.Equal(1L, account.Storage.UsedCells);
        Assert.Equal(79L, account.Storage.UsedBits);
        Assert.Equal(0L, account.Storage.UsedPublicCells);
        Assert.Equal(1737024140L, account.Storage.LastPaid);
        Assert.Equal(0L, account.Storage.DuePayment);
    }

    [Fact]
    public async Task GetAccountTransactionsAsync_WithBeforeLt_ReturnsLimitedTransactions()
    {
        // Arrange
        var address = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";

        // Act
        var result = await Client.Blockchain.GetAccountTransactionsAsync(address, beforeLt: 26199398000001, limit: 2, sortOrder: "desc");

        // Assert
        Assert.NotNull(result?.Transactions);
        Assert.Equal(2, result.Transactions.Count);
        
        var tx1 = result.Transactions[0];
        Assert.Equal("4858db492315628a8ce71e9021a0b0f9f6c7c39279f6665e85a8aab823930aed", tx1.Hash);
        Assert.Equal(26199390000001ul, tx1.Lt);
        Assert.True(tx1.Success);
        Assert.Equal(1727098485ul, tx1.Utime);
        Assert.Equal("uninit", tx1.OrigStatus);
        Assert.Equal("active", tx1.EndStatus);
        
        var tx2 = result.Transactions[1];
        Assert.Equal("6c7224447277d85e667b55c223e5a52f7f29912668121a095fb5e3d66bfa4094", tx2.Hash);
        Assert.Equal(26199377000001ul, tx2.Lt);
        Assert.True(tx2.Aborted);
    }

    [Fact]
    public async Task ExecuteGetMethodAsync_GetWalletAddress_ReturnsJettonWalletAddress()
    {
        var result = await Client.Blockchain.ExecuteGetMethodAsync(
            "0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457",
            "get_wallet_address",
            new List<string> { "0:9a33970f617bcd71acf2cd28357c067aa31859c02820d8f01d74c88063a8f4d8" });

        Assert.True(result.Success);
        Assert.Equal(0, result.ExitCode);
        Assert.NotNull(result.Stack);
        Assert.Single(result.Stack);
        Assert.Equal("cell", result.Stack[0].Type);
        Assert.Equal("b5ee9c720101010100240000438019a103ec1d82b0aa5dca1fe8d0a075fe36c073b9f99e331d4c26912b7e97102430", result.Stack[0].Cell);
        Assert.NotNull(result.Decoded);
        Assert.True(result.Decoded.Value.TryGetProperty("jetton_wallet_address", out var addr));
        Assert.Equal("0:cd081f60ec158552ee50ff468503aff1b6039dcfccf198ea6134895bf4b88121", addr.GetString());
    }

    [Fact]
    public async Task ExecuteMethodAsync_PostWithSlice_ReturnsJettonWalletAddress()
    {
        var request = new MethodExecutionRequest
        {
            Args = new List<MethodArg>
            {
                new() { Type = "slice", Value = "b5ee9c720101010100240000438019a103ec1d82b0aa5dca1fe8d0a075fe36c073b9f99e331d4c26912b7e97102430" }
            }
        };

        var result = await Client.Blockchain.ExecuteMethodAsync(
            "0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457",
            "get_wallet_address",
            request);

        Assert.True(result.Success);
        Assert.Equal(0, result.ExitCode);
        Assert.NotNull(result.Stack);
        Assert.Single(result.Stack);
        Assert.Equal("cell", result.Stack[0].Type);
        Assert.Equal("b5ee9c72010101010024000043800709dff886e82e2c2c5f171b69503067cf26625150839ae5a5f55bcca8e270a430", result.Stack[0].Cell);
        Assert.NotNull(result.Decoded);
        Assert.True(result.Decoded.Value.TryGetProperty("jetton_wallet_address", out var addr));
        Assert.Equal("0:384effc43741716162f8b8db4a81833e7933128a841cd72d2faade6547138521", addr.GetString());
    }

    [Fact]
    public async Task InspectAccountAsync_ReturnsInspectData()
    {
        var result = await Client.Blockchain.InspectAccountAsync("0:92765cfb183a71317d47768f37d5d9c10baa5af82b60c2113bc8056ff90fb457");

        Assert.NotNull(result);
        Assert.Equal("137ea9dd400bcfdb4d83a681683ce40a20d9e82c8d788bd945e22bf43ae0fabc", result.CodeHash);
        Assert.Equal("func", result.Compiler);
        Assert.NotNull(result.Methods);
        Assert.Equal(4, result.Methods.Count);
        Assert.Contains(result.Methods, m => m.Method == "get_version" && m.Id == 82320);
        Assert.Contains(result.Methods, m => m.Method == "get_jetton_data" && m.Id == 106029);
        Assert.Contains(result.Methods, m => m.Method == "get_admin_address" && m.Id == 94255);
        Assert.Contains(result.Methods, m => m.Method == "get_wallet_address" && m.Id == 103289);
    }
}

