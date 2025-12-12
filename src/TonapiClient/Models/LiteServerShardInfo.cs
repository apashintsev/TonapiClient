using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class LiteServerShardInfo
{
    [JsonPropertyName("id")]
    public RawBlockId Id { get; set; } = new();

    [JsonPropertyName("shardblk")]
    public RawBlockId Shardblk { get; set; } = new();

    [JsonPropertyName("shard_proof")]
    public string ShardProof { get; set; } = string.Empty;

    [JsonPropertyName("shard_descr")]
    public string ShardDescr { get; set; } = string.Empty;
}

