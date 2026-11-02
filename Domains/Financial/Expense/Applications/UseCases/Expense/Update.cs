namespace Wks.UseCases;

public class UpdateExpense(
    IExpenseRepository _expenseRepository,
    ICache _cacheService,
    ICreateActionPublisher _createActionPublisher,
    ISocket _socketService,
    IExpenseTransactionRepository _expenseTransactionRepository
) : IUpdateExpense
{
    public async Task<Expense> ExecuteAsync(
        Guid profileId,
        ExpenseDataTransformer.Update update
    )
    {
        Expense expense =
            await _expenseRepository.Raw.FirstOrDefaultAsync(category =>
                category.ProfileId == profileId && category.Id == update.Id
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE
            );

        Expense mapped = expense.Merge(update);
        _expenseRepository.Update(mapped);

        string cacheName = Format.ToCacheName(
            nameof(CacheNames.FINANCIAL_EXPENSE),
            profileId
        );
        await _cacheService.RemoveAsync(cacheName);

        BudgetUpdate(update.Budget, expense.Id);

        /// Summary:
        ///     Publishes an action to indicate the update of an expense, including the profile ID, action name, and serialized expense data.
        ///     This action can be used for logging, auditing, or triggering other processes related to the update of an expense.
        await _createActionPublisher.Execute(
            new([
                new(
                    profileId,
                    nameof(ActionNames.EXPENSE_UPDATE),
                    expense.Serialize()
                ),
            ])
        );

        await _socketService.InvokeAsync(
            profileId,
            nameof(HubMethodName.EXPENSE)
        );
        return mapped;
    }

    private void BudgetUpdate(long budget, Guid expenseId)
    {
        var transaction = _expenseTransactionRepository.Raw.FirstOrDefault(
            expenseTransaction =>
                expenseTransaction.ExpenseId == expenseId
                && expenseTransaction.IsLocked
        );

        switch (transaction)
        {
            case var alias when budget is 0 && alias.Justifiable():
                _expenseTransactionRepository.Remove(alias);
                return;

            case null:
                if (budget is not 0)
                    BudgetCreate(budget, expenseId);
                return;

            default:
                transaction.Amount = budget;
                _expenseTransactionRepository.Update(transaction);
                return;
        }
    }

    private void BudgetCreate(long budget, Guid expenseId)
    {
        ExpenseTransaction expenseTransaction = new(
            string.Empty,
            string.Empty,
            budget,
            Moment.Utc.Now,
            expenseId,
            false,
            true
        );

        _expenseTransactionRepository.Create(expenseTransaction);
    }
}
