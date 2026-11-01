namespace Wks.Databases;

///
/// Summary:
///     Represents a custom role created by an account to group system permissions.
///     Permissions are stored as a junction table of PerrmissionContant codes.
///
public class Role(string name, string code) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the role display name.
    ///
    /// Returns:
    ///     The human-readable name for identifying this role.
    ///
    [Required]
    public string Name { get; set; } = name;

    ///
    /// Summary:
    ///     Gets or sets the unique role code.
    ///
    /// Returns:
    ///     The unique code string used to reference this role in the system.
    ///
    [Required]
    public string Code { get; set; } = code;

    ///
    /// Summary:
    ///     Gets or sets the optional role description.
    ///
    /// Returns:
    ///     Optional description explaining the purpose of this role, or null if not provided.
    ///
    [AllowNull]
    public string? Description { get; set; }

    ///
    /// Summary:
    ///     Gets or sets whether this role is the default role created for the
    ///     account during signup.
    ///
    /// Returns:
    ///     True when the role is the account's default role hidden from
    ///     account-facing listings; otherwise false.
    ///
    [Required]
    public bool IsDefault { get; set; } = false;

    ///
    /// Summary:
    ///     Gets or sets the account identifier.
    ///
    /// Returns:
    ///     The unique identifier of the account that owns this role.
    ///
    [Required]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; }

    #region Reference
    ///
    /// Summary:
    ///     Gets or sets the collection of permissions assigned to this role.
    ///
    /// Returns:
    ///     The system permission codes grouped under this role.
    ///
    public ICollection<Permission> Permissions { get; set; }
    #endregion
}
