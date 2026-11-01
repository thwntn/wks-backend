namespace Wks.UseCases;

///
/// Summary:
///     Registers new user accounts with email and password.
///     Creates account and profile entities, handles email uniqueness validation.
///
public class SignupAuth(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository
) : ISignupAuth
{
    ///
    /// Summary:
    ///     Creates new user account with provided registration information.
    ///     Generates verification code and sets account status based on skipVerify flag.
    ///
    /// Parameters:
    ///     signup:
    ///         The signup data containing email, password, and name.
    ///
    ///     skipVerify:
    ///         Flag to bypass email verification and activate account immediately.
    ///
    /// Returns:
    ///     Signup result containing account ID, email, and verification code.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the email address already exists in the system.
    ///
    public async Task<AuthObject.SignupResult> ExecuteAsync(
        AuthDataTransformer.Signup signup,
        bool skipVerify = false
    )
    {
        /// Logic:
        ///     Check if email already exists in database.
        ///     Throw exception if email is already registered.
        ///
        bool existEmail = _accountRepository.Raw.Any(user =>
            user.Email == signup.Email
        );
        if (existEmail)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.EMAIL_EXIST
            );

        /// Logic:
        ///     Hash password using MD5 algorithm.
        ///     Generate random verification code.
        ///
        string hashPassword = Cryptography.Md5(signup.Password);
        string code = Cryptography.RandomCode();

        /// Logic:
        ///     Set account status based on skipVerify flag.
        ///     ValidateRequire: requires email confirmation.
        ///     Open: account immediately active without verification.
        ///
        AccountStatus accountStatus = AccountStatus.ValidateRequire;
        if (skipVerify)
            accountStatus = AccountStatus.Open;

        /// Logic:
        ///     Create new account entity with email, hashed password, and verification code.
        ///
        Account account = _accountRepository.Create(
            new Account(
                signup.Email,
                hashPassword,
                code,
                accountStatus,
                AccountNames.Basic
            )
        );

        /// Logic:
        ///     Create associated profile entity with name and email.
        ///     Link profile to account via AccountId.
        ///
        Profile profile = new(signup.Name, signup.Email, default);
        profile.AccountId = account.Id;

        _profileRepository.Create(profile);
        return new(default, default, account.Id, account.Email, code)
        {
            Account = account,
        };
    }
}
