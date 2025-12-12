using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the masterchain head information returned by /v2/blockchain/masterchain-head.
/// </summary>
public class MasterchainHead
{
  /// <summary>
  /// Gets or sets the number of transactions in the block.
  /// </summary>
  [JsonPropertyName("tx_quantity")]
  public ulong TransactionQuantity { get; set; }

  /// <summary>
  /// Gets or sets the aggregated value flow information for the block.
  /// </summary>
  [JsonPropertyName("value_flow")]
  public ValueFlow ValueFlow { get; set; } = new();

  /// <summary>
  /// Gets or sets the workchain identifier.
  /// </summary>
  [JsonPropertyName("workchain_id")]
  public int WorkchainId { get; set; }

  /// <summary>
  /// Gets or sets the shard identifier.
  /// </summary>
  [JsonPropertyName("shard")]
  public string Shard { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the block sequence number.
  /// </summary>
  [JsonPropertyName("seqno")]
  public ulong Seqno { get; set; }

  /// <summary>
  /// Gets or sets the root hash of the block.
  /// </summary>
  [JsonPropertyName("root_hash")]
  public string RootHash { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the file hash of the block.
  /// </summary>
  [JsonPropertyName("file_hash")]
  public string FileHash { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the global network identifier.
  /// </summary>
  [JsonPropertyName("global_id")]
  public int GlobalId { get; set; }

  /// <summary>
  /// Gets or sets the block version.
  /// </summary>
  [JsonPropertyName("version")]
  public int Version { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the block is created after merge.
  /// </summary>
  [JsonPropertyName("after_merge")]
  public bool AfterMerge { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the block is created before split.
  /// </summary>
  [JsonPropertyName("before_split")]
  public bool BeforeSplit { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the block is created after split.
  /// </summary>
  [JsonPropertyName("after_split")]
  public bool AfterSplit { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the block wants split.
  /// </summary>
  [JsonPropertyName("want_split")]
  public bool WantSplit { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the block wants merge.
  /// </summary>
  [JsonPropertyName("want_merge")]
  public bool WantMerge { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the block is a key block.
  /// </summary>
  [JsonPropertyName("key_block")]
  public bool KeyBlock { get; set; }

  /// <summary>
  /// Gets or sets the generation Unix timestamp in seconds.
  /// </summary>
  [JsonPropertyName("gen_utime")]
  public ulong GenerationUnixTime { get; set; }

  /// <summary>
  /// Gets or sets the starting logical time for the block.
  /// </summary>
  [JsonPropertyName("start_lt")]
  public ulong StartLogicalTime { get; set; }

  /// <summary>
  /// Gets or sets the ending logical time for the block.
  /// </summary>
  [JsonPropertyName("end_lt")]
  public ulong EndLogicalTime { get; set; }

  /// <summary>
  /// Gets or sets the vertical sequence number.
  /// </summary>
  [JsonPropertyName("vert_seqno")]
  public int VerticalSeqno { get; set; }

  /// <summary>
  /// Gets or sets the generation catchain sequence number.
  /// </summary>
  [JsonPropertyName("gen_catchain_seqno")]
  public ulong GenerationCatchainSeqno { get; set; }

  /// <summary>
  /// Gets or sets the minimum referenced masterchain sequence number.
  /// </summary>
  [JsonPropertyName("min_ref_mc_seqno")]
  public ulong MinReferencedMasterchainSeqno { get; set; }

  /// <summary>
  /// Gets or sets the previous key block sequence number.
  /// </summary>
  [JsonPropertyName("prev_key_block_seqno")]
  public ulong PreviousKeyBlockSeqno { get; set; }

  /// <summary>
  /// Gets or sets the generation software version.
  /// </summary>
  [JsonPropertyName("gen_software_version")]
  public int GenerationSoftwareVersion { get; set; }

  /// <summary>
  /// Gets or sets the generation software capabilities bitmask.
  /// </summary>
  [JsonPropertyName("gen_software_capabilities")]
  public ulong GenerationSoftwareCapabilities { get; set; }

  /// <summary>
  /// Gets or sets the master reference, if present.
  /// </summary>
  [JsonPropertyName("master_ref")]
  public string? MasterRef { get; set; }

  /// <summary>
  /// Gets or sets the list of previous block references.
  /// </summary>
  [JsonPropertyName("prev_refs")]
  public List<string> PreviousReferences { get; set; } = new();

  /// <summary>
  /// Gets or sets the input message descriptor length.
  /// </summary>
  [JsonPropertyName("in_msg_descr_length")]
  public int InMessageDescriptorLength { get; set; }

  /// <summary>
  /// Gets or sets the output message descriptor length.
  /// </summary>
  [JsonPropertyName("out_msg_descr_length")]
  public int OutMessageDescriptorLength { get; set; }

  /// <summary>
  /// Gets or sets the random seed of the block.
  /// </summary>
  [JsonPropertyName("rand_seed")]
  public string RandomSeed { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the identifier of the entity that created the block.
  /// </summary>
  [JsonPropertyName("created_by")]
  public string CreatedBy { get; set; } = string.Empty;
}
