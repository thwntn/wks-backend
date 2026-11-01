namespace Wks.Repositories;

public class CashDescriptionRepository(DatabaseContext databaseContext)
    : Repository<CashDescription>(databaseContext),
        ICashDescriptionRepository { }
