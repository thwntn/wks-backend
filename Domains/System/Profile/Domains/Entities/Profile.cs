namespace Wks.Databases;

///
/// Summary:
///     Represents a merchant or user profile that acts as the tenant owner across the system.
///     Stores identity, contact and media; registered hosts are owned through Domains.
///
public class Profile(string name, string email, string description)
    : Entity,
        IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the profile display name.
    ///
    /// Returns:
    ///     The human-readable name for identifying this profile.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the avatar image token.
    ///
    /// Returns:
    ///     The storage token for the profile avatar image.
    ///
    [Required]
    public string Avatar { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the cover picture image token.
    ///
    /// Returns:
    ///     The storage token for the profile cover picture.
    ///
    [Required]
    public string CoverPicture { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the optional birthday date.
    ///
    /// Returns:
    ///     Optional birthday date, or null if not provided.
    ///
    [AllowNull]
    public DateTime? Birthday { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the optional profile description.
    ///
    /// Returns:
    ///     Optional description of the profile, or null if not provided.
    ///
    [AllowNull]
    public string? Description { get; set; } = description;

    ///
    /// Summary:
    ///     Gets or sets the profile email address.
    ///
    /// Returns:
    ///     The email address associated with this profile.
    ///
    [Required]
    public string Email { get; set; } = email;

    ///
    /// Summary:
    ///      Stores the available wallet funds owned by this profile, in VND.
    ///      Administrator adjustments replace this value; eligible package purchases deduct the confirmed charge.
    ///
    /// Returns:
    ///      The current wallet balance, initially zero for a new profile.
    ///
    [Required]
    public decimal Balance { get; set; } = 0;

    ///
    /// Summary:
    ///     Gets or sets the profile phone number.
    ///
    /// Returns:
    ///     The contact phone number for this profile.
    ///
    [Required]
    public string Phone { get; set; } = string.Empty;

    ///
    /// Summary:
    ///     Gets or sets the optional profile address.
    ///
    /// Returns:
    ///     Optional physical address for this profile, or null if not provided.
    ///
    [AllowNull]
    public string? Address { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the account identifier.
    ///
    /// Returns:
    ///     The unique identifier of the account that owns this profile.
    ///
    [Required]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; }

    ///
    /// Summary:
    ///     Gets the avatar URL computed from the stored avatar token.
    ///
    /// Returns:
    ///     The public URL to the profile avatar image.
    ///
    [NotMapped]
    public string AvatarUrl
    {
        get
        {
            if (Avatar == string.Empty)
                return Reader.CreatePublicURL(FileConstant.PROFILE_AVATAR);
            return Reader.CreateURL(Avatar);
        }
    }

    ///
    /// Summary:
    ///     Gets the cover picture URL computed from the stored cover token.
    ///
    /// Returns:
    ///     The public URL to the profile cover picture image.
    ///
    [NotMapped]
    public string CoverPictureUrl
    {
        get
        {
            if (CoverPicture == string.Empty)
                return Reader.CreatePublicURL(FileConstant.PROFILE_COVER);
            return Reader.CreateURL(CoverPicture);
        }
    }

    ///
    /// Summary:
    ///     Gets the last login timestamp (not persisted to database).
    ///
    /// Returns:
    ///     The UTC timestamp of the most recent login for this profile.
    ///
    [NotMapped]
    public DateTime LastLogin { get; set; }

    #region Reference

    ///
    /// Summary:
    ///     Gets or sets shopping carts owned by this profile.
    /// Returns:
    ///     Customer and guest carts created for this profile's storefront.
    ///


    ///
    /// Summary:
    ///     Gets or sets the Google OAuth credential for customer login on this storefront.
    ///
    /// Returns:
    ///     The Google credential entity, or null when not configured.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of refresh tokens for this profile.
    ///
    /// Returns:
    ///     The active refresh token records associated with this profile.
    ///
    public ICollection<RefreshToken> RefreshTokens { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of notifications for this profile.
    ///
    /// Returns:
    ///     The notifications sent to this profile.
    ///
    public ICollection<Notification> Notifications { get; set; }



    ///
    /// Summary:
    ///     Gets or sets the collection of storage groups owned by this profile.
    ///
    /// Returns:
    ///     The storage groups that organize files for this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of group memberships for this profile.
    ///
    /// Returns:
    ///     The group member records associated with this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of plans owned by this profile.
    ///
    /// Returns:
    ///     The scheduled plan entries for this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of expense transactions for this profile.
    ///
    /// Returns:
    ///     The expense transaction records owned by this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of notes owned by this profile.
    ///
    /// Returns:
    ///     The personal notes created by this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of customers managed by this profile.
    ///
    /// Returns:
    ///     The customer records belonging to this profile's storefront.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of domains associated with this profile.
    ///
    /// Returns:
    ///     The domain records configured for this profile.
    ///
    [InverseProperty(nameof(Databases.Domain.Profile))]
    public ICollection<Domain> Domains { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of icons owned by this profile.
    ///
    /// Returns:
    ///     The icon records for this profile's storefront.
    ///
    public ICollection<Icon> Icons { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of actions logged for this profile.
    ///
    /// Returns:
    ///     The action audit records associated with this profile.
    ///
    public ICollection<Action> Actions { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of kanban categories owned by this profile.
    ///
    /// Returns:
    ///     The kanban board categories for this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of sale programs owned by this profile.
    ///
    /// Returns:
    ///     The sale programs configured for this profile's storefront.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of promotions owned by this profile.
    ///
    /// Returns:
    ///     The promotion records for this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of hashtags owned by this profile.
    ///
    /// Returns:
    ///     The hashtag records used for categorization across this profile.
    ///


    ///
    /// Summary:
    ///     Gets or sets the collection of broadcast content items owned by this profile.
    ///
    /// Returns:
    ///     The reusable content templates for this profile's broadcast campaigns.
    ///

    #endregion
}
