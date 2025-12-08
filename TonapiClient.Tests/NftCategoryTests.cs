using Xunit;
using TonapiClient.Models;

namespace TonapiClient.Tests;

public class NftCategoryTests : TestBase
{
    // Test addresses
    private const string NftOwnerAddress = "0:21137b0bc47669b3267f1de70cbb0cef5c728b8d8c7890451e8613b2d8998270";
    private const string TonBullrunCollectionAddress = "0:4db8f94ae7fb709a35cf4307154ac73213ae7724637c309b8ff025ab9a5a3fd8";

    [Fact]
    public async Task GetNftsHistory_WithAddressAndLimit2_ReturnsNftOperations()
    {
        // Arrange
        var accountId = NftOwnerAddress;
        var beforeLt = 25758317000002L;
        var limit = 2;

        // Act
        var result = await Client.Account.GetNftsHistoryAsync(accountId, limit, beforeLt);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Operations);
        
        // Should return operations (may be less than limit if no more data)
        Assert.True(result.Operations.Count > 0, "Should return at least one operation");
        Assert.True(result.Operations.Count <= limit, $"Should return at most {limit} operations, but got {result.Operations.Count}");
        
        // Verify first operation
        var firstOp = result.Operations[0];
        Assert.NotNull(firstOp);
        
        // Verify operation type
        Assert.Equal("transfer", firstOp.Operation);
        
        // Verify timestamp and lt
        Assert.Equal(1686564624, firstOp.Utime);
        Assert.Equal(12218603000003, firstOp.Lt);
        
        // Verify transaction hash
        Assert.NotNull(firstOp.TransactionHash);
        Assert.NotEmpty(firstOp.TransactionHash);
        Assert.Equal("768fda470ba3c1aec7650b11b3294a38389ca37305f6ec83c74d7f7090a98f7e", firstOp.TransactionHash);
        
        // Verify source
        Assert.NotNull(firstOp.Source);
        Assert.NotEmpty(firstOp.Source.Address);
        Assert.Equal("0:9cefcf05a66b29c8f98eaf05831f5e0802b70d6a6cca4bbe10a9d86f3a8de574", firstOp.Source.Address.ToLower());
        Assert.False(firstOp.Source.IsScam);
        Assert.True(firstOp.Source.IsWallet);
        
        // Verify destination
        Assert.NotNull(firstOp.Destination);
        Assert.NotEmpty(firstOp.Destination.Address);
        Assert.Equal(NftOwnerAddress.ToLower(), firstOp.Destination.Address.ToLower());
        Assert.Equal("TON Ecosystem Reserve (OLD)", firstOp.Destination.Name);
        Assert.False(firstOp.Destination.IsScam);
        Assert.True(firstOp.Destination.IsWallet);
        
        // Verify NFT item
        Assert.NotNull(firstOp.Item);
        Assert.NotEmpty(firstOp.Item.Address);
        Assert.Equal("0:417ac5c5d5ac3b8e82b2b8d0fc2697646d3594cb3218284497d1139e99da65c8", firstOp.Item.Address.ToLower());
        Assert.Equal(0, firstOp.Item.Index);
        Assert.False(firstOp.Item.Verified);
        
        // Verify item metadata
        Assert.NotNull(firstOp.Item.Metadata);
        
        // Verify previews
        Assert.NotNull(firstOp.Item.Previews);
        Assert.Equal(4, firstOp.Item.Previews.Count);
        
        // Verify first preview (5x5)
        var preview5x5 = firstOp.Item.Previews[0];
        Assert.Equal("5x5", preview5x5.Resolution);
        Assert.NotEmpty(preview5x5.Url);
        Assert.Contains("cache.tonapi.io", preview5x5.Url);
        
        // Verify second preview (100x100)
        var preview100x100 = firstOp.Item.Previews[1];
        Assert.Equal("100x100", preview100x100.Resolution);
        Assert.NotEmpty(preview100x100.Url);
        
        // Verify third preview (500x500)
        var preview500x500 = firstOp.Item.Previews[2];
        Assert.Equal("500x500", preview500x500.Resolution);
        Assert.NotEmpty(preview500x500.Url);
        
        // Verify fourth preview (1500x1500)
        var preview1500x1500 = firstOp.Item.Previews[3];
        Assert.Equal("1500x1500", preview1500x1500.Resolution);
        Assert.NotEmpty(preview1500x1500.Url);
        
        // Verify approved_by
        Assert.NotNull(firstOp.Item.ApprovedBy);
        Assert.Empty(firstOp.Item.ApprovedBy);
        
        // Verify trust
        Assert.Equal("none", firstOp.Item.Trust);
    }

    [Fact]
    public async Task GetCollections_WithLimit15AndOffset10_ReturnsNftCollections()
    {
        // Arrange
        var limit = 15;
        var offset = 10;

        // Act
        var result = await Client.Nft.GetCollectionsAsync(limit, offset);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Collections);
        
        // Should return collections (may be less than limit if no more data)
        Assert.True(result.Collections.Count > 0, "Should return at least one collection");
        Assert.True(result.Collections.Count <= limit, $"Should return at most {limit} collections, but got {result.Collections.Count}");
        
        // Verify first collection (Blue Eyes Hat)
        var firstCollection = result.Collections[0];
        Assert.NotNull(firstCollection);
        
        // Verify address
        Assert.NotEmpty(firstCollection.Address);
        Assert.Equal("0:9e27f0f676ff3d38efdff22838d457b9870278dd3dfe09341134fc4422813713", firstCollection.Address.ToLower());
        
        // Verify next_item_index
        Assert.Equal(-1, firstCollection.NextItemIndex);
        
        // Verify owner
        Assert.NotNull(firstCollection.Owner);
        Assert.Equal("0:0e2527ae08570105a7d201c1277cff5db52ac60869b0a98c0997c2a8fc9ca3c7", firstCollection.Owner.Address.ToLower());
        Assert.False(firstCollection.Owner.IsScam);
        Assert.True(firstCollection.Owner.IsWallet);
        
        // Verify raw_collection_content
        Assert.NotNull(firstCollection.RawCollectionContent);
        Assert.NotEmpty(firstCollection.RawCollectionContent);
        
        // Verify metadata
        Assert.NotNull(firstCollection.Metadata);
        Assert.True(firstCollection.Metadata.ContainsKey("name"));
        Assert.True(firstCollection.Metadata.ContainsKey("image"));
        Assert.True(firstCollection.Metadata.ContainsKey("description"));
        
        // Verify previews
        Assert.NotNull(firstCollection.Previews);
        Assert.Equal(4, firstCollection.Previews.Count);
        
        // Verify preview resolutions
        Assert.Equal("5x5", firstCollection.Previews[0].Resolution);
        Assert.Equal("100x100", firstCollection.Previews[1].Resolution);
        Assert.Equal("500x500", firstCollection.Previews[2].Resolution);
        Assert.Equal("1500x1500", firstCollection.Previews[3].Resolution);
        
        // Verify all previews have URLs
        foreach (var preview in firstCollection.Previews)
        {
            Assert.NotNull(preview.Url);
            Assert.NotEmpty(preview.Url);
            Assert.Contains("cache.tonapi.io", preview.Url);
        }
        
        // Verify approved_by
        Assert.NotNull(firstCollection.ApprovedBy);
        Assert.Empty(firstCollection.ApprovedBy);
        
        // Verify second collection (My team)
        if (result.Collections.Count > 1)
        {
            var secondCollection = result.Collections[1];
            Assert.NotEmpty(secondCollection.Address);
            Assert.Equal("0:67b058e9a827acb338d526d191114ae54d8afd654f055640a2d32830bfcd3ef0", secondCollection.Address.ToLower());
            Assert.Equal(0, secondCollection.NextItemIndex);
            Assert.NotNull(secondCollection.Owner);
            Assert.NotNull(secondCollection.Metadata);
        }
        
        // Verify Tonkeeper collection (has approved_by)
        var tonkeeperCollection = result.Collections.FirstOrDefault(c => 
            c.ApprovedBy != null && c.ApprovedBy.Contains("tonkeeper"));
        
        if (tonkeeperCollection != null)
        {
            Assert.NotNull(tonkeeperCollection.ApprovedBy);
            Assert.NotEmpty(tonkeeperCollection.ApprovedBy);
            Assert.Contains("tonkeeper", tonkeeperCollection.ApprovedBy);
            Assert.Equal("0:3971e42d84298ca7a1ca9cf206b3ad919e83570081e5b7641ecd54fd736c8250", tonkeeperCollection.Address.ToLower());
        }
    }

    [Fact]
    public async Task GetCollection_WithTonBullrunAddress_ReturnsCollectionDetails()
    {
        // Arrange
        var collectionAddress = TonBullrunCollectionAddress;

        // Act
        var result = await Client.Nft.GetCollectionAsync(collectionAddress);

        // Assert
        Assert.NotNull(result);
        
        // Verify address
        Assert.NotEmpty(result.Address);
        Assert.Equal("0:4db8f94ae7fb709a35cf4307154ac73213ae7724637c309b8ff025ab9a5a3fd8", result.Address.ToLower());
        
        // Verify next_item_index
        Assert.Equal(20, result.NextItemIndex);
        
        // Verify owner
        Assert.NotNull(result.Owner);
        Assert.NotEmpty(result.Owner.Address);
        Assert.Equal("0:e76266cae608e71d0c96261ee33434c00a77a382312d69ff7b8bea292b3a7bd9", result.Owner.Address.ToLower());
        Assert.False(result.Owner.IsScam);
        Assert.True(result.Owner.IsWallet);
        
        // Verify raw_collection_content
        Assert.NotNull(result.RawCollectionContent);
        Assert.NotEmpty(result.RawCollectionContent);
        Assert.Equal("b5ee9c720101010100690000ce0168747470733a2f2f6261667962656963687667737568636d6f6b36366f3266616533333775776a786d66796732733376727271626d783734766266706e79716d63686d2e697066732e6e667473746f726167652e6c696e6b2f6d657461646174612e6a736f6e", result.RawCollectionContent.ToLower());
        
        // Verify metadata
        Assert.NotNull(result.Metadata);
        Assert.True(result.Metadata.ContainsKey("name"));
        Assert.True(result.Metadata.ContainsKey("image"));
        Assert.True(result.Metadata.ContainsKey("description"));
        Assert.True(result.Metadata.ContainsKey("social_links"));
        
        // Verify metadata values (they are object type, need to convert)
        Assert.Equal("TON Bullrun", result.Metadata["name"].ToString());
        Assert.Contains("profile_pic.gif", result.Metadata["image"].ToString());
        Assert.Equal("Any desc", result.Metadata["description"].ToString());
        
        // Verify previews
        Assert.NotNull(result.Previews);
        Assert.Equal(4, result.Previews.Count);
        
        // Verify each preview resolution and URL
        var preview5x5 = result.Previews[0];
        Assert.Equal("5x5", preview5x5.Resolution);
        Assert.NotEmpty(preview5x5.Url);
        Assert.Contains("cache.tonapi.io", preview5x5.Url);
        
        var preview100x100 = result.Previews[1];
        Assert.Equal("100x100", preview100x100.Resolution);
        Assert.NotEmpty(preview100x100.Url);
        
        var preview500x500 = result.Previews[2];
        Assert.Equal("500x500", preview500x500.Resolution);
        Assert.NotEmpty(preview500x500.Url);
        
        var preview1500x1500 = result.Previews[3];
        Assert.Equal("1500x1500", preview1500x1500.Resolution);
        Assert.NotEmpty(preview1500x1500.Url);
        
        // Verify all previews are from cache.tonapi.io
        foreach (var preview in result.Previews)
        {
            Assert.Contains("cache.tonapi.io", preview.Url);
        }
        
        // Verify approved_by
        Assert.NotNull(result.ApprovedBy);
        Assert.Empty(result.ApprovedBy);
    }

    [Fact]
    public async Task GetCollectionsBulk_WithTwoAddresses_ReturnsMultipleCollections()
    {
        // Arrange
        var collectionAddresses = new List<string>
        {
            TonBullrunCollectionAddress,
            "0:dc482f3fcb0a58744a65f9b049b2172ed59d2817eb60a1af0a4f8136ce016199"
        };

        // Act
        var result = await Client.Nft.GetCollectionsBulkAsync(collectionAddresses);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Collections);
        
        // Should return 2 collections
        Assert.Equal(2, result.Collections.Count);
        
        // Verify first collection (dc482f3f...)
        var firstCollection = result.Collections[0];
        Assert.NotNull(firstCollection);
        Assert.Equal("0:dc482f3fcb0a58744a65f9b049b2172ed59d2817eb60a1af0a4f8136ce016199", firstCollection.Address.ToLower());
        Assert.Equal(1, firstCollection.NextItemIndex);
        
        // Verify first collection owner
        Assert.NotNull(firstCollection.Owner);
        Assert.Equal("0:eacc36100495fe002982e7d41093fee0250e44b634de3bb08b8d0f0e47f9158b", firstCollection.Owner.Address.ToLower());
        Assert.False(firstCollection.Owner.IsScam);
        Assert.True(firstCollection.Owner.IsWallet);
        
        // Verify first collection raw_collection_content
        Assert.NotNull(firstCollection.RawCollectionContent);
        Assert.Equal("b5ee9c7201010101004800008c01697066733a2f2f516d5259706753335071685a5847334879666772754d79457567534b6674684642476541794e7543426a737a47692f636f6c6c656374696f6e2e6a736f6e", firstCollection.RawCollectionContent.ToLower());
        
        // Verify first collection approved_by
        Assert.NotNull(firstCollection.ApprovedBy);
        Assert.Empty(firstCollection.ApprovedBy);
        
        // Verify second collection (4db8f94a... - TON Bullrun)
        var secondCollection = result.Collections[1];
        Assert.NotNull(secondCollection);
        Assert.Equal(TonBullrunCollectionAddress.ToLower(), secondCollection.Address.ToLower());
        Assert.Equal(20, secondCollection.NextItemIndex);
        
        // Verify second collection owner
        Assert.NotNull(secondCollection.Owner);
        Assert.Equal("0:e76266cae608e71d0c96261ee33434c00a77a382312d69ff7b8bea292b3a7bd9", secondCollection.Owner.Address.ToLower());
        Assert.False(secondCollection.Owner.IsScam);
        Assert.True(secondCollection.Owner.IsWallet);
        
        // Verify second collection raw_collection_content
        Assert.NotNull(secondCollection.RawCollectionContent);
        Assert.NotEmpty(secondCollection.RawCollectionContent);
        
        // Verify second collection metadata (TON Bullrun has metadata)
        Assert.NotNull(secondCollection.Metadata);
        Assert.True(secondCollection.Metadata.ContainsKey("name"));
        Assert.Equal("TON Bullrun", secondCollection.Metadata["name"].ToString());
        Assert.True(secondCollection.Metadata.ContainsKey("description"));
        Assert.Equal("Any desc", secondCollection.Metadata["description"].ToString());
        
        // Verify second collection previews
        Assert.NotNull(secondCollection.Previews);
        Assert.Equal(4, secondCollection.Previews.Count);
        
        // Verify second collection approved_by
        Assert.NotNull(secondCollection.ApprovedBy);
        Assert.Empty(secondCollection.ApprovedBy);
    }

    [Fact]
    public async Task GetCollectionItems_WithTonBullrunAndLimit3_ReturnsNftItems()
    {
        // Arrange
        var collectionAddress = TonBullrunCollectionAddress;
        var limit = 3;
        var offset = 0;

        // Act
        var result = await Client.Nft.GetCollectionItemsAsync(collectionAddress, limit, offset);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Items);
        
        // Should return exactly 3 items
        Assert.Equal(3, result.Items.Count);
        
        // Verify first NFT (Bull #1)
        var firstNft = result.Items[0];
        Assert.NotNull(firstNft);
        Assert.Equal("0:18525ab1e3cd8a4989902cc2ad91fa862ee57d8ac8b815a68de7dbf14069a00e", firstNft.Address.ToLower());
        Assert.Equal(0, firstNft.Index);
        
        // Verify first NFT owner
        Assert.NotNull(firstNft.Owner);
        Assert.Equal("0:e76266cae608e71d0c96261ee33434c00a77a382312d69ff7b8bea292b3a7bd9", firstNft.Owner.Address.ToLower());
        Assert.False(firstNft.Owner.IsScam);
        Assert.True(firstNft.Owner.IsWallet);
        
        // Verify first NFT collection
        Assert.NotNull(firstNft.Collection);
        Assert.Equal(TonBullrunCollectionAddress.ToLower(), firstNft.Collection.Address.ToLower());
        Assert.Equal("TON Bullrun", firstNft.Collection.Name);
        Assert.Equal("Any desc", firstNft.Collection.Description);
        
        // Verify first NFT verified and trust
        Assert.True(firstNft.Verified);
        Assert.Equal("none", firstNft.Trust);
        
        // Verify first NFT metadata
        Assert.NotNull(firstNft.Metadata);
        Assert.True(firstNft.Metadata.ContainsKey("name"));
        Assert.Equal("Bull #1", firstNft.Metadata["name"].ToString());
        Assert.True(firstNft.Metadata.ContainsKey("description"));
        Assert.Equal("Let the real Bullrun begin!", firstNft.Metadata["description"].ToString());
        Assert.True(firstNft.Metadata.ContainsKey("image"));
        Assert.Contains("1.png", firstNft.Metadata["image"].ToString());
        
        // Verify first NFT previews
        Assert.NotNull(firstNft.Previews);
        Assert.Equal(4, firstNft.Previews.Count);
        Assert.Equal("5x5", firstNft.Previews[0].Resolution);
        Assert.Equal("100x100", firstNft.Previews[1].Resolution);
        Assert.Equal("500x500", firstNft.Previews[2].Resolution);
        Assert.Equal("1500x1500", firstNft.Previews[3].Resolution);
        
        // Verify first NFT approved_by
        Assert.NotNull(firstNft.ApprovedBy);
        Assert.Empty(firstNft.ApprovedBy);
        
        // Verify second NFT (Bull #2)
        var secondNft = result.Items[1];
        Assert.NotNull(secondNft);
        Assert.Equal("0:dd57fe557bcbf49222910cadb217a476effc33cb19e5c6e88a87fd41fb17e433", secondNft.Address.ToLower());
        Assert.Equal(1, secondNft.Index);
        
        // Verify second NFT owner (same as first)
        Assert.NotNull(secondNft.Owner);
        Assert.Equal("0:e76266cae608e71d0c96261ee33434c00a77a382312d69ff7b8bea292b3a7bd9", secondNft.Owner.Address.ToLower());
        
        // Verify second NFT collection (same as first)
        Assert.NotNull(secondNft.Collection);
        Assert.Equal(TonBullrunCollectionAddress.ToLower(), secondNft.Collection.Address.ToLower());
        
        // Verify second NFT metadata
        Assert.NotNull(secondNft.Metadata);
        Assert.True(secondNft.Metadata.ContainsKey("name"));
        Assert.Equal("Bull #2", secondNft.Metadata["name"].ToString());
        Assert.True(secondNft.Metadata.ContainsKey("image"));
        Assert.Contains("2.png", secondNft.Metadata["image"].ToString());
        
        // Verify second NFT verified and trust
        Assert.True(secondNft.Verified);
        Assert.Equal("none", secondNft.Trust);
        
        // Verify second NFT previews
        Assert.NotNull(secondNft.Previews);
        Assert.Equal(4, secondNft.Previews.Count);
        
        // Verify third NFT (Bull #3)
        var thirdNft = result.Items[2];
        Assert.NotNull(thirdNft);
        Assert.Equal("0:128e66508f26535b6d8d74f50bb60b1efd43b8dff8db3efc1ff75818e00132de", thirdNft.Address.ToLower());
        Assert.Equal(2, thirdNft.Index);
        
        // Verify third NFT owner (same as others)
        Assert.NotNull(thirdNft.Owner);
        Assert.Equal("0:e76266cae608e71d0c96261ee33434c00a77a382312d69ff7b8bea292b3a7bd9", thirdNft.Owner.Address.ToLower());
        
        // Verify third NFT collection (same as others)
        Assert.NotNull(thirdNft.Collection);
        Assert.Equal(TonBullrunCollectionAddress.ToLower(), thirdNft.Collection.Address.ToLower());
        
        // Verify third NFT metadata
        Assert.NotNull(thirdNft.Metadata);
        Assert.True(thirdNft.Metadata.ContainsKey("name"));
        Assert.Equal("Bull #3", thirdNft.Metadata["name"].ToString());
        Assert.True(thirdNft.Metadata.ContainsKey("image"));
        Assert.Contains("3.png", thirdNft.Metadata["image"].ToString());
        
        // Verify third NFT verified and trust
        Assert.True(thirdNft.Verified);
        Assert.Equal("none", thirdNft.Trust);
        
        // Verify third NFT previews
        Assert.NotNull(thirdNft.Previews);
        Assert.Equal(4, thirdNft.Previews.Count);
        
        // Verify all items have the same owner
        Assert.All(result.Items, nft => 
            Assert.Equal("0:e76266cae608e71d0c96261ee33434c00a77a382312d69ff7b8bea292b3a7bd9", nft.Owner.Address.ToLower()));
        
        // Verify all items belong to the same collection
        Assert.All(result.Items, nft => 
            Assert.Equal(TonBullrunCollectionAddress.ToLower(), nft.Collection.Address.ToLower()));
        
        // Verify all items are verified
        Assert.All(result.Items, nft => Assert.True(nft.Verified));
        
        // Verify all items have trust="none"
        Assert.All(result.Items, nft => Assert.Equal("none", nft.Trust));
        
        // Verify all items have 4 previews
        Assert.All(result.Items, nft => Assert.Equal(4, nft.Previews.Count));
    }

    [Fact]
    public async Task GetItem_WithBull20Address_ReturnsNftItemDetails()
    {
        // Arrange
        var nftAddress = "0:c1546d73fa3bb896c6012f3381b898b26c1cced6e855a57f1b8586ba63749da6";

        // Act
        var result = await Client.Nft.GetItemAsync(nftAddress);

        // Assert
        Assert.NotNull(result);
        
        // Verify address
        Assert.NotEmpty(result.Address);
        Assert.Equal(nftAddress.ToLower(), result.Address.ToLower());
        
        // Verify index
        Assert.Equal(19, result.Index);
        
        // Verify owner
        Assert.NotNull(result.Owner);
        Assert.Equal(NftOwnerAddress.ToLower(), result.Owner.Address.ToLower());
        Assert.Equal("TON Ecosystem Reserve (OLD)", result.Owner.Name);
        Assert.False(result.Owner.IsScam);
        Assert.True(result.Owner.IsWallet);
        
        // Verify collection
        Assert.NotNull(result.Collection);
        Assert.Equal(TonBullrunCollectionAddress.ToLower(), result.Collection.Address.ToLower());
        Assert.Equal("TON Bullrun", result.Collection.Name);
        Assert.Equal("Any desc", result.Collection.Description);
        
        // Verify verified and trust
        Assert.True(result.Verified);
        Assert.Equal("none", result.Trust);
        
        // Verify metadata
        Assert.NotNull(result.Metadata);
        Assert.True(result.Metadata.ContainsKey("name"));
        Assert.Equal("Bull #20", result.Metadata["name"].ToString());
        Assert.True(result.Metadata.ContainsKey("description"));
        Assert.Equal("Let the real Bullrun begin!", result.Metadata["description"].ToString());
        Assert.True(result.Metadata.ContainsKey("image"));
        Assert.Contains("20.png", result.Metadata["image"].ToString());
        
        // Verify attributes in metadata
        Assert.True(result.Metadata.ContainsKey("attributes"));
        
        // Verify previews
        Assert.NotNull(result.Previews);
        Assert.Equal(4, result.Previews.Count);
        
        // Verify preview resolutions
        Assert.Equal("5x5", result.Previews[0].Resolution);
        Assert.NotEmpty(result.Previews[0].Url);
        Assert.Contains("cache.tonapi.io", result.Previews[0].Url);
        
        Assert.Equal("100x100", result.Previews[1].Resolution);
        Assert.NotEmpty(result.Previews[1].Url);
        
        Assert.Equal("500x500", result.Previews[2].Resolution);
        Assert.NotEmpty(result.Previews[2].Url);
        
        Assert.Equal("1500x1500", result.Previews[3].Resolution);
        Assert.NotEmpty(result.Previews[3].Url);
        
        // Verify all previews are from cache.tonapi.io
        foreach (var preview in result.Previews)
        {
            Assert.Contains("cache.tonapi.io", preview.Url);
        }
        
        // Verify approved_by
        Assert.NotNull(result.ApprovedBy);
        Assert.Empty(result.ApprovedBy);
    }
}

