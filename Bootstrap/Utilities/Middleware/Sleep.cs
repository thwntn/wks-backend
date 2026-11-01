namespace Wks.Mains;

//
// Summary:
//     Custom middleware for adding delays or processing time to HTTP requests.
//     Currently configured to pass through requests without delay, but can be enabled
//     for rate limiting or testing purposes by uncommenting the Thread.Sleep call.
//
// Parameters:
//   next:
//     The next middleware in the request pipeline.
//
public class Sleep(RequestDelegate next)
{
    //
    // Summary:
    //     The next middleware delegate in the request pipeline.
    //     Used to continue processing the HTTP request.
    //
    private readonly RequestDelegate _next = next;

    //
    // Summary:
    //     Processes the HTTP request and optionally adds a delay.
    //     Currently passes through requests immediately, but can be configured
    //     to add delays for rate limiting or testing purposes.
    //
    // Parameters:
    //   context:
    //     The HTTP context containing request and response information.
    //
    // Returns:
    //     A task that represents the asynchronous middleware operation.
    //
    public async Task Invoke(HttpContext context)
    {
        //
        // Summary:
        //     Optional delay that can be enabled for rate limiting or testing.
        //     Currently commented out to allow immediate request processing.
        //
        Thread.Sleep(5000);

        //
        // Summary:
        //     Continues processing the request through the middleware pipeline
        //     and returns the response to the client.
        //
        await _next(context);
    }
}
