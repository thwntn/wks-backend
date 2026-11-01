namespace Wks.Databases;

///
/// Summary:
///     Represents a domain host registered under a profile for storefront or redirect use.
///
public class Domain(Guid profileId, string host, DomainTypeEnum type)
    : Entity,
        IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the domain host name.
    ///
    /// Returns:
    ///     The full host string for this domain (e.g. shop.example.com).
    ///
    [Required]
    public string Host { get; set; } = host;

    ///
    /// Summary:
    ///     Gets or sets the domain usage type.
    ///
    /// Returns:
    ///     The type enum indicating whether this domain is for a shop storefront or redirect service.
    ///
    [Required]
    public DomainTypeEnum Type { get; set; } = type;

    ///
    /// Summary:
    ///     Gets or sets the profile identifier.
    ///
    /// Returns:
    ///     The unique identifier of the profile that owns this domain.
    ///
    [Required]
    public Guid ProfileId { get; set; } = profileId;

    [ForeignKey(nameof(ProfileId))]
    [InverseProperty(nameof(Profile.Domains))]
    public Profile Profile { get; set; }

    #region Reference
    ///
    /// Summary:
    ///     Gets or sets the collection of redirect links under this domain.
    ///
    /// Returns:
    ///     The redirect links associated with this domain.
    ///
    
    #endregion
}
