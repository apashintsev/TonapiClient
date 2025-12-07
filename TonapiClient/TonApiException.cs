namespace TonapiClient;

/// <summary>
/// Exception thrown when TON API request fails.
/// </summary>
public class TonApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TonApiException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code.</param>
    /// <param name="errorCode">API error code if available.</param>
    /// <param name="innerException">Inner exception.</param>
    public TonApiException(
        string message,
        int statusCode,
        long? errorCode,
        Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Gets the HTTP status code.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Gets the API error code if available.
    /// </summary>
    public long? ErrorCode { get; }
}
