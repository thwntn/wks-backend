namespace Wks.Mains;

///
/// Summary:
///     Provides startup logging utilities for application host information.
///     Displays environment, queue, and runtime details during application initialization.
///
public class Host
{
    //
    // Summary:
    //     Displays system startup information including host URL, message queue connection,
    //     and timezone configuration to the console for debugging and monitoring purposes.
    //     Logs each piece of information as a success message with green color formatting.
    //
    public static void Information()
    {
        //
        // Summary:
        //     Logs the host URL where the application is listening for requests.
        //
        Console.WriteLine($"[System] Now listening on: {Runtime.Host.Env()}");

        //
        // Summary:
        //     Logs the message queue host connection information.
        //
        Console.WriteLine($"[Message Queue] Connected to: {Runtime.QueueHost.Env()}");
    }
}
