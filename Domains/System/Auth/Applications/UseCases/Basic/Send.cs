namespace Wks.UseCases;

///
/// Summary:
///     Sends verification codes to user email addresses with rate limiting protection.
///     Manages email sending with cooldown periods and attempt limits to prevent abuse.
///
public class SendCodeAuth(ISendMail _sendMail, ICache _cache) : ISendCodeAuth
{
    private const int LIMIT_SEND_MESSAGE = 3;
    private const int LIMIT_PER_MESSAGE_TIME = 60;
    private const int LOCK_SEND_MESSAGE_TIME = 300;
    public const string ANCHOR_CODE = "__CODE__";
    private readonly bool IsDevelopment = Runtime
        .Mode.Env()
        .Equals(
            nameof(SystemModeEnum.Development),
            StringComparison.CurrentCultureIgnoreCase
        );

    ///
    /// Summary:
    ///     Sends confirmation code email to specified address.
    ///     Applies rate limiting and reads HTML template from metadata.
    ///
    /// Parameters:
    ///     email:
    ///         The recipient email address.
    ///
    ///     code:
    ///         The verification code to send.
    ///
    /// Returns:
    ///     Task representing the asynchronous email sending operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when rate limit exceeded or cooldown period not elapsed.
    ///
    public async Task ExecuteAsync(string email, string code)
    {
        /// Logic:
        ///     Read HTML template from cache or file system.
        ///     Cache template for subsequent requests to improve performance.
        ///
        string text = await _cache.CacheAsync(
            nameof(CacheNames.AUTH_TEMPLATE_CONFIRM),
            async () =>
                await File.ReadAllTextAsync(
                    Directory.GetCurrentDirectory()
                        + "/Shared/Metadata/Master/System/ConfirmCode.HTML"
                )
        );

        /// Logic:
        ///     Validate email send limits and cooldown periods.
        ///
        await Validate(email);

        /// Logic:
        ///     Replace placeholder with actual verification code.
        ///
        string content = text.Replace(ANCHOR_CODE, code);

        /// Logic:
        ///     In development mode, log the code to console.
        ///     In production mode, send actual email to user.
        ///
        if (IsDevelopment)
            Logger.Log(Format.ToCacheName(email, code));
        else
            await _sendMail.ExecuteAsync(
                Runtime.MailDomain.Env(),
                Runtime.MaiUserName.Env(),
                Runtime.MailApiKey.Env(),
                Runtime.MailFrom.Env(),
                email,
                MessageConstant.SYSTEM_SUBJECT_EMAIL_CONFIRM_CODE,
                content
            );
    }

    ///
    /// Summary:
    ///     Validates email sending limits and cooldown periods.
    ///     Enforces rate limiting rules to prevent spam and abuse.
    ///
    /// Parameters:
    ///     email:
    ///         The email address to validate sending permissions for.
    ///
    /// Returns:
    ///     Task representing the asynchronous validation operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when send limit reached or cooldown period not elapsed.
    ///
    private async Task Validate(string email)
    {
        DateTime now = Moment.Utc.Now;

        /// Logic:
        ///     Create cache keys for tracking send count and previous send time.
        ///     Use Base64 encoded email to ensure valid cache key format.
        ///
        string countName = Format.ToCacheName(
                nameof(CacheNames.AUTH_CONFIRM_COUNT_MESSAGE),
                Cryptography.Base64UrlEncode(email)
            ),
            previousName = Format.ToCacheName(
                nameof(CacheNames.AUTH_CONFIRM_PREVIOUS_TIME),
                Cryptography.Base64UrlEncode(email)
            );

        /// Logic:
        ///     Check previous send time and reset counter if lock period expired.
        ///     Get current send attempt count from cache.
        ///
        DateTime previous = await Previous(previousName, countName, now);
        int times = await _cache.GetAsync<int>(countName);

        /// Logic:
        ///     Validate send attempt limit (maximum 3 attempts).
        ///     Throw exception if limit reached.
        ///
        if (times >= LIMIT_SEND_MESSAGE)
            throw new HttpException(
                HttpStatus.Forbidden,
                string.Empty,
                ErrorCodeConstant.AUTH_CODE_LIMIT
            );

        /// Logic:
        ///     Validate cooldown period between sends (60 seconds).
        ///     Throw exception if sending too quickly.
        ///
        if (
            previous > DateTime.MinValue
            && (now - previous).TotalSeconds < LIMIT_PER_MESSAGE_TIME
        )
            throw new HttpException(
                HttpStatus.Forbidden,
                string.Empty,
                ErrorCodeConstant.AUTH_CODE_COOLDOWN
            );

        /// Logic:
        ///     Increment send counter and update previous send time.
        ///
        times++;
        await _cache.SetAsync(countName, times);
        await _cache.SetAsync(previousName, now);
    }

    ///
    /// Summary:
    ///     Retrieves previous send time and resets counter if lock period expired.
    ///     Manages counter reset logic based on time elapsed since last send.
    ///
    /// Parameters:
    ///     previousName:
    ///         The cache key for previous send timestamp.
    ///
    ///     countName:
    ///         The cache key for send attempt counter.
    ///
    ///     now:
    ///         The current UTC timestamp.
    ///
    /// Returns:
    ///     Previous send timestamp, or DateTime.MinValue if counter was reset.
    ///
    private async Task<DateTime> Previous(
        string previousName,
        string countName,
        DateTime now
    )
    {
        DateTime previous = await _cache.GetAsync<DateTime>(previousName);

        /// Logic:
        ///     If no previous send or lock period (300 seconds) not expired, return previous time.
        ///     Otherwise, reset both counter and timestamp, then return MinValue.
        ///
        if (
            previous == DateTime.MinValue
            || (now - previous).TotalSeconds < LOCK_SEND_MESSAGE_TIME
        )
            return previous;

        await _cache.RemoveAsync(countName);
        await _cache.RemoveAsync(previousName);
        return DateTime.MinValue;
    }
}
