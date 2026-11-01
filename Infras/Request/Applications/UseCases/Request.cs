using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace Wks.UseCases;

//
// Summary:
//     Implements HTTP request operations for making different types of HTTP requests.
//     Provides functionality for GET requests, JSON POST requests, and form-encoded requests.
//
public class RequestService(IHttpClientFactory _httpClientFactory) : IRequest
{
    ///
    /// Summary:
    ///     Executes an IRequest operation under the current Logger trace and records its lifecycle.
    ///     Covers request preparation, HTTP execution, response reading, and response deserialization.
    ///
    /// Logic:
    ///     Reuses the ambient trace or creates a scope when the operation has no caller trace.
    ///     Extracts only the destination host; URL paths, queries, headers, and payloads are not logged.
    ///     Writes Started before executing the action, then Completed for successful HTTP responses.
    ///     Writes Failed at Error level for unsuccessful HTTP responses, including the existing synthetic 503.
    ///     Records the exception type and rethrows preparation or parsing failures without changing their behavior.
    ///     Disposes the logging scope after the operation to restore the previous trace.
    ///
    /// Type parameters:
    ///   TResult:
    ///     The response data type returned by the existing IRequest operation.
    ///
    /// Parameters:
    ///   uri:
    ///     The destination URI used only to determine the host for log metadata.
    ///   method:
    ///     The HTTP method of the operation.
    ///   action:
    ///     The complete existing request operation, including its response conversion.
    ///
    /// Returns:
    ///     The original typed response with its status, data, error, and response time unchanged.
    ///
    private static async Task<HttpObject.Response<TResult>> ExecuteLoggedAsync<TResult>(
        string uri,
        HttpMethod method,
        Func<Task<HttpObject.Response<TResult>>> action
    )
    {
        using IDisposable scope = Logger.BeginScope(Logger.TraceId);
        Stopwatch watch = Stopwatch.StartNew();
        string host = string.Empty;
        if (Uri.TryCreate(uri, UriKind.Absolute, out Uri address))
            host = address.Host;

        Logger.Write(new LoggingSchema.Http { LogIndexName = Runtime.LogIndexName.Env(),
            Source = nameof(LogEventEnum.Http), Operation = method.Method,
            Status = nameof(LogEventEnum.Started), Host = host,
        });
        try
        {
            HttpObject.Response<TResult> response = await action();
            string status = nameof(LogEventEnum.Completed);
            string level = nameof(LogLevel.Information);
            if (response.IsSuccessStatusCode is false)
            {
                status = nameof(LogEventEnum.Failed);
                level = nameof(LogLevel.Error);
            }
            Logger.Write(new LoggingSchema.Http { LogIndexName = Runtime.LogIndexName.Env(),
                Source = nameof(LogEventEnum.Http), Operation = method.Method,
                Status = status, Host = host, StatusCode = response.StatusCode,
                DurationMs = watch.Elapsed.TotalMilliseconds,
            }, level);
            return response;
        }
        catch (Exception error)
        {
            Logger.Write(new LoggingSchema.Http { LogIndexName = Runtime.LogIndexName.Env(),
                Source = nameof(LogEventEnum.Http), Operation = method.Method,
                Status = nameof(LogEventEnum.Failed), Host = host,
                DurationMs = watch.Elapsed.TotalMilliseconds, Error = error.GetType().Name,
            }, nameof(LogLevel.Error));
            throw;
        }
    }

    //
    // Summary:
    //     Executes an HTTP request task with elapsed time measurement and error resilience.
    //     Returns a Duration object containing the response and response time in milliseconds.
    //     On network failure, returns a 503 ServiceUnavailable response instead of throwing.
    //
    // Parameters:
    //   request:
    //     The HTTP request task to execute and measure.
    //
    // Returns:
    //     A Duration object with response and elapsed time, never throws.
    //
    private static async Task<HttpObject.Duration> ExecuteSafe(
        Task<HttpResponseMessage> request
    )
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        try
        {
            HttpResponseMessage response = await request;
            stopwatch.Stop();
            return new(response, (decimal)stopwatch.Elapsed.TotalMilliseconds);
        }
        catch (Exception)
        {
            stopwatch.Stop();
            HttpResponseMessage failedResponse = new(
                System.Net.HttpStatusCode.ServiceUnavailable
            );
            return new(
                failedResponse,
                (decimal)stopwatch.Elapsed.TotalMilliseconds
            );
        }
    }

    //
    // Summary:
    //     Parses HTTP response content into a typed Response object.
    //     Handles success and failure cases by reading body and deserializing result.
    //
    // Type parameters:
    //   TResult:
    //     The target type to deserialize the response body into.
    //
    // Parameters:
    //   duration:
    //     The Duration object containing HTTP response and elapsed time.
    //
    // Returns:
    //     A Response object with success status, deserialized data, error, status code, and response time.
    //
    private static async Task<HttpObject.Response<TResult>> Result<TResult>(
        HttpObject.Duration duration
    )
    {
        string content = await duration.Response.Content.ReadAsStringAsync();

        if (duration.Response.IsSuccessStatusCode is false)
            return new(
                false,
                default,
                content,
                (int)duration.Response.StatusCode,
                duration.ResponseTime
            );

        TResult result =
            typeof(TResult) == typeof(string) ? (TResult)(object)content
            : string.IsNullOrWhiteSpace(content) ? default
            : content.Deserialize<TResult>();

        return new(
            true,
            result,
            string.Empty,
            (int)duration.Response.StatusCode,
            duration.ResponseTime
        );
    }

    //
    // Summary:
    //     Makes a GET request to the specified URI with query parameters and headers.
    //     Uses HttpClient to perform the request and returns the response content as a string.
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
    public async Task<HttpObject.Response<TResult>> GetAsync<TResult>(
        string uri,
        Dictionary<string, string> queries,
        Dictionary<string, string> headers
    ) =>
        await ExecuteLoggedAsync(uri, HttpMethod.Get, async () =>
        {
            //
            // Summary:
            //     Creates a new HttpClient instance for making the request.
            //
            HttpClient _ = _httpClientFactory.CreateClient();

            //
            // Summary:
            //     Adds the provided headers to the HTTP request.
            //
            if (headers.Justifiable())
                foreach (KeyValuePair<string, string> item in headers)
                    _.DefaultRequestHeaders.Add(item.Key, item.Value);

            if (queries.Justifiable())
                uri = QueryHelpers.AddQueryString(uri, queries);
            //
            // Summary:
            //     Makes the JSON POST request and handles the response.
            //
            HttpObject.Duration duration = await ExecuteSafe(_.GetAsync(uri));
            return await Result<TResult>(duration);
        });

    //
    // Summary:
    //     Makes a JSON POST request to the specified URI with data and headers.
    //     Serializes the data as JSON and deserializes the response into the specified type.
    //
    // Type parameters:
    //   TResult:
    //     The type to deserialize the response into.
    //
    // Parameters:
    //   uri:
    //     The target URI for the JSON POST request.
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
    public async Task<HttpObject.Response<TResult>> JsonAsync<TResult>(
        string uri,
        object data = default,
        Dictionary<string, string> headers = default
    ) =>
        await ExecuteLoggedAsync(uri, HttpMethod.Post, async () =>
        {
            //
            // Summary:
            //     Creates a new HttpClient instance for making the request.
            //
            HttpClient _ = _httpClientFactory.CreateClient();

            //
            // Summary:
            //     Adds the provided headers to the HTTP request.
            //
            if (headers.Justifiable())
                foreach (KeyValuePair<string, string> item in headers)
                    _.DefaultRequestHeaders.Add(item.Key, item.Value);

            StringContent content = new(
                data.Serialize(),
                Encoding.UTF8,
                MediaTypeNames.Application.Json
            );
            //
            // Summary:
            //     Makes the JSON POST request and handles the response.
            //
            HttpObject.Duration duration = await ExecuteSafe(
                _.PostAsync(uri, content)
            );
            return await Result<TResult>(duration);
        });

    //
    // Summary:
    //     Makes a form-encoded POST request to the specified URI with form data and headers.
    //     Sends form-encoded content and deserializes the response into the specified type.
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
    public async Task<HttpObject.Response<TResult>> FormUrlEncoded<TResult>(
        string uri,
        FormUrlEncodedContent formUrlEncodedContent,
        Dictionary<string, string> headers
    ) =>
        await ExecuteLoggedAsync(uri, HttpMethod.Post, async () =>
        {
            //
            // Summary:
            //     Creates a new HttpClient instance for making the request.
            //
            HttpClient _ = _httpClientFactory.CreateClient();

            //
            // Summary:
            //     Adds the provided headers to the HTTP request.
            //
            if (headers.Justifiable())
                foreach (KeyValuePair<string, string> item in headers)
                    _.DefaultRequestHeaders.Add(item.Key, item.Value);

            //
            // Summary:
            //     Makes the form-encoded POST request and handles the response.
            //
            HttpObject.Duration duration = await ExecuteSafe(
                _.PostAsync(uri, formUrlEncodedContent)
            );
            return await Result<TResult>(duration);
        });
}
