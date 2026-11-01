namespace Wks.Databases;

///
/// Summary:
///     Represents Google OAuth authentication data for user accounts.
///     Stores Google profile information linked to a local account.
///
public class Google : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the Google subject identifier.
    ///
    /// Returns:
    ///     The unique sub identifier assigned by Google to this user.
    ///
    [Required]
    public string Sub { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the display name from the Google profile.
    ///
    /// Returns:
    ///     The user's display name as returned by Google.
    ///
    [Required]
    public string Name { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the profile picture URL from Google.
    ///
    /// Returns:
    ///     Optional URL to the user's Google profile picture, or null if not available.
    ///
    [AllowNull]
    public string? Picture { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the account identifier.
    ///
    /// Returns:
    ///     The unique identifier of the local account linked to this Google credential.
    ///
    [Required]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; }
}
