namespace Wks.Databases;

///
/// Summary:
///     Represents a junction record linking a role to a system permission code.
///     Stores the permission as a PerrmissionContant string code — no separate permission table needed.
///
[PrimaryKey(nameof(RoleId), nameof(Code))]
public class Permission : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the role identifier.
    ///
    /// Returns:
    ///     The unique identifier of the role this permission belongs to.
    ///
    [Required]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the PerrmissionContant permission code.
    ///
    /// Returns:
    ///     The permission code string (e.g. MARKETING_DATA) granted by this role.
    ///
    [Required]
    public string Code { get; set; }
}
