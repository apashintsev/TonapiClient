namespace TonapiClient.Tests.V5R1;

/// <summary>
/// Contains numeric constants for TON blockchain operation codes (uint32 values).
/// Use these when building transactions with Builder.StoreUint().
/// </summary>
public static class PopularOpcodes
{
    /// <summary>
    /// Wallet signed external message (wallet_v5_r1).
    /// </summary>
    public const uint WalletSignedExternalV5R1 = 0x7369676e;

    /// <summary>
    /// Jetton transfer operation.
    /// </summary>
    public const uint JettonTransfer = 0x0f8a7ea5;

    /// <summary>
    /// Jetton internal transfer operation.
    /// </summary>
    public const uint JettonInternalTransfer = 0x178d4519;

    /// <summary>
    /// Jetton transfer notification to recipient.
    /// </summary>
    public const uint JettonNotify = 0x7362d09c;

    /// <summary>
    /// Excess message (returning unused TON).
    /// </summary>
    public const uint Excess = 0xd53276db;

    /// <summary>
    /// Text comment message.
    /// </summary>
    public const uint TextComment = 0x00000000;
}