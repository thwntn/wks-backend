namespace Wks.Repositories;

public class DomainRepository(DatabaseContext databaseContext)
    : Repository<Domain>(databaseContext),
        IDomainRepository { }
