namespace Wks.Databases;

///
/// Summary:
///     Represents an audit action event triggered by a profile, storing the type and payload.
///
public class Action(Guid profileId, string type, string jsonData)
    : Entity,
        IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the last updated timestamp of this action record.
    ///
    /// Returns:
    ///     The UTC timestamp of when this action was last updated.
    ///
    [Required]
    public DateTime Updated { get; set; } = Moment.Utc.Now;

    ///
    /// Summary:
    ///     Gets or sets the serialized JSON payload of this action.
    ///
    /// Returns:
    ///     The JSON string containing the action data.
    ///
    [Required]
    public string JsonData { get; set; } = jsonData;

    ///
    /// Summary:
    ///     Gets or sets the action type identifier.
    ///
    /// Returns:
    ///     The string identifier categorizing the type of action performed.
    ///
    [Required]
    public string Type { get; set; } = type;

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that performed this action.
    ///
    [Required]
    public Guid ProfileId { get; set; } = profileId;

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }
}
