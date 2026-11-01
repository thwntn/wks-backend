namespace Wks.Databases;

///
/// Summary:
///     Represents a single feature or change item belonging to an application version release.
///     Used to surface what's new entries to the frontend.
///
public class AppFeature(string title) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the feature title.
    ///
    /// Returns:
    ///     The short title describing this feature or change (e.g. New role management page).
    ///
    [Required]
    public string Title { get; set; } = title;

    ///
    /// Summary:
    ///     Gets or sets the optional detailed description.
    ///
    /// Returns:
    ///     Optional detailed description of the feature, or null if not provided.
    ///
    [AllowNull]
    public string? Description { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the version identifier.
    ///
    /// Returns:
    ///     The unique identifier of the version release this feature belongs to.
    ///
    [Required]
    public Guid VersionId { get; set; }

    [ForeignKey(nameof(VersionId))]
    public AppVersion AppVersion { get; set; }
}
