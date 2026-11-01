namespace Wks.UseCases;

//
// Summary:
//     Defines the contract for HTTP request operations.
//     Provides methods for making different types of HTTP requests including GET, JSON POST, and form-encoded requests.
//
public interface IRequest
{
    //
    // Summary:
    //     Makes a GET request to the specified URI with query parameters and headers.
    //
    // Parameters:
    //   uri:
    //     The target URI for the GET request.
    //   queries:
    //     Dictionary of query parameters to append to the URI.
    //   headers:
    //     Dictionary of HTTP headers to include in the request.
    //
    // Returns:
    //     A task that represents the asynchronous operation, returning the response content as a string.
    //
    Task<HttpObject.Response<TResult>> GetAsync<TResult>(
        string uri,
        Dictionary<string, string> queries = default,
        Dictionary<string, string> headers = default
    );

    //
    // Summary:
    //     Makes a JSON POST request to the specified URL with data and headers.
    //
    // Type parameters:
    //   TResult:
    //     The type to deserialize the response into.
    //
    // Parameters:
    //   url:
    //     The target URL for the JSON POST request.
    //   data:
    //     Dictionary of data to serialize as JSON in the request body.
    //   headers:
    //     Dictionary of HTTP headers to include in the request.
    //
    // Returns:
    //     A task that represents the asynchronous operation, returning a tuple containing:
    //     - Success status (bool)
    //     - Deserialized result (TResult)
    //     - Error message or response content (string)
    //
    Task<HttpObject.Response<TResult>> JsonAsync<TResult>(
        string url,
        object data = default,
        Dictionary<string, string> headers = default
    );

    //
    // Summary:
    //     Makes a form-encoded POST request to the specified URI with form data and headers.
    //
    // Type parameters:
    //   TResult:
    //     The type to deserialize the response into.
    //
    // Parameters:
    //   uri:
    //     The target URI for the form-encoded POST request.
    //   formUrlEncodedContent:
    //     The form-encoded content to send in the request body.
    //   headers:
    //     Dictionary of HTTP headers to include in the request.
    //
    // Returns:
    //     A task that represents the asynchronous operation, returning a tuple containing:
    //     - Success status (bool)
    //     - Deserialized result (TResult)
    //     - Error message or response content (string)
    //
    Task<HttpObject.Response<TResult>> FormUrlEncoded<TResult>(
        string uri,
        FormUrlEncodedContent formUrlEncodedContent,
        Dictionary<string, string> headers = default
    );
}
