using System.Text;
using Ton.Core.Boc;
using Ton.Core.Types;
using Ton.Crypto.Ed25519;

namespace TonapiClient.Tests.V5R1;

/// <summary>
/// Extension methods for <see cref="WalletV5R1"/>.
/// </summary>
public static class WalletV5R1Utils
{
  /// <summary>
  /// Normalizes message hash. For external-in messages, creates a special cell structure and returns its hash.
  /// For other message types, returns the body hash directly.
  /// </summary>
  /// <param name="message">The message to normalize.</param>
  /// <returns>The normalized hash as byte array.</returns>
  public static byte[] NormalizeHash(Message message)
  {
    // Check if message is not external-in
    if (message.Info is not CommonMessageInfo.ExternalIn)
    {
      return message.Body.Hash();
    }

    // For external-in messages, build special cell structure
    var cell = Builder.BeginCell()
      .StoreUint(2, 2) // external-in
      .StoreUint(0, 2) // addr_none
      .StoreAddress(((CommonMessageInfo.ExternalIn)message.Info).Dest)
      .StoreUint(0, 4) // import_fee = 0
      .StoreBit(false) // no StateInit ?????????????????????
      .StoreBit(true) // store body as reference
      .StoreRef(message.Body)
      .EndCell();

    return cell.Hash();
  }


  /// <summary>
  /// Create unsigned transfer
  /// </summary>
  public static Cell CreateUnsignedTransfer(
WalletIdV5R1 walletId,
      ulong seqno,
      List<MessageRelaxed> messages,
      SendMode sendMode,
      int? timeout = null,
      string? authType = null)
  {
    var actions = messages
        .Select(msg => (IWalletV5Action)new OutActionSendMsg(sendMode, msg))
        .ToList();

    return CreateRequestWithoutSign(walletId, seqno, actions, timeout, authType);
  }

  /// <summary>
  /// Create the same request as <c>CreateRequest</c> but return the unsigned signing message cell
  /// (i.e. the cell produced by <c>signingMessageBuilder.EndCell()</c> in the original method).
  /// </summary>
  public static Cell CreateRequestWithoutSign(
      WalletIdV5R1 walletId,
      ulong seqno,
      List<IWalletV5Action> actions,
      int? timeout = null,
      string? authType = null,
      ulong? queryId = null)
  {
    if (actions == null)
    {
      throw new ArgumentNullException(nameof(actions));
    }

    if (actions.Count > 255)
    {
      throw new ArgumentException("Maximum number of OutActions in a single request is 255");
    }

    authType ??= "external";

    // Extension auth doesn't need signing and is returned as in CreateRequest
    if (authType == "extension")
    {
      return Builder.BeginCell()
          .StoreUint(WalletV5R1.OpCodes.AuthExtension, 32)
          .StoreUint(queryId ?? 0, 64)
          .StoreOutListExtendedV5R1(actions)
          .EndCell();
    }

    // Patch send modes for safety like original CreateRequest
    actions = WalletV5R1Actions.PatchV5R1ActionsSendMode(actions, authType);

    // Build signing message (same as in CreateRequest up to EndCell)
    var signingMessageBuilder = Builder.BeginCell()
        .StoreUint(authType == "internal" ? WalletV5R1.OpCodes.AuthSignedInternal : WalletV5R1.OpCodes.AuthSignedExternal, 32);

    // Store wallet id into builder (same helper used by original class)
    WalletV5R1WalletIdHelper.StoreWalletIdV5R1(walletId)(signingMessageBuilder);

    // Handle seqno 0 special case
    if (seqno == 0)
    {
      for (var i = 0; i < 32; i++)
      {
        signingMessageBuilder.StoreBit(true);
      }
    }
    else
    {
      var actualTimeout = timeout ?? (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 120);
      signingMessageBuilder.StoreUint((ulong)actualTimeout, 32);
    }

    signingMessageBuilder
        .StoreUint((ulong)seqno, 32)
        .StoreOutListExtendedV5R1(actions);

    // Return unsigned signing message cell (no Ed25519 signature appended)
    var signingMessage = signingMessageBuilder.EndCell();
    return signingMessage;
  }

  /// <summary>
  /// Sign and pack signing message (V5 format)
  /// </summary>
  public static Cell SignAndPack(Cell signingMessage, byte[] secretKey)
  {
    // Sign and pack
    var signature = Ed25519.Sign(signingMessage.Hash(), secretKey);

    // Pack signature at tail (V5 format)
    return Builder.BeginCell()
        .StoreSlice(signingMessage.BeginParse())
        .StoreBuffer(signature)
        .EndCell();
  }

  /// <summary>
  /// Decodes a BOC (Bag of Cells) message and returns detailed information about it.
  /// </summary>
  /// <param name="bocBase64">Base64-encoded BOC string.</param>
  /// <returns>Decoded BOC message information.</returns>
  public static DecodedBocInfo DecodeBoc(string bocBase64)
  {
    byte[] bocBytes = Convert.FromBase64String(bocBase64);
    return DecodeBoc(bocBytes);
  }

  /// <summary>
  /// Decodes a BOC (Bag of Cells) message and returns detailed information about it.
  /// Based on TON TL-B specification: https://docs.ton.org/develop/data-formats/tl-b-language
  /// </summary>
  /// <param name="bocBytes">BOC bytes.</param>
  /// <returns>Decoded BOC message information.</returns>
  public static DecodedBocInfo DecodeBoc(byte[] bocBytes)
  {
    // Parse BOC to Cell
    Cell[] cells = Cell.FromBoc(bocBytes);
    Cell messageCell = cells[0];

    var info = new DecodedBocInfo
    {
      MessageCell = messageCell,
      CellHash = messageCell.Hash()
    };

    // Parse the cell to extract basic information
    var slice = messageCell.BeginParse();
    
    try
    {
      // According to TON TL-B spec:
      // ext_in_msg_info$10 = prefix 0b10
      // int_msg_info$0 = prefix 0b0
      // ext_out_msg_info$11 = prefix 0b11
      
      // Read first bit
      var firstBit = slice.LoadBit();
      
      if (!firstBit) // int_msg_info$0
      {
        info.MessageType = "Internal";
        info.IhrDisabled = slice.LoadBit();
        info.Bounce = slice.LoadBit();
        slice.LoadBit(); // bounced
        
        // Load source address
        var srcAddr = slice.LoadAddress();
        info.Source = srcAddr?.ToString();
        
        // Load destination address
        var destAddr = slice.LoadAddress();
        info.Destination = destAddr?.ToString();
        
        // Load value (CurrencyCollection)
        var coins = slice.LoadCoins();
        info.Value = coins.ToString();
        
        // Skip other fields: ihr_fee, fwd_fee, created_lt, created_at
      }
      else // First bit is 1
      {
        var secondBit = slice.LoadBit();
        
        if (!secondBit) // ext_in_msg_info$10
        {
          info.MessageType = "ExternalIn";
          
          // Load source (MsgAddressExt) - usually addr_none$00
          var srcAddr = slice.LoadAddress();
          info.Source = srcAddr?.ToString();
          
          // Load destination (MsgAddressInt)
          var destAddr = slice.LoadAddress();
          info.Destination = destAddr?.ToString();
          
          // import_fee:Grams
          var importFee = slice.LoadCoins();
        }
        else // ext_out_msg_info$11
        {
          info.MessageType = "ExternalOut";
          
          // Load source (MsgAddressInt)
          var srcAddr = slice.LoadAddress();
          info.Source = srcAddr?.ToString();
          
          // Load destination (MsgAddressExt)
          var destAddr = slice.LoadAddress();
          
          // created_lt:uint64 created_at:uint32
        }
      }
    }
    catch (Exception ex)
    {
      // If parsing fails, we still have the cell
      info.MessageType = $"Unknown";
      info.Destination = $"Error: {ex.GetType().Name}: {ex.Message}";
    }

    // Get cell depth and refs
    info.BodyRefsCount = messageCell.Refs != null ? messageCell.Refs.Count() : 0;

    return info;
  }
}

/// <summary>
/// Contains decoded information from a BOC message.
/// </summary>
public class DecodedBocInfo
{
  /// <summary>
  /// The parsed message cell.
  /// </summary>
  public Cell? MessageCell { get; set; }

  /// <summary>
  /// Hash of the cell.
  /// </summary>
  public byte[]? CellHash { get; set; }

  /// <summary>
  /// Type of message: ExternalIn, Internal, or ExternalOut.
  /// </summary>
  public string MessageType { get; set; } = string.Empty;

  /// <summary>
  /// Source address (if applicable).
  /// </summary>
  public string? Source { get; set; }

  /// <summary>
  /// Destination address.
  /// </summary>
  public string? Destination { get; set; }

  /// <summary>
  /// Value in nanotons (for internal messages).
  /// </summary>
  public string? Value { get; set; }

  /// <summary>
  /// Bounce flag (for internal messages).
  /// </summary>
  public bool? Bounce { get; set; }

  /// <summary>
  /// IHR disabled flag (for internal messages).
  /// </summary>
  public bool? IhrDisabled { get; set; }

  /// <summary>
  /// Number of references in the cell.
  /// </summary>
  public int? BodyRefsCount { get; set; }

  /// <summary>
  /// Returns a formatted string representation of the decoded BOC.
  /// </summary>
  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.AppendLine($"Message Type: {MessageType}");
    
    if (CellHash != null)
      sb.AppendLine($"Cell Hash: {Convert.ToHexString(CellHash)}");
    
    if (!string.IsNullOrEmpty(Source))
      sb.AppendLine($"Source: {Source}");
    
    if (!string.IsNullOrEmpty(Destination))
      sb.AppendLine($"Destination: {Destination}");
    
    if (!string.IsNullOrEmpty(Value))
      sb.AppendLine($"Value: {Value} nanotons");
    
    if (Bounce.HasValue)
      sb.AppendLine($"Bounce: {Bounce.Value}");
    
    if (IhrDisabled.HasValue)
      sb.AppendLine($"IHR Disabled: {IhrDisabled.Value}");
    
    if (BodyRefsCount.HasValue)
      sb.AppendLine($"Cell Refs: {BodyRefsCount.Value}");
    
    return sb.ToString();
  }
}
