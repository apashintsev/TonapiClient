using System.Text.Json.Serialization;

namespace TonapiClient.Models;

/// <summary>
/// Represents the type of transfer in a trace.
/// </summary>
public enum TransferType
{
  /// <summary>
  /// Unknown transfer type.
  /// </summary>
  Unknown,

  /// <summary>
  /// Native TON transfer.
  /// </summary>
  Native,

  /// <summary>
  /// Jetton (token) transfer.
  /// </summary>
  Jetton,
}

/// <summary>
/// Represents a trace of transaction chain.
/// </summary>
public class Trace
{
  /// <summary>
  /// Gets or sets the transaction.
  /// </summary>
  [JsonPropertyName("transaction")]
  public Transaction Transaction { get; set; } = new();

  /// <summary>
  /// Gets or sets the list of interfaces involved.
  /// </summary>
  [JsonPropertyName("interfaces")]
  public List<string> Interfaces { get; set; } = new();

  /// <summary>
  /// Gets or sets the children traces.
  /// </summary>
  [JsonPropertyName("children")]
  public List<Trace> Children { get; set; } = new();

  /// <summary>
  /// Gets or sets a value indicating whether this trace is emulated.
  /// </summary>
  [JsonPropertyName("emulated")]
  public bool Emulated { get; set; }

  /// <summary>
  /// Determines whether the trace is finalized.
  /// A trace is considered finalized when it is not emulated,
  /// has no internal messages in out_msgs, and all children are finalized.
  /// </summary>
  /// <returns>True if the trace is finalized; otherwise, false.</returns>
  public bool IsFinalized()
  {
    if (Emulated)
    {
      return false;
    }

    if (Transaction.OutMsgs != null)
    {
      foreach (var msg in Transaction.OutMsgs)
      {
        if (msg.MsgType == "int_msg")
        {
          return false;
        }
      }
    }

    if (Children != null)
    {
      foreach (var child in Children)
      {
        if (!child.IsFinalized())
        {
          return false;
        }
      }
    }

    return true;
  }

  /// <summary>
  /// Determines whether the trace contains any unsuccessful transactions.
  /// Recursively checks the current transaction and all children.
  /// </summary>
  /// <returns>True if at least one transaction has Success == false; otherwise, false.</returns>
  public bool HasUnsuccessfulTransactions()
  {
    // Check current transaction - unsuccessful only if Success is explicitly false
    if (Transaction.InMsg?.OpCode == Opcodes.JettonNotify)
    {
      return false;
    }

    if (Transaction.Success == false)
    {
      return true;
    }

    // Recursively check all children
    if (Children != null)
    {
      foreach (var child in Children)
      {
        if (child.HasUnsuccessfulTransactions())
        {
          return true;
        }
      }
    }

    return false;
  }

  /// <summary>
  /// Calculates the total fees for the entire trace.
  /// Recursively sums up total_fees from the current transaction and all children transactions.
  /// </summary>
  /// <returns>The sum of all total_fees in the trace tree.</returns>
  public ulong GetTotalFees()
  {
    ulong totalFees = Transaction.TotalFees;

    if (Children != null)
    {
      foreach (var child in Children)
      {
        totalFees += child.GetTotalFees();
      }
    }

    return totalFees;
  }

  /// <summary>
  /// Determines the type of transfer in the trace.
  /// </summary>
  /// <returns>The transfer type (Native, Jetton, or Unknown).</returns>
  public TransferType GetTransferType()
  {
    // Check if this is a jetton transfer by looking for jetton_transfer op_code
    if (Transaction.InMsg?.OpCode == Opcodes.JettonTransfer)
    {
      return TransferType.Jetton;
    }

    // Check in children for jetton operations
    if (Children != null && Children.Count > 0)
    {
      foreach (var child in Children)
      {
        if (child.Transaction.InMsg?.OpCode == Opcodes.JettonTransfer ||
            child.Transaction.InMsg?.OpCode == Opcodes.JettonInternalTransfer ||
            HasJettonNotify(child))
        {
          return TransferType.Jetton;
        }
      }

      // Check if this is a native transfer (has child with int_msg and value > 0)
      foreach (var child in Children)
      {
        if (child.Transaction.InMsg?.MsgType == "int_msg" &&
            child.Transaction.InMsg?.Value > 0 &&
            child.Transaction.InMsg?.OpCode != Opcodes.JettonTransfer &&
            child.Transaction.InMsg?.OpCode != Opcodes.JettonInternalTransfer)
        {
          return TransferType.Native;
        }
      }
    }

    return TransferType.Unknown;
  }

  /// <summary>
  /// Gets the sender address of the transfer.
  /// </summary>
  /// <returns>The sender address, or null if not found.</returns>
  public string? GetSender()
  {
    // For transfers initiated by wallet, the sender is the root transaction account
    if (Transaction.InMsg?.MsgType == "ext_in_msg")
    {
      return Transaction.Account?.Address;
    }

    // For incoming transfers, the sender is the source of the incoming message
    if (Transaction.InMsg?.Source != null)
    {
      return Transaction.InMsg.Source.Address;
    }

    return Transaction.Account?.Address;
  }

  /// <summary>
  /// Gets the recipient address of the transfer.
  /// </summary>
  /// <returns>The recipient address, or null if not found.</returns>
  public string? GetRecipient()
  {
    var transferType = GetTransferType();

    if (transferType == TransferType.Jetton)
    {
      // For jetton transfers, find the jetton_notify transaction
      var notifyTransaction = FindTransactionByOpCode(Opcodes.JettonNotify);
      if (notifyTransaction != null)
      {
        return notifyTransaction.Transaction.InMsg?.Destination?.Address;
      }

      // Alternative: look for jetton_transfer and extract destination from decoded_body
      // This would require parsing DecodedBody, so we'll keep the simpler approach
    }
    else if (transferType == TransferType.Native)
    {
      // For native transfers, find the first child with int_msg
      if (Children != null && Children.Count > 0)
      {
        foreach (var child in Children)
        {
          if (child.Transaction.InMsg?.MsgType == "int_msg" &&
              child.Transaction.InMsg?.Value > 0)
          {
            return child.Transaction.InMsg.Destination?.Address;
          }
        }
      }
    }

    return null;
  }

  /// <summary>
  /// Recursively searches for a transaction with the specified op_code.
  /// </summary>
  /// <param name="opCode">The op_code to search for.</param>
  /// <returns>The trace containing the transaction with the specified op_code, or null if not found.</returns>
  private Trace? FindTransactionByOpCode(string opCode)
  {
    if (Transaction.InMsg?.OpCode == opCode)
    {
      return this;
    }

    if (Children != null)
    {
      foreach (var child in Children)
      {
        var result = child.FindTransactionByOpCode(opCode);
        if (result != null)
        {
          return result;
        }
      }
    }

    return null;
  }

  /// <summary>
  /// Checks if the trace or its children contain a jetton_notify operation.
  /// </summary>
  /// <param name="trace">The trace to check.</param>
  /// <returns>True if a jetton_notify operation is found; otherwise, false.</returns>
  private bool HasJettonNotify(Trace trace)
  {
    if (trace.Transaction.InMsg?.OpCode == Opcodes.JettonNotify)
    {
      return true;
    }

    if (trace.Children != null)
    {
      foreach (var child in trace.Children)
      {
        if (HasJettonNotify(child))
        {
          return true;
        }
      }
    }

    return false;
  }
}
