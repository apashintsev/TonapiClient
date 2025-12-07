using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents a blockchain block.
/// </summary>
public class BlockchainBlock
{
    /// <summary>
    /// Gets or sets transactions count inside the block.
    /// </summary>
    [JsonPropertyName("tx_quantity")]
    public int TxQuantity { get; set; }

    /// <summary>
    /// Gets or sets the workchain ID.
    /// </summary>
    [JsonPropertyName("workchain_id")]
    public int WorkchainId { get; set; }

    /// <summary>
    /// Gets or sets the shard.
    /// </summary>
    [JsonPropertyName("shard")]
    public string Shard { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sequence number.
    /// </summary>
    [JsonPropertyName("seqno")]
    public ulong Seqno { get; set; }

    /// <summary>
    /// Gets or sets the root hash.
    /// </summary>
    [JsonPropertyName("root_hash")]
    public string RootHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file hash.
    /// </summary>
    [JsonPropertyName("file_hash")]
    public string FileHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the global ID.
    /// </summary>
    [JsonPropertyName("global_id")]
    public int GlobalId { get; set; }

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is after merge.
    /// </summary>
    [JsonPropertyName("after_merge")]
    public bool AfterMerge { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is before split.
    /// </summary>
    [JsonPropertyName("before_split")]
    public bool BeforeSplit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is after split.
    /// </summary>
    [JsonPropertyName("after_split")]
    public bool AfterSplit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this wants split.
    /// </summary>
    [JsonPropertyName("want_split")]
    public bool WantSplit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this wants merge.
    /// </summary>
    [JsonPropertyName("want_merge")]
    public bool WantMerge { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is key block.
    /// </summary>
    [JsonPropertyName("key_block")]
    public bool KeyBlock { get; set; }

    /// <summary>
    /// Gets or sets the generation unix timestamp.
    /// </summary>
    [JsonPropertyName("gen_utime")]
    public ulong GenUtime { get; set; }

    /// <summary>
    /// Gets or sets the start logical time.
    /// </summary>
    [JsonPropertyName("start_lt")]
    public ulong StartLt { get; set; }

    /// <summary>
    /// Gets or sets the end logical time.
    /// </summary>
    [JsonPropertyName("end_lt")]
    public ulong EndLt { get; set; }

    /// <summary>
    /// Gets or sets the validator list hash short.
    /// </summary>
    [JsonPropertyName("vert_seqno")]
    public int VertSeqno { get; set; }

    /// <summary>
    /// Gets or sets the generation catchain sequence number.
    /// </summary>
    [JsonPropertyName("gen_catchain_seqno")]
    public int GenCatchainSeqno { get; set; }

    /// <summary>
    /// Gets or sets the minimum referenced masterchain sequence number.
    /// </summary>
    [JsonPropertyName("min_ref_mc_seqno")]
    public int MinRefMcSeqno { get; set; }

    /// <summary>
    /// Gets or sets the previous key block sequence number.
    /// </summary>
    [JsonPropertyName("prev_key_block_seqno")]
    public int PrevKeyBlockSeqno { get; set; }

    /// <summary>
    /// Gets or sets the generation software version.
    /// </summary>
    [JsonPropertyName("gen_software_version")]
    public int? GenSoftwareVersion { get; set; }

    /// <summary>
    /// Gets or sets the generation software capabilities.
    /// </summary>
    [JsonPropertyName("gen_software_capabilities")]
    public long? GenSoftwareCapabilities { get; set; }

    /// <summary>
    /// Gets or sets the master reference (for workchain blocks).
    /// </summary>
    [JsonPropertyName("master_ref")]
    public string? MasterRef { get; set; }

    /// <summary>
    /// Gets or sets references to previous blocks.
    /// </summary>
    [JsonPropertyName("prev_refs")]
    public List<string> PreviousReferences { get; set; } = new();

    /// <summary>
    /// Gets or sets the value flow.
    /// </summary>
    [JsonPropertyName("value_flow")]
    public ValueFlow? ValueFlow { get; set; }

    /// <summary>
    /// Gets or sets the in message descriptors count.
    /// </summary>
    [JsonPropertyName("in_msg_descr_length")]
    public int InMsgDescrLength { get; set; }

    /// <summary>
    /// Gets or sets the out message descriptors count.
    /// </summary>
    [JsonPropertyName("out_msg_descr_length")]
    public int OutMsgDescrLength { get; set; }

    /// <summary>
    /// Gets or sets the rand seed.
    /// </summary>
    [JsonPropertyName("rand_seed")]
    public string RandSeed { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the created by.
    /// </summary>
    [JsonPropertyName("created_by")]
    public string CreatedBy { get; set; } = string.Empty;
}
