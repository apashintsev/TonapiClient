using Ton.Core.Addresses;
using Ton.Core.Boc;
using Ton.Core.Types;

namespace TonapiClient.Tests.V5R1;

/// <summary>
/// Extension methods for Builder to enable fluent API.
/// </summary>
public static class BuilderExtensions
{
  /// <summary>
  /// Stores a Message into the builder using fluent API.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="message">The message to store.</param>
  /// <param name="forceRef">Force storing init and body as references.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreMessage(this Builder builder, Message message, bool forceRef = false)
  {
    message.Store(builder, forceRef);
    return builder;
  }

  /// <summary>
  /// Stores a MessageRelaxed into the builder using fluent API.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="message">The relaxed message to store.</param>
  /// <param name="forceRef">Force storing init and body as references.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreMessageRelaxed(this Builder builder, MessageRelaxed message, bool forceRef = false)
  {
    message.Store(builder, forceRef);
    return builder;
  }

  /// <summary>
  /// Applies a store action to the builder (functional style).
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="storeAction">The action that stores data.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder Store(this Builder builder, Action<Builder> storeAction)
  {
    storeAction(builder);
    return builder;
  }

  /// <summary>
  /// Stores an Address into the builder using fluent API.
  /// Stores addr_none (0b00) if address is null.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="address">The address to store (null for addr_none).</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreAddressOrNone(this Builder builder, Address? address)
  {
    if (address != null)
    {
      builder.StoreAddress(address);
    }
    else
    {
      builder.StoreUint(0, 2); // addr_none
    }
    return builder;
  }

  /// <summary>
  /// Stores a StateInit into the builder using fluent API.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="stateInit">The state init to store.</param>
  /// <param name="asRef">Whether to store as reference.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreStateInit(this Builder builder, StateInit stateInit, bool asRef = false)
  {
    if (asRef)
    {
      var initCell = Builder.BeginCell();
      stateInit.Store(initCell);
      builder.StoreRef(initCell.EndCell());
    }
    else
    {
      stateInit.Store(builder);
    }
    return builder;
  }

  /// <summary>
  /// Stores an optional StateInit into the builder.
  /// Stores 0 bit if null, or 1 bit followed by StateInit if not null.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="stateInit">The optional state init.</param>
  /// <param name="asRef">Whether to store StateInit as reference (if not null).</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreMaybeStateInit(this Builder builder, StateInit? stateInit, bool asRef = false)
  {
    if (stateInit != null)
    {
      builder.StoreBit(true);
      builder.StoreStateInit(stateInit, asRef);
    }
    else
    {
      builder.StoreBit(false);
    }
    return builder;
  }

  /// <summary>
  /// Stores a Cell into the builder, either inline or as reference.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="cell">The cell to store.</param>
  /// <param name="asRef">Whether to store as reference.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreCell(this Builder builder, Cell cell, bool asRef = false)
  {
    if (asRef)
    {
      builder.StoreRef(cell);
    }
    else
    {
      builder.StoreBuilder(cell.AsBuilder());
    }
    return builder;
  }

  /// <summary>
  /// Stores an optional Cell with a presence bit.
  /// Stores 0 bit if null, or 1 bit followed by Cell if not null.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="cell">The optional cell.</param>
  /// <param name="asRef">Whether to store Cell as reference (if not null).</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreMaybeCell(this Builder builder, Cell? cell, bool asRef = true)
  {
    if (cell != null)
    {
      builder.StoreBit(true);
      builder.StoreCell(cell, asRef);
    }
    else
    {
      builder.StoreBit(false);
    }
    return builder;
  }

  /// <summary>
  /// Stores a CommonMessageInfo into the builder using fluent API.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="info">The message info to store.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreCommonMessageInfo(this Builder builder, CommonMessageInfo info)
  {
    info.Store(builder);
    return builder;
  }

  /// <summary>
  /// Stores a CommonMessageInfoRelaxed into the builder using fluent API.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="info">The relaxed message info to store.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreCommonMessageInfoRelaxed(this Builder builder, CommonMessageInfoRelaxed info)
  {
    info.Store(builder);
    return builder;
  }

  /// <summary>
  /// Stores a string as a tail (snake format) into the builder.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="text">The text to store.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreText(this Builder builder, string text)
  {
    builder.StoreStringTail(text);
    return builder;
  }

  /// <summary>
  /// Stores a coins amount (varuint16) into the builder.
  /// </summary>
  /// <param name="builder">The builder.</param>
  /// <param name="amount">The amount in nanotons.</param>
  /// <returns>The builder for chaining.</returns>
  public static Builder StoreCoinsAmount(this Builder builder, ulong amount)
  {
    builder.StoreCoins(amount);
    return builder;
  }
}

/// <summary>
/// Functional storers for composable storage operations.
/// </summary>
public static class Storers
{
  /// <summary>
  /// Creates a store action for Message (functional style).
  /// </summary>
  /// <param name="message">The message to store.</param>
  /// <param name="forceRef">Force storing init and body as references.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> Message(Message message, bool forceRef = false)
  {
    return builder => message.Store(builder, forceRef);
  }

  /// <summary>
  /// Creates a store action for MessageRelaxed (functional style).
  /// </summary>
  /// <param name="message">The relaxed message to store.</param>
  /// <param name="forceRef">Force storing init and body as references.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> MessageRelaxed(MessageRelaxed message, bool forceRef = false)
  {
    return builder => message.Store(builder, forceRef);
  }

  /// <summary>
  /// Creates a store action for Address (functional style).
  /// </summary>
  /// <param name="address">The address to store (null for addr_none).</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> Address(Address? address)
  {
    return builder => builder.StoreAddressOrNone(address);
  }

  /// <summary>
  /// Creates a store action for StateInit (functional style).
  /// </summary>
  /// <param name="stateInit">The state init to store.</param>
  /// <param name="asRef">Whether to store as reference.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> StateInit(StateInit stateInit, bool asRef = false)
  {
    return builder => builder.StoreStateInit(stateInit, asRef);
  }

  /// <summary>
  /// Creates a store action for Cell (functional style).
  /// </summary>
  /// <param name="cell">The cell to store.</param>
  /// <param name="asRef">Whether to store as reference.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> Cell(Cell cell, bool asRef = false)
  {
    return builder => builder.StoreCell(cell, asRef);
  }

  /// <summary>
  /// Creates a store action for CommonMessageInfo (functional style).
  /// </summary>
  /// <param name="info">The message info to store.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> CommonMessageInfo(CommonMessageInfo info)
  {
    return builder => info.Store(builder);
  }

  /// <summary>
  /// Creates a store action for uint (functional style).
  /// </summary>
  /// <param name="value">The value to store.</param>
  /// <param name="bits">Number of bits.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> Uint(ulong value, int bits)
  {
    return builder => builder.StoreUint(value, bits);
  }

  /// <summary>
  /// Creates a store action for bit (functional style).
  /// </summary>
  /// <param name="value">The bit value.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> Bit(bool value)
  {
    return builder => builder.StoreBit(value);
  }

  /// <summary>
  /// Creates a store action for string tail (functional style).
  /// </summary>
  /// <param name="text">The text to store.</param>
  /// <returns>A store action.</returns>
  public static Action<Builder> Text(string text)
  {
    return builder => builder.StoreStringTail(text);
  }

  /// <summary>
  /// Combines multiple store actions into one (composition).
  /// </summary>
  /// <param name="actions">The actions to combine.</param>
  /// <returns>A combined store action.</returns>
  public static Action<Builder> Combine(params Action<Builder>[] actions)
  {
    return builder =>
    {
      foreach (var action in actions)
      {
        action(builder);
      }
    };
  }

  /// <summary>
  /// Creates a conditional store action.
  /// </summary>
  /// <param name="condition">The condition.</param>
  /// <param name="trueAction">Action to execute if condition is true.</param>
  /// <param name="falseAction">Action to execute if condition is false (optional).</param>
  /// <returns>A conditional store action.</returns>
  public static Action<Builder> If(bool condition, Action<Builder> trueAction, Action<Builder>? falseAction = null)
  {
    return builder =>
    {
      if (condition)
      {
        trueAction(builder);
      }
      else
      {
        falseAction?.Invoke(builder);
      }
    };
  }
}

