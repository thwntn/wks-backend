namespace Wks.Mains;

// Summary:
//     HTTP status codes used for custom exception handling.
//     Provides standardized status codes for different types of errors.
//
public enum HttpStatus
{
    // Summary:
    //     Bad Request status code (400) for invalid client requests.
    //
    BadRequest = 400,

    Unauthorized = 401,

    // Summary:
    //     Forbidden status code (403) for unauthorized access attempts.
    //
    Forbidden = 403,

    NotFound = 404,

    // Summary:
    //     OK status code (200) for successful operations.
    //
    Ok = 200,

    // Summary:
    //     Internal Server Error status code (500) for server-side errors.
    //
    InternalServerError = 500,
}

// Summary:
//     Custom exception class for HTTP-specific errors with status codes and values.
//     Allows throwing exceptions with specific HTTP status codes and response values.
//
// Parameters:
//   statusCode:
//     The HTTP status code to return in the response.
//   value:
//     The value to include in the response body.
//
public class HttpException(
    HttpStatus statusCode,
    string value,
    int errorCode = 0,
    object data = default
) : Exception
{
    public int ErrorCode { get; } = errorCode;

    // Summary:
    //     Gets the HTTP status code as an integer for the response.
    //
    public int StatusCode { get; } = (int)statusCode;

    // Summary:
    //     Gets the value to include in the response body.
    //
    public string Value { get; } = value;

    //
    // Summary:
    //     Gets the additional data payload for detailed error context.
    //
    // Returns:
    //     An object containing structured error details, or null when not provided.
    //
    public new object Data { get; } = data;
}

// Summary:
//     Global exception filter for handling custom HTTP exceptions.
//     Converts HttpException instances to appropriate HTTP responses.
//
public class ExceptionFilter : IActionFilter, IOrderedFilter
{
    // Summary:
    //     Gets the order of execution for this filter.
    //     Set to high priority to ensure it runs after other filters.
    //
    public int Order => int.MaxValue - 10;

    // Summary:
    //     Called before the action method is executed.
    //     No specific logic implemented for pre-execution handling.
    //
    // Parameters:
    //   context:
    //     The action executing context containing information about the action.
    //
    public void OnActionExecuting(ActionExecutingContext context) { }

    // Summary:
    //     Called after the action method is executed.
    //     Handles HttpException instances by converting them to HTTP responses.
    //
    // Parameters:
    //   context:
    //     The action executed context containing information about the completed action.
    //
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Summary:
        //     Checks if the exception is a custom HttpException and handles it accordingly.
        //     Creates an ObjectResult with the appropriate status code and value.
        //
        if (context.Exception is HttpException httpException)
        {
            context.Result = new ObjectResult(
                new ExceptionResponse(
                    httpException.ErrorCode,
                    httpException.Value,
                    context.ActionDescriptor.Id
                )
            )
            {
                StatusCode = httpException.StatusCode,
            };
            context.ExceptionHandled = true;
        }
    }
}
