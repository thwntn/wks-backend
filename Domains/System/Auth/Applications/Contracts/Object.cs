namespace Wks.Objects;

///
/// Summary:
///     Contains data transfer objects for authentication operations.
///     Defines response and result structures for auth-related use cases.
///
public class AuthObject
{
    ///
    /// Summary:
    ///     Response object for password-based authentication.
    ///     Contains account information, profile data, and assigned roles.
    ///
    public class UsePasswordResponse
    {
        [NotMapped]
        public Guid Id { get; set; }

        [NotMapped]
        public string CreatedAt { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string Code { get; set; }

        [NotMapped]
        public AccountStatus AccountStatus { get; set; }

        [NotMapped]
        public AccountNames AccountType { get; set; }

        [NotMapped]
        public ProfileResponse Profile { get; set; }

        ///
        /// Summary:
        ///     Custom roles assigned to this account.
        ///
        [NotMapped]
        public List<RoleResponse> Roles { get; set; }
    }

    ///
    /// Summary:
    ///     Role summary for embedding in account responses.
    ///     Maps from Role entity including its permission codes.
    ///
    public class RoleResponse
    {
        [NotMapped]
        public Guid Id { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string Code { get; set; }

        ///
        /// Summary:
        ///     Permission entries belonging to this role.
        ///     Maps from Role.Permissions collection (RoleId + Code junction).
        ///
        [NotMapped]
        public List<PermissionResponse> Permissions { get; set; }
    }

    ///
    /// Summary:
    ///     Permission code entry within a role response.
    ///     Represents a single PerrmissionContant code granted by the parent role.
    ///
    public class PermissionResponse
    {
        [NotMapped]
        public Guid Id { get; set; }

        [NotMapped]
        public string Code { get; set; }
    }

    ///
    /// Summary:
    ///     Response object for user profile information.
    ///     Contains detailed user profile data including personal information and media.
    ///
    public class ProfileResponse
    {
        [NotMapped]
        public decimal Balance { get; set; }

        [NotMapped]
        public string Id { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string Avatar { get; set; }

        [NotMapped]
        public string AvatarUrl { get; set; }

        [NotMapped]
        public string Birthday { get; set; }

        [NotMapped]
        public string Description { get; set; }

        [NotMapped]
        public string Email { get; set; }

        [NotMapped]
        public string Phone { get; set; }

        [NotMapped]
        public string CoverPicture { get; set; }

        [NotMapped]
        public string CoverPictureUrl { get; set; }

        [NotMapped]
        public string Address { get; set; }

        [NotMapped]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public DateTime LastLogin { get; set; }

        [NotMapped]
        public UsePasswordResponse Account { get; set; }
    }

    ///
    /// Summary:
    ///     Result object for successful signin operations.
    ///     Contains JWT tokens and account identifier.
    ///
    public class SigninResult(
        string accessToken,
        string refreshToken,
        Guid accountId = default
    )
    {
        public string AccessToken { get; set; } = accessToken;
        public string RefreshToken { get; set; } = refreshToken;
        public Guid AccountId { get; set; } = accountId;
    }

    ///
    /// Summary:
    ///     Response object for completed signup operations returned to client.
    ///     Contains authentication tokens and account identifier.
    ///
    public class SignupResponse(
        string accessToken,
        string refreshToken,
        Guid accountId
    )
    {
        public string AccessToken { get; set; } = accessToken;
        public string RefreshToken { get; set; } = refreshToken;
        public Guid AccountId { get; set; } = accountId;
    }

    ///
    /// Summary:
    ///     Result object for internal signup operations.
    ///     Contains tokens, account details, and verification code for processing.
    ///
    public class SignupResult(
        string accessToken,
        string refreshToken,
        Guid accountId,
        string email,
        string code
    )
    {
        public string AccessToken { get; set; } = accessToken;
        public string RefreshToken { get; set; } = refreshToken;
        public Guid AccountId { get; set; } = accountId;
        public string Email { get; set; } = email;
        public string Code { get; set; } = code;
        public Account Account { get; set; }
    }

    ///
    /// Summary:
    ///     Result object for Google OAuth validation.
    ///     Contains Google profile data and account existence status.
    ///
    public class GoogleValidate(
        bool signUpRequire,
        string email,
        string password,
        string displayName,
        Account account
    )
    {
        public bool SignupRequire { get; set; } = signUpRequire;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string DisplayName { get; set; } = displayName;
        public Account Account { get; set; } = account;
    }
}
