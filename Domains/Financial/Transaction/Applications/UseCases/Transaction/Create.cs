using System.Threading.Tasks;

namespace Wks.UseCases;

public class CreateExpenseTransaction(
    IExpenseTransactionRepository _expenseTransactionRepository,
    ICache _cacheService,
    ICreateActionPublisher _createActionPublisher,
    ICreateNotification _createNotification
) : ICreateExpenseTransaction
{
    public async Task<ExpenseTransaction> ExecuteAsync(
        Guid profileId,
        Guid expenseId,
        ExpenseTransactionDataTransformer.Create create
    )
    {
        ExpenseTransaction expenseTransaction = create.MapTo<ExpenseTransaction>();
        expenseTransaction.ExpenseId = expenseId;
        expenseTransaction.CreatedAt = Moment.Utc.Now;

        _expenseTransactionRepository.Create(expenseTransaction);
        await ResetCache(profileId);

        await _createNotification.ExecuteAsync(
            profileId,
            profileId,
            NotificationNames.CREATED_EXPENSE_TRANSACTION,
            expenseTransaction
        );

        /// Summary:
        ///    Publish action after creating an expense transaction, which includes the profile ID, action name, and serialized expense transaction data.
        ///   This allows the system to track and log the creation of expense transactions for auditing and user activity purposes.
        await _createActionPublisher.Execute(
            new([
                new(
                    profileId,
                    nameof(ActionNames.EXPENSE_TRANSACTION_CREATE),
                    expenseTransaction.Serialize()
                ),
            ])
        );

        return expenseTransaction;
    }

    private async Task ResetCache(Guid profileId)
    {
        List<string> cacheNames =
        [
            Format.ToCacheName(
                nameof(CacheNames.FINANCIAL_EXPENSE_TRANSACTION),
                profileId
            ),
            Format.ToCacheName(nameof(CacheNames.FINANCIAL_EXPENSE), profileId),
        ];
        foreach (string cacheName in cacheNames)
            await _cacheService.RemoveAsync(cacheName);
    }
}
