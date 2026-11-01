using FlexLabs.EntityFrameworkCore.Upsert;

namespace Wks.Repositories;

///
/// Summary:
///     Provides a generic repository implementation for entity persistence.
///     Encapsulates common data access operations using Entity Framework.
///
public class Repository<TEntity>(DatabaseContext databaseContext)
    where TEntity : class, IEntity
{
    private readonly DatabaseContext _databaseContext = databaseContext;
    private readonly DbSet<TEntity> _databaseSet =
        databaseContext.Set<TEntity>();

    ///
    /// Summary:
    ///     Gets the raw DbSet for direct database operations.
    ///     Provides access to the underlying Entity Framework DbSet.
    ///
    /// Returns:
    ///     Raw DbSet<TEntity> for direct database queries and operations.
    ///
    public DbSet<TEntity> Raw
    {
        get => _databaseSet;
    }

    ///
    /// Summary:
    ///     Retrieves an entity by its unique identifier.
    ///     Searches for the entity with the specified ID in the database.
    ///
    /// Parameters:
    ///   id:
    ///     The unique identifier of the entity to retrieve.
    ///
    /// Returns:
    ///     Task containing the entity if found, or throws exception if not found.
    ///
    /// Exceptions:
    ///   HttpException:
    ///     Thrown when entity with the specified ID is not found.
    ///
    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        TEntity record =
            await _databaseSet.FirstOrDefaultAsync(entity => entity.Id == id)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ID
            );
        return record;
    }

    ///
    /// Summary:
    ///     Filters entities based on the specified predicate expression.
    ///     Returns a queryable collection of entities that match the condition.
    ///
    /// Parameters:
    ///   predicate:
    ///     The filter expression to apply to the entities.
    ///
    /// Returns:
    ///     IQueryable collection of entities that match the predicate condition.
    ///
    public IQueryable<TEntity> Where(
        Expression<Func<TEntity, bool>> predicate
    ) =>
        _databaseSet
            .Where(predicate)
            .OrderByDescending(entity => entity.CreatedAt);

    ///
    /// Summary:
    ///     Filters entities asynchronously based on the specified predicate expression.
    ///     Returns all matching entities as a list.
    ///
    /// Parameters:
    ///   predicate:
    ///     The filter expression to apply to the entities.
    ///
    /// Returns:
    ///     Task containing a list of entities that match the predicate condition.
    ///
    public async Task<List<TEntity>> WhereAsync(
        Expression<Func<TEntity, bool>> predicate
    ) =>
        await EntityFrameworkQueryableExtensions.ToListAsync(
            _databaseSet
                .Where(predicate)
                .OrderByDescending(entity => entity.CreatedAt)
        );

    ///
    /// Summary:
    ///     Retrieves all entities ordered by creation date in descending order.
    ///     Returns a queryable collection of all entities in the database.
    ///
    /// Returns:
    ///     IQueryable collection of all entities ordered by CreatedAt descending.
    ///
    public async Task<List<TEntity>> ListAsync() =>
        await EntityFrameworkQueryableExtensions.ToListAsync(
            _databaseSet.OrderByDescending(entity => entity.CreatedAt)
        );

    ///
    /// Summary:
    ///     Creates a new entity in the database.
    ///     Adds the specified entity to the database set.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to create in the database.
    ///
    /// Returns:
    ///     The created entity with updated database values.
    ///
    public TEntity Create(TEntity entity)
    {
        _databaseSet.Add(entity);
        return entity;
    }

    ///
    /// Summary:
    ///     Creates multiple entities in the database in a single operation.
    ///     Adds the specified collection of entities to the database set.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entity objects to create in the database.
    ///
    /// Returns:
    ///     The collection of created entities with updated database values.
    ///
    public IEnumerable<TEntity> Create(IEnumerable<TEntity> entities)
    {
        _databaseSet.AddRange(entities);
        return entities;
    }

    ///
    /// Summary:
    ///     Updates an existing entity in the database.
    ///     Modifies the specified entity and tracks it for persistence.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to update in the database.
    ///
    /// Returns:
    ///     The updated entity with current database values.
    ///
    public TEntity Update(TEntity entity)
    {
        entity.UpdatedAt = Moment.Utc.Now;

        /// Logic:
        ///     Only attach the entity when it is not already tracked by the context.
        ///     Calling DbContext.Update on an entity whose key is already tracked
        ///     throws an identity-conflict; a tracked entity's changes are persisted
        ///     automatically on save, so re-attaching is both unnecessary and unsafe.
        EntityEntry<TEntity> tracked = _databaseContext
            .ChangeTracker.Entries<TEntity>()
            .FirstOrDefault(entry => entry.Entity.Id == entity.Id);

        if (tracked.Justifiable() is false)
            _databaseContext.Update(entity);
        else if (ReferenceEquals(tracked.Entity, entity) is false)
            tracked.CurrentValues.SetValues(entity);

        return entity;
    }

    ///
    /// Summary:
    ///     Updates multiple entities in the database in a single operation.
    ///     Modifies the specified collection of entities and tracks them for persistence.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entity objects to update in the database.
    ///
    /// Returns:
    ///     The collection of updated entities with current database values.
    ///
    public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
    {
        /// Logic:
        ///     Update each entity individually so the tracked-entity check is applied
        ///     per item. Calling UpdateRange on an entity whose key is already tracked
        ///     throws an identity-conflict; a tracked entity's changes are persisted
        ///     automatically on save, so re-attaching is both unnecessary and unsafe.
        foreach (TEntity entity in entities)
            Update(entity);

        return entities;
    }

    ///
    /// Summary:
    ///     Permanently removes an entity from the database.
    ///     Deletes the specified entity from the DbSet.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to remove from the database.
    ///
    /// Returns:
    ///     The removed entity.
    ///
    public TEntity Remove(TEntity entity)
    {
        _databaseSet.Remove(entity);
        return entity;
    }

    ///
    /// Summary:
    ///     Permanently removes a collection of entities from the database.
    ///     Deletes the specified entities from the DbSet.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entity objects to remove from the database.
    ///
    /// Returns:
    ///     The removed collection of entities.
    ///
    public IEnumerable<TEntity> Remove(IEnumerable<TEntity> entities)
    {
        _databaseSet.RemoveRange(entities);
        return entities;
    }

    ///
    /// Summary:
    ///     Performs a soft delete on an entity by marking it as deleted.
    ///     Sets DeletedAt and DeletedBy properties without removing it from the database.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to soft delete.
    ///   profileId:
    ///     The ID of the profile performing the soft delete operation.
    ///
    /// Returns:
    ///     The soft deleted entity with deletion metadata set.
    ///
    public TEntity SoftRemove(TEntity entity, Guid profileId)
    {
        entity.DeletedAt = Moment.Utc.Now;
        entity.DeletedBy = profileId;

        Update(entity);
        return entity;
    }

    ///
    /// Summary:
    ///     Restores an entity that was previously soft deleted.
    ///     Clears deleted metadata and marks the entity as active again.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to restore.
    ///
    /// Returns:
    ///     The restored entity with deleted fields cleared.
    ///
    public TEntity Restore(TEntity entity)
    {
        entity.DeletedAt = default;
        entity.DeletedBy = default;

        Update(entity);
        return entity;
    }

    ///
    /// Summary:
    ///     Performs a soft delete on multiple entities by marking them as deleted.
    ///     Sets DeletedAt and DeletedBy properties for each entity in the collection.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entity objects to soft delete.
    ///   profileId:
    ///     The ID of the profile performing the soft delete operation.
    ///
    /// Returns:
    ///     The collection of soft deleted entities with deletion metadata set.
    ///
    public IEnumerable<TEntity> SoftRemove(
        IEnumerable<TEntity> entities,
        Guid profileId
    )
    {
        IEnumerable<TEntity> records = entities.Select(entity =>
        {
            entity.DeletedBy = profileId;
            entity.DeletedAt = Moment.Utc.Now;
            return entity;
        });

        Update(records);
        return entities;
    }

    ///
    /// Summary:
    ///     Restores multiple entities that were previously soft deleted.
    ///     Clears deleted metadata across the collection.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entity objects to restore.
    ///
    /// Returns:
    ///     The restored collection of entities with deleted fields cleared.
    ///
    public IEnumerable<TEntity> Restore(IEnumerable<TEntity> entities)
    {
        IEnumerable<TEntity> records = entities.Select(entity =>
        {
            entity.DeletedBy = default;
            entity.DeletedAt = default;
            return entity;
        });

        Update(records);
        return entities;
    }

    ///
    /// Summary:
    ///     Includes related entities in the query using eager loading.
    ///     Specifies which navigation properties to include in the query result.
    ///
    /// Parameters:
    ///   navigationPropertyPath:
    ///     The navigation property expression to include in the query.
    ///
    /// Returns:
    ///     IIncludableQueryable with the specified navigation property included.
    ///
    public IIncludableQueryable<TEntity, P> Include<P>(
        Expression<Func<TEntity, P>> navigationPropertyPath
    ) => _databaseSet.Include(navigationPropertyPath);

    ///
    /// Summary:
    ///     Retrieves the first entity that matches the specified predicate condition.
    ///     Returns the first matching entity or null if no entity matches the condition.
    ///
    /// Parameters:
    ///   predicate:
    ///     The filter expression to apply to the entities.
    ///
    /// Returns:
    ///     Task containing the first matching entity, or null if no entity is found.
    ///
    public Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate
    ) => _databaseSet.FirstOrDefaultAsync(predicate);

    ///
    /// Summary:
    ///     Creates or updates a single entity using an upsert operation.
    ///     Inserts the entity when it does not exist, or updates it when it does.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to upsert.
    ///
    /// Returns:
    ///     A builder for configuring and executing the upsert command.
    ///
    public UpsertCommandBuilder<TEntity> Upsert(TEntity entity) =>
        _databaseSet.Upsert(entity);

    ///
    /// Summary:
    ///     Creates or updates multiple entities using an upsert operation.
    ///     Inserts missing entities and updates existing ones in a batch.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entities to upsert.
    ///
    /// Returns:
    ///     A builder for configuring and executing the bulk upsert command.
    ///
    public UpsertCommandBuilder<TEntity> Upsert(
        IEnumerable<TEntity> entities
    ) => _databaseSet.UpsertRange(entities);

    ///
    /// Summary:
    ///     Performs a bulk update of multiple entities using the database provider's bulk update capability.
    ///
    /// Parameters:
    ///   entities:
    ///     The collection of entity objects to update.
    ///   config:
    ///     Optional bulk update configuration settings.
    ///
    /// Returns:
    ///     Task containing the updated entity collection.
    ///
    public async Task<IEnumerable<TEntity>> BulkUpdateOrInsertAsync(
        IEnumerable<TEntity> entities,
        BulkConfig config = default
    )
    {
        await _databaseContext.BulkInsertOrUpdateAsync(entities, config);
        return entities;
    }

    ///
    /// Summary:
    ///     Gets the underlying database context for advanced operations.
    ///     Provides access to the Entity Framework DatabaseContext instance.
    ///
    /// Returns:
    ///     DatabaseContext instance for direct database operations.
    ////     Gets the underlying database context for advanced operations.
    ///     Provides access to the Entity Framework DatabaseContext instance.
    ///
    /// Returns:
    ///     DatabaseContext instance for direct database operations.
    ///
    public DatabaseContext GetContext() => _databaseContext;
}
