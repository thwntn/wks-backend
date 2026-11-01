namespace Wks.UseCases;

public class InfoExpense(DatabaseContext _databaseContext) : IInfoExpense
{
    public async Task<Expense> ExecuteAsync(Guid profileId, Guid expenseId)
    {
        Expense expense =
            await _databaseContext.Expense.FirstOrDefaultAsync(expense =>
                expense.Id == expenseId && expense.ProfileId == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE
            );

        return expense;
    }
}
