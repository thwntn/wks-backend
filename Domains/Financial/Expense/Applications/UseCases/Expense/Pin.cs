namespace Wks.UseCases;

public class PinExpense(IExpenseRepository _expenseRepository) : IPinExpense
{
    public async Task<Expense> ExecuteAsync(Guid profileId, Guid expenseId)
    {
        Expense expense = await _expenseRepository.GetByIdAsync(expenseId);

        if (profileId.CompareTo(expense.ProfileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE
            );

        // If expense is pinned -> error message
        if (expense.IsPinned)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.EXPENSE_IS_PINNED
            );

        expense.IsPinned = true;
        _expenseRepository.Update(expense);

        return expense;
    }
}
