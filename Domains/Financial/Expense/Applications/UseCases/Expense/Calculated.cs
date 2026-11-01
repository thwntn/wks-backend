namespace Wks.UseCases;

public class CalculatedExpense() : ICalculatedExpense
{
    public void Execute(List<Expense> expenses)
    {
        foreach (Expense expense in expenses)
        {
            expense.Inflow = expense
                .ExpenseTransactions.Where(expenseTransaction => expenseTransaction.Spend == false)
                .Sum(expenseTransaction => (expenseTransaction.Amount));

            expense.Outflow = expense
                .ExpenseTransactions.Where(expenseTransaction => expenseTransaction.Spend)
                .Sum(expenseTransaction => (expenseTransaction.Amount));
        }
    }
}
