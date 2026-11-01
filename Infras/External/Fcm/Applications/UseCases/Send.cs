using WebPush;

namespace Wks.UseCases;

///
/// Summary:
///     Delivers one Web Push notification to an FCM (or other) push endpoint.
///     VAPID signing and payload encryption are handled by the Web Push client.
///     This use case does not load or delete subscriptions.
///
public class SendFcm : ISendFcm
{
    const string TTL = "TTL";
    const int TTL_SECONDS = 86400;
    const int CREATED = 201;
    const int NOT_FOUND = 404;
    const int GONE = 410;
    const int UNAVAILABLE = 503;

    ///
    /// Summary:
    ///     Builds the subscription and VAPID details, then sends the JSON payload.
    ///     404 and 410 mark the subscription expired. Other failures return the
    ///     status so the caller can continue with the remaining devices.
    ///
    public async Task<HttpObject.Response<FcmObject.SendResult>> ExecuteAsync(
        string subject,
        string publicKey,
        string privateKey,
        string endpoint,
        string p256dh,
        string auth,
        FcmObject.Message message
    )
    {
        /// Logic:
        ///     Encrypt and sign through the Web Push client, then map the HTTP
        ///     status into a result the caller can retry or use to drop the device.
        ///
        Stopwatch stopwatch = Stopwatch.StartNew();
        PushSubscription subscription = new(endpoint, p256dh, auth);
        VapidDetails vapid = new(subject, publicKey, privateKey);
        Dictionary<string, object> options = new() { [TTL] = TTL_SECONDS };

        using WebPushClient client = new();
        client.SetVapidDetails(vapid);
        try
        {
            await client.SendNotificationAsync(
                subscription,
                message.Serialize(),
                options
            );
            stopwatch.Stop();

            return new(
                true,
                new FcmObject.SendResult
                {
                    StatusCode = CREATED,
                    Expired = false,
                },
                null,
                CREATED,
                (decimal)stopwatch.Elapsed.TotalMilliseconds
            );
        }
        catch (WebPushException exception)
        {
            stopwatch.Stop();
            int statusCode = (int)exception.StatusCode;
            if (statusCode == 0)
                statusCode = UNAVAILABLE;

            return new(
                false,
                new FcmObject.SendResult
                {
                    StatusCode = statusCode,
                    Expired = statusCode is NOT_FOUND or GONE,
                },
                exception.Message,
                statusCode,
                (decimal)stopwatch.Elapsed.TotalMilliseconds
            );
        }
        catch (Exception exception)
        {
            stopwatch.Stop();
            return new(
                false,
                new FcmObject.SendResult
                {
                    StatusCode = UNAVAILABLE,
                    Expired = false,
                },
                exception.Message,
                UNAVAILABLE,
                (decimal)stopwatch.Elapsed.TotalMilliseconds
            );
        }
    }
}
