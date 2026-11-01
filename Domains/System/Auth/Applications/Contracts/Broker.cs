namespace Wks.Objects;

public class AuthBroker
{
    ///
    /// Summary:
    ///     Message object for account initialization queue tasks.
    ///     Contains account identifier for background setup processing.
    ///
    public class InitialAccountMessage(Guid accountId)
    {
        public Guid AccountId { get; set; } = accountId;
    }
}
