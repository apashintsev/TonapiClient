namespace TonapiClient.Tests;

public class LiteServerCategoryTests : TestBase
{
    [Fact]
    public async Task GetMasterchainInfoAsync_ReturnsInfo()
    {
        var result = await Client.LiteServer.GetMasterchainInfoAsync();

        Assert.NotNull(result);
        Assert.Equal(-1, result.Last.Workchain);
        Assert.NotEmpty(result.Last.Shard);
        Assert.True(result.Last.Seqno > 0);
        Assert.NotEmpty(result.Last.RootHash);
        Assert.NotEmpty(result.Last.FileHash);
        Assert.NotEmpty(result.StateRootHash);
        Assert.Equal(-1, result.Init.Workchain);
        Assert.NotEmpty(result.Init.RootHash);
        Assert.NotEmpty(result.Init.FileHash);
    }

    [Fact]
    public async Task GetTimeAsync_ReturnsTime()
    {
        var result = await Client.LiteServer.GetTimeAsync();

        Assert.True(result > 0);
    }

    [Fact]
    public async Task GetBlockAsync_ReturnsBlock()
    {
        var info = await Client.LiteServer.GetMasterchainInfoAsync();
        var blockId = $"({info.Last.Workchain},{info.Last.Shard},{info.Last.Seqno},{info.Last.RootHash},{info.Last.FileHash})";
        var result = await Client.LiteServer.GetBlockAsync(blockId);

        Assert.NotNull(result);
        Assert.Equal(-1, result.Id.Workchain);
        Assert.NotEmpty(result.Id.Shard);
        Assert.True(result.Id.Seqno > 0);
        Assert.NotEmpty(result.Id.RootHash);
        Assert.NotEmpty(result.Id.FileHash);
        Assert.NotEmpty(result.Data);
    }

    [Fact]
    public async Task GetAccountStateAsync_WithTargetBlock_ReturnsState()
    {
        var accountId = "0:08c1cd46e7c5f238f5a47375b208c532da16f891beb94706916cf0010c87b2cd";
        var info = await Client.LiteServer.GetMasterchainInfoAsync();
        var targetBlock = $"({info.Last.Workchain},{info.Last.Shard},{info.Last.Seqno},{info.Last.RootHash},{info.Last.FileHash})";
        var result = await Client.LiteServer.GetAccountStateAsync(accountId, targetBlock);

        Assert.NotNull(result);
        Assert.Equal(-1, result.Id.Workchain);
        Assert.NotEmpty(result.Id.Shard);
        Assert.True(result.Id.Seqno > 0);
        Assert.True(result.ShardBlock.Workchain >= 0);
        Assert.NotEmpty(result.ShardProof);
        Assert.NotEmpty(result.Proof);
        Assert.NotEmpty(result.State);
    }

    [Fact]
    public async Task GetShardInfoAsync_ReturnsShardInfo()
    {
        var info = await Client.LiteServer.GetMasterchainInfoAsync();
        var blockId = $"({info.Last.Workchain},{info.Last.Shard},{info.Last.Seqno},{info.Last.RootHash},{info.Last.FileHash})";
        var result = await Client.LiteServer.GetShardInfoAsync(blockId, 1, 1, false);

        Assert.NotNull(result);
        Assert.Equal(-1, result.Id.Workchain);
        Assert.NotEmpty(result.Id.Shard);
        Assert.True(result.Id.Seqno > 0);
        Assert.NotEmpty(result.ShardProof);
    }

    [Fact]
    public async Task GetAllShardsInfoAsync_ReturnsAllShards()
    {
        var info = await Client.LiteServer.GetMasterchainInfoAsync();
        var blockId = $"({info.Last.Workchain},{info.Last.Shard},{info.Last.Seqno},{info.Last.RootHash},{info.Last.FileHash})";
        var result = await Client.LiteServer.GetAllShardsInfoAsync(blockId);

        Assert.NotNull(result);
        Assert.Equal(-1, result.Id.Workchain);
        Assert.NotEmpty(result.Id.Shard);
        Assert.True(result.Id.Seqno > 0);
        Assert.NotEmpty(result.Data);
    }

    [Fact]
    public async Task GetTransactionsAsync_ReturnsTransactions()
    {
        var accountId = "0:94aa09fe231de4bb384f02428a8aaa9741acec27df0add54828b8409dd94c60b";
        var result = await Client.LiteServer.GetTransactionsAsync(accountId, count: 10, lt: "26199398000001", hash: "7e50bd10bf8a31511a2508bb48328e9ba2727a0d6522dfe2b1be0a1a24858ead");

        Assert.NotNull(result);
        Assert.NotEmpty(result.Ids);
        Assert.NotEmpty(result.Transactions);
    }
}

