namespace Wks.UseCases;

public class RemoveExpense(
    IExpenseRepository _expenseRepository,
    ICreateActionPublisher _createActionPublisher
) : IRemoveExpense
{
    public async Task<string> ExecuteAsync(
        Guid profileId,
        IEnumerable<Guid> expenseIds
    )
    {
        IEnumerable<Expense> expenses =
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _expenseRepository.Where(expense =>
                    expenseIds.Contains(expense.Id)
                    && expense.ProfileId == profileId
                )
            );

        /// Summary:
        ///    Removes expenses by their IDs and logs the deletion action.
        await _createActionPublisher.Execute(
            new([
                new(
                    profileId,
                    nameof(ActionNames.EXPENSE_DELETE),
                    expenses.Serialize()
                ),
            ])
        );

        _expenseRepository.Remove(expenses);
        return string.Empty;
    }
}
