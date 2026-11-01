namespace Wks.Databases;

///
/// Summary:
///     Represents an application version release.
///     When Reset is true, the frontend must force all active sessions to logout and re-login.
///
public class AppVersion(string name, string version) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the human-readable release name.
    ///
    /// Returns:
    ///     The release name string (e.g. v1.2.0 - Dashboard Revamp).
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the semantic version string.
    ///
    /// Returns:
    ///     The version string in semantic format (e.g. 1.2.0).
    ///
    [Required]
    public string Version { get; set; } = version;

    ///
    /// Summary:
    ///     Gets or sets whether this release requires a forced session reset.
    ///
    /// Returns:
    ///     True if all active sessions must be invalidated and re-authenticated; false otherwise.
    ///
    [Required]
    public bool Reset { get; set; } = false;

    ///
    /// Summary:
    ///     Gets or sets whether this release is highlighted.
    ///
    /// Returns:
    ///     True if the release should be emphasized (e.g. featured in "What's new"); false otherwise.
    ///
    [Required]
    public bool Highlight { get; set; } = false;

    ///
    /// Summary:
    ///     Gets or sets the optional release notes.
    ///
    /// Returns:
    ///     Optional high-level description or release notes for this version, or null if not provided.
    ///
    [AllowNull]
    public string? Description { get; set; }

    #region Reference
    ///
    /// Summary:
    ///     Gets or sets the collection of features introduced in this version.
    ///
    /// Returns:
    ///     The feature records describing changes or additions in this release.
    ///
    public ICollection<AppFeature> Features { get; set; }
    #endregion
}
