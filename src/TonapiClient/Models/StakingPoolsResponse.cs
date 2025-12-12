using TonapiClient.Models;

namespace TonapiClient;

public partial class TonApiClient
{
    private class StakingPoolsResponse
  {
    [System.Text.Json.Serialization.JsonPropertyName("pools")]
    public List<PoolInfo> Pools { get; set; } = new();
  }
}
