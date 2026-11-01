namespace Wks.UseCases;

public class UpdateExpenseTransaction(
    IExpenseTransactionRepository _expenseTransactionRepository
) : IUpdateExpenseTransaction
{
    public ExpenseTransaction Execute(
        ExpenseTransactionDataTransformer.Update updateParams
    )
    {
        ExpenseTransaction expenseTransaction =
            _expenseTransactionRepository
                .Include(expenseTransaction => expenseTransaction.Expense)
                .FirstOrDefault(expenseTransaction =>
                    expenseTransaction.Id == updateParams.Id
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE_TRANSACTION
            );

        ExpenseTransaction merge = expenseTransaction.Merge(updateParams);
        _expenseTransactionRepository.Update(merge);

        return merge;
    }
}
