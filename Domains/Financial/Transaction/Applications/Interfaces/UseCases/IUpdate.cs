namespace Wks.Interfaces;

public interface IUpdateExpenseTransaction
{
    ExpenseTransaction Execute(ExpenseTransactionDataTransformer.Update update);
}
