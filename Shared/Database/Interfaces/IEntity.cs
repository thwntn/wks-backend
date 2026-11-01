namespace Wks.Interfaces;

///
/// Summary:
///     Defines the base contract for all persistent entities in the system.
///     Provides common auditing properties including identity, lifecycle
///     timestamps, and soft delete information.
///     All domain entities must implement this interface to ensure
///     consistency in data management and tracking.
///
public interface IEntity
{
    ///
    /// Summary:
    ///     Gets the primary key identifier of the entity.
    ///
    /// Returns:
    ///     A globally unique identifier (GUID) representing the entity ID.
    ///
    Guid Id { get; }

    ///
    /// Summary:
    ///     Gets or sets the timestamp when the entity was created.
    ///
    /// Returns:
    ///     A UTC DateTime indicating when the entity was initially created.
    ///
    DateTime CreatedAt { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the timestamp when the entity was soft-deleted.
    ///
    /// Returns:
    ///     A nullable UTC DateTime representing the deletion time,
    ///     or null if the entity is active.
    ///
    DateTime? DeletedAt { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the timestamp when the entity was last updated.
    ///
    /// Returns:
    ///     A nullable UTC DateTime representing the last update time,
    ///     or null if the entity has never been modified.
    ///
    DateTime? UpdatedAt { get; set; }

    ///
    /// Summary:
    ///     Gets or sets the identifier of the profile that performed
    ///     the soft delete operation.
    ///
    /// Returns:
    ///     A nullable GUID representing the profile ID responsible
    ///     for deleting the entity, or null if not deleted.
    ///
    Guid? DeletedBy { get; set; }

    ///
    /// Summary:
    ///     Restores a soft-deleted entity to active state.
    ///
    /// Logic:
    ///     Sets DeletedAt to null.
    ///     Sets DeletedBy to null.
    ///
    /// Exceptions:
    ///     None.
    ///
    void Restore();
}
