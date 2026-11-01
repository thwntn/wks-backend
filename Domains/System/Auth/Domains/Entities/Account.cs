namespace Wks.Databases;

///
/// Summary:
///     Represents a user account in the authentication system.
///     Stores credentials, account type, status, and authentication tokens.
///
public class Account(
    string email,
    string hashPassword,
    string code,
    AccountStatus accountStatus,
    AccountNames AccountType
) : Entity, IEntity
{
    ///
    /// Summary:
    ///     Gets or sets the email address used as the primary login identifier.
    ///
    /// Returns:
    ///     The email address string in standard format.
    ///
    [Required]
    public string Email { get; set; } = email;

    ///
    /// Summary:
    ///     Gets or sets the hashed password for authentication.
    ///
    /// Returns:
    ///     The MD5 hashed password string.
    ///
    [Required]
    public string HashPassword { get; set; } = hashPassword;

    ///
    /// Summary:
    ///     Gets or sets the current account status.
    ///
    /// Returns:
    ///     The account status enum indicating open, validation required, or other states.
    ///
    [Required]
    public AccountStatus AccountStatus { get; set; } = accountStatus;

    ///
    /// Summary:
    ///     Gets or sets the email verification code.
    ///
    /// Returns:
    ///     The verification code sent to the user for account confirmation.
    ///
    [Required]
    public string Code { get; set; } = code;

    ///
    /// Summary:
    ///     Gets or sets the authentication method type for this account.
    ///
    /// Returns:
    ///     The account type enum indicating email, Google, or other authentication method.
    ///
    [Required]
    public AccountNames AccountType { get; set; } = AccountType;

    ///
    /// Summary:
    ///     Gets or sets the optional parent account identifier.
    ///
    /// Returns:
    ///     The unique identifier of the parent account, or null if this is a root account.
    ///
    [AllowNull]
    public Guid? Identifier { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the optional Google credential identifier.
    ///
    /// Returns:
    ///     The unique identifier of the linked Google credential, or null if not using Google OAuth.
    ///
    [AllowNull]
    public Guid? GoogleId { get; set; }

    [ForeignKey(nameof(GoogleId))]
    public Google? Google { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the JWT access token for the current session.
    ///
    /// Returns:
    ///     The JWT access token string (not persisted to database).
    ///
    [NotMapped]
    public string AccessToken { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the JWT refresh token for session renewal.
    ///
    /// Returns:
    ///     The JWT refresh token string (not persisted to database).
    ///
    [NotMapped]
    public string RefreshToken { get; set; }

    #region Reference
    ///
    /// Summary:
    ///     Gets or sets the associated profile navigation property.
    ///
    /// Returns:
    ///     The profile containing user information and preferences for this account.
    ///
    public Profile Profile { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of login history records.
    ///
    /// Returns:
    ///     The login activity records for this account.
    ///
    public ICollection<LoginAccount> LoginAccounts { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the collection of roles assigned to this account.
    ///
    /// Returns:
    ///     The roles that define permissions for this account.
    ///
    public ICollection<Role> Roles { get; set; }
    #endregion
}
