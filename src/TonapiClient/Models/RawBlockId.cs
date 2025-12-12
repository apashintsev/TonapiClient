using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class RawBlockId
{
    [JsonPropertyName("workchain")]
    public int Workchain { get; set; }

    [JsonPropertyName("shard")]
    public string Shard { get; set; } = string.Empty;

    [JsonPropertyName("seqno")]
    public long Seqno { get; set; }

    [JsonPropertyName("root_hash")]
    public string RootHash { get; set; } = string.Empty;

    [JsonPropertyName("file_hash")]
    public string FileHash { get; set; } = string.Empty;
}
