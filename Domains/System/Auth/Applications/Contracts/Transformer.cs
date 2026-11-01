namespace Wks.DataTransformers;

///
/// Summary:
///     Contains data transformer objects for authentication operations.
///     Provides structured input models for various authentication and user management endpoints.
///
public class AuthDataTransformer
{
    ///
    /// Summary:
    ///     Data structure for Google authentication operations.
    ///     Contains email and password credentials for Google sign-in.
    ///
    public class Google
    {
        ///
        /// Summary:
        ///     The email address for Google authentication.
        ///
        [JsonRequired]
        public string Email { get; set; }

        ///
        /// Summary:
        ///     The password for Google authentication.
        ///
        [JsonRequired]
        public string Password { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for confirming verification codes.
    ///     Contains account ID and verification code for account activation.
    ///
    public class ConfirmCode
    {
        ///
        /// Summary:
        ///     The unique identifier of the account to confirm.
        ///
        [JsonRequired]
        public Guid AccountId { get; set; }

        ///
        /// Summary:
        ///     The verification code sent to the user's email.
        ///
        [JsonRequired]
        public string Code { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for changing user password.
    ///     Contains current credentials and new password with confirmation.
    ///
    public class ChangePassword
    {
        ///
        /// Summary:
        ///     The email address of the account.
        ///
        [JsonRequired]
        public string Email { get; set; }

        ///
        /// Summary:
        ///     The current password for verification.
        ///
        [JsonRequired]
        public string Password { get; set; }

        ///
        /// Summary:
        ///     The new password to be set.
        ///
        [JsonRequired]
        public string NewPassword { get; set; }

        ///
        /// Summary:
        ///     The confirmation of the new password (must match NewPassword).
        ///
        [JsonRequired]
        public string ConfirmPassword { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for password reset requests.
    ///     Contains email address for initiating password reset process.
    ///
    public class ResetPassword
    {
        ///
        /// Summary:
        ///     The email address for password reset.
        ///
        [JsonRequired]
        public string Email { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for email verification requests.
    ///     Contains email address to verify existence in system.
    ///
    public class VerifyEmail
    {
        ///
        /// Summary:
        ///     The email address to be verified.
        ///
        [JsonRequired]
        public string Email { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for user sign-in operations.
    ///     Contains email and password credentials for authentication.
    ///
    public class Signin
    {
        ///
        /// Summary:
        ///     The email address for sign-in.
        ///
        [JsonRequired]
        public string Email { get; set; }

        ///
        /// Summary:
        ///     The password for sign-in.
        ///
        [JsonRequired]
        public string Password { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for user registration.
    ///     Contains user information for creating new accounts.
    ///
    public class Signup
    {
        ///
        /// Summary:
        ///     The display name of the user.
        ///
        [JsonRequired]
        public string Name { get; set; }

        ///
        /// Summary:
        ///     The email address for the new account.
        ///
        [JsonRequired]
        public string Email { get; set; }

        ///
        /// Summary:
        ///     The password for the new account.
        ///
        [JsonRequired]
        public string Password { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for requesting confirmation code emails.
    ///     Contains email address for sending verification codes.
    ///
    public class TemplateConfirmCode
    {
        ///
        /// Summary:
        ///     The email address to send confirmation code to.
        ///
        [JsonRequired]
        public string Email { get; set; }
    }

    ///
    /// Summary:
    ///     Data structure for Google OAuth sign-in.
    ///     Contains authorization code received from Google OAuth callback.
    ///
    public class SigninWithGoogle
    {
        ///
        /// Summary:
        ///     The Google OAuth authorization code.
        ///
        [JsonRequired]
        public string Code { get; set; }
    }
}
