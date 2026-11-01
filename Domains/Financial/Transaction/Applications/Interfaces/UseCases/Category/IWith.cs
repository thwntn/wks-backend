namespace Wks.Interfaces;

public interface IWithCategoryExpenseTransaction
{
    IEnumerable<ExpenseTransaction> Execute(Guid expenseId);
}
