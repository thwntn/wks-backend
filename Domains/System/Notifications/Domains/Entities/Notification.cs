namespace Wks.Databases;

///
/// Summary:
///     Represents a system notification sent to a profile.
///
public class Notification : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the notification type.
    ///
    /// Returns:
    ///     The notification category used to determine how it is rendered.
    ///
    [Required]
    public NotificationNames Type { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the raw JSON payload of the notification.
    ///
    /// Returns:
    ///     The serialized JSON string containing notification data.
    ///
    [Required]
    public string JsonData { get; set; }

    ///
    /// Summary:
    ///     Gets the deserialized notification payload.
    ///
    /// Returns:
    ///     The deserialized object from the JSON data, or an empty object if not parseable.
    ///
    [NotMapped]
    public object ParseJson
    {
        get
        {
            if (JsonData.Justifiable())
                return JsonData.Deserialize<object>();
            else
                return new { };
        }
    }

    ///
    /// Summary:
    ///     Gets or sets whether the notification has been read.
    ///
    /// Returns:
    ///     True if the notification was read by the recipient; false if unread.
    ///
    [Required]
    public bool IsRead { get; set; }

    ///
    /// Summary:
    ///     Gets or sets whether the notification has been handled.
    ///
    /// Returns:
    ///     True if the notification action was processed; false otherwise.
    ///
    [Required]
    public bool Handle { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the identifier of the sender profile.
    ///
    /// Returns:
    ///     The unique identifier of the profile that sent this notification.
    ///
    [Required]
    public Guid FromId { get; set; }

    [NotMapped]
    public Profile From { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the recipient profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile receiving this notification.
    ///
    [Required]
    public Guid ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }
}
