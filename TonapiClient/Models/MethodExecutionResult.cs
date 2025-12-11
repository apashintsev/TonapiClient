using System.Text.Json;
using System.Text.Json.Serialization;

namespace TonapiClient.Models;

public class MethodExecutionResult
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("exit_code")]
    public int ExitCode { get; set; }

    [JsonPropertyName("stack")]
    public List<StackItem>? Stack { get; set; }

    [JsonPropertyName("decoded")]
    public JsonElement? Decoded { get; set; }
}
