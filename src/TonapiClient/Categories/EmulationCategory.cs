using TonapiClient.Models;

namespace TonapiClient.Categories;

/// <summary>
/// Emulation category methods.
/// </summary>
public class EmulationCategory : CategoryBase
{
    internal EmulationCategory(TonApiClient client) : base(client) { }

    /// <summary>
    /// Decode a message.
    /// </summary>
    public async Task<DecodedMessage> DecodeMessageAsync(string boc, CancellationToken ct = default)
    {
        var request = new DecodeMessageRequest { Boc = boc };
        return await PostAsync<DecodeMessageRequest, DecodedMessage>("/v2/message/decode", request, ct);
    }
}
