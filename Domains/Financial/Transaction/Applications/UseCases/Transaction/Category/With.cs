namespace Wks.UseCases;

public class WithCategoryExpenseTransaction(DatabaseContext _databaseContext)
    : IWithCategoryExpenseTransaction
{
    public IEnumerable<ExpenseTransaction> Execute(Guid expenseId)
    {
        IEnumerable<ExpenseTransaction> expenseTransactions =
            _databaseContext.ExpenseTransaction.Where(expenseTransaction =>
                expenseTransaction.ExpenseId == expenseId
            );
        return expenseTransactions;
    }
}
