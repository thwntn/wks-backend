namespace Wks.Repositories;

public class ExpenseRepository(DatabaseContext databaseContext)
    : Repository<Expense>(databaseContext),
        IExpenseRepository { }
