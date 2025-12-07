namespace TonapiClient.Models;

/// <summary>
/// Contains constants for TON blockchain operation codes.
/// </summary>
public static class Opcodes
{
  /// <summary>
  /// Wallet signed external message (wallet_v5_r1).
  /// </summary>
  public const string WalletSignedExternalV5R1 = "0x7369676e";

  /// <summary>
  /// Jetton transfer operation.
  /// </summary>
  public const string JettonTransfer = "0x0f8a7ea5";

  /// <summary>
  /// Jetton internal transfer operation.
  /// </summary>
  public const string JettonInternalTransfer = "0x178d4519";

  /// <summary>
  /// Jetton transfer notification to recipient.
  /// </summary>
  public const string JettonNotify = "0x7362d09c";

  /// <summary>
  /// Excess message (returning unused TON).
  /// </summary>
  public const string Excess = "0xd53276db";

  /// <summary>
  /// Text comment message.
  /// </summary>
  public const string TextComment = "0x00000000";
}

