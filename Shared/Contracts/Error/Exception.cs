namespace Wks.Objects;

///
/// Summary:
///     Represents a standardized exception response for API error handling.
///     Used to return consistent error information to clients.
///
public class ExceptionResponse(int errorCode, string message, string traceId)
{
    ///
    /// Summary:
    ///     Application-specific error code for client-side error handling.
    ///
    public int ErrorCode { get; set; } = errorCode;

    ///
    /// Summary:
    ///     Human-readable error message describing the exception.
    ///
    public string Message { get; set; } = message;

    ///
    /// Summary:
    ///     Unique trace identifier for tracking and debugging the error.
    ///
    public string TraceId { get; set; } = traceId;
}
