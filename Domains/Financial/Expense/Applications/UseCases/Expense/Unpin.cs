namespace Wks.UseCases;

public class UnpinExpense(IExpenseRepository _expenseRepository) : IUnpinExpense
{
    public async Task<Expense> ExecuteAsync(Guid profileId, Guid expenseId)
    {
        Expense expense = await _expenseRepository.GetByIdAsync(expenseId);

        if (profileId.CompareTo(expense.ProfileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE
            );

        // Summary:
        //     If expense is not pinned -> error message
        if (expense.IsPinned is false)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.EXPENSE_IS_NOT_PINNED
            );

        expense.IsPinned = false;
        _expenseRepository.Update(expense);

        return expense;
    }
}
