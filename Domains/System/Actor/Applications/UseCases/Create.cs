namespace Wks.UseCases;

///
/// Summary:
///     Use case for creating a sub-account (actor) under a parent basic account.
///     Creates the actor account, its profile and an initial login record.
///
public class CreateActor(
    IAccountRepository _accountRepository,
    IProfileRepository _profileRepository,
    ILoginAccountRepository _loginAccountRepository
) : ICreateActor
{
    ///
    /// Summary:
    ///     Creates a sub-account owned by the parent account of the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent (basic) account.
    ///
    ///     createParams:
    ///         The actor creation parameters including name, email and password.
    ///
    /// Returns:
    ///     The created actor account.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the parent account is not found or the email already exists.
    ///
    /// Logic:
    ///     Resolves the parent basic account from the profile identifier.
    ///     Guards against a duplicate email across all accounts.
    ///     Creates the actor account linked to the parent via Identifier.
    ///     Creates the actor profile and an initial login record.
    ///
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        ActorObject.CreateParams createParams
    )
    {
        Account parent = await ResolveParent(profileId);
        await EnsureEmailAvailable(createParams.Email);

        return CreateSubAccount(parent, createParams);
    }

    ///
    /// Summary:
    ///     Resolves the parent basic account that owns the given profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    /// Returns:
    ///     The parent basic account.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when no basic account matches the profile.
    ///
    private async Task<Account> ResolveParent(Guid profileId)
    {
        return await _accountRepository.Raw.FirstOrDefaultAsync(account =>
                account.Profile.Id == profileId
                && account.AccountType == AccountNames.Basic
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );
    }

    ///
    /// Summary:
    ///     Ensures the email is not already used by any account.
    ///
    /// Parameters:
    ///     email:
    ///         The email to validate for uniqueness.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the email already exists.
    ///
    private async Task EnsureEmailAvailable(string email)
    {
        bool exists = await _accountRepository.Raw.AnyAsync(account =>
            account.Email == email
        );
        if (exists)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.EMAIL_EXIST
            );
    }

    ///
    /// Summary:
    ///     Creates the actor account, its profile and an initial login record.
    ///
    /// Parameters:
    ///     parent:
    ///         The parent basic account the actor belongs to.
    ///
    ///     createParams:
    ///         The actor creation parameters.
    ///
    /// Returns:
    ///     The created actor account.
    ///
    private Account CreateSubAccount(
        Account parent,
        ActorObject.CreateParams createParams
    )
    {
        string hashPassword = Cryptography.Md5(createParams.Password);

        Account actor = new(
            createParams.Email,
            hashPassword,
            string.Empty,
            AccountStatus.Open,
            AccountNames.Actor
        )
        {
            Identifier = parent.Id,
        };
        _accountRepository.Create(actor);

        Profile profile = new(
            createParams.Name,
            createParams.Email,
            createParams.Description
        )
        {
            AccountId = actor.Id,
        };
        _profileRepository.Create(profile);

        _loginAccountRepository.Create(
            new LoginAccount { AccountId = actor.Id, CreatedAt = Moment.Utc.Now }
        );

        return actor;
    }
}
