namespace Wks.Repositories;

public class CashRepository(DatabaseContext databaseContext)
    : Repository<Cash>(databaseContext),
        ICashRepository { }
