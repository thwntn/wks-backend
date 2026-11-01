using FlexLabs.EntityFrameworkCore.Upsert;

namespace Wks.Interfaces;

///
/// Summary:
///     Defines the generic repository contract for entity persistence operations.
///     Provides methods for querying, creating, updating, removing, soft deleting,
///     and bulk operations on entities.
///
public interface IRepository<TEntity>
    where TEntity : class
{
    ///
    /// Summary:
    ///     Gets the raw DbSet for direct database operations.
    ///     Provides access to the underlying Entity Framework DbSet.
    ///
    /// Returns:
    ///     Raw DbSet<TEntity> for direct database queries and operations.
    ///
    DbSet<TEntity> Raw { get; }

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
    Task<TEntity> GetByIdAsync(Guid id);

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
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

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
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);

    ///
    /// Summary:
    ///     Includes related entities in the query using eager loading.
    ///     Specifies which navigation properties to include in the query result.
    ///
    /// Parameters:
    ///   property:
    ///     The navigation property expression to include in the query.
    ///
    /// Returns:
    ///     IIncludableQueryable with the specified navigation property included.
    ///
    IIncludableQueryable<TEntity, Property> Include<Property>(
        Expression<Func<TEntity, Property>> property
    );

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
    Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate
    );

    ///
    /// Summary:
    ///     Retrieves all entities ordered by creation date in descending order.
    ///     Returns a queryable collection of all entities in the database.
    ///
    /// Returns:
    ///     IQueryable collection of all entities ordered by CreatedAt descending.
    ///
    Task<List<TEntity>> ListAsync();

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
    TEntity Create(TEntity entity);

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
    IEnumerable<TEntity> Create(IEnumerable<TEntity> entities);

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
    TEntity Update(TEntity entity);

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
    IEnumerable<TEntity> Update(IEnumerable<TEntity> entities);

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
    TEntity Remove(TEntity entity);

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
    IEnumerable<TEntity> Remove(IEnumerable<TEntity> entities);

    ///
    /// Summary:
    ///     Performs a soft delete on an entity by marking it as deleted.
    ///     Sets DeletedAt and DeletedBy properties without removing it from the database.
    ///
    /// Parameters:
    ///   softRemove:
    ///     The entity object to soft delete.
    ///   profileId:
    ///     The ID of the profile performing the soft delete operation.
    ///
    /// Returns:
    ///     The soft deleted entity with deletion metadata set.
    ///
    TEntity SoftRemove(TEntity softRemove, Guid profileId);

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
    TEntity Restore(TEntity entity);

    ///
    /// Summary:
    ///     Performs a soft delete on multiple entities by marking them as deleted.
    ///     Sets DeletedAt and DeletedBy properties for each entity in the collection.
    ///
    /// Parameters:
    ///   softRemoves:
    ///     The collection of entity objects to soft delete.
    ///   profileId:
    ///     The ID of the profile performing the soft delete operation.
    ///
    /// Returns:
    ///     The collection of soft deleted entities with deletion metadata set.
    ///
    IEnumerable<TEntity> SoftRemove(
        IEnumerable<TEntity> softRemoves,
        Guid profileId
    );

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
    IEnumerable<TEntity> Restore(IEnumerable<TEntity> entities);

    ///
    /// Summary:
    ///     Creates or updates a single entity using an upsert operation.
    ///     Inserts the entity when missing or updates it when present.
    ///
    /// Parameters:
    ///   entity:
    ///     The entity object to upsert.
    ///
    /// Returns:
    ///     A builder for configuring and executing the upsert command.
    ///
    UpsertCommandBuilder<TEntity> Upsert(TEntity entity);

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
    UpsertCommandBuilder<TEntity> Upsert(IEnumerable<TEntity> entities);

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
    Task<IEnumerable<TEntity>> BulkUpdateOrInsertAsync(
        IEnumerable<TEntity> entities,
        BulkConfig config = default
    );

    ///
    /// Summary:
    ///     Gets the underlying database context for advanced operations.
    ///     Provides access to the Entity Framework DatabaseContext instance.
    ///
    /// Returns:
    ///     DatabaseContext instance for direct database operations.
    ///
    DatabaseContext GetContext();
}
