namespace Wks.Objects;

///
/// Summary:
///     Contracts for a single Web Push delivery through an FCM endpoint.
///     The payload is the JSON the service worker reads to show a notification.
///
public class FcmObject
{
    ///
    /// Summary:
    ///     Notification content serialized into the encrypted push payload.
    ///
    public class Message
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Badge { get; set; }
        public string Tag { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }

    ///
    /// Summary:
    ///     Outcome of one send. Expired is true when the push service returns
    ///     404 or 410, which means the subscription should be removed.
    ///
    public class SendResult
    {
        public int StatusCode { get; set; }
        public bool Expired { get; set; }
    }
}
