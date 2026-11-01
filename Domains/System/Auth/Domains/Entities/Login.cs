namespace Wks.Databases;

///
/// Summary:
///     Represents a login history record for account activity tracking.
///     Records each successful login attempt with its associated account.
///
public class LoginAccount : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the account identifier.
    ///
    /// Returns:
    ///     The unique identifier of the account that performed this login.
    ///
    [Required]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; }
}
