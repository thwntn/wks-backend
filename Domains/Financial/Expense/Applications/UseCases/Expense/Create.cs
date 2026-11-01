namespace Wks.UseCases;

public class CreateExpense(
    IExpenseRepository _expenseRepository,
    ICache _cacheService,
    ISocket _socketService,
    ICreateActionPublisher _createActionPublisher,
    IExpenseTransactionRepository _expenseTransactionRepository
) : ICreateExpense
{
    public async Task<Expense> ExecuteAsync(Guid profileId, ExpenseDataTransformer.Create create)
    {
        Expense expense = create.MapTo<Expense>();
        expense.ProfileId = profileId;

        _expenseRepository.Create(expense);

        string nameofCache = nameof(Expense) + profileId;
        await _cacheService.RemoveAsync(nameofCache);

        if (create.Budget is not 0)
            BudgetCreate(create.Budget, expense.Id);

        /// Summary:
        ///     Publishes an action to indicate the creation of an expense, including the profile ID, action name, and serialized expense data.
        ///     This action can be used for logging, auditing, or triggering other processes related to the creation of an expense.
        await _createActionPublisher.Execute(
            new([new(profileId, nameof(ActionNames.EXPENSE_CREATE), expense.Serialize())])
        );

        await _socketService.InvokeAsync(profileId, nameof(HubMethodName.EXPENSE));
        return expense;
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
