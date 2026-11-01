namespace Wks.Repositories;

public class ExpenseTransactionRepository(DatabaseContext databaseContext)
    : Repository<ExpenseTransaction>(databaseContext),
        IExpenseTransactionRepository { }
