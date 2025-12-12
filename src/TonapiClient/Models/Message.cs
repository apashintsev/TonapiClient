using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a TON blockchain message.
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the message type (e.g., "int_msg", "ext_in_msg", "ext_out_msg").
    /// </summary>
    [JsonPropertyName("msg_type")]
    public string? MsgType { get; set; }

    /// <summary>
    /// Gets or sets the message hash.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source account address.
    /// </summary>
    [JsonPropertyName("source")]
    public AccountAddress? Source { get; set; }

    /// <summary>
    /// Gets or sets the destination account address.
    /// </summary>
    [JsonPropertyName("destination")]
    public AccountAddress? Destination { get; set; }

    /// <summary>
    /// Gets or sets the value transferred in nanotons.
    /// </summary>
    [JsonPropertyName("value")]
    public long Value { get; set; }

    /// <summary>
    /// Gets or sets the message body hash.
    /// </summary>
    [JsonPropertyName("body_hash")]
    public string BodyHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message opcode.
    /// </summary>
    [JsonPropertyName("op_code")]
    public string? OpCode { get; set; }

    /// <summary>
    /// Gets or sets the message creation logical time.
    /// </summary>
    [JsonPropertyName("created_lt")]
    public long CreatedLt { get; set; }

    /// <summary>
    /// Gets or sets the decoded message body if available.
    /// </summary>
    [JsonPropertyName("decoded_body")]
    public object? DecodedBody { get; set; }

    /// <summary>
    /// Gets or sets the raw message body.
    /// </summary>
    [JsonPropertyName("raw_body")]
    public string? RawBody { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether IHR (Instant Hypercube Routing) is disabled.
    /// </summary>
    [JsonPropertyName("ihr_disabled")]
    public bool? IhrDisabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message should bounce on failure.
    /// </summary>
    [JsonPropertyName("bounce")]
    public bool? Bounce { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message has bounced.
    /// </summary>
    [JsonPropertyName("bounced")]
    public bool? Bounced { get; set; }

    /// <summary>
    /// Gets or sets the forward fee in nanotons.
    /// </summary>
    [JsonPropertyName("fwd_fee")]
    public ulong? FwdFee { get; set; }

    /// <summary>
    /// Gets or sets the IHR fee in nanotons.
    /// </summary>
    [JsonPropertyName("ihr_fee")]
    public ulong? IhrFee { get; set; }

    /// <summary>
    /// Gets or sets the import fee in nanotons.
    /// </summary>
    [JsonPropertyName("import_fee")]
    public ulong? ImportFee { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the message was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public ulong? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the state initialization data.
    /// </summary>
    [JsonPropertyName("init")]
    public StateInit? Init { get; set; }

    /// <summary>
    /// Gets or sets the decoded operation name.
    /// </summary>
    [JsonPropertyName("decoded_op_name")]
    public string? DecodedOpName { get; set; }

    /// <summary>
    /// Gets the message type (alias for MsgType).
    /// </summary>
    public string? MessageType => MsgType;
}
