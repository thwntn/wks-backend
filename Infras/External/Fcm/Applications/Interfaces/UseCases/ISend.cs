namespace Wks.Interfaces;

///
/// Summary:
///     Sends one Web Push notification to a browser subscription stored as an
///     FCM endpoint plus its p256dh and auth keys.
///
public interface ISendFcm
{
    ///
    /// Summary:
    ///     Encrypts the message, signs it with VAPID, and posts it to the
    ///     subscription endpoint. A failed device is returned, not thrown.
    ///
    /// Parameters:
    ///     subject:
    ///         VAPID subject, typically a mailto address.
    ///
    ///     publicKey:
    ///         VAPID public key.
    ///
    ///     privateKey:
    ///         VAPID private key. Stays on the server.
    ///
    ///     endpoint:
    ///         Push service URL from the browser subscription.
    ///
    ///     p256dh:
    ///         Client public key used to encrypt the payload.
    ///
    ///     auth:
    ///         Client auth secret used to encrypt the payload.
    ///
    ///     message:
    ///         Notification content delivered to the service worker.
    ///
    /// Returns:
    ///     The push status. Expired is true for 404 and 410.
    ///
    Task<HttpObject.Response<FcmObject.SendResult>> ExecuteAsync(
        string subject,
        string publicKey,
        string privateKey,
        string endpoint,
        string p256dh,
        string auth,
        FcmObject.Message message
    );
}
