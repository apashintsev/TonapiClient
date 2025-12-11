using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class MethodArg
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public class MethodExecutionRequest
{
    [JsonPropertyName("args")]
    public List<MethodArg> Args { get; set; } = new();
}

